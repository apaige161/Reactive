using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Persistance;

namespace API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) //dependancy injection container
        {
            //all services go here

            //controller service
            services.AddControllers();

            //database service
            services.AddDbContext<DataContext>(opt => 
            {
                //connection string
                opt.UseSqlite(Configuration.GetConnectionString("DefaultConnection"));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) //middleware container
        {
            if (env.IsDevelopment())
            {
                //if an exception is thrown, this will display a detailed description of what happened
                app.UseDeveloperExceptionPage();
            }

            //any http request will be redirected to https
            // app.UseHttpsRedirection();

            //enables routing
            app.UseRouting();

            app.UseAuthorization();

            //map controller endpoints into api
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
