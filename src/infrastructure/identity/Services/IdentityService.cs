using CleanArchitecture.Application.Common.Interfaces.Services;
using CleanArchitecture.Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure.Identity.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public IdentityService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<string> GetUserNameAsync(string userId)
        {
            var user = await _userManager.Users.FirstAsync(u => u.Id == userId);

            return user.UserName;
        }
        public async Task<string> CreateUserAsync(string userName, string password)
        {
            var user = new ApplicationUser
            {
                UserName = userName,
                Email = userName,
            };

            var result = await _userManager.CreateAsync(user, password);

            return result.Succeeded ? user.Id : null;
        }

        public async Task<bool> DeleteUserAsync(string userId)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

            if (user != null)
            {
                var result = await _userManager.DeleteAsync(user);
                return result.Succeeded;

            }

            return false;
        }
    }
}
