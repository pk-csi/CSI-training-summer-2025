using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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
                    options.UseSqlServer("Server=.\\SQLEXPRESS;Database=MoviesDb;Trusted_Connection=True;TrustServerCertificate=True;");
                });
            });

            var app = builder.Build();
            var dbContext = app.Services.GetRequiredService<MoviesDbContext>();
            dbContext.Database.Migrate();
        }
    }
}
