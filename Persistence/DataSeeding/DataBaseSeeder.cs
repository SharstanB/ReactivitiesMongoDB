using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Persistence;
using Persistence.IdentityEnitities;

namespace API
{
    public  class DataBaseSeeder : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<DataBaseSeeder> _logger;
        public DataBaseSeeder(IServiceProvider serviceProvider, ILogger<DataBaseSeeder> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }
        public async Task StartAsync(CancellationToken cancellationToken)
        {

            using var scope = _serviceProvider.CreateScope();
            var services = scope.ServiceProvider;
            try
            {
                var context = services.GetRequiredService<AppDBContext>();
                var userManager = services.GetRequiredService<UserManager<AppUser>>();
                await context.Database.MigrateAsync();
                await StartSeeding(context, userManager);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during migration");
            }
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;

        public static async Task StartSeeding(AppDBContext context, UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var users = new List<AppUser>
            {
                new() {Email = "jack@gmail.com", UserName="jk@gmail.com", Name = "Jack B." },
                new() {Email = "sam@gmail.com", UserName="sm@gmail.com", Name = "Sam Altman"},
                new() {Email = "jane@gmail.com", UserName="jn@gmail.com", Name= "Jane John"}
            };
                foreach (var user in users)
                {
                    await userManager.CreateAsync(user, "Pa$$w0rd ");
                }
            }
            if (!context.Categories.Any())
            {
                await SeedCategory(context);
            }
            if (!context.Cities.Any())
            {
                await SeedCity(context);
            }
            var categories = context.Categories.ToList();
            var cities = context.Cities.ToList();

            var catergoriesCount = categories.Count();
            var citiesCount = cities.Count();

            if (context.Activities.Any()) return;

            var activities = new List<Activity>()
            {
                new Activity()
                {
                    Title = "Past Activity 1",
                    Date = DateTime.UtcNow.AddMonths(-2),
                    Description = "Activity 2 months ago",
                    Category = categories[Random.Shared.Next(catergoriesCount)],
                    CategoryId = categories[Random.Shared.Next(catergoriesCount)].Id,
                    City ="London",
                    Venue = "Pub",
                }, new Activity()
                {
                    Title = "Past Activity 2",
                    Date = DateTime.UtcNow.AddMonths(-1),
                    Description = "Activity 1 month ago",
                    Category =categories[Random.Shared.Next(catergoriesCount)] ,
                    CategoryId = categories[Random.Shared.Next(catergoriesCount)].Id,
                    City =  "Paris",
                    Venue = "Cinema",
                }, new Activity()
                {
                    Title = "Future Activity 1",
                    Date = DateTime.UtcNow.AddMonths(1),
                    Description = "Activity 1 month in future",
                    Category =categories[Random.Shared.Next(catergoriesCount)] ,
                    CategoryId = categories[Random.Shared.Next(catergoriesCount)].Id,
                    City =  "Aleppo",
                }, new Activity()
                {
                    Title = "Future Activity 2",
                    Date = DateTime.UtcNow.AddMonths(2),
                    Description = "Activity 2 months in future",
                    Category =categories[Random.Shared.Next(catergoriesCount)] ,
                    CategoryId = categories[Random.Shared.Next(catergoriesCount)].Id,
                    City =  "Hama",
                    Venue = "Park",
                }, new Activity()
                {
                    Title = "Future Activity 3",
                    Date = DateTime.UtcNow.AddMonths(3),
                    Description = "Activity 3 months in future",
                    Category =categories[Random.Shared.Next(catergoriesCount)] ,
                    CategoryId = categories[Random.Shared.Next(catergoriesCount)].Id,
                    City =   "Homs",
                    Venue = "Pub",
                }, new Activity()
                {
                    Title = "Future Activity 4",
                    Date = DateTime.UtcNow.AddMonths(4),
                    Description = "Activity 4 months in future",
                    Category =categories[Random.Shared.Next(catergoriesCount)] ,
                    CategoryId = categories[Random.Shared.Next(catergoriesCount)].Id,
                    City =  "Istanbul",
                    Venue = "Pub",
                }, new Activity()
                {
                    Title = "Future Activity 5",
                    Date = DateTime.UtcNow.AddMonths(5),
                    Description = "Activity 5 months in future",
                    Category = categories[Random.Shared.Next(catergoriesCount)],
                    CategoryId = categories[Random.Shared.Next(catergoriesCount)].Id,
                    City =  "Erbil",
                    Venue = "Pub",
                }
            };
            await context.Activities.AddRangeAsync(activities);
            await context.SaveChangesAsync();

        }

        public static async Task SeedCity(AppDBContext context)
        {
            if (context.Cities.Any()) return;
            var cities = new List<City>()
            {
                new City()
                {
                    CityName = "Aleppo",
                }, new City()
                {
                    CityName = "Damascus",
                }, new City()
                {
                    CityName = "Homs",
                }, new City()
                {
                    CityName = "Hama",
                },
                new City()
                {
                    CityName = "Dir Al-zor",
                }, new City()
                {
                    CityName = "Darha",
                }
             };

            await context.Cities.AddRangeAsync(cities);
            await context.SaveChangesAsync();
        }

        public static async Task SeedCategory(AppDBContext context)
        {
            if (context.Categories.Any()) return;
            var categories = new List<Category>()
            {
                new Category()
                {
                    Name = "Drinks",
                }, new Category()
                {
                    Name = "Culture",
                }, new Category()
                {
                    Name = "Film",
                }, new Category()
                {
                    Name = "Food",
                },
                new Category()
                {
                    Name = "Music",
                }, new Category()
                {
                    Name = "Travel",
                }
             };

            await context.Categories.AddRangeAsync(categories);
            await context.SaveChangesAsync();
        }

      
    }
}
