using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PMDB_docker.Business;
using PMDB_docker.Data;
using PMDB_docker.Interfaces;
using PMDB_docker.Models;

namespace PMDB_docker.BusinessTests
{
    [TestClass()]
    public class MovieLogicTests
    {
        private MovieDto _movie;
        private List<MovieDto> _movieList;
        private readonly IMovieData _movieData;
        private readonly IMovieLogic _movieLogic;

        public MovieLogicTests()
        {
            _movieData = new MovieDatabaseHandler("server=meelsnet.nl;user id=pmdb;persistsecurityinfo=True;database=pmdb;password=IqtOPJ8Udt0O;");
            _movieLogic = new MovieLogic(_movieData);
        }
        

        [TestMethod()]
        public void GetSpecificMovie_GetMovie_SeeIfValueIsCorrect()
        {
            _movie = _movieLogic.GetMovie(888);
            Assert.AreEqual("It Chapter Two", _movie.Title);
        }
        [TestMethod()]
        public void GetEveryMovie_GetAllMovies_SeeIfListIsPopulated()
        {
            _movieList = _movieLogic.GetAllMovies();

            Assert.IsTrue(_movieList.Count > 0);
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
            _movieList = _movieLogic.GetAllMoviesForListPage();

            foreach (var movie in _movieList)
            {
                if (movie.ShortenedPlot != null)
                    Assert.IsTrue(movie.ShortenedPlot.Length <= 100);
            }
        }
        [TestMethod()]
        public void RemoveMovieFromList_RemoveMovie_SeeIfMovieHasBeenRemoved()
        {
            List<MovieDto> movies = new List<MovieDto>();
            MovieDto movie = new MovieDto()
            {
                Title = "NEW MOVIE"
            };
            _movieLogic.Add(movie);
            int i;

            movies = _movieLogic.GetAllMovies();
            movie = _movieLogic.GetMovie(movies.Max(m => m.Id));
            i = movies.Count();

            movies = _movieLogic.RemoveMovie(movie);
            

            Assert.IsTrue(movies.Count < i);


        }

        [TestMethod()]
        public void FormatRuntimeFromIntToTime_FormatRuntime_AreEqual()
        {
            // Arrange
            _movieList = _movieLogic.GetAllMovies();
            _movie = _movieLogic.GetMovie(888);

            string time;

            // Act
            time = _movieLogic.FormatRuntime(_movie.Runtime);

            // Assert
            Assert.AreEqual("02:49", time);
        }

        [TestMethod()]
        public void FormatRuntimeFromIntToTimeWithNoIntSet_FormatRuntime_AreEqual()
        {
            // Arrange
            MovieDto movie = new MovieDto();
            string time;

            // Act
            time = _movieLogic.FormatRuntime(movie.Runtime);

            // Assert
            Assert.AreEqual("-", time);
        }

        [TestMethod()]
        public void GetRandomMovies_GetSixRandomMovies_AreEqual()
        {
            // Act
            _movieList = _movieLogic.GetSixRandomMovies();

            // Assert
            Assert.AreEqual(6, _movieList.Count);
        }

        [TestMethod()]
        public void SearchForAMovieWithString_SearchMovie_IsTrue()
        {
            // Act
            _movieList = _movieLogic.SearchMovie("mission");

            // Assert
            Assert.IsTrue(_movieList.Count > 0);
        }
    }
}