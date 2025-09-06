using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Polly;

namespace Ordering.API.Extensions
{
    public static class DbExtension
    {
        public static IHost MigrateDatabase<TContext>(this IHost host, Action<TContext, IServiceProvider> seeder)
            where TContext : DbContext
        {
            using (var scope = host.Services.CreateScope())
            {
                var services= scope.ServiceProvider;
                var logger =services.GetRequiredService<ILogger<TContext>>();
                var context = services.GetService<TContext>();

                try
                {
                    logger.LogInformation($"Started db migration: {typeof(TContext).Name}");
                    var retry = Policy.Handle<SqlException>()
                        .WaitAndRetry(
                           retryCount: 5,
                           sleepDurationProvider: retryAttempts => TimeSpan.FromSeconds(Math.Pow(2, retryAttempts)),
                           onRetry: (excption, span, count) =>
                           {
                               logger.LogInformation($"Retrying because of {excption} {span}");
                           });
                    retry.Execute(() => CallSeeder(seeder, context, services));
                    logger.LogInformation($"finished db migration: {typeof(TContext).Name}");


                }
                catch (Exception ex)
                {

                    logger.LogInformation($"An error occured while migration database :{typeof(TContext).Name}");

                }
            }
            return host;

        }

        private static void CallSeeder<TContext>(Action<TContext, IServiceProvider> seeder, TContext? context, IServiceProvider services) where TContext : DbContext
        {
            context.Database.Migrate();
            seeder(context, services);
        }



    }
}
