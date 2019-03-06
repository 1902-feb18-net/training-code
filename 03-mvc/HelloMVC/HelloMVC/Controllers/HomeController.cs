using HelloMVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Diagnostics;

namespace HelloMVC.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            // now we know to to "print"/read data
            // we could access some UserRepository here
            // which accesses some DbContext
            var user = new User
            {
                Username = "Fred",
                Address = new List<Address>
                {
                    new Address { Street = "123 Main St", CityState = "Reston, VA" },
                    new Address { Street = "321 Broad St", CityState = "Dallas, TX" }
                }
            };
            // the View() method defined on parent class Controller
            // looks for a View with the name of the current method.
            return View(user);
        }

        public IActionResult IndexWithId(int id)
        {
            var user = new User { Username = $"Fred #{id}" };
            // this won't work, there is no IndexWithId view.
            //return View(user);
            // this one won't work, the Index view requires non-null model.
            //return View("Index");
            return View("Index", user);
        }

        [HttpPost] // this method receives POST, not GET
        public IActionResult IndexWithUser(IFormCollection collection)
        {
            return View("Index", new User { Username = collection["Username"] });
        }

        //[HttpPost] // this method receives POST, not GET
        //public IActionResult IndexWithUser(User user)
        //{
        //    // we're going to receive form data, and "model binding" will occur
        //    // to this "user" object.
        //    return View("Index", user);
        //}

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
