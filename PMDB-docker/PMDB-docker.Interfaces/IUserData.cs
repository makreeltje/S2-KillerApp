using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;
using PMDB_docker.Models;

namespace PMDB_docker.Interfaces
{
    public interface IUserData
    {
        public IEnumerable<UserDto> GetAllUsers();
        public void AddUser(UserDto user);
        public void EditUser(UserDto user);

        public void RemoveUser(int id);
    }
}
