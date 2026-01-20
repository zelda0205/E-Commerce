using ZELDA.Data;

namespace ZELDA.Seeders
{
    public class OrdersSeeds
    {
        public static async Task SeedOrders(
            WebApplication app,
            IConfiguration configuration)
        {
            using var scope = app.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            if (context.Orders.Any())
                return;

            // Seed orders here
            await context.SaveChangesAsync();
        }
    }
}
