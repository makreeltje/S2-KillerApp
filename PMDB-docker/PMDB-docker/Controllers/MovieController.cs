using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PMDB_docker.Models;
using PMDB_docker.ViewModels;
using PMDB_docker.Interfaces;

namespace PMDB_docker.Controllers
{
    public class MovieController : Controller
    {
        private readonly IMovieRepository _movieRepository;
        // GET: /Movie/

        //public IActionResult Index()
        //{
        //    List<Movie> movie = Movie.GetAllMovies();
        //    return View(movie);
        //}
        public MovieController(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public ViewResult Index()
        {
            var model = _movieRepository.GetAllMoviesForDetailsPage();
            return View(model);
        }

        public ViewResult Details(int? id)
        {
            MovieDetailsViewModel movieDetailsViewModel = new MovieDetailsViewModel()
            {
                Movie = _movieRepository.GetMovie(id ?? 1),
                PageTitle = "Movie Details"
            };
            return View(movieDetailsViewModel);
        }

        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(MovieDto movie)
        {
            if (ModelState.IsValid)
            {
                MovieDto newMovie = _movieRepository.Add(movie);
                //return RedirectToAction("details", new {id = newMovie.Id});
            }

            return View();
        }



        // GET: /Movie/ 

        //public IActionResult Search(string query)
        //{
        //    List<Business.Movie> movie = Business.Movie.GetQueryMovies(query);
        //    return View(movie);
        //}
    }
}