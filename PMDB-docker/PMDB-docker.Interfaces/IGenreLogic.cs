using System;
using System.Collections.Generic;
using System.Text;
using PMDB_docker.Models;
using TMDbLib.Objects.General;

namespace PMDB_docker.Interfaces
{
    public interface IGenreLogic
    {
        List<GenreDto> GetAllGenres();
        bool CheckGenre(string genre);
        void AddGenre(string genre);
        List<GenreDto> GetGenreForMovie(int movieId);
    }
}
