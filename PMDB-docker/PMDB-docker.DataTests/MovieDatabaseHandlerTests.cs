using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PMDB_docker.Data;
using PMDB_docker.Interfaces;
using PMDB_docker.Models;

namespace PMDB_docker.DataTests
{
    [TestClass()]
    public class MovieDatabaseHandlerTests
    {
        private readonly IMovieData _movieData = new MovieDatabaseHandler("server=meelsnet.nl;user id=pmdb;persistsecurityinfo=True;database=pmdb;password=IqtOPJ8Udt0O;");
        private List<MovieDto> _movieList;
        private MovieDto _movie;

        [TestInitialize]
        public void TestInit()
        {
            _movieList = new List<MovieDto>();
        }

        [TestMethod()]
        public void FillListWithMovies_GetAllMoviesTest_IsTrue()
        {
            // Arrange
            _movieList = new List<MovieDto>();

            // Act
            _movieList = _movieData.GetAllMovies();

            // Assert
            Assert.IsTrue(_movieList.Count > 0);
        }

        [TestMethod()]
        public void GrabRandomMovieFromDatabase_GetRandomMovie_IsTrue()
        {
            // Arrange
            _movie = new MovieDto();
            MovieDto newMovie = new MovieDto();

            // Act
            newMovie = _movieData.GetRandomMovie();
            

            // Assert
            Assert.IsTrue(_movie != newMovie);
        }

        [TestMethod()]
        public void AddNewMovieToDatabase_AddMovie_IsTrue()
        {
            // Arrange
            _movie = new MovieDto();

            // Act
            _movie.Title = "NEW MOVIE";
            _movieData.AddMovie(_movie);
            _movieList = _movieData.GetAllMovies();
            _movie.Id = _movieList.Find(t => t.Title == "NEW MOVIE").Id;


            // Assert
            Assert.IsTrue(_movieList.Find(t => t.Title == "NEW MOVIE").Title == "NEW MOVIE");

            // Cleanup
            _movieData.RemoveMovie(_movie);
        }

        [TestMethod()]
        public void RemoveMovieToDatabase_RemoveMovie_IsFalse()
        {
            // Arrange
            _movie = new MovieDto();
            _movie.Title = "NEW MOVIE";
            _movieData.AddMovie(_movie);
            _movieList = _movieData.GetAllMovies();
            _movie.Id = _movieList.Last().Id;

            // Act
            _movieData.RemoveMovie(_movie);
            _movieList = _movieData.GetAllMovies();

            // Assert
            Assert.IsFalse(_movieList.Last().Id == _movie.Id);
        }

        [TestMethod()]
        public void EditAnExistingMovie_EditMovie_IsTrue()
        {
            // Arrange
            _movie = new MovieDto();
            _movie.Title = "NEW MOVIE";
            _movieData.AddMovie(_movie);
            _movieList = _movieData.GetAllMovies();
            _movie.Id = _movieList.Last().Id;

            // Act
            _movie.Title = "EDITED MOVIE";
            _movieData.EditMovie(_movie);
            _movieList = _movieData.GetAllMovies();

            // Assert
            Assert.IsTrue(_movieList.Last().Title == "EDITED MOVIE");

            // Cleanup
            _movieData.RemoveMovie(_movie);
        }

        //[TestMethod()]
        //public void CheckConnectionBetweenGenreAndMovie_UpdateGenres_IsTrue()
        //{
        //    // Arrange
        //    List<GenreDto> genres = new List<GenreDto>();
        //    GenreDto genre = new GenreDto()
        //    {
        //        Id = 20,
        //        Name = "Horror"
        //    };
        //    genres.Add(genre);

        //    // Act
        //    _movieData.CheckGenreConnection(genres, 888);

        //    // Assert
        //    Assert.IsTrue();

        //    // Cleanup
        //}

        [TestMethod()]
        public void SearchForSpecificMovie_SearchMovie_IsTrue()
        {
            // Arrange
            _movieList = new List<MovieDto>();

            // Act
            _movieList = _movieData.SearchMovie("Mission");

            // Assert
            Assert.IsTrue(_movieList.Count > 0);

            // Cleanup
        }
    }
}