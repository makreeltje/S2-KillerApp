using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using PMDB_docker.Interfaces;
using PMDB_docker.Models;
using PMDB_docker.ViewModels;

namespace PMDB_docker.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserLogic _userRepository;
#pragma warning disable 618
        private readonly IHostingEnvironment _hostingEnvironment;
#pragma warning restore 618

        [Obsolete]
        public UserController(IUserLogic userRepository, IHostingEnvironment hostingEnvironment)
        {
            _userRepository = userRepository;
            _hostingEnvironment = hostingEnvironment;
        }

        public ViewResult Index()
        {
            return View();
        }

        [HttpGet]
        public ViewResult Register()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Register(UserRegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                //string uniqueFileName = null;
                //if (model.Photo != null)
                //{
                //    string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "img/profile");
                //    uniqueFileName = Guid.NewGuid() + "_" + model.Photo.FileName;
                //    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                //    model.Photo.CopyTo(new FileStream(filePath, FileMode.Create));
                //}

                UserDto user = new UserDto
                {
                    //Country = model.Country,
                    //DateOfBirth = model.DateOfBirth,
                    DateOfRegistration = DateTime.Now,
                    Email = model.Email,
                    //FirstName = model.FirstName,
                    //Gender = model.Gender,
                    //LastName = model.LastName,
                    //Mobile = model.Mobile,
                    //Number = model.Number,
                    //ProfileImage = uniqueFileName,
                    //Street = model.Street,
                    //PostalCode = model.PostalCode,
                    Password = model.Password,
                    Username = model.Username
                };
                _userRepository.Add(user);
                return RedirectToAction("details", new {id = user.Id});
            }

            return View();
        }

        public ViewResult Details(int? id)
        {
            UserDetailsViewModel userDetailsViewModel = new UserDetailsViewModel()
            {
                User = _userRepository.GetUser(id ?? 1),
                PageTitle = "Movie Details"
            };
            return View(userDetailsViewModel);
        }

        [HttpGet]
        public ViewResult Edit()
        {
            return View();
        }

        //[HttpPost]
        //public ViewResult Edit()
        //{
        //    return View();
        //}
    }
}