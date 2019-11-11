using iitu.web.example.Models.Movies;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iitu.web.example.Data
{
    public class MoviesContext : DbContext
    {
        public MoviesContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Movie> Movies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie>().HasData(new Movie
            {
                Id = -1,
                Author = "Todd Phillips",
                Genre = "Crime , Drama , Thriller",
                Name = "Joker",
                Poster = "https://dz7u9q3vpd4eo.cloudfront.net/admin-uploads/posters/mxt_movies_poster/joker_dabf394a-d4f2-4b68-90e2-011ed6b54012_poster.png?d=270x360&q=20",
                CreatedAt = new DateTime(2019, 10, 3)
            },
            new Movie
            {
                Id = -2,
                Author = "David Leitch",
                Genre = "Action , Adventure",
                Name = "Fast & Furious Presents: Hobbs & Shaw",
                Poster = "https://dz7u9q3vpd4eo.cloudfront.net/admin-uploads/posters/mxt_movies_poster/fast-furious-presents-hobbs-shaw_14d1ab4f-c90c-46d1-9e04-f7d69f285ebd_poster.png?d=270x360&q=20",
                CreatedAt = new DateTime(2019, 8, 2)
            },
            new Movie
            {
                Id = -3,
                Author = "Jon Favreau",
                Genre = "Adventure , Animation , Drama , Family , Musical",
                Name = "The Lion King",
                Poster = "https://dz7u9q3vpd4eo.cloudfront.net/admin-uploads/posters/mxt_movies_poster/the-lion-king_3904aadc-3a07-4836-892f-763b2dfdeea3_poster.png?d=270x360&q=20",
                CreatedAt = new DateTime(2019, 7, 19)
            },
            new Movie
            {
                Id = -4,
                Author = "Joachim Rønning",
                Genre = "Adventure , Family , Fantasy",
                Name = "Maleficent: Mistress of Evil",
                Poster = "https://dz7u9q3vpd4eo.cloudfront.net/admin-uploads/posters/mxt_movies_poster/maleficent-mistress-of-evil_c8507e61-a6b3-404d-b8c5-df6f74bc62be_poster.png?d=270x360&q=20",
                CreatedAt = new DateTime(2019, 10, 18)
            });
        }
    }
}
