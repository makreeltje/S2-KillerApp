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
        public IEnumerable<MovieDto> GetAllMovies()
        {
            List<MovieDto> movies = new List<MovieDto>();

            try
            {
                using MySqlConnection conn = new MySqlConnection(connectionString);
                {
                    string query = "SELECT * FROM movie ORDER BY sortTitle";
                    using MySqlCommand command = new MySqlCommand(query, conn);
                    {
                        conn.Open();
                        var reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            MovieDto dto = new MovieDto();
                            dto.Id = reader.GetInt32(0);
                            if (!reader.IsDBNull(2))
                                dto.TmdbId = reader.GetString(2);
                            dto.Title = reader.GetString(3);
                            if (!reader.IsDBNull(7))
                                dto.Overview = reader.GetString(7);
                            else
                                dto.Overview = "";
                            if (!reader.IsDBNull(8))
                                dto.Image = reader.GetString(8);
                            if (!reader.IsDBNull(9))
                                dto.Runtime = reader.GetInt32(9);
                            if (!reader.IsDBNull(10))
                                dto.ReleaseDateTime = reader.GetDateTime(10);
                            if (!reader.IsDBNull(11))
                                dto.Website = reader.GetString(11);
                            if (!reader.IsDBNull(12))
                                dto.Studio = reader.GetString(12);
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

        public MovieDto AddMovie(MovieDto movie)
        {
            throw new NotImplementedException();
        }

        public void RemoveMovie(int id)
        {
            try
            {
                using MySqlConnection conn = new MySqlConnection(connectionString);
                string query = $"DELETE FROM movie WHERE id = {id}";
                using MySqlCommand command = new MySqlCommand(query, conn);
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
            string query = "UPDATE users SET username = @username, email = @email, firstName = @firstName, lastName = @lastName, street = @street, nr = @nr, postalCode = @postalCode, country = @country, phone = @phone, mobile = @mobile, dob = @dob, image = @image, gender = @gender WHERE id = @id";
            using MySqlConnection conn = new MySqlConnection(connectionString);
            using MySqlCommand command = new MySqlCommand(query, conn);
            try
            {
                command.Parameters.AddWithValue("@imdbID", movie.ImdbId);
                command.Parameters.AddWithValue("@tmdbID", movie.TmdbId);
                command.Parameters.AddWithValue("@title", movie.Title);
                command.Parameters.AddWithValue("@plot", movie.Overview);
                command.Parameters.AddWithValue("@image", movie.Image);
                command.Parameters.AddWithValue("@runtime", movie.Runtime);
                command.Parameters.AddWithValue("@releaseDate", movie.ReleaseDate.ToString("yyyy-MM-dd"));
                command.Parameters.AddWithValue("@website", movie.Website);
                command.Parameters.AddWithValue("@studio", movie.Studio);
                command.Parameters.AddWithValue("@budget", movie.Budget);
                command.Parameters.AddWithValue("@revenue", movie.Revenue);
                command.Parameters.AddWithValue("@status", movie.Status);
                command.Parameters.AddWithValue("@posterBackdrop", movie.PosterBackdrop);
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
