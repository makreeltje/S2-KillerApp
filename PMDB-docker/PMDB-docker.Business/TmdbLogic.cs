using PMDB_docker.Interfaces;
using PMDB_docker.Models;
using System;
using System.Collections.Generic;
using TMDbLib.Client;
using TMDbLib.Objects.Credit;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Movies;

namespace PMDB_docker.Business
{
    public class TmdbLogic : ITmdbLogic
    {
        private readonly TMDbClient _client;
        private Movie _tmdbMovie;
        private IGenreLogic _genreLogic;

        public TmdbLogic(IGenreLogic genreLogic)
        {
            _genreLogic = genreLogic;
            _client = new TMDbClient("8e8b06b1cb21b2d3f36f8bd44c933672");
            
        }
        public MovieDto UpdateMovie(MovieDto movie)
        {
            _tmdbMovie = GetMovie(movie.TmdbId);
            if (movie.Title != _tmdbMovie.Title)
                movie.Title = UpdateTitle(movie.Title);
            if (movie.PosterPath != _tmdbMovie.PosterPath)
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

            //if (_tmdbMovie.ProductionCompanies.Count != 0)
            //{
            //    if (movie.Studio != _tmdbMovie.ProductionCompanies[0].Name)
            //    {
            //        movie.Studio = _tmdbMovie.ProductionCompanies[1].Name;
            //    }
            //}
            if (movie.Website != _tmdbMovie.Homepage)
                movie.Website = UpdateWebSite(movie.Website);
            if (movie.ReleaseDate != _tmdbMovie.ReleaseDate)
                movie.ReleaseDate = UpdateReleaseDate(movie.ReleaseDate);
            if (movie.PosterBackdrop != _tmdbMovie.BackdropPath)
                movie.PosterBackdrop = UpdatePosterBackdrop(movie.PosterBackdrop);
            if (movie.Status != _tmdbMovie.Status)
                movie.Status = UpdateStatus(movie.Status);
            movie.Genre = UpdateGenre(_tmdbMovie.Genres);
            return movie;
        }

        private Movie GetMovie(string tmdbId)
        {
            return _client.GetMovieAsync(tmdbId).Result;
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

        // TODO: Connect genres to movie
        public List<GenreDto> UpdateGenre(List<Genre> tmdbGenres)
        {
            List<GenreDto> genres = new List<GenreDto>();
            foreach (var item in tmdbGenres)
            {
                if (!_genreLogic.CheckGenre(item.Name))
                    _genreLogic.AddGenre(item.Name);
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
            return null;
        }
        // TODO: Implement database insertion of credits
        // TODO: Add credit to list and return list
        public List<Credit> UpdateActors(List<Credit> credits)
        {
            return null;
        }
    }
}
