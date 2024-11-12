using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BoysCoffe.ViewModel
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Username is required")]
        [MinLength(8, ErrorMessage = "Username must be at least 8 characters long.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\W).+$", ErrorMessage = "Password must contain uppercase, lowercase letters, and a symbol.")]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; }

        public string Email { get; set; }
    }
}