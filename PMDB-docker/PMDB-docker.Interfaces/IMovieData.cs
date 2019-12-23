using System;
using System.Collections.Generic;
using System.Text;
using PMDB_docker.Models;

namespace PMDB_docker.Interfaces
{
    public interface IMovieData
    {
        IEnumerable<MovieDto> GetAllMovies();
        MovieDto AddMovie(MovieDto movie);
        void RemoveMovie(int id);
        void EditMovie(MovieDto movie);
    }
}
