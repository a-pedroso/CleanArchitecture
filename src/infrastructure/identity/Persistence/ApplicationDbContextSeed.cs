using CleanArchitecture.Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure.Persistence
{
    public static class IdentityContextSeed
    {
        public static async Task SeedDefaultUserAsync(UserManager<ApplicationUser> userManager)
        {
            var defaultUser = new ApplicationUser { UserName = "administrator@localhost", Email = "administrator@localhost" };

            if (userManager.Users.All(u => u.UserName != defaultUser.UserName))
            {
                await userManager.CreateAsync(defaultUser, "Administrator1!");
            }
        }
    }
}
