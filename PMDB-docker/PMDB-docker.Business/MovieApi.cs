using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using PMDB_docker.Interfaces;
using PMDB_docker.Models;

namespace PMDB_docker.Business
{
    public class MovieApi : IMovieApi
    {
        static HttpClient client = new HttpClient();
        async Task IMovieApi.RunAsync()
        {
            client.BaseAddress = new Uri("https://api.themoviedb.org/3/movie/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        async Task<MovieApiDto> IMovieApi.GetMovieAsync(string path)
        {
            MovieApiDto movieApi = null;
            HttpResponseMessage response = await client.GetAsync("https://api.themoviedb.org/3/movie/" + path + "?api_key=8e8b06b1cb21b2d3f36f8bd44c933672&language=en-US");
            if (response.IsSuccessStatusCode)
            {
                movieApi = await response.Content.ReadAsAsync<MovieApiDto>();
            }
            return movieApi;
        }
    }
}
