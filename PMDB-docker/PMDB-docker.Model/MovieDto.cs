#nullable enable
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
        public string ImdbId { get; set; }
        public string TmdbId { get; set; }
        [Required, MaxLength(50, ErrorMessage = "Title cannot exceed 50 characters")]
        public string Title { get; set; }
        [Display(Name = "Plot of the movie")]
        [Required]
        public string Overview { get; set; }
        public string ShortenedPlot { get; set; }
        public string Image { get; set; }
        public int Runtime { get; set; }
        public DateTime ReleaseDate { get; set; }
        [Required]
        public string Website { get; set; }
        public string Studio { get; set; }
        [Required]
        public GenreDto? Genre { get; set; }
        public int Revenue { get; set; }
        public int Budget { get; set; }
        public string Status { get; set; }
        public string PosterBackdrop { get; set; }
        public double average_rating { get; set; }
        
        
        
    }
}
