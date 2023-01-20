using Microsoft.EntityFrameworkCore;
using ServicesManager.Persistence;

namespace ServicesManager.API.Extensions
{
    public static class MigrationManagerMiddleware
    {
        public static IApplicationBuilder MigrateDatabase(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                using (var appContext = scope.ServiceProvider.GetRequiredService<ServicesDbContext>())
                {
                    try
                    {
                        appContext.Database.Migrate();
                    }
                    catch (Exception ex)
                    {
                        //Log errors or do anything you think it's needed
                        throw;
                    }
                }
            }
            return app;
        }
    }
}
