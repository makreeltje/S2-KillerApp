using System;
using System.Collections.Generic;
using System.Text;

namespace PMDB_docker.Data.Movie
{
    public class MovieDto
    {
        public int Id { get; set; }
        public string ImdbId { get; set; }
        public string TmdbId { get; set; }
        public string Title { get; set; }
        public string Plot { get; set; }
        public int Runtime { get; set; }
        public string ReleaseDate { get; set; }
        public string Website { get; set; }
        public string Studio { get; set; }
    }
}
