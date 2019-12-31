using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using PMDB_docker.Interfaces;
using PMDB_docker.Models;
using TMDbLib.Objects.People;

namespace PMDB_docker.Data
{
    public class PersonDatabaseHandler : IPersonData
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public PersonDatabaseHandler(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("MovieDatabase");
        }
        public List<PeopleDto> GetAllPeople()
        {
            List<PeopleDto> people = new List<PeopleDto>();
            string query = "SELECT * FROM people";

            using MySqlConnection conn = new MySqlConnection(_connectionString);
            using MySqlCommand command = new MySqlCommand(query, conn);
            try
            {
                conn.Open();
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    PeopleDto dto = new PeopleDto();

                    dto.Id = reader.GetInt32(0);
                    dto.TmdbId = reader.GetInt32(1);
                    if (!reader.IsDBNull(2))
                        dto.Name = reader.GetString(2);
                    if (!reader.IsDBNull(3))
                        dto.Gender = reader.GetInt32(3);
                    if (!reader.IsDBNull(4))
                        dto.Biography = reader.GetString(4);
                    if (!reader.IsDBNull(5))
                        dto.PlaceOfBirth = reader.GetString(5);
                    if (!reader.IsDBNull(6))
                        dto.DateOfBirth = reader.GetDateTime(6);
                    if (!reader.IsDBNull(7))
                        dto.DateOfDeath = reader.GetDateTime(7);
                    if (!reader.IsDBNull(8))
                        dto.KnownFor = reader.GetString(8); 
                    if (!reader.IsDBNull(9))
                        dto.ProfilePath = reader.GetString(9);
                    if (!reader.IsDBNull(10))
                        dto.HomePage = reader.GetString(10);
                    people.Add(dto);
                }
            }
            catch (MySqlException sex)
            {
                throw new DataException(sex.Message);
            }

            return people;
        }

        public int CheckPerson(PeopleDto person)
        {
            int result;
            string query = "SELECT COUNT(*) FROM people WHERE tmdbID = @tmdbID";
            using MySqlConnection conn = new MySqlConnection(_connectionString);
            using MySqlCommand command = new MySqlCommand(query, conn);
            try
            {
                command.Parameters.AddWithValue("@tmdbID", person.TmdbId);
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

        public void AddPerson(PeopleDto person)
        {
            string query = "INSERT INTO people SET tmdbID = @tmdbID, name = @name, gender = @gender, biography = @biography, pob = @pob, dob = @dob, dod = @dod, knownFor = @knownFor, profilePath = @profilePath, homePage = @homePage";
            using MySqlConnection conn = new MySqlConnection(_connectionString);
            using MySqlCommand command = new MySqlCommand(query, conn);
            try
            {
                command.Parameters.AddWithValue("@tmdbId", person.TmdbId);
                command.Parameters.AddWithValue("@name", person.Name);
                command.Parameters.AddWithValue("@gender", person.Gender);
                command.Parameters.AddWithValue("@biography", person.Biography);
                command.Parameters.AddWithValue("@pob", person.PlaceOfBirth);
                command.Parameters.AddWithValue("@dob", person.DateOfBirth);
                command.Parameters.AddWithValue("@dod", person.DateOfDeath);
                command.Parameters.AddWithValue("@knownFor", person.KnownFor);
                command.Parameters.AddWithValue("@profilePath", person.ProfilePath);
                command.Parameters.AddWithValue("@homePage", person.HomePage);
                conn.Open();
                command.ExecuteNonQuery();
            }
            catch (MySqlException sex)
            {
                throw new DataException(sex.Message);
            }
        }

        public List<RoleDto> GetPeopleForMovie(int movieId)
        {
            List<RoleDto> person = new List<RoleDto>();
            string query = "SELECT * FROM role WHERE movieID = @movieID";
            using MySqlConnection conn = new MySqlConnection(_connectionString);
            using MySqlCommand command = new MySqlCommand(query, conn);
            try
            {
                command.Parameters.AddWithValue("@movieID", movieId);
                conn.Open();
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    RoleDto dto = new RoleDto();
                    dto.Id = reader.GetInt32(0);
                    if (!reader.IsDBNull(4))
                        dto.Name = reader.GetString(4);
                    if (!reader.IsDBNull(5))
                        dto.Character = reader.GetString(5);
                    if (!reader.IsDBNull(6))
                        dto.Order = reader.GetInt32(6);
                    if (!reader.IsDBNull(7))
                        dto.Department = reader.GetString(7);
                    if (!reader.IsDBNull(8))
                        dto.Job = reader.GetString(8);
                    if (!reader.IsDBNull(9))
                        dto.ProfilePath = reader.GetString(9);

                    person.Add(dto);
                }
            }
            catch (MySqlException sex)
            {
                throw new DataException(sex.Message);
            }

            return person;
        }
    }
}
