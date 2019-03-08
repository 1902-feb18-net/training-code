using Microsoft.AspNetCore.Mvc;
using Moq;
using MoviesSite.App.Controllers;
using MoviesSite.App.ViewModels;
using MoviesSite.BLL;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MoviesSite.Tests.App.Controllers
{
    public class CreateTests
    {
        [Fact]
        public void GetCreateReturnsEmptyViewForEmptyGenres()
        {
            // arrange

            // controller needs a repo.
            // my automated tests should not alter my actual app's database.
            // i could try to provide a MovieDbRepo... but that needs a DbContext
            //    i could use InMemory or SQLite for that DbContext.....

            // but now we are quite far from "unit test"
            //     that would really be an "integration test"
            // so how can we unit test?
            // i do have a MovieRepository that doesn't need DbContext... but
            //   that would still be integration test.

            var fakeRepo = new FakeMovieRepository();
            var sut = new MoviesController(fakeRepo);
            // i need some IMovieRepo that gives GetAllGenres as empty

            // act
            ActionResult result = sut.Create();

            // assert
            //   ... that the return value is actually a ViewResult
            //   we could rely on thrown exceptions from downcast
            //var viewResult = (ViewResult)result;

            // here we cast and assert that the cast worked
            ViewResult viewResult = Assert.IsAssignableFrom<ViewResult>(result);

            // assert that the model is a MovieViewModel
            MovieViewModel viewModel = Assert.IsAssignableFrom<MovieViewModel>(viewResult.Model);

            // assert that genres is empty
            Assert.Empty(viewModel.Genres);
        }

        [Fact]
        public void GetCreateReturnsGenresView()
        {
            // arrange
            var genres = new List<Genre> { new Genre { Id = 1, Name = "Action" } };
            var mockRepo = new Mock<IMovieRepository>();
            //   set up the mock object
            mockRepo.Setup(r => r.AllGenres()).Returns(genres);
            // give the set-up mock object to the object to be tested.
            var sut = new MoviesController(mockRepo.Object);

            // act
            var result = sut.Create();

            // assert
            var viewResult = Assert.IsAssignableFrom<ViewResult>(result);
            // IsAssignableFrom also does a null check
            var viewModel = Assert.IsAssignableFrom<MovieViewModel>(viewResult.Model);

            // we can compare that two collections have equal elements
            //  with Assert.True and LINQ SequenceEqual
            //    (but be aware that uses == for equality, we often want value equality
            //     for reference types here... so we often write like this instead..)
            Assert.Equal(expected: genres.Count, actual: viewModel.Genres.Count);

            for (var i = 0; i < genres.Count; i++)
            {
                Assert.Equal(expected: genres[i].Id, actual: viewModel.Genres[i].Id);
                Assert.Equal(expected: genres[i].Name, actual: viewModel.Genres[i].Name);
            }

            // some more complex stuff with mock setup.....
            mockRepo.Setup(x => x.AllMoviesWithGenre(null)).Throws<ArgumentNullException>();

            // we give parameters to the lambda we setup with It.IsAny, It.IsNotNull, etc.
            mockRepo.Setup(x => x.AllMoviesWithGenre(It.IsNotNull<Genre>())).Returns(new Movie[0]);
            // for async methods... we have ReturnsAsync
        }
    }

    // fakes substitute for dependencies of objects we want to test,
    //   making those tests actual unit tests. we assume that the fake works
    //   (it should be very simple in logic)
    // mocks are easier to set up than fakes, but, bit of learning curve.
    public class FakeMovieRepository : IMovieRepository
    {
        public IEnumerable<Genre> AllGenres() => new List<Genre>();

        public IEnumerable<Movie> AllMovies() => throw new NotImplementedException();
        public Task<IEnumerable<Movie>> AllMoviesAsync() => throw new NotImplementedException();
        public IEnumerable<Movie> AllMoviesWithGenre(Genre genre) => throw new NotImplementedException();
        public void CreateMovie(Movie movie) => throw new NotImplementedException();
        public void DeleteMovie(int id) => throw new NotImplementedException();
        public Task DeleteMovieAsync(int id) => throw new NotImplementedException();
        public Genre GenreById(int id) => throw new NotImplementedException();
        public Movie MovieById(int id) => throw new NotImplementedException();
        public void UpdateMovie(int id, Movie movie) => throw new NotImplementedException();
    }
}
