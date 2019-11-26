using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace PMDB_docker.Models
{
    public class MovieContainer : IMovieContainerData
    {
        private List<Movie> _movieList;

        public MovieContainer()
        {
            _movieList = new List<Movie>()
            {
                new Movie() {Id = 1, Title = "Ant-Man", Plot = "Movie about an Ant-Man", Genre = Genre.Action},
                new Movie() { Id = 2, Title = "Cars", Plot = "Movie about cars", Genre = Genre.Animation},
                new Movie() {Id = 3, Title = "Dunkirk", Plot = "Movie about WWII", Genre = Genre.History}
            };
        }
        public Movie GetMovie(int Id)
        {
            return _movieList.FirstOrDefault(m => m.Id == Id);
        }

        public IEnumerable<Movie> GetAllMovies()
        {
            return _movieList;
        }

        public Movie Add(Movie movie)
        {
            movie.Id = _movieList.Max(m => m.Id) + 1;
            _movieList.Add(movie);
            return movie;
        }
    }
}
