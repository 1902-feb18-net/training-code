using Microsoft.EntityFrameworkCore;
using MoviesSite.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesSite.DataAccess
{
    public class MovieDbRepository : IMovieRepository
    {
        private readonly MovieDbContext _dbContext;

        public MovieDbRepository(MovieDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Genre> AllGenres()
        {
            return _dbContext.Genre.ToList();
        }

        public IEnumerable<Movie> AllMovies()
        {
            return _dbContext.Movie.Include(m => m.Genre).ToList();
        }

        public async Task<IEnumerable<Movie>> AllMoviesAsync()
        {
            return await _dbContext.Movie.Include(m => m.Genre).ToListAsync();
        }

        public IEnumerable<Movie> AllMoviesWithGenre(Genre genre)
        {
            return _dbContext.Movie.Include(m => m.Genre)
                .Where(m => m.Genre.Id == genre.Id).ToList();
        }

        public Movie MovieById(int id)
        {
            return _dbContext.Movie.Include(m => m.Genre).First(m => m.Id == id);
        }

        public Genre GenreById(int id)
        {
            return _dbContext.Genre.First(g => g.Id == id);
        }

        public void CreateMovie(Movie movie)
        {
            _dbContext.Movie.Add(movie);
            _dbContext.SaveChanges();
        }

        public void UpdateMovie(int id, Movie movie)
        {
            var oldMovie = MovieById(id);
            oldMovie.Title = movie.Title;
            oldMovie.Genre = movie.Genre is null ? null : GenreById(movie.Genre.Id);
            oldMovie.DateReleased = movie.DateReleased;
            _dbContext.SaveChanges();
        }

        public void DeleteMovie(int id)
        {
            _dbContext.Movie.Remove(MovieById(id));
            _dbContext.SaveChanges();
        }

        public async Task DeleteMovieAsync(int id)
        {
            _dbContext.Movie.Remove(MovieById(id));
            await _dbContext.SaveChangesAsync();
        }
    }
}
