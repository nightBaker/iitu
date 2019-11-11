using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iitu.web.example.Data;
using iitu.web.example.Models.Movies;
using iitu.web.example.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace iitu.web.example.Controllers
{
    public class MoviesController : Controller
    {
        private readonly MovieService _movieService;

        public MoviesController(MovieService movieService)
        {
            _movieService = movieService;
        }

        public async Task<IActionResult> Index()
        {
            var movies = await _movieService.GetMovies();
            return View(movies);
        }

        [HttpGet]
        public IActionResult Add()
        {            
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(Movie movie)
        {

            await _movieService.AddAndSave(movie);

            var movies = await _movieService.GetMovies();

            return View("Index",movies);
        }

        public async Task<IActionResult> Search(string text)
        {
            var searchedMovies = await _movieService.Search(text);
            return View("Index", searchedMovies);
        }

    }
}