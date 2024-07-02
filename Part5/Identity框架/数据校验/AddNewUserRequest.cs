using System.ComponentModel.DataAnnotations;

namespace Identity框架.数据校验
{
    public class AddNewUserRequest
    {
        //[MinLength(3)]
        //[MaxLength(10)]
        public string UserName { get; set; }
        //[Required]
        public string Email { get; set; }

        //[Compare(nameof(Password2))]
        public string Password { get; set; }
        public string Password2 { get; set; }
    }
}
