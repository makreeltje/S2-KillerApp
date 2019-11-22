using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace PMDB_docker.Data.Movie
{
    public class MovieDatabaseHandler
    {
        // TODO: Connection string aanpassen!
        private string connectionString = "server=meelsnet.nl;user id=root;persistsecurityinfo=True;database=pmdb;password=Rsam.0255!;";

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
                        while (reader.Read())
                        {
                            MovieDto dto = new MovieDto();
                            dto.Title = reader.GetString(3);
                            dto.Plot = reader.GetString(7);
                            dto.Runtime = reader.GetInt32(8);
                            dto.ReleaseDate = reader.GetString(9);
                            dto.Website = reader.GetString(10);
                            dto.Studio = reader.GetString(11);

                            movies.Add(dto);
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
