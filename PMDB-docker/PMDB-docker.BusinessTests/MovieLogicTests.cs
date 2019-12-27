using Microsoft.VisualStudio.TestTools.UnitTesting;
using PMDB_docker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PMDB_docker.Business;
using PMDB_docker.Data;
using PMDB_docker.Data.Movie;
using PMDB_docker.Interfaces;

namespace PMDB_docker.Models.Tests
{
    [TestClass()]
    public class MovieLogicTests
    {
        private MovieLogic _movieLogic;
        private IMovieData _movieData;
        private ITmdbLogic _tmdbLogic;
        private IGenreLogic _genreLogic;
        private IGenreData _genreData;

        [TestInitialize]
        public void TestInit()
        {
            _movieData = new MovieDatabaseHandler();
            _genreData = new GenreDatabaseHandler();
            _genreLogic = new GenreLogic(_genreData);
            _movieLogic = new MovieLogic(_movieData,_tmdbLogic,_genreLogic);
        }
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
                if(movie.ShortenedPlot != null)
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