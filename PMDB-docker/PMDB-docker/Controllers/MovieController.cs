using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PMDB_docker.Interfaces;
using PMDB_docker.Models;
using PMDB_docker.ViewModels;
using TMDbLib.Objects.Credit;

namespace PMDB_docker.Controllers
{
    public class MovieController : Controller
    {
        private readonly IMovieLogic _movieRepository;
        private readonly IGenreLogic _genreRepository;
        private readonly IPersonLogic _peopleRepository;
        private readonly ITmdbLogic _tmdbRepository;
        // GET: /Movie/

        //public IActionResult Index()
        //{
        //    List<Movie> movie = Movie.GetAllMovies();
        //    return View(movie);
        //}
        public MovieController(IMovieLogic movieRepository, IGenreLogic genreRepository, ITmdbLogic tmdbLogic, IPersonLogic personRepository)
        {
            _movieRepository = movieRepository;
            _genreRepository = genreRepository;
            _tmdbRepository = tmdbLogic;
            _peopleRepository = personRepository;
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
        [HttpGet]
        public ViewResult Details(int? id)
        {
            MovieDto movie = _movieRepository.GetMovie(id.Value);
            movie.Genre = _genreRepository.GetGenreForMovie(movie.Id);
            movie.People = _peopleRepository.GetPeopleForMovie(movie.Id);
            MovieDetailsViewModel movieDetailsViewModel = new MovieDetailsViewModel()
            {
                Movie = movie,
                PageTitle = movie.Title,
                RuntimeTimeFormat = _movieRepository.FormatRuntime(movie.Runtime)
            };

            return View(movieDetailsViewModel);
        }

        [HttpPost]
        public IActionResult Details(int id)
        {
            MovieDto movie = _movieRepository.GetMovie(id);
            movie = _tmdbRepository.UpdateMovie(movie);
            _genreRepository.CheckIfGenreExists(movie.Genre);
            _movieRepository.UpdateGenres(movie.Genre, movie.Id);
            _movieRepository.UpdateMovie(movie);
            foreach (var item in movie.People)
            {
                _peopleRepository.CheckIfPersonExists(_tmdbRepository.UpdatePeople(item.TmdbId));
            }
            _movieRepository.UpdatePeopleMovie(movie.People, movie.Id);

            return RedirectToAction("Details", movie.Id);
        }
    }
}