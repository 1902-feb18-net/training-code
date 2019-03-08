using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MoviesSite.App.ViewModels;
using MoviesSite.BLL;

namespace MoviesSite.App.Controllers
{
    // ways to get data from the controller to the view.
    // 1. (strongly-typed view) the model.
    //     often will be a view model
    //     view can only take one model, so, if you need several,
    //     either that's a collection type of some kind, or,
    //     you make a new view model that contains the several you need.
    // 2. ViewData - a key-value pair dictionary
    //      this object is reachable via a property on Controller.
    //      we can assign values in the controller, and access them in the view.
    //      (during the same HTTP request! it's cleared between requests.)
    // 3. ViewBag - a different way to access ViewData - "dynamic" type.
    //       set properties instead of using indexing syntax w/ string.

    // 4. TempData - a key-value pair dictionary
    //      the values inside it survive across requests "magically"
    //      (by default - stored using cookies sent to the client
    //       which are then sent back to the server on subsequent requests.
    //       - but, we can configure other providers for TempData in Startup.)
    //     e.g. we can use this to incrementally build some model
    //      that needs many forms to be submitted, not just one

    //    with regular ["key"] access, the value you access gets deleted after the
    //       current request. we can circumvent that with methods
    //         - .Peek("key")
    //                accesses value without marking for deletion
    //         - .Keep("key")
    //                unmarks a value for deletion

    // the other way we can keep data around to incrementally build something like
    // an Order is with hidden form fields (corresponding to view model properties ofc.).

    /*
    <form asp-action="Post">
        <!-- other non-hidden form fields --> 
        @for(int i = 0; i<Model.Count; i++)
        {
            @Html.Hidden($"items[{i}].Id", Model[i].Id)
            @Html.Hidden($"items[{i}].Name", Model[i].Name)
        }
        <input type = "submit" value="Post" class="btn btn-default" />
    </form>
    */

        // attribute routing - in contrast to global/convention-based routing
    [Route("Films/[action]/{id?}")]
    public class MoviesController : Controller
    {
        private readonly ILogger<MoviesController> _logger;

        // the moviescontroller depends on MovieRepository.
        // instead of the controller instantiating its own dependency (new MovieRepo)
        // ASP.NET gives us the ability to have that dependency "injected".

        // two steps to set up dependency injection -
        // 1. register the dep. as a service in Startup.ConfigureServices.
        // 2. request the service (typically, by just having it as ctor parameter.)
        public MoviesController(IMovieRepository movieRepo, ILogger<MoviesController> logger)
        {
            MovieRepo = movieRepo;
            _logger = logger;
        }

        public IMovieRepository MovieRepo { get; set; }

        // GET: Movies
        [Route("{num:int?}")]
        [Route("Index/{num:int?}")]
        [Route("[controller]/asdfasdf/{num:int?}/ShowAllMovies")]
        public async Task<ActionResult> Index([FromQuery] int num, [FromServices] IList<Movie> asdf)
        {
            // we have [FromQuery] to get that param from query string ("?key=val" in URL)
            // we have [FromForm]/[FromBody] to get it from a form submission.
            // we have [FromRoute] to get it from route parameter (defined in attr or global routes)

            // we also have [FromServices]
            //   to ask for some service exactly like constructor parameters do.

            IEnumerable<Movie> movies = await MovieRepo.AllMoviesAsync();
            var viewModels = movies.Select(m => new MovieViewModel
            {
                Id = m.Id,
                Title = m.Title,
                Genre = m.Genre,
                ReleaseDate = m.DateReleased
            }).ToList();

            // "dynamic" type - compile-time type checking turned off
            //  because we can add new properties to that object at any time.

            ViewBag.numOfMovies = viewModels.Count;
            ViewData["currentTime"] = DateTime.Now;
            ViewData["numFromActionMethod"] = num;

            if (TempData.ContainsKey("counter"))
            {
                _logger.LogInformation("counter found in tempdata, increasing.");
                TempData["counter"] = ((int)TempData["counter"]) + 1;
            }
            else
            {
                TempData["counter"] = num;
            }

            return View(viewModels);
        }

