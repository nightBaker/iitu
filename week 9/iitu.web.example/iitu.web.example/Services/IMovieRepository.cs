using iitu.web.example.Models.Movies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace iitu.web.example.Services
{
    public interface IMovieRepository
    {
        void Add(Movie movie);
        Task Save();
        Task<List<Movie>> GetAll();
        Task<List<Movie>> GetMovies(Expression<Func<Movie, bool>> predicate);

    }
}
