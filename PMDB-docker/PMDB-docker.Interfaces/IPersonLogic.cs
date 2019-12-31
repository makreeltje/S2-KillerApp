using System;
using System.Collections.Generic;
using System.Text;
using PMDB_docker.Models;

namespace PMDB_docker.Interfaces
{
    public interface IPersonLogic
    {
        void CheckIfPersonExists(PeopleDto person);
        List<RoleDto> GetPeopleForMovie(int movieId);
    }
}
