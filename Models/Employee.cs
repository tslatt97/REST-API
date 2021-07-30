﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Employees.Models
{

    // Employee objekt
    public class Employee
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string EmploymentDate { get; set; }
    }
}
