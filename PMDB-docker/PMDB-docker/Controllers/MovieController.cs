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
        private readonly IMovieLogic _movieRepository;
        // GET: /Movie/

        //public IActionResult Index()
        //{
        //    List<Movie> movie = Movie.GetAllMovies();
        //    return View(movie);
        //}
        public MovieController(IMovieLogic movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public ViewResult Index()
        {
            MovieListViewModel movieListViewModel = new MovieListViewModel()
            {
                Movies = _movieRepository.GetAllMoviesForListPage(),
                PageTitle = "All Movies"
            };
            return View(movieListViewModel);
        }

        public ViewResult Details(int id)
        {
            MovieDto movie = _movieRepository.GetMovie(id);
            _movieRepository.UpdateMovie(_movieRepository.GetMovie(id));
            MovieDetailsViewModel movieDetailsViewModel = new MovieDetailsViewModel()
            {
                Movie = movie,
                PageTitle = "Movie Details",
                RuntimeTimeFormat = _movieRepository.FormatRuntime(movie.Runtime)
            };
            return View(movieDetailsViewModel);
        }

        //[HttpGet]
        //public ViewResult Create()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public IActionResult Create(MovieDto movie)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        MovieDto newMovie = _movieRepository.Add(movie);
        //        //return RedirectToAction("details", new {id = newMovie.Id});
        //    }

        //    return View();
        //}
    }
}