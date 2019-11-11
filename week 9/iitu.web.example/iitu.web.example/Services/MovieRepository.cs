using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using iitu.web.example.Data;
using iitu.web.example.Models.Movies;
using Microsoft.EntityFrameworkCore;

namespace iitu.web.example.Services
{
    public class MovieRepository : IMovieRepository
    {
        readonly MoviesContext _context;

        public MovieRepository(MoviesContext context)
        {
            _context = context;
        }

        public void Add(Movie movie)
        {
            _context.Add(movie);
        }

        public Task<List<Movie>> GetAll()
        {
            return _context.Movies.ToListAsync();
        }

        public  Task<List<Movie>> GetMovies(Expression<Func<Movie, bool>> predicate)
        {
            return  _context.Movies.Where(predicate).ToListAsync();
        }

        public Task Save()
        {
            return _context.SaveChangesAsync();
        }
    }
}
