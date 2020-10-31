﻿using System;
using System.ComponentModel.DataAnnotations;

namespace LearnEveryDay.Models
{
    public class UserType
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }
    }
}