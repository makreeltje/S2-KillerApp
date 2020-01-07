using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Threading.Tasks;
using PMDB_docker.Models;
using X.PagedList;

namespace PMDB_docker.ViewModels
{
    public class MovieListViewModel
    {
        public string SearchQuery { get; set; }
        public string PageTitle { get; set; }
        public IPagedList<MovieDto> Movies { get; set; }
    }
}
