using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoviesSite.App.ViewModels;
using MoviesSite.BLL;

namespace MoviesSite.App.Controllers
{
    public class MoviesController : Controller
    {
        private static readonly List<Movie> _moviesDb;
        private static readonly List<Genre> _genreDb;

#pragma warning disable S3963 // "static" fields should be initialized inline
        static MoviesController()
        {
            _genreDb = new List<Genre>
            {
                new Genre { Id = 1, Name = "Action" },
                new Genre { Id = 2, Name = "Drama" }
            };
            _moviesDb = new List<Movie>
            {
                new Movie
                {
                    Id = 1,
                    Title = "Star Wars IV",
                    DateReleased = new DateTime(1970, 1, 1),
                    Genre = _genreDb[0] // action
                }
            };
        }
#pragma warning restore S3963 // "static" fields should be initialized inline

        public MovieRepository MovieRepo { get; set; } =
            new MovieRepository(_moviesDb, _genreDb);

        // GET: Movies
        public ActionResult Index()
        {
            IEnumerable<Movie> movies = MovieRepo.AllMovies();
            var viewModels = movies.Select(m => new MovieViewModel
            {
                Id = m.Id,
                Title = m.Title,
                Genre = m.Genre,
                ReleaseDate = m.DateReleased
            });
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
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                MovieRepo.DeleteMovie(id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}