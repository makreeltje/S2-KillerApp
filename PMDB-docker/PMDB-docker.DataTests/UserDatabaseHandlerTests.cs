using Microsoft.VisualStudio.TestTools.UnitTesting;
using PMDB_docker.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Extensions.Configuration;
using PMDB_docker.Interfaces;
using PMDB_docker.Models;

namespace PMDB_docker.Data.Tests
{
    [TestClass()]
    public class UserDatabaseHandlerTests
    {
        private readonly IUserData _userData = new UserDatabaseHandler("server=meelsnet.nl;user id=pmdb;persistsecurityinfo=True;database=pmdb;password=IqtOPJ8Udt0O;");
        private List<UserDto> _userList;

        [TestInitialize]
        public void TestInit()
        {
            _userList = new List<UserDto>();
        }

        [TestMethod()]
        public void GetUsers_GetAllUsers_SeeIfListIsFilled()
        {
            // Arrange
            _userList = new List<UserDto>();

            // Act
            _userList = _userData.GetAllUsers();

            // Assert
            Assert.IsTrue(_userList.Count > 0);
        }

        [TestMethod()]
        public void AddUserToDatabase_AddUser_SeeIfUserIsCreated()
        {
            // Arrange
            UserDto user = new UserDto();
            user.Username = "Test";
            user.Email = "test@test.nl";
            user.Password = "test123";

            // Act
            _userData.AddUser(user);
            _userList = _userData.GetAllUsers();

            // Assert
            Assert.IsTrue(_userList.Last().Username == "Test" && _userList.Last().Email == "test@test.nl");

            _userData.RemoveUser(_userList.Last().Id);

        }

        [TestMethod()]
        public void ChangeUsersUsername_EditUser_SeeIfChangeHasBeenMade()
        {
            // Arrange
            UserDto user = new UserDto();
            user.Username = "Test";
            user.Email = "test@test.nl";
            user.Password = "test123";

            // Act
            _userData.AddUser(user);
            _userList = _userData.GetAllUsers();

            // Arrange
            user.Username = "Seemand";
            user.Id = _userList.Last().Id;

            // Act
            _userData.EditUser(user);
            _userList = new List<UserDto>(_userData.GetAllUsers());

            Assert.IsTrue(_userList.Last().Username == "Seemand");

            _userData.RemoveUser(_userList.Last().Id);
        }

        [TestMethod()]
        public void RemoveLastUserInList_RemoveUser_SeeIfUserStillExists()
        {
            // Arrange
            UserDto user = new UserDto();
            user.Username = "Test";
            user.Email = "test@test.nl";
            user.Password = "test123";

            // Act
            _userData.AddUser(user);
            _userList = _userData.GetAllUsers();
            user.Id = _userList.Last().Id;
            _userData.RemoveUser(_userList.Last().Id);
            _userList = _userData.GetAllUsers();

            // Assert
            Assert.IsFalse(_userList.Last().Id == user.Id);
        }
    }
}