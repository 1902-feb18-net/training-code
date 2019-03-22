using CharacterMvc.ApiModels;
using CharacterMvc.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace CharacterMvc.Controllers
{
    public class CharacterController : AServiceController
    {
        public CharacterController(HttpClient httpClient, IConfiguration configuration)
            : base(httpClient, configuration)
        {
        }

        // GET: Character
        public async Task<ActionResult> Index()
        {
            HttpRequestMessage request = CreateRequestToService(HttpMethod.Get,
                Configuration["ServiceEndpoints:Character"]);

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
                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    return RedirectToAction("Login", "Account");
                }
                return View("Error", new ErrorViewModel());
            }
            var jsonString = await response.Content.ReadAsStringAsync();
            List<ApiCharacter> characters = JsonConvert.DeserializeObject<List<ApiCharacter>>(jsonString);

            return View(characters);
        }

        // GET: Character/Details/5
        public async Task<ActionResult> Details(int id)
        {
            HttpRequestMessage request = CreateRequestToService(HttpMethod.Get,
                $"{Configuration["ServiceEndpoints:Character"]}/{id}");

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
                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    return RedirectToAction("Login", "Account");
                }
                return View("Error", new ErrorViewModel());
            }
            var jsonString = await response.Content.ReadAsStringAsync();
            ApiCharacter character = JsonConvert.DeserializeObject<ApiCharacter>(jsonString);

            return View(character);
        }

        // GET: Character/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Character/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind("Name")] ApiCharacter character)
        {
            if (!ModelState.IsValid)
            {
                return View(character);
            }

            HttpRequestMessage request = CreateRequestToService(HttpMethod.Post,
                Configuration["ServiceEndpoints:Character"], character);

            HttpResponseMessage response;
            try
            {
                response = await HttpClient.SendAsync(request);
            }
            catch (HttpRequestException)
            {
                ModelState.AddModelError("", "Unexpected server error");
                return View(character);
            }

            if (!response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    return RedirectToAction("Login", "Account");
                }
                ModelState.AddModelError("", "Unexpected server error");
                return View(character);
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Character/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            HttpRequestMessage request = CreateRequestToService(HttpMethod.Get,
                   $"{Configuration["ServiceEndpoints:Character"]}/{id}");

            HttpResponseMessage response;
            try
            {
                response = await HttpClient.SendAsync(request);
            }
            catch (HttpRequestException)
            {
                ModelState.AddModelError("", "Unexpected server error");
                return View();
            }
            if (!response.IsSuccessStatusCode)
            {
                switch (response.StatusCode)
                {
                    case HttpStatusCode.Unauthorized:
                        return RedirectToAction("Login", "Account");
                    case HttpStatusCode.NotFound:
                        return View("Error", new ErrorViewModel());
                    default:
                        ModelState.AddModelError("", "Unexpected server error");
                        return View();
                }
            }
            var jsonString = await response.Content.ReadAsStringAsync();
            ApiCharacter character = JsonConvert.DeserializeObject<ApiCharacter>(jsonString);

            return View(character);
        }

        // POST: Character/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, ApiCharacter character)
        {
            if (!ModelState.IsValid)
            {
                return View(character);
            }

            HttpRequestMessage request = CreateRequestToService(HttpMethod.Put,
                $"{Configuration["ServiceEndpoints:Character"]}/{id}", character);

            HttpResponseMessage response;
            try
            {
                response = await HttpClient.SendAsync(request);
            }
            catch (HttpRequestException)
            {
                ModelState.AddModelError("", "Unexpected server error");
                return View(character);
            }

            if (!response.IsSuccessStatusCode)
            {
                switch (response.StatusCode)
                {
                    case HttpStatusCode.Unauthorized:
                        return RedirectToAction("Login", "Account");
                    case HttpStatusCode.NotFound:
                        return View("Error", new ErrorViewModel());
                    default:
                        ModelState.AddModelError("", "Unexpected server error");
                        return View(character);
                }
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Character/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            if (!(Account?.Roles?.Contains("admin") ?? false))
            {
                // access denied
                return View("Error", new ErrorViewModel());
            }
            // implementation of GET Details identical
            return await Details(id);
        }

        // POST: Character/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, ApiCharacter character)
        {
            if (!(Account?.Roles?.Contains("admin") ?? false))
            {
                // access denied
                return View("Error", new ErrorViewModel());
            }
            if (!ModelState.IsValid)
            {
                return View(character);
            }

            HttpRequestMessage request = CreateRequestToService(HttpMethod.Delete,
                $"{Configuration["ServiceEndpoints:Character"]}/{id}");

            HttpResponseMessage response;
            try
            {
                response = await HttpClient.SendAsync(request);
            }
            catch (HttpRequestException)
            {
                ModelState.AddModelError("", "Unexpected server error");
                return View(character);
            }

            if (!response.IsSuccessStatusCode)
            {
                switch (response.StatusCode)
                {
                    case HttpStatusCode.Unauthorized:
                        return RedirectToAction("Login", "Account");
                    case HttpStatusCode.NotFound:
                        return View("Error", new ErrorViewModel());
                    default:
                        ModelState.AddModelError("", "Unexpected server error");
                        return View();
                }
            }

            return RedirectToAction(nameof(Index));
        }
    }
}