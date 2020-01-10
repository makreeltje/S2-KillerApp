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
    public class UserLogicTests
    {
        private UserDto _user;
        private List<UserDto> _userList;
        private readonly IUserLogic _userLogic;
        private readonly IUserData _userData;

        public UserLogicTests()
        {
            _userData = new UserDatabaseHandler("server=meelsnet.nl;user id=pmdb;persistsecurityinfo=True;database=pmdb;password=IqtOPJ8Udt0O;");
            _userLogic = new UserLogic(_userData);
        }

        [TestInitialize]
        public void InitTest()
        {
            _user = new UserDto()
            {
                Username = "Testpersoon",
                Email = "test@email.nl",
                Password = "testpassword"
            };
            _userLogic.Add(_user);
            _userList = new List<UserDto>(_userLogic.GetAllUsers());
            _user.Id = _userList.Last().Id;
        }

        [TestMethod()]
        public void GetSpecificUser_GetUser_IsTrue()
        {
            // Arrange
            UserDto test = new UserDto();

            // Act
            test = _userLogic.GetUser(_user.Id);

            // Assert
            Assert.IsTrue(test.Username == "Testpersoon");
        }

        [TestMethod()]
        public void AddNewUser_Add_IsTrue()
        {
            // Arrange
            UserDto user = new UserDto()
            {
                Username = "Testpersoon",
                Email = "test@email.nl",
                Password = "testpassword"
            };

            // Act
            _userLogic.Add(_user);
            _userLogic.GetAllUsers();
            user.Id = _userList.Last().Id;

            // Assert
            Assert.IsTrue(user.Id > 0);

            _userLogic.Delete(user.Id);
        }

        [TestMethod()]
        public void DeleteTheNewlyCreatedUser_Delete_IsFalse()
        {
            // Arrange
            UserDto user = new UserDto()
            {
                Username = "Testpersoon",
                Email = "test@email.nl",
                Password = "testpassword"
            };
            _userLogic.Add(_user);
            _userLogic.GetAllUsers();
            user.Id = _userList.Last().Id;

            // Act
            _userLogic.Delete(user.Id);
            _userList = _userLogic.GetAllUsers();

            // Assert
            Assert.IsFalse(_userList.Exists(i => i.Id == user.Id));

            
        }

        [TestMethod()]
        public void ModifyAnExistingUserFromUserList_Edit_IsTrue()
        {
            // Arrange
            _user.FirstName = "VeryTest";

            // Act
            _userLogic.Edit(_user);
            _userLogic.GetAllUsers();

            // Assert
            Assert.IsFalse(_userList.Find(i => i.Id == _user.Id).FirstName == "VeryTest");


        }

        [TestCleanup]
        public void Cleanup()
        {
            _userLogic.Delete(_user.Id);
        }
    }
}