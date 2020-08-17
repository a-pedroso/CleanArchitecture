using System.Threading.Tasks;

namespace CleanArchitecture.Application.Common.Interfaces.Services
{
    public interface IIdentityService
    {
        Task<string> GetUserNameAsync(string userId);

        Task<string> CreateUserAsync(string userName, string password);

        Task<bool> DeleteUserAsync(string userId);
    }
}
