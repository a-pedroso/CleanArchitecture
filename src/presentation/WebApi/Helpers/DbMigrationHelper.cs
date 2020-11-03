using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArchitecture.WebApi.Helpers
{
    public static class DbMigrationHelper<TdbContext>
        where TdbContext : DbContext
    {
        public static async Task EnsureDatabaseMigratedAsync(IServiceScope scope) 
        {
            using var context = scope.ServiceProvider.GetRequiredService<TdbContext>();

            await context.Database.MigrateAsync();
        }
    }
}
