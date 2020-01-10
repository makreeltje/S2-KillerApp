using System;
using System.Collections.Generic;
using PMDB_docker.Interfaces;
using PMDB_docker.Models;

namespace PMDB_docker.Business
{
    public class MovieLogic : IMovieLogic
    {
        private List<MovieDto> _movieList;
        private readonly IMovieData _movieData;

        // TODO: Via een constructor mee geven wat voor een data structuur je wilt gebruiken, denk bij Mock, inMemory of database
        //MovieDatabaseHandler handler = new MovieDatabaseHandler();

        public MovieLogic(IMovieData movieData)
        {
            _movieData = movieData;
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
            _movieData.AddMovie(movie);
            _movieList = _movieData.GetAllMovies();
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
            _movieList.RemoveAt(_movieList.FindIndex(m => m.Id == movie.Id));
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

        public List<MovieDto> SearchMovie(string query)
        {
            _movieList = _movieData.SearchMovie(query);
            return _movieList;
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
