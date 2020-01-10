using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PMDB_docker.Business;
using PMDB_docker.Models;

namespace PMDB_docker.BusinessTests
{
    [TestClass()]
    public class TmdbLogicTests
    {
        private readonly TmdbLogic _tmdbLogic;
        private MovieDto _movie;

        public TmdbLogicTests()
        {
            _tmdbLogic = new TmdbLogic();
        }

        [TestInitialize]
        public void InitTest()
        {
            _movie = new MovieDto()
            {
                TmdbId = 509967
            };
        }

        [TestMethod()]
        public void UpdateAllInformationForSpecificMovie_UpdateMovie_IsTrue()
        {
            _movie = _tmdbLogic.UpdateMovie(_movie);

            Assert.IsTrue(_movie.Title == "6 Underground");
            Assert.IsTrue(_movie.Runtime == 127);
            Assert.IsTrue(_movie.Budget == 150000000);
            Assert.IsTrue(_movie.PosterPath == "https://image.tmdb.org/t/p/w600_and_h900_bestv2/lnWkyG3LLgbbrIEeyl5mK5VRFe4.jpg");
        }

        [TestMethod()]
        public void UpdateAllInformationForSpecificMovieWithNoPoster_UpdateMovie_IsTrue()
        {
            MovieDto movie = new MovieDto();
            movie.TmdbId = 505642;
            _movie = _tmdbLogic.UpdateMovie(movie);
            Assert.IsTrue(movie.PosterPath == "~/img/noimage-cover.png");
        }

        [TestMethod()]
        public void GetReviewsForSpecificMovie_GetReviews_IsTrue()
        {
            List<ReviewDto> reviews = new List<ReviewDto>();
            reviews = _tmdbLogic.GetReviews(_movie.TmdbId);

            Assert.IsTrue(reviews.Count > 0);
        }

        [TestMethod()]
        public void GetVideosForSpecificMovie_GetVideos_IsTrue()
        {
            List<VideosDto> videos = new List<VideosDto>();
            videos = _tmdbLogic.GetVideos(_movie.TmdbId);

            Assert.IsTrue(videos.Count > 0);
        }

        [TestMethod()]
        public void GetBackdropsForSpecificMovie_GetBackdrops_IsTrue()
        {
            List<ImageDto> backdrops = new List<ImageDto>();
            backdrops = _tmdbLogic.GetBackdrops(_movie.TmdbId);

            Assert.IsTrue(backdrops.Count > 0);
        }

        [TestMethod()]
        public void GetPostersForSpecificMovie_GetPosters_IsTrue()
        {
            List<ImageDto> posters = new List<ImageDto>();
            posters = _tmdbLogic.GetPosters(_movie.TmdbId);

            Assert.IsTrue(posters.Count > 0);
        }

        [TestMethod()]
        public void GetPersonDetails_UpdatePeople_IsTrue()
        {
            PeopleDto person = new PeopleDto();
            person.TmdbId = 172069;
            person = _tmdbLogic.UpdatePeople(person.TmdbId);

            Assert.IsTrue(person.Name == "Chadwick Boseman");
        }

        [TestMethod()]
        public void GetPersonDetailsWithoutImage_UpdatePeople_IsTrue()
        {
            PeopleDto person = new PeopleDto();
            person.TmdbId = 2487586;
            person = _tmdbLogic.UpdatePeople(person.TmdbId);
            
            Assert.IsTrue(person.ProfilePath == "~/img/noimage-face.png");
        }
    }
}