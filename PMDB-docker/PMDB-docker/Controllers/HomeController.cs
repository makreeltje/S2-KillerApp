using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PMDB_docker.Interfaces;
using PMDB_docker.Models;
using PMDB_docker.ViewModels;

namespace PMDB_docker.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMovieLogic _movieLogic;

        public HomeController(ILogger<HomeController> logger, IMovieLogic movieLogic)
        {
            _logger = logger;
            _movieLogic = movieLogic;
        }

        public IActionResult Index()
        {
            MovieRandomViewModel movieRandomViewModel = new MovieRandomViewModel()
            {
                Movies = _movieLogic.GetSixRandomMovies()
            };
            return View(movieRandomViewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
