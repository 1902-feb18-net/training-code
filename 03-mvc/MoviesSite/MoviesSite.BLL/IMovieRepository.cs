using System.Collections.Generic;

namespace MoviesSite.BLL
{
    public interface IMovieRepository
    {
        IEnumerable<Genre> AllGenres();
        IEnumerable<Movie> AllMovies();
        IEnumerable<Movie> AllMoviesWithGenre(Genre genre);
        void CreateMovie(Movie movie);
        void DeleteMovie(int id);
        Genre GenreById(int id);
        Movie MovieById(int id);
        void UpdateMovie(int id, Movie movie);
    }
}