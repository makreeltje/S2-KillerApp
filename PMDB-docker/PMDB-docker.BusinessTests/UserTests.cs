using Microsoft.VisualStudio.TestTools.UnitTesting;
using PMDB_docker.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using PMDB_docker.Interfaces;
using PMDB_docker.Models;

namespace PMDB_docker.Business.Tests
{
    [TestClass()]
    public class UserTests
    {
        private UserDto _user;
        public List<UserDto> _userList;
        private IUserLogic _userLogic;


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
        public void GetSpecificUser_GetUser_SeeIfUserHasBeenGrabbed()
        {
            UserDto test = new UserDto();

            test = _userLogic.GetUser(_user.Id);

            Assert.IsTrue(test.Username == "Testpersoon");
            _userLogic.Delete(_user.Id);
        }

        [TestMethod()]
        public void AddTest()
        {
            throw new NotImplementedException();
        }
    }
}