using CleanArchitecture.Infrastructure.Identity.Models;
using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Reflection;

namespace CleanArchitecture.Infrastructure.Identity.Context
{
    public class IdentityContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        public IdentityContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) 
                : base(options, operationalStoreOptions)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            builder.HasDefaultSchema("Identity");

            base.OnModelCreating(builder);
        }
    }
}
