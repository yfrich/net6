using ASPNETMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace ASPNETMVC.Controllers
{
    public class TestController : Controller
    {
        public IActionResult Demo1()
        {
            //return View();
            var model = new Person("RichFu", true, DateTime.Now);
            return View(model);
        }
        public void AAA(int aa)
        {
            
        }
    }
}
