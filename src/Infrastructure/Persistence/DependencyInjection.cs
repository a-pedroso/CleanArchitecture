using CleanArchitecture.Application.Common.Interfaces.Repositories;
using CleanArchitecture.Application.Features.Products;
using CleanArchitecture.Infrastructure.Persistence.Context;
using CleanArchitecture.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Infrastructure.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructurePersistence(this IServiceCollection services, IConfiguration configuration)
        {
            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseInMemoryDatabase("ApplicationDb"));
            }
            else
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(
                        configuration.GetConnectionString("DefaultConnection"),
                        b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
            }
            
            #region Repositories
            services.AddScoped(typeof(IGenericRepository<,>), typeof(GenericRepository<,>))
                    .AddScoped<IProductRepository, ProductRepository>();
            #endregion

            return services;
        }
    }
}
