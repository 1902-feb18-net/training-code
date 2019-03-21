using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CharacterMvc.Controllers
{
    public abstract class AServiceController : Controller
    {
        public HttpClient HttpClient { get; }
        public Uri ServiceUrl { get; }
        public string ServiceCookieName { get; }

        public AServiceController(HttpClient httpClient, IConfiguration configuration)
        {
            HttpClient = httpClient;

            ServiceUrl = new Uri(configuration["ServiceUrl"]);
            ServiceCookieName = configuration["ServiceCookieName"];
        }

        public HttpRequestMessage CreateRequestToService(HttpMethod method,
            string relativeUrl, object body = null)
        {
            var url = new Uri(ServiceUrl, relativeUrl);
            var apiRequest = new HttpRequestMessage(method, url);

            if (body != null)
            {
                var jsonString = JsonConvert.SerializeObject(body);
                apiRequest.Content = new StringContent(jsonString, Encoding.UTF8,
                    "application/json");
            }

            // get the value of the app's auth cookie from the browser's request.
            // (if present) and copy it to the api request.
            var cookieValue = Request.Cookies[ServiceCookieName];

            if (cookieValue != null)
            {
                var headerValue = new CookieHeaderValue(ServiceCookieName, cookieValue);
                apiRequest.Headers.Add("Cookie", headerValue.ToString());
            }

            return apiRequest;
        }
    }
}
