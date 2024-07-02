using Microsoft.AspNetCore.Identity;

namespace Identity框架
{
    public class MyUser : IdentityUser<long>
    {
        public string? WeiXinAccount { get; set; }
        public long JWTVersion { get; set; }
    }
}
