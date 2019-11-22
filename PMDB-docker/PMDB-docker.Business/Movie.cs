using System;
using System.Collections.Generic;
using System.Text;
using PMDB_docker.Data.Movie;

namespace PMDB_docker.Business
{
    public class Movie
    {
        static MovieDatabaseHandler handler = new MovieDatabaseHandler();

        public Movie(string title, string plot, int runtime, string studio, string website)
        {
            Title = title;
            Plot = plot;
            Runtime = runtime;
            Studio = studio;
            Website = website;
        }

        public string Title { get; }
        public string Plot { get; }
        public int Runtime { get; }
        public string Studio { get; }
        public string Website { get; }

        public static List<Movie> GetAllMovies()
        {
            List<Movie> allMovies = new List<Movie>();

            var movieFromDatabase = handler.GetAllMovies();

            foreach (var movieDto in movieFromDatabase)
            {
                Movie movie = new Movie(movieDto.Title, movieDto.Plot, movieDto.Runtime, movieDto.Studio, movieDto.Website);
                allMovies.Add(movie);
            }

            return allMovies;
        }
    }
}
