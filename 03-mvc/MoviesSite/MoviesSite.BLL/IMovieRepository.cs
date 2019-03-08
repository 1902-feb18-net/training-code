using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoviesSite.BLL
{
    public interface IMovieRepository
    {
        IEnumerable<Genre> AllGenres();
        IEnumerable<Movie> AllMovies();
        Task<IEnumerable<Movie>> AllMoviesAsync();
        IEnumerable<Movie> AllMoviesWithGenre(Genre genre);
        void CreateMovie(Movie movie);
        void DeleteMovie(int id);
        Task DeleteMovieAsync(int id);
        Genre GenreById(int id);
        Movie MovieById(int id);
        void UpdateMovie(int id, Movie movie);
    }
}