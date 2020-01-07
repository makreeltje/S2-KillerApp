using Microsoft.VisualStudio.TestTools.UnitTesting;
using PMDB_docker.Interfaces;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using PMDB_docker.Business;
using PMDB_docker.Data;
using PMDB_docker.Data.Movie;

namespace PMDB_docker.Models.Tests
{
    [TestClass()]
    public class MovieLogicTests
    {
        private static readonly IMovieData _movieData = new MovieDatabaseHandler("server=meelsnet.nl;user id=pmdb;persistsecurityinfo=True;database=pmdb;password=IqtOPJ8Udt0O;");
        private MovieLogic _movieLogic = new MovieLogic(_movieData);
        

        [TestMethod()]
        public void GetSpecificMovie_GetMovie_SeeIfValueIsCorrect()
        {
            MovieDto movie = new MovieDto();
            movie = _movieLogic.GetMovie(888);
            Assert.AreEqual("It Chapter Two", movie.Title);
        }
        [TestMethod()]
        public void GetEveryMovie_GetAllMovies_SeeIfListIsPopulated()
        {
            List<MovieDto> movies = new List<MovieDto>();

            movies = _movieLogic.GetAllMovies();

            Assert.IsTrue(movies.Count > 0);
        }

        [TestMethod()]
        public void AddNewMovie_Add_CheckIfMovieHasBeenAdded()
        {
            List<MovieDto> movies = new List<MovieDto>();
            MovieDto movie = new MovieDto();
            int i;

            movies = _movieLogic.GetAllMovies();
            i = movies.Count;

            movie.Id = movies.Max(m => m.Id);
            movies.Add(movie);

            Assert.IsTrue(movies.Count > i);
        }

        [TestMethod()]
        public void GetMoviesWithShortenedOverview_GetAllMoviesForListPage_SeeIfOverviewDoesNotExceedSpecifiedThreshold()
        {
            List<MovieDto> movies = new List<MovieDto>();

            movies = _movieLogic.GetAllMoviesForListPage();

            foreach (var movie in movies)
            {
                if (movie.ShortenedPlot != null)
                    Assert.IsTrue(movie.ShortenedPlot.Length <= 100);
            }
        }
        [TestMethod()]
        public void RemoveMovieFromList_RemoveMovie_SeeIfMovieHasBeenRemoved()
        {
            List<MovieDto> movies = new List<MovieDto>();
            MovieDto movie = new MovieDto();
            int i;

            movies = _movieLogic.GetAllMovies();
            movie = _movieLogic.GetMovie(movies.Last().Id);
            i = movies.Count();

            movies = _movieLogic.RemoveMovie(movie);

            Assert.IsTrue(movies.Count < i);

            _movieLogic.Add(movie);


        }
    }
}