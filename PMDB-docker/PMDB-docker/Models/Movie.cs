using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PMDB_docker.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Plot { get; set; }

        public Genre Genre { get; set; }
        //public string Runtime { get; set; }
        //public string Studio { get; set; }
        //public string Website { get; set; }
        //public string ReleaseDate { get; set; }
    }
}
