using CharacterMvc.ApiModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace CharacterMvc.Controllers
{
    public abstract class AServiceController : Controller
    {
        public HttpClient HttpClient { get; }
        public IConfiguration Configuration { get; }
        public Uri ServiceUrl { get; }

        public ApiAccountDetails Account { get; set; }

        protected AServiceController(HttpClient httpClient, IConfiguration configuration)
        {
            HttpClient = httpClient;
            Configuration = configuration;
            ServiceUrl = new Uri(Configuration["ServiceUrl"]);
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
            var cookieName = Configuration["ServiceCookieName"];
            var cookieValue = Request.Cookies[cookieName];

            if (cookieValue != null)
            {
                var headerValue = new CookieHeaderValue(cookieName, cookieValue);
                apiRequest.Headers.Add("Cookie", headerValue.ToString());
            }

            return apiRequest;
        }
    }
}