        // GET: Movies/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                var movie = MovieRepo.MovieById(id);
                var viewModel = new MovieViewModel
                {
                    Id = movie.Id,
                    Title = movie.Title,
                    ReleaseDate = movie.DateReleased,
                    Genre = movie.Genre,
                    Genres = MovieRepo.AllGenres().ToList()
                };
                return View(viewModel);
            }
            catch (InvalidOperationException ex)
            {
                // should log that, and redirect to error page
                return RedirectToAction("Error", "Home");
            }
        }

        // GET: Movies/Create
        public ActionResult Create()
        {
            var viewModel = new MovieViewModel
            {
                Genres = MovieRepo.AllGenres().ToList()
            };
            // give the Create view values for its dropdown
            return View(viewModel);
        }

        // POST: Movies/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MovieViewModel viewModel)
        {
            try
            {
                if (viewModel.Genre is null || viewModel.Genre.Id == 0)
                {
                    // i could use [Required] attr to achieve this
                    // but here is example of manual adding of model error.
                    ModelState.AddModelError("Genre", "Required genre");

                    _logger.LogWarning("genre is null, returning with model error.");
                }
                if (!ModelState.IsValid)
                {
                    return View(viewModel);
                }

                // we convert from the view model back and forth to the BLL class
                // when needed.
                // the view talks in terms of view model now; but the repo
                // talks in terms of Movie.
                var movie = new Movie
                {
                    Id = viewModel.Id,
                    Title = viewModel.Title,
                    DateReleased = viewModel.ReleaseDate,
                    Genre = MovieRepo.GenreById(viewModel.Genre.Id)
                };

                MovieRepo.CreateMovie(movie);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(viewModel);
            }
        }

        // GET: Movies/Edit/5
        public ActionResult Edit(int id)
        {
            // with edit, we like to pre-populate the existing values
            try
            {
                var movie = MovieRepo.MovieById(id);
                var viewModel = new MovieViewModel
                {
                    Id = movie.Id,
                    Title = movie.Title,
                    ReleaseDate = movie.DateReleased,
                    Genre = movie.Genre,
                    Genres = MovieRepo.AllGenres().ToList()
                };
                return View(viewModel);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex, "No such movie with id.");
                // log that, and redirect to error page
                return RedirectToAction("Error", "Home");
            }
        }

        // POST: Movies/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, MovieViewModel viewModel)
        {
            try
            {
                // here we are validating the user input
                if (!ModelState.IsValid)
                {
                    // the DataAnnotations on the view model
                    // prompt client-side validation, but also,
                    // during model binding (when the user's form data
                    // is put into the parameters of this action method)
                    // we also check all those same conditions.
                    // ModelState will have error objects inside it that we
                    // check right now.
                    viewModel.Genres = MovieRepo.AllGenres().ToList();
                    return View(viewModel);
                }

                var movie = new Movie
                {
                    Id = viewModel.Id,
                    Title = viewModel.Title,
                    DateReleased = viewModel.ReleaseDate,
                    Genre = MovieRepo.GenreById(viewModel.Genre.Id)
                };

                MovieRepo.UpdateMovie(id, movie);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(viewModel);
            }
        }

        // GET: Movies/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                var movie = MovieRepo.MovieById(id);
                var viewModel = new MovieViewModel
                {
                    Id = movie.Id,
                    Title = movie.Title,
                    ReleaseDate = movie.DateReleased,
                    Genre = movie.Genre
                };
                return View(viewModel);
            }
            catch (InvalidOperationException ex)
            {
                // should log that, and redirect to error page
                return RedirectToAction("Error", "Home");
            }
        }

        // POST: Movies/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                await MovieRepo.DeleteMovieAsync(id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                // should provide some error message
                return RedirectToAction(nameof(Index));
            }
        }
    }
}