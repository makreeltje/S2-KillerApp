using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using PMDB_docker.Interfaces;
using PMDB_docker.Models;
using TMDbLib.Objects.General;

namespace PMDB_docker.Data
{
    public class GenreDatabaseHandler : IGenreData
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public GenreDatabaseHandler(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("MovieDatabase");
        }
        public List<GenreDto> GetAllGenres()
        {
            List<GenreDto> genres = new List<GenreDto>();
            string query = "SELECT * FROM genre";
            using MySqlConnection conn = new MySqlConnection(_connectionString);
            using MySqlCommand command = new MySqlCommand(query, conn);
            try
            {
                conn.Open();
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    GenreDto dto = new GenreDto 
                        {
                            Id = reader.GetInt32(0), 
                            Name = reader.GetString(1)
                        };
                    genres.Add(dto);
                }
            }
            catch (MySqlException sex)
            {
                throw new DataException(sex.Message);
            }

            return genres;
        }

        public int CheckGenre(string genre)
        {
            int result;
            string query = "SELECT COUNT(*) FROM genre WHERE name = @name";
            using MySqlConnection conn = new MySqlConnection(_connectionString);
            using MySqlCommand command = new MySqlCommand(query, conn);
            try
            {
                command.Parameters.AddWithValue("@name", genre);
                conn.Open();
                command.ExecuteNonQuery();
                MySqlDataReader reader = command.ExecuteReader();
                reader.Read();
                result = reader.GetInt32(0);
            }
            catch (MySqlException sex)
            {
                throw new DataException(sex.Message);
            }
            return result;
        }

        public void AddGenre(string genre)
        {
            string query = "INSERT INTO genre SET name = @name";
            using MySqlConnection conn = new MySqlConnection(_connectionString);
            using MySqlCommand command = new MySqlCommand(query, conn);
            try
            {
                command.Parameters.AddWithValue("@name", genre);
                conn.Open();
                command.ExecuteNonQuery();
            }
            catch (MySqlException sex)
            {
                throw new DataException(sex.Message);
            }

        }

        public List<GenreDto> GetGenreForMovie(int movieId)
        {
            List<GenreDto> genres = new List<GenreDto>(); 
            string query = "SELECT g.id, g.name FROM genre g INNER JOIN moviegenre mg ON g.id = mg.genreID INNER JOIN movie m ON m.id = mg.movieID WHERE m.id = @movieId";
            using MySqlConnection conn = new MySqlConnection(_connectionString);
            using MySqlCommand command = new MySqlCommand(query, conn);
            try
            {
                command.Parameters.AddWithValue("@movieId", movieId);
                conn.Open();
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    GenreDto dto = new GenreDto
                    {
                        Id = reader.GetInt32(0), 
                        Name = reader.GetString(1)
                    };
                    genres.Add(dto);
                }
            }
            catch (MySqlException sex)
            {
                throw new DataException(sex.Message);
            }

            return genres;
        }
    }
}
