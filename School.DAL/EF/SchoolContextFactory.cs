﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace School.DAL.EF
{
    public class SchoolContextFactory
    :IDesignTimeDbContextFactory<SchoolContext>
    {
        public SchoolContext CreateDbContext(string[] args)
        {
            var optionBuilder = new DbContextOptionsBuilder<SchoolContext>();
            
            var connectionString =
                @"data source=DESKTOP-8TNDF2C\SQLEXPRESS; initial catalog=SchoolDB_v1.0.0;integrated security=True; MultipleActiveResultSets=True;App=EntityFramework";

            optionBuilder.UseSqlServer(connectionString, options =>
                    options.EnableRetryOnFailure())
                .ConfigureWarnings(warnings =>
                    warnings.Throw(RelationalEventId.QueryPossibleUnintendedUseOfEqualsWarning));

            return new SchoolContext(optionBuilder.Options);
        }
    }
}