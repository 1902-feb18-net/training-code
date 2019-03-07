using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MoviesSite.BLL;
using MoviesSite.DataAccess;

namespace MoviesSite.App
{
    public class Startup
    {
        private static readonly List<Movie> _moviesDb = new List<Movie>();
        private static readonly List<Genre> _genreDb = new List<Genre>();
        
        private static void SeedDatabase()
        {
            _genreDb.AddRange(new[]
            {
                new Genre { Id = 1, Name = "Action" },
                new Genre { Id = 2, Name = "Drama" }
            });
            _moviesDb.AddRange(new[]
            {
                new Movie
                {
                    Id = 1,
                    Title = "Star Wars IV",
                    DateReleased = new DateTime(1970, 1, 1),
                    Genre = _genreDb[0] // action
                }
            });
        }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            SeedDatabase();
        }

        // this config objects is pulled from many sources by default...
        // appsettings.json, appsettings.Development.json, environment variables,
        // user secrets (how we will put connection string.).
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

            // here, we register "services" to be injected when asked for.

            // adds service for "MovieRepository" type, with "scoped" lifetime
            //services.AddScoped<MovieRepository>();
            // when we register services under an interface, we use two type parameters
            // like this: for interface, and for implementation.
            services.AddScoped<IMovieRepository, MovieDbRepository>();
            // "scoped" lifetime means, one concrete object per HTTP request.

            // because the dbcontext has "scoped" lifetime, and the repository
            // USES that dbcontext, it must also have scoped (or lesser) lifetime

            // add services for the two IList types asked for by MovieRepo.
            // ("singleton" lifetime)
            services.AddSingleton<IList<Movie>>(_moviesDb);
            services.AddSingleton<IList<Genre>>(_genreDb);
            // when using "singleton" lifetime, we can just make the instance ourselves
            // and give it to the service provider.
            // "singleton" means, we might ask for this service 1000 times, and all
            // will get the same concrete object.

            // DbContext is going to be required by MovieRepository now...
            // so, we will register it as a service
            // (also, so that Add-Migration/Update-Database can see it.)
            services.AddDbContext<MovieDbContext>(builder =>
                builder.UseSqlServer(Configuration.GetConnectionString("MovieDB")));
            // this will seek in Configuration for
            // "MovieDB" value inside "ConnectionStrings" section.
            //    we should add that in "user secrets"

            //  should look like this since i named it "MovieDB":
            //      {
            //        "ConnectionStrings": {
            //          "MovieDB": "<your-connection-string>"
            //        }
            //      }

            // AddDbContext is how we register DbContexts as services. it uses
            //  "scoped" lifetime by default (new DbContext for each HTTP request.)

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

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
