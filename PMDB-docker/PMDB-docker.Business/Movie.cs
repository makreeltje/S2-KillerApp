using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using PMDB_docker.Data.Movie;

namespace PMDB_docker.Business
{
    public class Movie
    {
        static MovieDatabaseHandler handler = new MovieDatabaseHandler();

        public Movie(int id, string title, string plot, string runtime, string studio, string website, string releaseDate)
        {
            Id = id;
            Title = title;
            Plot = plot;
            Runtime = runtime;
            Studio = studio;
            Website = website;
            ReleaseDate = releaseDate;
        }

        public int Id { get; set; }
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
            int id = 0;
            foreach (var movieDto in movieFromDatabase)
            {
                id++;
                TimeSpan time = TimeSpan.FromMinutes(movieDto.Runtime);
                string str = time.ToString(@"hh\:mm");
                string plot;
                if (movieDto.Plot.Length > 100)
                {
                    plot = movieDto.Plot.Substring(0, 100) + "...";
                }
                else
                {
                    plot = movieDto.Plot;
                }
                Movie movie = new Movie(id, movieDto.Title, plot, str, movieDto.Studio, movieDto.Website, movieDto.ReleaseDate);
                allMovies.Add(movie);
            }

            return allMovies;
        }

        public static List<Movie> GetQueryMovies(string searchTerm)
        {
            List<Movie> allMovies = new List<Movie>();
            string cleanTitle;
            if (searchTerm.Contains(" "))
            {
                cleanTitle = searchTerm.Replace(" ", "");
            }
            else
            {
                cleanTitle = searchTerm;
            }
            var movieFromDatabase = handler.GetQueryMovies(cleanTitle);
            int id = 0;
            foreach (var movieDto in movieFromDatabase)
            {
                id++;
                TimeSpan time = TimeSpan.FromMinutes(movieDto.Runtime);
                string str = time.ToString(@"hh\:mm");
                string plot;
                if (movieDto.Plot.Length > 100)
                {
                    plot = movieDto.Plot.Substring(0, 100) + "...";
                }
                else
                {
                    plot = movieDto.Plot;
                }
                Movie movie = new Movie(id, movieDto.Title, plot, str, movieDto.Studio, movieDto.Website, movieDto.ReleaseDate);
                allMovies.Add(movie);
            }

            return allMovies;
        }
    }
}
