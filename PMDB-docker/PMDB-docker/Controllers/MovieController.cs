using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PMDB_docker.Models;
using PMDB_docker.ViewModels;
using Movie = PMDB_docker.Business.Movie;

namespace PMDB_docker.Controllers
{
    public class MovieController : Controller
    {
        private readonly IMovieContainerData _movieRepository;
        // GET: /Movie/

        //public IActionResult Index()
        //{
        //    List<Movie> movie = Movie.GetAllMovies();
        //    return View(movie);
        //}
        public MovieController(IMovieContainerData movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public ViewResult Index()
        {
            var model = _movieRepository.GetAllMovies();
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
        public RedirectToActionResult Create(Models.Movie movie)
        {
            Models.Movie newMovie = _movieRepository.Add(movie);
            return RedirectToAction("details", new { id = newMovie});
        }



        // GET: /Movie/ 

        //public IActionResult Search(string query)
        //{
        //    List<Business.Movie> movie = Business.Movie.GetQueryMovies(query);
        //    return View(movie);
        //}
    }
}