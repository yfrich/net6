using EFCoreBooks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ASPNETCOREWebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly BookDbContext bookDBContext;
        private readonly PersonDbContext personDBContext;
        public BookController(BookDbContext bookDBContext, PersonDbContext personDBContext)
        {
            this.bookDBContext = bookDBContext;
            this.personDBContext = personDBContext;
        }
        [HttpGet]
        public string Demo1()
        {
            int c = bookDBContext.Books.Count();
            return $"c={c},a={personDBContext.Persons.Count()}";
        }
    }
}
