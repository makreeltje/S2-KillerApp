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
                            dto.Title = reader.GetString(3);
                            if (!reader.IsDBNull(7))
                                dto.Plot = reader.GetString(7);
                            else
                                dto.Plot = "";
                            if (reader.IsDBNull(8))
                                dto.Image = "~/img/noimage-cover.png";
                            else
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

        public MovieDto EditMovie(int id)
        {
            throw new NotImplementedException();
        }
    }
}
