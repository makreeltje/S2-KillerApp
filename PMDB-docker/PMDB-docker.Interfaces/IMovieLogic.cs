using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PMDB_docker.Models;

namespace PMDB_docker.Interfaces
{
    public interface IMovieLogic
    {
        MovieDto GetMovie(int Id);
        IEnumerable<MovieDto> GetAllMovies();
        MovieDto Add(MovieDto movie);
        IEnumerable<MovieDto> GetAllMoviesForDetailsPage();
        IEnumerable<MovieDto> RemoveMovie(int id);
    }
}
