using System;
using System.Collections.Generic;
using System.Configuration;
using MySql.Data.MySqlClient;
using PMDB_docker.Interfaces;
using PMDB_docker.Models;

namespace PMDB_docker.Data.Movie
{
    public class MovieDatabaseHandler : IMovieData
    {
        // TODO: Connection string aanpassen!
        private readonly string connectionString =
            "server=meelsnet.nl;user id=root;persistsecurityinfo=True;database=pmdb;password=Rsam.0255!;";

        // Grabs all movies
        public List<MovieDto> GetAllMovies()
        {
            List<MovieDto> movies = new List<MovieDto>();

            try
            {
                using MySqlConnection conn = new MySqlConnection(connectionString);
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
                                dto.TmdbId = reader.GetString(2);
                            dto.Title = reader.GetString(3);
                            if (!reader.IsDBNull(4))
                                dto.Overview = reader.GetString(4);
                            if (!reader.IsDBNull(5))
                                dto.Image = reader.GetString(5);
                            if (!reader.IsDBNull(6))
                                dto.Runtime = reader.GetInt32(6);
                            if (!reader.IsDBNull(7))
                                dto.ReleaseDate = reader.GetDateTime(7);
                            if (!reader.IsDBNull(8))
                                dto.Website = reader.GetString(8);
                            if (!reader.IsDBNull(9))
                                dto.Studio = reader.GetString(9);
                            if (!reader.IsDBNull(10))
                                dto.Budget = reader.GetInt64(10);
                            if (!reader.IsDBNull(11))
                                dto.Revenue = reader.GetInt64(11);
                            if (!reader.IsDBNull(12))
                                dto.Status = reader.GetString(12);
                            if (!reader.IsDBNull(13))
                                dto.PosterBackdrop = reader.GetString(13);
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

        public void AddMovie(MovieDto movie)
        {
            throw new NotImplementedException();
        }

        public void RemoveMovie(MovieDto movie)
        {
            string query = $"DELETE FROM movie WHERE id = @id";
            using MySqlConnection conn = new MySqlConnection(connectionString);
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
            string query = "UPDATE movie SET title = @title, overview = @overview, image = @image, runtime = @runtime, releaseDate = @releaseDate, website = @website, studio = @studio, budget = @budget, revenue = @revenue, status = @status, posterBackdrop = @posterBackdrop, averageRating = @averageRating WHERE id = @id";
            using MySqlConnection conn = new MySqlConnection(connectionString);
            using MySqlCommand command = new MySqlCommand(query, conn);
            try
            {
                command.Parameters.AddWithValue("@id", movie.Id);
                command.Parameters.AddWithValue("@title", movie.Title);
                command.Parameters.AddWithValue("@overview", movie.Overview);
                command.Parameters.AddWithValue("@image", movie.Image);
                command.Parameters.AddWithValue("@runtime", movie.Runtime);
                command.Parameters.AddWithValue("@releaseDate", movie.ReleaseDate?.ToString("yyyy-MM-dd"));
                command.Parameters.AddWithValue("@website", movie.Website);
                command.Parameters.AddWithValue("@studio", movie.Studio);
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
    }
}
