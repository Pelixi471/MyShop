﻿using System.ComponentModel.DataAnnotations;

namespace HttpModels
{
    public class AuthenticationRequest
    {
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;
    }
}
