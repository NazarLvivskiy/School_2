﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace School.DAL.Entities
{
    public class Class:EntityBase
    {
        [Required(ErrorMessage = "It's required value!")]
        [StringLength(10,ErrorMessage = "Name cannot be longer than 10 characters.")]
        public string Name { get; set; }
        
        public int? TeacherId { get; set; }
        
        [ForeignKey("TeacherId")]
        public Teacher Teacher { get; set; }
        
        public ICollection<Student> Students { get; set; }

        public Class()
        {
            Students = new List<Student>();
        }
    }
}