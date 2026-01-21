using ZELDA.Data;
using ZELDA.Models;

namespace ZELDA.Seeders
{
    public static class CategorySeeder
    {
        public static async Task SeedCategories(IApplicationBuilder applicationBuilder)
        {
            using var serviceScope = applicationBuilder.ApplicationServices.CreateScope();

            var _context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();

            _context!.Database.EnsureCreated();

            if (!_context.Categories!.Any())
            {
                await _context.Categories!.AddRangeAsync(new List<Category>()
                {
                    new Category
                    {
                        Name = "Clothes",
                        Description = "maumau",
                    },
                    new Category
                    {
                        Name = "Accessories",
                        Description = "mamau",
                    },
                    new Category
                    {
                        Name = "Bags",
                        Description = "mamau",
                    },
                });

                await _context.SaveChangesAsync();
            }
        }
    }
}