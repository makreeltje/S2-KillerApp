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
                movie.ShortenedPlot = ShortenPlotText(movie.Overview);
                //movie.ReleaseDate = movie.ReleaseDate.ToString("dd-MM-yyyy");
            }
            return _movieList;
        }

        public List<MovieDto> RemoveMovie(int id)
        {
            _movieData.RemoveMovie(id);
            _movieList.RemoveAt(id);
            return _movieList;
        }

        public void UpdateMovieInformation(MovieDto movie)
        {
            TMDbClient client = new TMDbClient("8e8b06b1cb21b2d3f36f8bd44c933672");
            TMDbLib.Objects.Movies.Movie tmdbMovie = client.GetMovieAsync(movie.TmdbId).Result;
            MovieApiDto movieApi = new MovieApiDto();

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

            _movieData.EditMovie(movie);
        }
    }
}
