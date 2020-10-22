using System;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Persistance
{
    //scaffold database - code first

    //DbContext is what allows the program to querry things in the database

    //add this class as a service so it can be injected into other parts of the application
    public class DataContext : DbContext
    {
        //step 1: go to nuget package manager, install entityFrameworkCore && sqlite
        //step 2: >dotnet restore
        //step 3: resolve any version issues
        //step 4: create a constructor that inherits from the base class
        //step 5: add DbContext in services
        //step 6: add "ConnectionStrings": {"DefaultConnection": "Data source=reactive.db"}, a the top of appsettings.json in the API

        //step 7: (extend db start here) add property of entity
        //step 8: >dotnet ef migrations add NewThing -p Persistance/ -s API/
            //-p tells ef where the datacontext is and -s for the startup project

        //get options from base class
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        //pass in properties of type Dbset
        //specify the entity for the DbSet
        //the name of the prop will be the table name in sqlite
        //Values will be the table name
        public DbSet<Value> Values { get; set; }

        //accessible only in this class
        //model the value entity
        protected override void OnModelCreating(ModelBuilder builder)
        {
            //seed data
            builder.Entity<Value>()
                .HasData(
                    new Value { Id = 1, Name = "Value 101"},
                    new Value { Id = 2, Name = "Value 102"},
                    new Value { Id = 3, Name = "Value 103"}
                );
        }
    }
}
