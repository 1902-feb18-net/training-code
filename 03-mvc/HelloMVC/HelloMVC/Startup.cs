using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HelloMVC
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            // here we register services that later on we can request from the framework.
            // we will use that for dependency injection.
            // that enables great separation of concerns

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            // here we configure "convention-based routing" aka global routing.
            // routing is the process/rules by which we decide WHICH controller and
            // WHICH action method will be instantiated and called to handle the current
            // request.
            app.UseMvc(routes =>
            {
                // mvc checks these "route conventions" one by one in order
                // looking for a match.

                // if the url looks like: "GoToHome"
                // then, delegate to home controller, index action.
                routes.MapRoute(
                    name: "homealias",
                    template: "GoToHome/{id?}",
                    defaults: new { Controller = "Home", Action = "Index" });
                routes.MapRoute(
                    name: "default",
                    // the "id?" at the end here is an optional route parameter "id"
                    template: "{controller=Home}/{action=Index}/{id?}");
                // we could put a catch-all at the very bottom for e.g. page not found
            });
            // C# lets us make objects of anonymous type or anonymous class.
        }
    }
}
