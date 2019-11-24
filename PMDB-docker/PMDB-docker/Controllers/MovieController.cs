using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PMDB_docker.Business;

namespace PMDB_docker.Controllers
{
    public class MovieController : Controller
    {
        // GET: /Movie/

        public IActionResult Index()
        {
            List<Movie> movie = Movie.GetAllMovies();
            return View(movie);
        }

        // GET: /Movie/ 

        public IActionResult Search(string query)
        {
            List<Movie> movie = Movie.GetQueryMovies(query);
            return View(movie);
        }
    }
}