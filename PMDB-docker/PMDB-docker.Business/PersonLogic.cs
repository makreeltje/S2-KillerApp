using PMDB_docker.Interfaces;
using PMDB_docker.Models;
using System.Collections.Generic;

namespace PMDB_docker.Business
{
    public class PersonLogic : IPersonLogic
    {
        private List<RoleDto> _personList;
        private readonly IPersonData _personData;

        public PersonLogic(IPersonData personData)
        {
            _personData = personData;
            //_personList = new List<RoleDto>(_personData.GetAllPeople());
        }
        public void CheckIfPersonExists(PeopleDto person)
        {
            if (_personData.CheckPerson(person) < 1)
                AddPerson(person);
        }

        public List<RoleDto> GetPeopleForMovie(int movieId)
        {
            _personList = _personData.GetPeopleForMovie(movieId);
            return _personList;
        }


        private void AddPerson(PeopleDto person)
        {
            _personData.AddPerson(person);
        }
    }
}
