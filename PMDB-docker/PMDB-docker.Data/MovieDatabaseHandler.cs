using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using PMDB_docker.Interfaces;
using PMDB_docker.Models;
using System;
using System.Collections.Generic;

namespace PMDB_docker.Data.Movie
{
    public class MovieDatabaseHandler : IMovieData
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public MovieDatabaseHandler(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("MovieDatabase");
        }

        // Grabs all movies
        public List<MovieDto> GetAllMovies()
        {
            List<MovieDto> movies = new List<MovieDto>();

            try
            {
                using MySqlConnection conn = new MySqlConnection(_connectionString);
                {
                    string query = "SELECT * FROM movie ORDER BY releaseDate DESC";
                    using MySqlCommand command = new MySqlCommand(query, conn);
                    {
                        conn.Open();
                        var reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            MovieDto dto = new MovieDto();
                            dto.Id = reader.GetInt32(0);
                            if (!reader.IsDBNull(1))
                                dto.ImdbId = reader.GetString(1);
                            if (!reader.IsDBNull(2))
                                dto.TmdbId = reader.GetInt32(2);
                            dto.Title = reader.GetString(3);
                            if (!reader.IsDBNull(4))
                                dto.Overview = reader.GetString(4);
                            if (!reader.IsDBNull(5))
                                dto.PosterPath = reader.GetString(5);
                            if (!reader.IsDBNull(6))
                                dto.Runtime = reader.GetInt32(6);
                            if (!reader.IsDBNull(7))
                                dto.ReleaseDate = reader.GetDateTime(7);
                            if (!reader.IsDBNull(8))
                                dto.Website = reader.GetString(8);
                            if (!reader.IsDBNull(9))
                                dto.Budget = reader.GetInt64(9);
                            if (!reader.IsDBNull(10))
                                dto.Revenue = reader.GetInt64(10);
                            if (!reader.IsDBNull(11))
                                dto.Status = reader.GetString(11);
                            if (!reader.IsDBNull(12))
                                dto.PosterBackdrop = reader.GetString(12);
                            if (!reader.IsDBNull(13))
                                dto.AverageRating = reader.GetDouble(13);
                            if (!reader.IsDBNull(14))
                                dto.LastModified = reader.GetDateTime(14);
                            movies.Add(dto);
                        }
                    }
                }
            }
            catch (MySqlException sex)
            {
                throw new DataException(sex.Message);
            }

