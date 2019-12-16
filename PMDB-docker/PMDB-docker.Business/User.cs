using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using PMDB_docker.Data;
using PMDB_docker.Data.Movie;
using PMDB_docker.Interfaces;
using PMDB_docker.Models;

namespace PMDB_docker.Business
{
    public class User : IUserLogic
    {
        private readonly List<UserDto> _userList;
        UserDatabaseHandler handler = new UserDatabaseHandler();

        // TODO: Via een constructor mee geven wat voor een data structuur je wilt gebruiken, denk bij Mock, inMemory of database
        //MovieDatabaseHandler handler = new MovieDatabaseHandler();

        public User()
        {
            _userList = new List<UserDto>(handler.GetAllUsers());
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
            handler.AddUser(user);
            return user;
        }

        public UserDto Update(UserDto userChanges)
        {
            UserDto user = _userList.FirstOrDefault(u => u.Id == userChanges.Id);
            if (user != null)
                user = userChanges;
            return user;
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
