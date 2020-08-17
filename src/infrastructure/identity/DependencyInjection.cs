using CleanArchitecture.Application.Common.Interfaces.Services;
using CleanArchitecture.Infrastructure.Identity.Context;
using CleanArchitecture.Infrastructure.Identity.Models;
using CleanArchitecture.Infrastructure.Identity.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureIdentity(this IServiceCollection services, IConfiguration configuration)
        {
            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<IdentityContext>(options =>
                    options.UseInMemoryDatabase("IdentityDb"));
            }
            else
            {
                services.AddDbContext<IdentityContext>(options =>
                    options.UseSqlServer(
                        configuration.GetConnectionString("DefaultConnection"),
                        b => b.MigrationsAssembly(typeof(IdentityContext).Assembly.FullName)));
            }

            services.AddDefaultIdentity<ApplicationUser>()
                .AddEntityFrameworkStores<IdentityContext>();
            
            services.AddIdentityServer()
                .AddApiAuthorization<ApplicationUser, IdentityContext>();

            services.AddTransient<IIdentityService, IdentityService>();

            services.AddAuthentication().AddIdentityServerJwt();

            return services;
        }
    }
}
