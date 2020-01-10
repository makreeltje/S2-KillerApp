using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using PMDB_docker.Interfaces;
using PMDB_docker.Models;

namespace PMDB_docker.Business
{
    public class UserLogic : IUserLogic
    {
        private List<UserDto> _userList;
        private readonly IUserData _userData;

        // TODO: Via een constructor mee geven wat voor een data structuur je wilt gebruiken, denk aan Mock, inMemory of database

        public UserLogic(IUserData userData)
        {
            _userData = userData;
            _userList = new List<UserDto>(_userData.GetAllUsers());
        }

        public UserDto GetUser(int id)
        {
            return _userList.FirstOrDefault(m => m.Id == id);
        }

        public List<UserDto> GetAllUsers()
        {
            _userList = _userData.GetAllUsers();
            return _userList;
        }

        public UserDto Add(UserDto user)
        {
            _userData.AddUser(user);
            _userList = _userData.GetAllUsers();
            user.Id = _userList.Last().Id;

            return user;
        }

        public void Edit(UserDto userChanges)
        {
            _userData.EditUser(userChanges);
        }

        public void Delete(int id)
        {
            _userData.RemoveUser(id);
        }
        
    }
}
