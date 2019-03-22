using CharacterMvc.ApiModels;
using CharacterMvc.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace CharacterMvc.Controllers
{
    public class AccountController : AServiceController
    {
        public AccountController(HttpClient httpClient, IConfiguration configuration)
            : base(httpClient, configuration)
        {
        }

        // GET: /Account/Login
        public ActionResult Login()
        {
            return View();
        }

        // POST: /Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(ApiLogin login)
        {
            if (!ModelState.IsValid)
            {
                return View(login);
            }

            HttpRequestMessage request = CreateRequestToService(HttpMethod.Post,
                Configuration["ServiceEndpoints:AccountLogin"], login);

            HttpResponseMessage response;
            try
            {
                response = await HttpClient.SendAsync(request);
            }
            catch
            {
                ModelState.AddModelError("", "Unexpected server error");
                return View(login);
            }

            if (!response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    // login failed because bad credentials
                    ModelState.AddModelError("", "Login or password incorrect.");
                }
                else
                {
                    ModelState.AddModelError("", "Unexpected server error");
                }
                return View(login);
            }

            var success = PassCookiesToClient(response);
            if (!success)
            {
                ModelState.AddModelError("", "Unexpected server error");
                return View(login);
            }

            // login success
            return RedirectToAction("Index", "Home");
        }

        // should also have register and logout

        // POST: /Account/Logout
        [HttpPost]
        public async Task<ActionResult> Logout()
        {
            HttpRequestMessage request = CreateRequestToService(HttpMethod.Post,
                Configuration["ServiceEndpoints:AccountLogout"]);

            HttpResponseMessage response;
            try
            {
                response = await HttpClient.SendAsync(request);
            }
            catch (HttpRequestException)
            {
                return View("Error", new ErrorViewModel());
            }

            if (!response.IsSuccessStatusCode)
            {
                return View("Error", new ErrorViewModel());
            }

            var success = PassCookiesToClient(response);
            if (!success)
            {
                return View("Error", new ErrorViewModel());
            }

            // logout success
            return RedirectToAction("Index", "Home");
        }

        // GET: /Account/Register
        public ActionResult Register()
        {
            return View();
        }

        // POST: /Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(ApiRegister register)
        {
            if (!ModelState.IsValid)
            {
                return View(register);
            }

            HttpRequestMessage request = CreateRequestToService(HttpMethod.Post,
                Configuration["ServiceEndpoints:AccountRegister"], register);

            HttpResponseMessage response;
            try
            {
                response = await HttpClient.SendAsync(request);
            }
            catch (HttpRequestException)
            {
                ModelState.AddModelError("", "Unexpected server error");
                return View(register);
            }

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError("", "Unexpected server error");
                return View(register);
            }

            var success = PassCookiesToClient(response);
            if (!success)
            {
                ModelState.AddModelError("", "Unexpected server error");
                return View(register);
            }

            // login success
            return RedirectToAction("Index", "Home");
        }

        private bool PassCookiesToClient(HttpResponseMessage apiResponse)
        {
            // the header value contains both the name and value of the cookie itself
            if (apiResponse.Headers.TryGetValues("Set-Cookie", out IEnumerable<string> values) &&
                values.FirstOrDefault(x => x.StartsWith(Configuration["ServiceCookieName"])) is string headerValue)
            {
                // copy that cookie to the response we will send to the client
                Response.Headers.Add("Set-Cookie", headerValue);
                return true;
            }
            return false;
        }
    }
}