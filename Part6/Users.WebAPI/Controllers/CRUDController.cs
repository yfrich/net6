using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Users.Domain;
using Users.Domain.Entities;
using Users.Infrastructure;

namespace Users.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CRUDController : ControllerBase
    {
        private readonly IUserRepository userRepository;
        private readonly UserDbContext userDbContext;

        public CRUDController(IUserRepository userRepository, UserDbContext userDbContext)
        {
            this.userRepository = userRepository;
            this.userDbContext = userDbContext;
        }

        [HttpPost]
        [UnitOfWork(typeof(UserDbContext))]
        public async Task<IActionResult> AddUser(AddUserRequest req)
        {
            if (await userRepository.FindOneAsync(req.phoneNumber) != null)
            {
                return BadRequest("手机号已经存在");
            }
            var user = new User(req.phoneNumber);
            user.ChangePassword(req.password);
            userDbContext.Users.Add(user);
            return Ok("完成");
        }
    }
}
