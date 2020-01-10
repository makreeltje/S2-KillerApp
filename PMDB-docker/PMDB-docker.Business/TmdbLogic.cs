using PMDB_docker.Interfaces;
using PMDB_docker.Models;
using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;
using TMDbLib.Client;
using TMDbLib.Objects.Credit;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Lists;
using TMDbLib.Objects.Movies;
using TMDbLib.Objects.People;
using TMDbLib.Objects.Reviews;

namespace PMDB_docker.Business
{
    public class TmdbLogic : ITmdbLogic
    {
        private readonly TMDbClient _client;
        private Movie _tmdbMovie;
        private List<Credits> _tmdbMovieCredits;

        public TmdbLogic()
        {
            _client = new TMDbClient("8e8b06b1cb21b2d3f36f8bd44c933672");

        }
        public MovieDto UpdateMovie(MovieDto movie)
        {
            _tmdbMovie = GetMovie(movie.TmdbId);
            _tmdbMovieCredits = GetMovieCredits(movie.TmdbId);
            if (movie.Title != _tmdbMovie.Title)
                movie.Title = UpdateTitle(movie.Title);
            movie.PosterPath = UpdatePosterPath(movie.PosterPath);
            if (movie.Overview != _tmdbMovie.Overview)
                movie.Overview = UpdateOverview(movie.Overview);
            if (movie.AverageRating != _tmdbMovie.VoteAverage)
                movie.AverageRating = UpdateAverageRating(movie.AverageRating);
            if (movie.Runtime != _tmdbMovie.Runtime)
                movie.Runtime = UpdateRuntime(movie.Runtime);
            if (movie.Budget != _tmdbMovie.Budget)
                movie.Budget = UpdateBudget(movie.Budget);
            if (movie.Revenue != _tmdbMovie.Revenue)
                movie.Revenue = UpdateRevenue(movie.Revenue);
            if (movie.Website != _tmdbMovie.Homepage)
                movie.Website = UpdateWebSite(movie.Website);
            if (movie.ReleaseDate != _tmdbMovie.ReleaseDate)
                movie.ReleaseDate = UpdateReleaseDate(movie.ReleaseDate);
            if (movie.PosterBackdrop != _tmdbMovie.BackdropPath)
                movie.PosterBackdrop = UpdatePosterBackdrop(movie.PosterBackdrop);
            if (movie.Status != _tmdbMovie.Status)
                movie.Status = UpdateStatus(movie.Status);
            movie.Genre = UpdateGenre(_tmdbMovie.Genres);
            movie.People = UpdatePeopleMovie(_tmdbMovieCredits);
            return movie;
        }

        private Movie GetMovie(int tmdbId)
        {
            return _client.GetMovieAsync(tmdbId).Result;
        }
        private List<Credits> GetMovieCredits(int tmdbId)
        {
            List<Credits> credits = new List<Credits> {_client.GetMovieCreditsAsync(tmdbId).Result};
            return credits;
        }

        private string UpdateTitle(string title)
        {
            title = _tmdbMovie.Title;
            return title;
        }

        private string UpdateOverview(string overview)
        {
            overview = _tmdbMovie.Overview;
            return overview;
        }

        private string UpdatePosterPath(string image)
        {
            string imagePrefix = "https://image.tmdb.org/t/p/w600_and_h900_bestv2";
            if (_tmdbMovie.PosterPath == null)
                return "~/img/noimage-cover.png";
            image = $"{imagePrefix}{_tmdbMovie.PosterPath}";
            return image;
        }

        private int? UpdateRuntime(int? runtime)
        {
            runtime = _tmdbMovie.Runtime;
            return runtime;
        }

        private DateTime? UpdateReleaseDate(DateTime? releaseDate)
        {
            releaseDate = _tmdbMovie.ReleaseDate;
            return releaseDate;
        }

        private string UpdateWebSite(string website)
        {
            website = _tmdbMovie.Homepage;
            return website;
        }

        private long? UpdateBudget(long? budget)
        {
            budget = _tmdbMovie.Budget;
            return budget;
        }

        private long? UpdateRevenue(long? revenue)
        {
            revenue = _tmdbMovie.Revenue;
            return revenue;
        }

        private string UpdateStatus(string status)
        {
            status = _tmdbMovie.Status;
            return status;
        }

        private string UpdatePosterBackdrop(string posterBackdrop)
        {
            string posterBackdropPrefix = "https://image.tmdb.org/t/p/w1400_and_h450_face";
            posterBackdrop = $"{posterBackdropPrefix}{_tmdbMovie.BackdropPath}";
            return posterBackdrop;
        }

        private double? UpdateAverageRating(double? averageRating)
        {
            averageRating = _tmdbMovie.VoteAverage;
            return averageRating;
        }

