using System;
using System.Collections.Generic;
using System.Text;
using PMDB_docker.Data.Movie;

namespace PMDB_docker.Business
{
    public class Movie
    {
        static MovieDatabaseHandler handler = new MovieDatabaseHandler();

        public Movie(string title, string plot, string runtime, string studio, string website, string releaseDate)
        {
            Title = title;
            Plot = plot;
            Runtime = runtime;
            Studio = studio;
            Website = website;
            ReleaseDate = releaseDate;
        }

        public string Title { get; }
        public string Plot { get; }
        public string Runtime { get; }
        public string Studio { get; }
        public string Website { get; }
        public string ReleaseDate { get; set; }

        public static List<Movie> GetAllMovies()
        {
            List<Movie> allMovies = new List<Movie>();

            var movieFromDatabase = handler.GetAllMovies();

            foreach (var movieDto in movieFromDatabase)
            {
                TimeSpan time = TimeSpan.FromMinutes(movieDto.Runtime);
                string str = time.ToString(@"hh\:mm");
                Movie movie = new Movie(movieDto.Title, movieDto.Plot, str, movieDto.Studio, movieDto.Website, movieDto.ReleaseDate);
                allMovies.Add(movie);
            }

            return allMovies;
        }
    }
}
