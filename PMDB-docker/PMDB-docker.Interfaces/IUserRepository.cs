using System;
using System.Collections.Generic;
using System.Text;
using PMDB_docker.Models;

namespace PMDB_docker.Interfaces
{
    public interface IUserRepository
    {
        UserDto GetUser(int id);
        IEnumerable<UserDto> GetAllUsers();
        UserDto Add(UserDto user);
        UserDto Update(UserDto userChanges);
        UserDto Delete(int id);

    }
}
