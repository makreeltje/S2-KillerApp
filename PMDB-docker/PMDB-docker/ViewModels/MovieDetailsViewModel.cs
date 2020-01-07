using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Threading.Tasks;
using PMDB_docker.Models;

namespace PMDB_docker.ViewModels
{
    public class MovieDetailsViewModel
    {
        public MovieDto Movie { get; set; }
        public string PageTitle { get; set; }
        public string RuntimeTimeFormat { get; set; }
        public List<ReviewDto> Reviews { get; set; }
        public List<VideosDto> Videos { get; set; }
        public List<ImageDto> Backdrops { get; set; }
        public List<ImageDto> Posters { get; set; }
    }
}
