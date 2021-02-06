using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace CleanArchitecture.WebApi.Helpers
{
    public static class DbMigrationHelper<TdbContext>
        where TdbContext : DbContext
    {
        static int tries;

        public static async Task EnsureDatabaseMigratedAsync(IServiceScope scope) 
        {
            if(tries == 4)
            {
                throw new Exception("EnsureDatabaseMigratedAsync FAILED");
            }

            try
            {
                tries++;
                using var context = scope.ServiceProvider.GetRequiredService<TdbContext>();
                await context.Database.MigrateAsync();
            }
            catch(Exception)
            {
                await Task.Delay(TimeSpan.FromSeconds(15));
                await EnsureDatabaseMigratedAsync(scope);
            }
        }
    }
}
