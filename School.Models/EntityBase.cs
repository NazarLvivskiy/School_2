﻿using System.ComponentModel.DataAnnotations;

namespace School.Models
{
    public abstract class EntityBase
    {
        [Key] public int Id { get; set; }

        [Timestamp] public byte[] Timestamp { get; set; }
        
    }
}