using System;
using System.Collections.Generic;
using System.Text;
using PMDB_docker.Models;

namespace PMDB_docker.Interfaces
{
    public interface IMovieData
    {
        List<MovieDto> GetAllMovies();
        MovieDto GetRandomMovie();
        void AddMovie(MovieDto movie);
        void RemoveMovie(MovieDto movie);
        void EditMovie(MovieDto movie);
        //void UpdateGenres(string genres, int movieId);
        List<MovieDto> SearchMovie(string searchQuery);
        void CheckGenreConnection(List<GenreDto> genres, int movieId);
        void CheckPeopleConnection(List<RoleDto> role, int movieId);
    }
}
