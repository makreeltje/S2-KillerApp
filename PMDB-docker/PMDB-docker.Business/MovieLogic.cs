using System;
using PMDB_docker.Interfaces;
using System.Collections.Generic;
using System.Linq;
using PMDB_docker.Business;
using TMDbLib.Client;

namespace PMDB_docker.Models
{
    public class MovieLogic : IMovieLogic
    {
        private readonly List<MovieDto> _movieList;
        private readonly IMovieData _movieData;
        private readonly ITmdbLogic _tmdbLogic;

        // TODO: Via een constructor mee geven wat voor een data structuur je wilt gebruiken, denk bij Mock, inMemory of database
        //MovieDatabaseHandler handler = new MovieDatabaseHandler();

        public MovieLogic(IMovieData movieData, ITmdbLogic tmdbLogic)
        {
            _movieData = movieData;
            _tmdbLogic = tmdbLogic;
            _movieList = new List<MovieDto>(_movieData.GetAllMovies());

        }
        public MovieDto GetMovie(int Id)
        {
            return _movieList.FirstOrDefault(m => m.Id == Id);
        }

        public List<MovieDto> GetAllMovies()
        {
            return _movieList;
        }

        public MovieDto Add(MovieDto movie)
        {
            movie.Id = _movieList.Max(m => m.Id) + 1;
            _movieList.Add(movie);
            return movie;
        }

        private string ShortenOverviewText(string text)
        {
            int maxLenght = 100;
            string plotShortened;
            if (text != null)
            {
                if (text.Length > maxLenght)
                {
                    plotShortened = text.Substring(0, maxLenght) + "...";
                    return plotShortened;
                }
            }
            return text;
        }

        public List<MovieDto> GetAllMoviesForListPage()
        {
            foreach (var movie in _movieList)
            {
                movie.ShortenedPlot = ShortenOverviewText(movie.Overview);
            }
            return _movieList;
        }

        public List<MovieDto> RemoveMovie(MovieDto movie)
        {
            _movieData.RemoveMovie(movie);
            _movieList.RemoveAt(movie.Id);
            return _movieList;
        }

        public void UpdateMovie(MovieDto movie)
        {
            _movieData.EditMovie(_tmdbLogic.UpdateMovie(movie));
        }

        public string FormatRuntime(int? runtime)
        {
            if (runtime != null)
            {
                TimeSpan ts = TimeSpan.FromMinutes((int)runtime);
                return $"{ts.Hours:00}:{ts.Minutes:00}";
            }

            return "-";

        }
    }
}
