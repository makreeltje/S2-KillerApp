using System;
using System.Collections.Generic;
using System.Text;

namespace PMDB_docker.Models
{
    public class PeopleDto
    {
        public int Id { get; set; }
        public int TmdbId { get; set; }
        public string Name { get; set; }
        public int Gender { get; set; }
        public string Biography { get; set; }
        public string PlaceOfBirth { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public DateTime? DateOfDeath { get; set; }
        public string KnownFor { get; set; }
        public string ProfilePath { get; set; }
        public string HomePage { get; set; }
    }
}
