using System;
using PMDB_docker.Interfaces;
using System.Collections.Generic;
using System.Linq;
using TMDbLib.Client;

namespace PMDB_docker.Models
{
    public class Movie : IMovieLogic
    {
        private readonly List<MovieDto> _movieList;
        private readonly IMovieData _movieData;

        // TODO: Via een constructor mee geven wat voor een data structuur je wilt gebruiken, denk bij Mock, inMemory of database
        //MovieDatabaseHandler handler = new MovieDatabaseHandler();

        public Movie(IMovieData movieData)
        {
            _movieData = movieData;
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
            TMDbClient client = new TMDbClient("8e8b06b1cb21b2d3f36f8bd44c933672");
            TMDbLib.Objects.Movies.Movie tmdbMovie = client.GetMovieAsync(movie.TmdbId).Result;

            if (movie.Image == null)
            {
                string image = "https://image.tmdb.org/t/p/w600_and_h900_bestv2";
                if (movie.TmdbId != null)
                {
                    movie.Image = image + tmdbMovie.PosterPath;
                }
            }

            if (movie.Overview == null || movie.Overview != tmdbMovie.Overview)
            {
                movie.Overview = tmdbMovie.Overview;
            }
            if (movie.AverageRating == null || movie.AverageRating != tmdbMovie.VoteAverage)
            {
                movie.AverageRating = tmdbMovie.VoteAverage;
            }
            if (movie.Runtime == null || movie.Runtime != tmdbMovie.Runtime)
            {
                movie.Runtime = tmdbMovie.Runtime;
            }
            if (movie.Budget == null || movie.Budget != tmdbMovie.Budget)
            {
                movie.Budget = tmdbMovie.Budget;
            }
            if (movie.Revenue == null || movie.Revenue != tmdbMovie.Revenue)
            {
                movie.Revenue = tmdbMovie.Revenue;
            }

            if (tmdbMovie.ProductionCompanies.Count != 0)
            {
                if (movie.Studio == null || movie.Studio != tmdbMovie.ProductionCompanies[0].Name)
                {
                    movie.Studio = tmdbMovie.ProductionCompanies[1].Name;
                }
            }
            
            if (movie.Website == null || movie.Website != tmdbMovie.Homepage)
            {
                movie.Website = tmdbMovie.Homepage;
            }
            if (movie.ReleaseDate == null || movie.ReleaseDate != tmdbMovie.ReleaseDate)
            {
                movie.ReleaseDate = tmdbMovie.ReleaseDate;
            }
            if (movie.PosterBackdrop == null || movie.PosterBackdrop != tmdbMovie.BackdropPath)
            {
                string image = "https://image.tmdb.org/t/p/w1400_and_h450_face";
                movie.PosterBackdrop = $"{image}{tmdbMovie.BackdropPath}";
            }
            if (movie.Status == null || movie.Status != tmdbMovie.Status)
            {
                movie.Status = tmdbMovie.Status;
            }

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
    }
}
