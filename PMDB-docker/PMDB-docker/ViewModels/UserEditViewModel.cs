using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using PMDB_docker.Models;

namespace PMDB_docker.ViewModels
{
    public class UserEditViewModel
    {
        public UserDto User { get; set; }
        public int Id { get; set; }
        public string PageTitle { get; set; }
        public string ExistingPhotoPath { get; set; }
        public IFormFile Photo { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public UserDto.Gender Genders { get; set; }
        public string Email { get; set; }
        public string Street { get; set; }
        public int? Number { get; set; }
        public string PostalCode { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
    }
}
