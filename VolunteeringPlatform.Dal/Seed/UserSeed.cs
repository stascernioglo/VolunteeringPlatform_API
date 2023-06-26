using Microsoft.AspNetCore.Identity;
using VolunteeringPlatform.Dal.Constants;
using VolunteeringPlatform.Domain.Auth;

namespace VolunteeringPlatform.Dal.Seed
{
    public class UserSeed
    {
        public static async Task Seed(UserManager<User> userManager, RoleManager<Role> roleManager)
        {

            if (await roleManager.FindByNameAsync(RoleConstants.Admin) == null)
            {
                await roleManager.CreateAsync(new Role() { Name = RoleConstants.Admin });
            }

            if (await roleManager.FindByNameAsync(RoleConstants.User) == null)
            {
                await roleManager.CreateAsync(new Role() { Name = RoleConstants.User });
            }

            if (await roleManager.FindByNameAsync(RoleConstants.Volunteer) == null)
            {
                await roleManager.CreateAsync(new Role() { Name = RoleConstants.Volunteer });
            }

            if (await roleManager.FindByNameAsync(RoleConstants.Organization) == null)
            {
                await roleManager.CreateAsync(new Role() { Name = RoleConstants.Organization });
            }
            
            if (!userManager.Users.Any())
            {
                var user = new User()
                {
                    UserName = "admin",
                    Email = "admin@volunteeringplatform.com",
                    FullName = "Administrator"
                };

                await userManager.CreateAsync(user, "Admin.11");
                await userManager.AddToRoleAsync(user, RoleConstants.Admin);
            }
        }
    }
}