        public List<GenreDto> UpdateGenre(List<Genre> tmdbGenres)
        {
            List<GenreDto> genres = new List<GenreDto>();
            foreach (var item in tmdbGenres)
            {
                GenreDto genre = new GenreDto();
                genre.Name = item.Name;
                genres.Add(genre);
            }
            return genres;
        }
        // TODO: Implement database insertion of production companies
        // TODO: Add prodComps to list and return list
        public List<ProductionCompany> UpdateProductionCompanies(List<ProductionCompany> productionCompanies)
        {
            throw new NotImplementedException();
        }
        public List<RoleDto> UpdatePeopleMovie(List<Credits> credits)
        {
            string imagePrefix = "https://image.tmdb.org/t/p/w138_and_h175_face";
            List<RoleDto> people = new List<RoleDto>();
            foreach (var credit in credits)
            {
                foreach (var cast in credit.Cast)
                {
                    RoleDto person = new RoleDto();
                    person.TmdbId = cast.Id;
                    person.Name = cast.Name;
                    person.Character = cast.Character;
                    person.Order = cast.Order;
                    person.ProfilePath = $"{imagePrefix}{cast.ProfilePath}";
                    people.Add(person);
                }
                foreach (var crew in credit.Crew)
                {
                    RoleDto person = new RoleDto();
                    person.TmdbId = crew.Id;
                    person.Name = crew.Name;
                    person.Job = crew.Job;
                    person.Department = crew.Department;
                    person.ProfilePath = $"{imagePrefix}{crew.ProfilePath}";
                    people.Add(person);
                }
            }

            return people;
        }

        public PeopleDto UpdatePeople(int tmdbId)
        {
            PeopleDto person = new PeopleDto();
            Person tmdbPerson = new Person();
            string imagePrefix = "https://image.tmdb.org/t/p/w600_and_h900_bestv2";
            tmdbPerson = _client.GetPersonAsync(tmdbId).Result;
            person.TmdbId = tmdbPerson.Id;
            person.Name = tmdbPerson.Name;
            person.Gender = (int)tmdbPerson.Gender;
            person.Biography = tmdbPerson.Biography;
            person.PlaceOfBirth = tmdbPerson.PlaceOfBirth;
            person.DateOfBirth = tmdbPerson.Birthday;
            person.DateOfDeath = tmdbPerson.Deathday;
            //person.KnownFor = 
            if (tmdbPerson.ProfilePath == null)
                person.ProfilePath = "~/img/noimage-face.png";
            else
                person.ProfilePath = $"{imagePrefix}{tmdbPerson.ProfilePath}";
            person.HomePage = tmdbPerson.Homepage;

            return person;
        }

        public List<ReviewDto> GetReviews(int tmdbId)
        {
            SearchContainer<ReviewBase> tmdbReviews = _client.GetMovieReviewsAsync(tmdbId).Result;
            List<ReviewDto> reviews = new List<ReviewDto>();

            foreach (var review in tmdbReviews.Results)
            {
                ReviewDto newReview = new ReviewDto()
                {
                    Author = review.Author,
                    Content = review.Content
                };
                reviews.Add(newReview);
            }

            return reviews;
        }

        public List<VideosDto> GetVideos(int tmdbId)
        {
            ResultContainer<Video> tmdbVideos = _client.GetMovieVideosAsync(tmdbId).Result;
            List<VideosDto> videos = new List<VideosDto>();

            string urlPrefix = "https://www.youtube.com/embed/";

            foreach (var video in tmdbVideos.Results)
            {
                if (video.Site == "YouTube")
                {
                    VideosDto newVideo = new VideosDto()
                    {
                        Key = $"{urlPrefix}{video.Key}"
                    };
                    videos.Add(newVideo);
                }
            }

            return videos;
        }

        public List<ImageDto> GetBackdrops(int tmdbId)
        {
            Task<ImagesWithId> tmdbImages = _client.GetMovieImagesAsync(tmdbId);
            List<ImageDto> images = new List<ImageDto>();

            string urlPrefix = "https://image.tmdb.org/t/p/w533_and_h300_bestv2";

            foreach (var backdrop in tmdbImages.Result.Backdrops)
            {
                ImageDto newImage = new ImageDto()
                {
                    Path = $"{urlPrefix}{backdrop.FilePath}"
                };
                images.Add(newImage);
            }

            return images;
        }

        public List<ImageDto> GetPosters(int tmdbId)
        {
            Task<ImagesWithId> tmdbImages = _client.GetMovieImagesAsync(tmdbId);
            List<ImageDto> images = new List<ImageDto>();

            string urlPrefix = "https://image.tmdb.org/t/p/w533_and_h300_bestv2";

            foreach (var backdrop in tmdbImages.Result.Posters)
            {
                ImageDto newImage = new ImageDto()
                {
                    Path = $"{urlPrefix}{backdrop.FilePath}"
                };
                images.Add(newImage);
            }

            return images;
        }
    }
}
