using System;
using PMDB_docker.Interfaces;
using System.Collections.Generic;
using System.Linq;
using PMDB_docker.Business;
using TMDbLib.Client;
using TMDbLib.Objects.People;

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
            return _movieList.Find(m => m.Id == Id);
        }

        public List<MovieDto> GetSixRandomMovies()
        {
            _movieList.Clear();
            for (int i = 0; i < 6; i++)
            {
                MovieDto movie = new MovieDto();
                movie = _movieData.GetRandomMovie();
                _movieList.Add(movie);
            }

            return _movieList;
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
            int maxLenght = 97;
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
            _movieData.EditMovie(movie);
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

        public void UpdateGenres(List<GenreDto> genres, int movieId)
        {
            _movieData.CheckGenreConnection(genres, movieId);
        }

        public void UpdatePeopleMovie(List<RoleDto> role, int movieId)
        {
            _movieData.CheckPeopleConnection(role, movieId);
        }
    }
}
