using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Movies.Domain;
using Movies.Infrastructure.Persistance;

namespace Movies.Application
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var builder = Host.CreateDefaultBuilder();
            builder.ConfigureServices((context, services) =>
            {
                services.AddDbContext<MoviesDbContext>(options => {
                    options.UseSqlServer("Server=(LocalDB)\\.;Database=MoviesDb;Trusted_Connection=True;TrustServerCertificate=True;");
                });
            });

            var app = builder.Build();
            var dbContext = app.Services.GetRequiredService<MoviesDbContext>();
            dbContext.Database.Migrate();

            SeedData(dbContext);

            var horrorGenres = dbContext.Genres
                .Where(x => x.Name == "Horror")
                .ToList();
            var horror = horrorGenres.FirstOrDefault();
            if (horror != null)
            {
                horror.Name = "Horror Movie";
                dbContext.SaveChanges();
            }
        }

        private static void SeedData(MoviesDbContext dbContext)
        {
            SeedGenres(dbContext);
            SeedMovies(dbContext);
            SeedActors(dbContext);
            Console.WriteLine("Data seeded!");
        }

        private static void SeedActors(MoviesDbContext dbContext)
        {
            if (dbContext.Actors.Any())
            {
                return; // Data already seeded
            }

            var stalone = new Actor
            {
                FirstName = "Sylvester",
                LastName = "Stallone",
            };

            var arnold = new Actor
            {
                FirstName = "Arnold",
                LastName = "Schwarzenegger",
            };

            dbContext.Actors.Add(stalone);
            dbContext.Actors.Add(arnold);

            var rambo = dbContext.Movies.FirstOrDefault(x => x.Title == "Rambo");
            if (rambo != null)
            {
                rambo.MovieActors.Add(new MovieActor
                {
                    Actor = stalone,
                    Movie = rambo
                });
            }

            var terminator = dbContext.Movies.FirstOrDefault(x => x.Title == "Terminator");
            if (terminator != null)
            {
                terminator.MovieActors.Add(new MovieActor
                {
                    Actor = arnold,
                    Movie = terminator
                });
            }

            dbContext.SaveChanges();
        }

        private static void SeedMovies(MoviesDbContext dbContext)
        {
            if (dbContext.Movies.Any())
            {
                return; // Data already seeded
            }

            var actionGenre = dbContext.Genres.FirstOrDefault(x => x.Name == "Action");
            if(actionGenre == null)
            {
                throw new InvalidOperationException("Action genre not found");
            }

            var rambo = new Movie
            {
                Title = "Rambo",
                Genre = actionGenre,
                Minutes = 120,
            };

            var terminator = new Movie
            {
                Title = "Terminator",
                Genre = actionGenre,
                Minutes = 130,
            };

            dbContext.Movies.Add(rambo);
            dbContext.Movies.Add(terminator);
            dbContext.SaveChanges();
        }

        private static void SeedGenres(MoviesDbContext dbContext)
        {
            if (dbContext.Genres.Any())
            {
                return; // Data already seeded
            }

            dbContext.Genres.AddRange(
                new Genre { Name = "Action" },
                new Genre { Name = "Comedy" },
                new Genre { Name = "Drama" },
                new Genre { Name = "Horror" },
                new Genre { Name = "Sci-Fi" }
            );
            dbContext.SaveChanges();
        }
    }
}
