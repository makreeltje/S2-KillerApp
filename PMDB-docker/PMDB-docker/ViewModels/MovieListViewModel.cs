using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Threading.Tasks;
using PMDB_docker.Models;

namespace PMDB_docker.ViewModels
{
    public class MovieListViewModel
    {
        public string PageTitle { get; set; }
        public List<MovieDto> Movies { get; set; }
    }
}
