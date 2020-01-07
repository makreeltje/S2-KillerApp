using System;
using System.Collections.Generic;
using System.Text;
using PMDB_docker.Models;

namespace PMDB_docker.Interfaces
{
    public interface IPersonData
    {
        List<PeopleDto> GetAllPeople();
        int CheckPerson(PeopleDto person);
        void AddPerson(PeopleDto person);
        List<RoleDto> GetPeopleForMovie(int movieId);
        void RemovePerson(PeopleDto person);
    }
}
