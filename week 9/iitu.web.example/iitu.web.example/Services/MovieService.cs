using iitu.web.example.Data;
using iitu.web.example.Models.Movies;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iitu.web.example.Services
{
    public class MovieService
    {
        private readonly IMovieRepository _movieRepo;

        public MovieService(IMovieRepository movieRepo)
        {
            _movieRepo = movieRepo;
        }

        public async Task<List<Movie>> GetMovies()
        {
            return await _movieRepo.GetAll();            
        }

        public async Task AddAndSave(Movie movie)
        {
            _movieRepo.Add(movie);
            await _movieRepo.Save();
        }

        public async Task<List<Movie>> Search(string text)
        {
            text = text.ToLower();
            var searchedMovies = await _movieRepo.GetMovies(movie => movie.Name.ToLower().Contains(text)
                                            || movie.Genre.ToLower().Contains(text)
                                            || movie.Author.ToLower().Contains(text));

            return searchedMovies;
        }


    }
}
