using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CharacterMvc.Models;
using Microsoft.Extensions.Configuration;
using System.Net.Http;

namespace CharacterMvc.Controllers
{
    public class HomeController : AServiceController
    {
        public HomeController(HttpClient httpClient, IConfiguration configuration)
            : base(httpClient, configuration)
        { }

        public IActionResult Index()
        {
            return View();
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
