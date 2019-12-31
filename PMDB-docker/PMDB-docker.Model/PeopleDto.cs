using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace PMDB_docker.Models
{
    public class PeopleDto
    {
        //public enum Gender
        //{
        //    Unknown,
        //    Female,
        //    Male
        //}
        public int Id { get; set; }
        public string Character { get; set; }
        public string CreditId { get; set; }
        public int Gender { get; set; }
        public int PeopleId { get; set; }
        public string Name { get; set; }
        public int Order { get; set; }
        public string ProfilePath { get; set; }
    }
}
