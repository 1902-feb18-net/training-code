using CharacterRest;
using CharacterRestDAL;
using CharacterRestService.ApiModels;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Threading.Tasks;

namespace CharacterRestService
{
    public class Startup
    {
        private readonly ILogger _logger;

        public Startup(IConfiguration configuration, ILogger<Startup> logger)
        {
            Configuration = configuration;
            _logger = logger;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<ICharacterRepository, CharacterRepository>();

            services.AddSingleton<IMapper, Mapper>();

            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("AppDb")));

            // set up the database (via code-first, manually) and the dbcontext for auth
            // we could just plug in IdentityDbContext here without subclassing
            services.AddDbContext<AuthDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("AuthDb")));

            // set up the "repositories" for auth stuff
            // here we could subclass
            services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                // we could just use defaults and not set anything on options
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                // many options here
            })
                .AddEntityFrameworkStores<AuthDbContext>();

            // set up the cookies for authentication
            // to plug in to the authentication filters.
            var cookieName = Configuration["AuthCookieName"];
            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.IsEssential = true;
                options.Cookie.SecurePolicy = CookieSecurePolicy.None;
                options.Cookie.Name = cookieName;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
                options.Events = new CookieAuthenticationEvents
                {
                    OnRedirectToLogin = context =>
                    {
                        // prevent redirect, just return unauthorized
                        _logger.LogInformation("Replacing redirect to login with 401");
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        context.Response.Headers.Remove("Location");
                        // we use Task.FromResult when we're in an async context
                        // but there's nothing to await.
                        return Task.FromResult(0);
                    },
                    OnRedirectToAccessDenied = context =>
                    {
                        // prevent redirect, just return forbidden
                        _logger.LogInformation("Replacing redirect to access denied with 403");
                        context.Response.StatusCode = StatusCodes.Status403Forbidden;
                        context.Response.Headers.Remove("Location");
                        return Task.FromResult(0);
                    }
                };
            });
            _logger.LogInformation("Configured application cookie: {CookieName}", cookieName);

            // enable authentication middleware
            services.AddAuthentication();

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                builder =>
                {
                    // for dev scenario, we can be pretty tolerant
                    // in prod, we should be restrictive, we would fill in
                    // only the origins where our Angular app was hosted.
                    builder.WithOrigins(new[]
                    {
                        "http://localhost:4200",
                        "http://escalona1902pokeangular.azurewebsites.net",
                        "https://escalona1902pokeangular.azurewebsites.net"
                    })
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });
            });

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

            // have to define that policy name above
            app.UseCors("AllowAll");

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();
            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            var swaggerUrl = Configuration["SwaggerEndpointUrl"];
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint(swaggerUrl, "Character API V1");
            });
            _logger.LogInformation("Configured Swagger endpoint: {SwaggerEndpointUrl}", swaggerUrl);

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
