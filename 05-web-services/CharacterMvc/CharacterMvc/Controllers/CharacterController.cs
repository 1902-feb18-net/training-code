using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using CharacterMvc.ApiModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace CharacterMvc.Controllers
{
    public class CharacterController : AServiceController
    {
        public CharacterController(HttpClient httpClient, IConfiguration configuration)
            : base(httpClient, configuration)
        { }

        // GET: Character
        public async Task<ActionResult> Index()
        {
            var request = CreateRequestToService(HttpMethod.Get, "/api/character");

            var response = await HttpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    return RedirectToAction("Login", "Account");
                }
                return View("Error");
            }

            var jsonString = await response.Content.ReadAsStringAsync();

            var characters = JsonConvert.DeserializeObject<List<ApiCharacter>>(jsonString);

            return View(characters);
        }

        // GET: Character/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        // GET: Character/Create
        public ActionResult Create()
        {
            if (!(AccountDetails?.Roles?.Contains("admin") ?? false))
            {
                // access denied
                return View("Error");
            }
            return View();
        }

        // POST: Character/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ApiCharacter character)
        {
            if (!(AccountDetails?.Roles?.Contains("admin") ?? false))
            {
                // access denied
                return View("Error");
            }
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(character);
                }

                var request = CreateRequestToService(HttpMethod.Post, "/api/character",
                    character);

                var response = await HttpClient.SendAsync(request);

                if (!response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        return RedirectToAction("Login", "Account");
                    }
                    return View("Error");
                }
                
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                // log it
                return View(character);
            }
        }

        //// GET: Character/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: Character/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add update logic here

        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: Character/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: Character/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add delete logic here

        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}