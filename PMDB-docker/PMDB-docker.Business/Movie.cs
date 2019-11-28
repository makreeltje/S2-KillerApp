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

        public Movie()
        {
            _movieList = new List<MovieDto>()
            {
                new MovieDto() {Id = 1, Title = "Ant-Man", Plot = "Movie about an Ant-Man", Genre = GenreDto.Action, Website = "http://ant-man.com"},
                new MovieDto() { Id = 2, Title = "Cars", Plot = "Movie about cars", Genre = GenreDto.Animation, Website = "http://cars.com"},
                new MovieDto() {Id = 3, Title = "Dunkirk", Plot = "Movie about WWII", Genre = GenreDto.History, Website = "http://dunkirk.com"}
            };
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
    }
}
