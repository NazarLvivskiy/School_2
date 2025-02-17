﻿using System;
using System.Collections.Generic;
using System.Linq;
using School.BLL.Dto;
using School.BLL.Services;


namespace School.UI
{
    public class Ui
    {
        private static StudentService _studentService;
        public Ui(MainService main)
        {
            _studentService = new StudentService(main.GetDb());
            
        }
        public static void ShowFullInfo(StudentDto s)
        {
            if (s!= null)
            {
                var @class = s.ClassId != null ? _studentService.GetClassForId(s.ClassId)?.Name : "no class";
                Console.WriteLine($"FullName: {s.FullName}\n" +
                                  $"Age:      {s.Age}\n" +
                                  $"Class:    {@class}\n" +
                                  $"Gender:   {s.Gender}\n" +
                                  "Subjects:           \n" +
                                  ShowSubjects(_studentService.GetSubjects(s.Id)));
            }
            else
            {
                Console.WriteLine("\n\nYou entered bas value.\n Pls enter correct value :)\n\n");
            }
            
        }

        private static string ShowSubjects(IEnumerable<SubjectDto> subjects)
        {
            return subjects.Aggregate("\t\t", (current, subject) => current + (subject.Name + "\n\t\t"));
        }
        
        public static void ShowMyTeacher(TeacherDto t)
        {
            if (t!= null)
            {
                Console.WriteLine($"FullName: {t.FullName}\n" +
                                  $"Age:      {t.Age}\n");
            }
            else
            {
                Console.WriteLine("This student doesn't have teacher\\class");
            }
            
        }

        public static void ShowMyClassmates(ICollection<StudentDto> students)
        {
            if (students?.Count>=1)
            {
                Console.WriteLine("\n\n");
                foreach (var student in students)
                {
                    Console.WriteLine(student.FullName);
                }
                Console.WriteLine("\n\n");
            }
            else
            {
                Console.WriteLine("\n\nNo classmates (:\n\n");
            }
        }
    }
}