﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using PMDB_docker.Interfaces;
using PMDB_docker.Models;

namespace PMDB_docker.Business
{
    public class User : IUserLogic
    {
        private readonly List<UserDto> _userList;
        private readonly IUserData _userData;

        // TODO: Via een constructor mee geven wat voor een data structuur je wilt gebruiken, denk aan Mock, inMemory of database

        public User(IUserData userData)
        {
            _userData = userData;
            _userList = new List<UserDto>(_userData.GetAllUsers());
        }
        public UserDto GetUser(int id)
        {
            return _userList.FirstOrDefault(m => m.Id == id);
        }

        public IEnumerable<UserDto> GetAllUsers()
        {
            throw new NotImplementedException();
        }

        public UserDto Add(UserDto user)
        {
            user.Id = _userList.Max(m => m.Id) + 1;
            _userList.Add(user);
            _userData.AddUser(user);
            return user;
        }

        public void Edit(UserDto userChanges)
        {
            _userData.EditUser(userChanges);
        }

        public UserDto Delete(int id)
        {
             UserDto user =_userList.FirstOrDefault(u => u.Id == id);
             if (user != null)
                 _userList.Remove(user);
             return user;
        }
        
    }
}
