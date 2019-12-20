using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using MySql.Data.MySqlClient;
using PMDB_docker.Interfaces;
using PMDB_docker.Models;

namespace PMDB_docker.Data
{
    public class UserDatabaseHandler : IUserData

    {
        // TODO: Connection string aanpassen!
        private readonly string connectionString =
            "server=meelsnet.nl;user id=root;persistsecurityinfo=True;database=pmdb;password=Rsam.0255!;";

        // Grabs all movies
        public IEnumerable<UserDto> GetAllUsers()
        {
            List<UserDto> users = new List<UserDto>();

            string query = "SELECT * FROM users";
            using MySqlConnection conn = new MySqlConnection(connectionString);
            using MySqlCommand command = new MySqlCommand(query, conn);
            try
            {
                
                
                
                conn.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    UserDto dto = new UserDto();
                    dto.Id = reader.GetInt32(0);
                    dto.Username = reader.GetString(1);
                    dto.Password = reader.GetString(2);
                    dto.Email = reader.GetString(3);
                    if (!reader.IsDBNull(4))
                        dto.FirstName = reader.GetString(4);
                    if (!reader.IsDBNull(5))
                        dto.LastName = reader.GetString(5);
                    if (!reader.IsDBNull(6))
                        dto.Street = reader.GetString(6);
                    if (!reader.IsDBNull(7))
                        dto.Number = reader.GetInt32(7);
                    if (!reader.IsDBNull(8))
                        dto.PostalCode = reader.GetString(8);
                    if (!reader.IsDBNull(9))
                        dto.Country = reader.GetString(9);
                    if (!reader.IsDBNull(10))
                        dto.Phone = reader.GetString(10);
                    if (!reader.IsDBNull(11))
                        dto.Mobile = reader.GetString(11);
                    if (!reader.IsDBNull(12))
                        dto.DateOfBirth = reader.GetDateTime(12);
                    dto.DateOfRegistration = reader.GetDateTime(13);
                    if (!reader.IsDBNull(14))
                        dto.ProfileImage = reader.GetString(14);
                    if (!reader.IsDBNull(15))
                        dto.Genders = (UserDto.Gender)reader.GetInt32(15);
                    users.Add(dto);
                }
            }
            catch (MySqlException sex)
            {
                throw new DataException(sex.Message);
            }

            return users;
        }

        public void AddUser(UserDto user)
        {
            try
            {
                using MySqlConnection conn = new MySqlConnection(connectionString);
                string query = "INSERT INTO users (username, password, email, dor)" +
                               $"VALUES ('{user.Username}','{user.Password}','{user.Email}', '{user.DateOfRegistration:yyyy-MM-dd HH:mm:ss}')";
                using MySqlCommand command = new MySqlCommand(query, conn);
                conn.Open();
                command.ExecuteNonQuery();
            }
            catch (MySqlException sex)
            {
                throw new DataException(sex.Message);
            }
        }

        public void EditUser(UserDto user)
        {
<<<<<<< HEAD
            string query = "UPDATE users SET username = @username, email = @email, firstName = @firstName, lastName = @lastName, street = @street, nr = @nr, postalCode = @postalCode, country = @country, phone = @phone, mobile = @mobile, dob = @dob, image = @image, gender = @gender WHERE id = @id";
=======
            string query = "UPDATE [users] SET email = @email, firstName = @firstName, lastName = @lastName, street = @street, nr = @nr, postalCode = @postalCode, country = @country, phone = @phone, mobile = @mobile, dob = @dob, image = @image, gender = @gender WHERE id = @id";
>>>>>>> parent of aaa9fe6... User
            using MySqlConnection conn = new MySqlConnection(connectionString);
            using MySqlCommand command = new MySqlCommand(query, conn);
            try
            {
<<<<<<< HEAD
                command.Parameters.AddWithValue("@username", user.Username);
                command.Parameters.AddWithValue("@email", user.Email);
                command.Parameters.AddWithValue("@firstName", user.FirstName);
                command.Parameters.AddWithValue("@lastName", user.LastName);
                command.Parameters.AddWithValue("@street", user.Street);
                command.Parameters.AddWithValue("@nr", user.Number);
                command.Parameters.AddWithValue("@postalCode", user.PostalCode);
                command.Parameters.AddWithValue("@country", user.Country);
                command.Parameters.AddWithValue("@phone", user.Phone);
                command.Parameters.AddWithValue("@mobile", user.Mobile);
                command.Parameters.AddWithValue("@dob", user.DateOfBirth.ToString("yyyy-MM-dd"));
                command.Parameters.AddWithValue("@image", user.ProfileImage);
                command.Parameters.AddWithValue("@gender", user.Genders);
                command.Parameters.AddWithValue("@id", user.Id);
                conn.Open();
                command.ExecuteNonQuery();
            }
            catch (MySqlException sex)
            {
                throw new DataException(sex.Message);
            }
        }

        public void RemoveUser(int id)
        {
            string query = "DELETE FROM users WHERE id = @id";
            using MySqlConnection conn = new MySqlConnection(connectionString);
            using MySqlCommand command = new MySqlCommand(query, conn);
            try
            {
                command.Parameters.AddWithValue("@id", id);
=======
                List<MySqlParameter> p = new List<MySqlParameter>();
                p.Add(new MySqlParameter("@email", user.Email));
                p.Add(new MySqlParameter("@firstName", user.FirstName));
                p.Add(new MySqlParameter("@lastName", user.LastName));
                p.Add(new MySqlParameter("@street", user.Street));
                p.Add(new MySqlParameter("@nr", user.Number));
                p.Add(new MySqlParameter("@postalCode", user.PostalCode));
                p.Add(new MySqlParameter("@country", user.Country));
                p.Add(new MySqlParameter("@phone", user.Phone));
                p.Add(new MySqlParameter("@mobile", user.Mobile));
                p.Add(new MySqlParameter("@dob", user.DateOfBirth));
                p.Add(new MySqlParameter("@image", user.ProfileImage));
                p.Add(new MySqlParameter("@gender", user.Genders));
>>>>>>> parent of aaa9fe6... User
                conn.Open();
                GetExample(command, p.ToArray());
                command.ExecuteNonQuery();
                command.Parameters.Clear();
            }
            catch (MySqlException sex)
            {
                throw new DataException(sex.Message);
            }
            
            conn.Open();
            command.ExecuteNonQuery();
        }

        private void GetExample(MySqlCommand command, params MySqlParameter[] p)
        {
            if (p != null && p.Any())
            {
                command.Parameters.AddRange(p);
            }
        }
    }
}