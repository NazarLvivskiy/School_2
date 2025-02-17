﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using School.DAL.Entities;

namespace School.DAL
{
    public class SchoolDbContext:DbContext
    {
        public DbSet<Student> Students { get; set; }

        public DbSet<Teacher> Teachers { get; set; }

        public DbSet<Subject> Subjects { get; set; }

        public DbSet<Class> Classes { get; set; }

        private readonly IConfiguration _configuration;

        public SchoolDbContext()
        {
            _configuration = new ConfigurationBuilder().AddJsonFile(@"E:\Tasks\School\School.DAL\appsettings.json").Build();
        }
        
        public SchoolDbContext(DbContextOptions options) :base(options){}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured) return;

            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer(connectionString,
                    options => 
                        options.EnableRetryOnFailure())
                . ConfigureWarnings (warnings=>
                    warnings.Throw(RelationalEventId.QueryPossibleUnintendedUseOfEqualsWarning) ) ;
        }
    }
}