using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;

namespace PMDB_docker.Models
{
    public class RoleDto
    {
        //public enum Gender
        //{
        //    Unknown,
        //    Female,
        //    Male
        //}
        public int Id { get; set; }
        public int TmdbId { get; set; }
        public string Name { get; set; }
        public string Character { get; set; }
        public int? Order { get; set; }
        public string Department { get; set; }
        public string Job { get; set; }
        public string ProfilePath { get; set; }
    }
}
