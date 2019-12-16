using System;
using System.Collections.Generic;
using System.Text;
using PMDB_docker.Models;

namespace PMDB_docker.Interfaces
{
    public interface IUserData
    {
        public IEnumerable<UserDto> GetAllUsers();
        public void AddUser(UserDto user);
    }
}
