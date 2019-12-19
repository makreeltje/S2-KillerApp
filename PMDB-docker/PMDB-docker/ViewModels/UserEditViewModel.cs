using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using PMDB_docker.Models;

namespace PMDB_docker.ViewModels
{
    public class UserEditViewModel : UserDetailsViewModel
    {
        public string PageTitle { get; set; }
        public string ExistingPhotoPath { get; set; }
        public IFormFile Photo { get; set; }
    }
}
