using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using VolunteeringPlatform.Dal;
using VolunteeringPlatform.Dal.Seed;
using VolunteeringPlatform.Domain.Auth;

namespace VolunteeringPlatform.API.Infrastructure.Extentions
{
    public static class HostExtentions
    {
        public static async Task SeedData(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<VolunteeringPlatformDbContext>();
                    var userManager = services.GetRequiredService<UserManager<User>>();
                    var roleManager = services.GetRequiredService<RoleManager<Role>>();
                    context.Database.Migrate();

                    await UserSeed.Seed(userManager, roleManager);

                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occured during migration");
                }
            }
        }
    }
}
