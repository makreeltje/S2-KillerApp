using Microsoft.VisualStudio.TestTools.UnitTesting;
using PMDB_docker.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PMDB_docker.Interfaces;
using PMDB_docker.Models;

namespace PMDB_docker.Data.Tests
{
    [TestClass()]
    public class GenreDatabaseHandlerTests
    {
        private readonly IGenreData _genreData = new GenreDatabaseHandler("server=meelsnet.nl;user id=pmdb;persistsecurityinfo=True;database=pmdb;password=IqtOPJ8Udt0O;");
        private List<GenreDto> _genreList;

        [TestMethod()]
        public void GetAllGenresFromTheDatabase_GetAllGenres_IsTrue()
        {
            // Arrange
            _genreList = new List<GenreDto>();

            // Act
            _genreList = _genreData.GetAllGenres();

            // Assert
            Assert.IsTrue(_genreList.Count > 0);
        }

        [TestMethod()]
        public void CheckIfGenreExistsInDatabase_CheckGenre_IsTrue()
        {
            // Arrange
            GenreDto genre = new GenreDto()
            {
                Name = "Action"
            };

            // Act // Assert
            Assert.IsTrue(_genreData.CheckGenre(genre.Name) == 1);
        }

        [TestMethod()]
        public void CheckIfGenreDoesNotExistsInDatabase_CheckGenre_IsFalse()
        {
            // Arrange
            GenreDto genre = new GenreDto()
            {
                Name = "Pannekoek"
            };

            // Act // Assert
            Assert.IsFalse(_genreData.CheckGenre(genre.Name) == 1);
        }

        [TestMethod()]
        public void AddNewGenreToDatabase_AddGenre_IsTrue()
        {
            // Arrange
            _genreList = new List<GenreDto>();
            GenreDto genre = new GenreDto()
            {
                Name = "Pannekoek"
            };

            // Act
            _genreData.AddGenre(genre.Name);
            _genreList = _genreData.GetAllGenres();


            // Assert
            Assert.IsTrue(_genreList.Last().Name == genre.Name);
        }

        [TestMethod()]
        public void GetAllGenresFromSpecificMovie_GetGenreForMovie_IsTrue()
        {
            // Arrange
            _genreList = new List<GenreDto>();

            // Act
            _genreList = _genreData.GetGenreForMovie(888);


            // Assert
            Assert.IsTrue(_genreList.Count > 0);
        }

    }
}