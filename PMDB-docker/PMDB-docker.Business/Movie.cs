using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySqlX.XDevAPI;
using PMDB_docker.Data.Movie;
using PMDB_docker.Models;
using PMDB_docker.Interfaces;

namespace PMDB_docker.Models
{
    public class Movie : IMovieRepository
    {
        private readonly List<MovieDto> _movieList;
        MovieDatabaseHandler handler = new MovieDatabaseHandler();

        // TODO: Via een constructor mee geven wat voor een data structuur je wilt gebruiken, denk bij Mock, inMemory of database
        //MovieDatabaseHandler handler = new MovieDatabaseHandler();

        public Movie()
        {
            _movieList = new List<MovieDto>(handler.GetAllMovies());
        }
        public MovieDto GetMovie(int Id)
        {
            return _movieList.FirstOrDefault(m => m.Id == Id);
        }

        public IEnumerable<MovieDto> GetAllMovies()
        {
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

        public IEnumerable<MovieDto> GetAllMoviesForDetailsPage()
        {
            foreach (var movie in _movieList)
            {
                movie.ShortenedPlot = ShortenPlotText(movie.Plot);
                movie.ReleaseDate = movie.ReleaseDateTime.ToString("dd-MM-yyyy");
            }
            return _movieList;
        }
    }
}
