using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PMDB_docker.Models;
using PMDB_docker.Interfaces;

namespace PMDB_docker.Models
{
    public class Movie : IMovieLogic
    {
        private readonly List<MovieDto> _movieList;
        private readonly IMovieData _movieData;
        private readonly IMovieApi _movieApi;

        // TODO: Via een constructor mee geven wat voor een data structuur je wilt gebruiken, denk bij Mock, inMemory of database
        //MovieDatabaseHandler handler = new MovieDatabaseHandler();

        public Movie(IMovieData movieData, IMovieApi movieApi)
        {
            _movieData = movieData;
            _movieApi = movieApi;
            _movieList = new List<MovieDto>(_movieData.GetAllMovies());
            
        }
        public MovieDto GetMovie(int Id)
        {
            return _movieList.FirstOrDefault(m => m.Id == Id);
        }

        public List<MovieDto> GetAllMovies()
        {
            int i = 0;
            foreach (var movie in _movieList)
            {
                if (movie.Image == null)
                {
                    MovieApiDto movieapi = new MovieApiDto();
                    string image = "https://image.tmdb.org/t/p/w600_and_h900_bestv2";
                    string imageAdditive;
                    if (movie.TmdbId != null)
                    {
                        imageAdditive = _movieApi.GetMovieAsync(movie.TmdbId).ToString();
                        //movie.Image = $"{image}{imageAdditive}";
                    }
                    if (i >= 10)
                        break;
                    i++;
                }
            }
            return _movieList;
        }

        public MovieDto Add(MovieDto movie)
        {
            movie.Id = _movieList.Max(m => m.Id) + 1;
            _movieList.Add(movie);
            return movie;
        }

        private string ShortenPlotText(string text)
        {
            int maxLenght = 100;
            string plotShortened;
            if (text.Length > maxLenght)
            {
                plotShortened = text.Substring(0, maxLenght) + "...";
                return plotShortened;
            }

            return text;
        }

        public List<MovieDto> GetAllMoviesForDetailsPage()
        {
            foreach (var movie in _movieList)
            {
                movie.ShortenedPlot = ShortenPlotText(movie.Plot);
                movie.ReleaseDate = movie.ReleaseDateTime.ToString("dd-MM-yyyy");
            }
            return _movieList;
        }

        public List<MovieDto> RemoveMovie(int id)
        {
            _movieData.RemoveMovie(id);
            _movieList.RemoveAt(id);
            return _movieList;
        }
    }
}
