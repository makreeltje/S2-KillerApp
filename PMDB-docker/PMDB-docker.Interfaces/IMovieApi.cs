using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PMDB_docker.Models;

namespace PMDB_docker.Interfaces
{
    public interface IMovieApi
    {
        Task RunAsync();
        Task<MovieApiDto> GetMovieAsync(string path);
    }
}
