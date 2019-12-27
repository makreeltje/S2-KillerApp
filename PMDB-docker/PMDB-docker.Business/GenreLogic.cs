using System;
using System.Collections.Generic;
using System.Text;
using PMDB_docker.Interfaces;
using PMDB_docker.Models;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Lists;

namespace PMDB_docker.Business
{
    public class GenreLogic : IGenreLogic
    {
        private List<GenreDto> _genreList;
        private readonly IGenreData _genreData;

        public GenreLogic(IGenreData genreData)
        {
            _genreData = genreData;
            _genreList = new List<GenreDto>(_genreData.GetAllGenres());
        }

        public void AddGenre(string genre)
        {
            _genreData.AddGenre(genre);
            _genreList = _genreData.GetAllGenres();
        }

        public List<GenreDto> GetGenreForMovie(int movieId)
        {
            return _genreData.GetGenreForMovie(movieId);
        }

        public List<GenreDto> GetAllGenres()
        {
            throw new NotImplementedException();
        }

        public bool CheckGenre(string genre)
        {
            if (_genreData.CheckGenre(genre) == 1)
                return true;
            return false;
        }
    }
}
