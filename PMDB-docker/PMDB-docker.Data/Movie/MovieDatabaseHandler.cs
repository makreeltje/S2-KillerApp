using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using PMDB_docker.Models;

namespace PMDB_docker.Data.Movie
{
    public class MovieDatabaseHandler
    {
        // TODO: Connection string aanpassen!
        private readonly string connectionString = "server=meelsnet.nl;user id=root;persistsecurityinfo=True;database=pmdb;password=Rsam.0255!;";

        // Grabs all movies
        public List<MovieDto> GetAllMovies()
        {
            List<MovieDto> movies = new List<MovieDto>();

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    string query = "SELECT * FROM movie ORDER BY sortTitle";
                    using (MySqlCommand command = new MySqlCommand(query, conn))
                    {
                        conn.Open();
                        var reader = command.ExecuteReader();
                        int id = 0;
                        while (reader.Read())
                        {
                            MovieDto dto = new MovieDto();
                            dto.Id = id;
                            dto.Title = reader.GetString(3);
                            dto.Plot = reader.GetString(7);
                            if (reader.IsDBNull(8))
                                dto.Image = "~/img/noimage-cover.png";
                            else
                                dto.Image = reader.GetString(8);
                            dto.Runtime = reader.GetInt32(9);
                            dto.ReleaseDateTime = reader.GetDateTime(10);
                            dto.Website = reader.GetString(11);
                            dto.Studio = reader.GetString(12);
                            movies.Add(dto);
                            id++;
                        }
                    }
                }
            }
            catch (MySqlException sex)
            {
                throw new MovieDataException(sex.Message);
            }

            return movies;
        }
    }
}
