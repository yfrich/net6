using aspnetcoreCancel1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace aspnetcoreCancel1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            await Download3Async("https://www.youzack.com", 100, cancellationToken);
            return View();
        }
        static async Task Download3Async(string url, int n, CancellationToken cancellationToken)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                for (int i = 0; i < n; i++)
                {
                    var resp = await httpClient.GetAsync(url, cancellationToken);
                    string html = await resp.Content.ReadAsStringAsync();
                    Debug.WriteLine(i);
                    //Console.WriteLine($"{DateTime.Now}:{html.Substring(0, 100)}");
                    //if (cancellationToken.IsCancellationRequested)
                    //{
                    //    Console.WriteLine("请求被取消");
                    //    break;
                    //}
                }
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