            return movies;
        }

        public MovieDto GetRandomMovie()
        {
            MovieDto movie = new MovieDto();
            string query = $"SELECT * FROM movie ORDER BY RAND() LIMIT 1";
            using MySqlConnection conn = new MySqlConnection(_connectionString);
            using MySqlCommand command = new MySqlCommand(query, conn);
            try
            {
                conn.Open();
                MySqlDataReader reader = command.ExecuteReader();
                reader.Read();

                movie.Id = reader.GetInt32(0); 
                if (!reader.IsDBNull(2))
                    movie.TmdbId = reader.GetInt32(2);
                movie.Title = reader.GetString(3);
                if (!reader.IsDBNull(5))
                    movie.PosterPath = reader.GetString(5);
                if (!reader.IsDBNull(7))
                    movie.ReleaseDate = reader.GetDateTime(7);

            }
            catch (MySqlException sex)
            {
                throw new DataException(sex.Message);
            }
            return movie;
        }

        public void AddMovie(MovieDto movie)
        {
            throw new NotImplementedException();
        }

        public void RemoveMovie(MovieDto movie)
        {
            string query = $"DELETE FROM movie WHERE id = @id";
            using MySqlConnection conn = new MySqlConnection(_connectionString);
            using MySqlCommand command = new MySqlCommand(query, conn);
            try
            {
                command.Parameters.AddWithValue("@id", movie.Id);
                conn.Open();
                command.ExecuteNonQuery();
            }
            catch (MySqlException sex)
            {
                throw new DataException(sex.Message);
            }
        }

        public void EditMovie(MovieDto movie)
        {
            string query = "UPDATE movie SET title = @title, overview = @overview, image = @image, runtime = @runtime, releaseDate = @releaseDate, website = @website, budget = @budget, revenue = @revenue, status = @status, posterBackdrop = @posterBackdrop, averageRating = @averageRating WHERE id = @id";
            using MySqlConnection conn = new MySqlConnection(_connectionString);
            using MySqlCommand command = new MySqlCommand(query, conn);
            try
            {
                command.Parameters.AddWithValue("@id", movie.Id);
                command.Parameters.AddWithValue("@title", movie.Title);
                command.Parameters.AddWithValue("@overview", movie.Overview);
                command.Parameters.AddWithValue("@image", movie.PosterPath);
                command.Parameters.AddWithValue("@runtime", movie.Runtime);
                command.Parameters.AddWithValue("@releaseDate", movie.ReleaseDate?.ToString("yyyy-MM-dd"));
                command.Parameters.AddWithValue("@website", movie.Website);
                command.Parameters.AddWithValue("@budget", movie.Budget);
                command.Parameters.AddWithValue("@revenue", movie.Revenue);
                command.Parameters.AddWithValue("@status", movie.Status);
                command.Parameters.AddWithValue("@posterBackdrop", movie.PosterBackdrop);
                command.Parameters.AddWithValue("@averageRating", movie.AverageRating);
                conn.Open();
                command.ExecuteNonQuery();
            }
            catch (MySqlException sex)
            {
                throw new DataException(sex.Message);
            }
        }

        private void UpdateGenres(string genre, int movieId)
        {
            string query = "INSERT INTO moviegenre (movieID, genreID)" +
                           "VALUES ((SELECT id FROM movie WHERE id = @movieId), (SELECT id FROM genre WHERE name = @genre))";
            using MySqlConnection conn = new MySqlConnection(_connectionString);
            using MySqlCommand command = new MySqlCommand(query, conn);
            try
            {
                command.Parameters.AddWithValue("@genre", genre);
                command.Parameters.AddWithValue("@movieId", movieId);
                conn.Open();
                command.ExecuteNonQuery();
                command.Parameters.Clear();
            }
            catch (MySqlException sex)
            {
                throw new DataException(sex.Message);
            }
        }

        public void CheckGenreConnection(List<GenreDto> genres, int movieId)
        {
            int result;
            string query = "SELECT COUNT(*) FROM moviegenre WHERE movieID = (SELECT id FROM movie WHERE id = @movieId) AND genreID = (SELECT id FROM genre WHERE name = @genre)";
            foreach (var genre in genres)
            {
                using MySqlConnection conn = new MySqlConnection(_connectionString);
                using MySqlCommand command = new MySqlCommand(query, conn);

                try
                {
                    command.Parameters.AddWithValue("@movieId", movieId);
                    command.Parameters.AddWithValue("@genre", genre.Name);
                    conn.Open();
                    command.ExecuteNonQuery();
                    MySqlDataReader reader = command.ExecuteReader();
                    reader.Read();
                    result = reader.GetInt32(0);
                    if (result < 1)
                        UpdateGenres(genre.Name, movieId);
                    command.Parameters.Clear();
                }
                catch (MySqlException sex)
                {
                    throw new DataException(sex.Message);
                }
            }
        }

        public void CheckPeopleConnection(List<RoleDto> roles, int movieId)
        {
            int result;
            string query = "SELECT COUNT(*) FROM role WHERE movieID = (SELECT id FROM movie WHERE id = @movieId) AND peopleID = (SELECT id FROM people WHERE tmdbID = @tmdbID)";
            foreach (var role in roles)
            {
                using MySqlConnection conn = new MySqlConnection(_connectionString);
                using MySqlCommand command = new MySqlCommand(query, conn);

                try
                {
                    command.Parameters.AddWithValue("@movieId", movieId);
                    command.Parameters.AddWithValue("@tmdbID", role.TmdbId);
                    conn.Open();
                    command.ExecuteNonQuery();
                    MySqlDataReader reader = command.ExecuteReader();
                    reader.Read();
                    result = reader.GetInt32(0);
                    if (result < 1)
                        UpdatePeople(role, movieId);
                    command.Parameters.Clear();
                }
                catch (MySqlException sex)
                {
                    throw new DataException(sex.Message);
                }
            }
        }
        private void UpdatePeople(RoleDto role, int movieId)
        {
            string query = "INSERT INTO role SET movieID = (SELECT id FROM movie WHERE id = @movieId), peopleID = (SELECT id FROM people WHERE tmdbID = @tmdbID), tmdbID = @tmdbID, name = @name, charName = @character, movieOrder = @order, department = @department, job = @job, profilePath = @profilePath";
            using MySqlConnection conn = new MySqlConnection(_connectionString);
            using MySqlCommand command = new MySqlCommand(query, conn);
            try
            {
                command.Parameters.AddWithValue("@tmdbID", role.TmdbId);
                command.Parameters.AddWithValue("@movieId", movieId);
                command.Parameters.AddWithValue("@name", role.Name);
                command.Parameters.AddWithValue("@character", role.Character);
                command.Parameters.AddWithValue("@order", role.Order);
                command.Parameters.AddWithValue("@department", role.Department);
                command.Parameters.AddWithValue("@job", role.Job);
                command.Parameters.AddWithValue("@profilePath", role.ProfilePath);
                conn.Open();
                command.ExecuteNonQuery();
                command.Parameters.Clear();
            }
            catch (MySqlException sex)
            {
                throw new DataException(sex.Message);
            }
        }
    }
}
