﻿using System;
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
                int id = 0;
                while (reader.Read())
                {
                    UserDto dto = new UserDto();
                    dto.Id = id;
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
                    id++;
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
            string query = "UPDATE users SET email = @email, firstName = @firstName, lastName = @lastName, street = @street, nr = @nr, postalCode = @postalCode, country = @country, phone = @phone, mobile = @mobile, dob = @dob, image = @image, gender = @gender WHERE id = @id";
            using MySqlConnection conn = new MySqlConnection(connectionString);
            using MySqlCommand command = new MySqlCommand(query, conn);
            try
            {
                
                command.Parameters.AddWithValue("email", user.Email);
                command.Parameters.AddWithValue("firstName", user.FirstName);
                command.Parameters.AddWithValue("lastName", user.LastName);
                command.Parameters.AddWithValue("street", user.Street);
                command.Parameters.AddWithValue("nr", user.Number);
                command.Parameters.AddWithValue("postalCode", user.PostalCode);
                command.Parameters.AddWithValue("country", user.Country);
                command.Parameters.AddWithValue("phone", user.Phone);
                command.Parameters.AddWithValue("mobile", user.Mobile);
                command.Parameters.AddWithValue("dob", user.DateOfBirth.ToString("yyyy-MM-dd"));
                command.Parameters.AddWithValue("image", user.ProfileImage);
                command.Parameters.AddWithValue("gender", user.Genders);
                command.Parameters.AddWithValue("id", user.Id + 1);
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