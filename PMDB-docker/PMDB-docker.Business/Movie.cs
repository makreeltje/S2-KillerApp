using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PMDB_docker.Data.Movie;
using PMDB_docker.Models;
using PMDB_docker.Interfaces;

namespace PMDB_docker.Models
{
    public class Movie : IMovieContainerData
    {
        private readonly List<MovieDto> _movieList;

        // TODO: Via een constructor mee geven wat voor een data structuur je wilt gebruiken, denk bij Mock, inMemory of database
        MovieDatabaseHandler handler = new MovieDatabaseHandler();

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

        private string CorrectDateFormat(string date)
        {
            string dateCorrected = date.Remove(date.IndexOf(" "));
            return dateCorrected;
        }

        public IEnumerable<MovieDto> GetAllMoviesForDetailsPage()
        {
            foreach (var movie in _movieList)
            {
                movie.Plot = ShortenPlotText(movie.Plot);
                if (movie.ReleaseDate != "")
                {
                    movie.ReleaseDate = CorrectDateFormat(movie.ReleaseDate);
                }
                else
                {
                    movie.ReleaseDate = movie.ReleaseDate;
                }
            }

            return _movieList;
        }
    }
}
