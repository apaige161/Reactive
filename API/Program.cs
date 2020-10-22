using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Persistance;

namespace API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            //get dataContext service
            using(var scope = host.Services.CreateScope())
            {
                //reference to services
                var services = scope.ServiceProvider;

                //attempt to get DatabaseContext and then migrate database
                try
                {
                    //access DataContext
                    var context = services.GetRequiredService<DataContext>();

                    //migrate database -applies any pending migrations to the database or create a new one if there is not one yet
                    context.Database.Migrate();
                }
                catch (Exception ex)
                {
                    //reference logger services
                    //reference ILogger to use the Program class
                    var logger = services.GetRequiredService<ILogger<Program>>();

                    //log the error to the console
                    logger.LogError(ex, "An error occored during migration");
                }
            }

            //run the rest of the program
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
