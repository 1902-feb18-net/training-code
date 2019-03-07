using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoviesSite.BLL;

namespace MoviesSite.App.Controllers
{
    public class MoviesController : Controller
    {
        private static readonly List<Movie> _moviesDb = new List<Movie>();
        private static readonly List<Genre> _genreDb = new List<Genre>();

        public MovieRepository MovieRepo { get; set; } =
            new MovieRepository(_moviesDb, _genreDb);

        // GET: Movies
        public ActionResult Index()
        {
            IEnumerable<Movie> movies = MovieRepo.AllMovies();
            return View(movies);
        }

        // GET: Movies/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                var movie = MovieRepo.MovieById(id);
                return View(movie);
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
            return View();
        }

        // POST: Movies/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Movie movie)
        {
            try
            {
                MovieRepo.CreateMovie(movie);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(movie);
            }
        }

        // GET: Movies/Edit/5
        public ActionResult Edit(int id)
        {
            // with edit, we like to pre-populate the existing values
            try
            {
                var movie = MovieRepo.MovieById(id);
                return View(movie);
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
        public ActionResult Edit(int id, Movie movie)
        {
            try
            {
                // here we would validate the user input

                MovieRepo.UpdateMovie(id, movie);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(movie);
            }
        }

        // GET: Movies/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                var movie = MovieRepo.MovieById(id);
                return View(movie);
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