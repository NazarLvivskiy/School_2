﻿using System;
using System.Linq;
using School.DAL.Datalnitialization;
using School.DAL.EF;
using School.DAL.EF.Repository;
using School.Models;

namespace School.Production
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("***** PRODUCTION *****\n");
            using (var context = new SchoolContext())
            {
                MyDatalnitializer.RecreateDatabase(context);
                MyDatalnitializer.InitializeData(context);
                
            }

            using (var repo = new StudentRepository())
            {
                Console.WriteLine(repo.GetOne(10));
            }
        }
    }
}