using CharacterMvc.ApiModels;
using CharacterMvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CharacterMvc.Filters
{
    public class GetAccountDetailsFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(
            ActionExecutingContext context,
            ActionExecutionDelegate next)
        {
            // do something before the action executes
            // if the controller is an aservicecontroller, then
            // fetch the details, otherwise, do nothing.
            if (context.Controller is AServiceController controller)
            {
                var request = controller.CreateRequestToService(HttpMethod.Get,
                    "/api/account/details");

                var response = await controller.HttpClient.SendAsync(request);

                if (!response.IsSuccessStatusCode)
                {
                    // setting "Result" in a filter short-circuits the rest
                    // of the MVC pipeline
                    // but i won't do that, i should just log it.
                }
                else
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    var details = JsonConvert.DeserializeObject<ApiAccountDetails>(jsonString);
                    controller.ViewData["accountDetails"] = details;
                    controller.AccountDetails = details;
                }
            }

            await next();
        }
    }
}
