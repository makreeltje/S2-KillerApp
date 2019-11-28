using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PMDB_docker.Models;

namespace PMDB_docker.Interfaces
{
    public interface IMovieContainerData
    {
        MovieDto GetMovie(int Id);
        IEnumerable<MovieDto> GetAllMovies();
        MovieDto Add(MovieDto movie);
    }
}
