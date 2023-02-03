﻿using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    public class RegisterRequest
    {
        [Required]
        public string? FirstName { get; set; }

        [Required]
        public string? LastName { get; set; }

        [Required]
        public string? Username { get; set; }

        [Required]
        public string? Password { get; set; }
    }
}
