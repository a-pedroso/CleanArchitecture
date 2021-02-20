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

        public static async Task EnsureDatabaseMigratedAsync(IServiceScope scope, Exception exception = null) 
        {
            if(tries == 4)
            {
                throw exception;
            }

            try
            {
                // wait some seconds to make sure the mssql is already up
                await Task.Delay(TimeSpan.FromSeconds(10));

                tries++;
                using var context = scope.ServiceProvider.GetRequiredService<TdbContext>();
                await context.Database.MigrateAsync();
            }
            catch(Exception ex)
            {
                await EnsureDatabaseMigratedAsync(scope, ex);
            }
        }
    }
}
