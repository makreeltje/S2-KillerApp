using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using PMDB_docker.Models;

namespace PMDB_docker.ViewModels
{
    public class UserRegisterViewModel : UserDto
    {
        public int Id { get; set; }
        public string PageTitle { get; set; }
    }
}
