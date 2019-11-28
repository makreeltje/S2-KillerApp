using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PMDB_docker.Models
{
    public class MovieDto
    {
        public int Id { get; set; }
        [Required, MaxLength(50, ErrorMessage = "Title cannot exceed 50 characters")]
        public string Title { get; set; }
        [Display(Name = "Plot of the movie")]
        [Required]
        public string Plot { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9]+\:\/\/[a-zA-Z0-9]+\.[a-zA-Z0-9]+$", ErrorMessage = "Invalid website name")]
        public string Website { get; set; }
        [Required]
        public GenreDto? Genre { get; set; }
        //public string Runtime { get; set; }
        //public string Studio { get; set; }
        //public string Website { get; set; }
        //public string ReleaseDate { get; set; }
    }
}
