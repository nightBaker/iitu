using iitu.web.example.Models.Movies;
using iitu.web.example.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace iitu.web.example.tests
{
    public class MovieServiceTests
    {
        [Fact]
        public async Task AddTest()
        {
            var fakeRepository = Mock.Of<IMovieRepository>();
            var movieService = new MovieService(fakeRepository);

            var movie = new Movie() { Name = "test movie" };
            await movieService.AddAndSave(movie);
        }

        [Fact]
        public async Task GetMoviesTest()
        {
            var movies = new List<Movie>
            {
                new Movie() { Name = "test movie 1" },
                new Movie() { Name = "test movie 2" },
            };

            var fakeRepositoryMock = new Mock<IMovieRepository>();
            fakeRepositoryMock.Setup(x => x.GetAll()).ReturnsAsync(movies);


            var movieService = new MovieService(fakeRepositoryMock.Object);

            var resultMovies = await movieService.GetMovies();

            Assert.Collection(resultMovies, movie =>
            {
                Assert.Equal("test movie 1", movie.Name);
            },
            movie =>
            {
                Assert.Equal("test movie 2", movie.Name);
            });
        }

        [Fact]
        public async Task SearchTest()
        {
            var movies = new List<Movie>
            {
                new Movie() { Name = "test movie 1" },
                new Movie() { Name = "test movie 2" },
            };

            var fakeRepositoryMock = new Mock<IMovieRepository>();
            fakeRepositoryMock.Setup(x => x.GetMovies(It.IsAny<Expression<Func<Movie,bool>>>())).ReturnsAsync(movies);


            var movieService = new MovieService(fakeRepositoryMock.Object);

            var resultMovies = await movieService.Search("TEST");

            Assert.Collection(resultMovies, movie =>
            {
                Assert.Equal("test movie 1", movie.Name);
            },
            movie =>
            {
                Assert.Equal("test movie 2", movie.Name);
            });
        }
    }
}
