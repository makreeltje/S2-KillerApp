using Microsoft.VisualStudio.TestTools.UnitTesting;
using PMDB_docker.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PMDB_docker.Interfaces;
using PMDB_docker.Models;
using TMDbLib.Objects.Lists;

namespace PMDB_docker.Data.Tests
{
    

    [TestClass()]
    public class PersonDatabaseHandlerTests
    {
        private readonly IPersonData _personData = new PersonDatabaseHandler("server=meelsnet.nl;user id=pmdb;persistsecurityinfo=True;database=pmdb;password=IqtOPJ8Udt0O;");
        private List<PeopleDto> _persons;

        [TestMethod()]
        public void GetEveryPersonFromDatabase_GetAllPeople_IsTrue()
        {
            // Arrange
            _persons = new List<PeopleDto>();

            // Act
            _persons = _personData.GetAllPeople();

            // Assert
            Assert.IsTrue(_persons.Count > 0);
        }

        [TestMethod()]
        public void CheckIfPersonExistsInDatabase_CheckPerson_IsTrue()
        {
            // Arrange
            PeopleDto person = new PeopleDto()
            {
                TmdbId = 25072
            };

            // Act // Assert
            Assert.IsTrue(_personData.CheckPerson(person) == 1);
        }

        [TestMethod()]
        public void AddNewPersonToTheDatabase_AddPerson_IsTrue()
        {
            // Arrange
            _persons = new List<PeopleDto>();
            PeopleDto person = new PeopleDto()
            {
                TmdbId = 9999999
            };

            // Act
            _personData.AddPerson(person);
            _persons = _personData.GetAllPeople();

            // Assert
            Assert.IsTrue(_persons.Last().TmdbId == person.TmdbId);

            // Cleanup
            _personData.RemovePerson(person);
        }

        [TestMethod()]
        public void GetPeopleThatHaveConnectionToSpecificMovie_GetPeopleForMovie_IsTrue()
        {
            // Arrange
            _persons = new List<PeopleDto>();
            List<RoleDto> roles = new List<RoleDto>();

            // Act
            roles = _personData.GetPeopleForMovie(707);

            // Assert
            Assert.IsTrue(roles.Count > 0);
        }

        [TestMethod()]
        public void RemovePersonFromTheDatabase_RemovePerson_IsFalse()
        {
            // Arrange
            _persons = new List<PeopleDto>();
            PeopleDto person = new PeopleDto()
            {
                TmdbId = 999999
            };
            _personData.AddPerson(person);
            _persons = _personData.GetAllPeople();
            person.Id = _persons.Last().Id;

            // Act
            _personData.RemovePerson(person);
            _persons = _personData.GetAllPeople();

            // Assert
            Assert.IsFalse(_persons.Last().Id == person.Id);
        }
    }
}