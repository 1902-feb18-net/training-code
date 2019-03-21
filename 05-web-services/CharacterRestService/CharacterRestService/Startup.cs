using CharacterRestDAL;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Threading.Tasks;

namespace CharacterRestService
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
            // set up the database (via code-first, manually) and the dbcontext for auth
            // we could just plug in IdentityDbContext here without subclassing
            services.AddDbContext<AuthDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("AuthDb")));

            // set up the "repositories" for auth stuff
            // here we could subclass
            services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                // we could just use defaults and not set anything on options
                options.Password.RequiredLength = 12;
                options.Password.RequireNonAlphanumeric = true;
                // many options here
            })
                .AddEntityFrameworkStores<AuthDbContext>();

            // set up the cookies for authentication
            // to plug in to the authentication filters.
            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.Name = "CharacterServiceAuth";
                options.ExpireTimeSpan = TimeSpan.FromMinutes(1);
                options.Events = new CookieAuthenticationEvents
                {
                    OnRedirectToLogin = context =>
                    {
                        // prevent redirect, just return unauthorized
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        context.Response.Headers.Remove("Location");
                        // we use Task.FromResult when we're in an async context
                        // but there's nothing to await.
                        return Task.FromResult(0);
                    },
                    OnRedirectToAccessDenied = context =>
                    {
                        // prevent redirect, just return forbidden
                        context.Response.StatusCode = StatusCodes.Status403Forbidden;
                        context.Response.Headers.Remove("Location");
                        // we use Task.FromResult when we're in an async context
                        // but there's nothing to await.
                        return Task.FromResult(0);
                    }
                };
            });

            // enable authentication middleware
            services.AddAuthentication();

            services.AddMvc(options =>
            {
                options.ReturnHttpNotAcceptable = true;
            })
                .AddXmlSerializerFormatters()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Character API", Version = "v1" });
            });
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseAuthentication();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();
            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Character API V1");
            });

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
