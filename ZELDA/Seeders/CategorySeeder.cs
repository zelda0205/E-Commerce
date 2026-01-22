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
                        Description = "crochet_clothes",
                    },
                    new Category
                    {
                        Name = "Accessories",
                        Description = "crochet_accessories",
                    },
                    new Category
                    {
                        Name = "Bags",
                        Description = "crochet_bags",
                    },
                });

                await _context.SaveChangesAsync();
            }
        }
    }
}