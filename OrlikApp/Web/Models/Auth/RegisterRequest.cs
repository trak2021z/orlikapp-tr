using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models.Auth
{
    public class RegisterRequest
    {
        [Required(ErrorMessage = "Nazwa użytkownika jest wymagana")]
        [StringLength(60, ErrorMessage = "Login jest zbyt długi")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Hasło jest wymagane")]
        [StringLength(60, ErrorMessage = "Hasło jest zbyt długie")]
        [MinLength(6, ErrorMessage = "Hasło musi mieć przynajmniej 6 znaków")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Hasło jest wymagane")]
        [Compare("Password", ErrorMessage = "Hasła muszą być identyczne")]
        public string RepeatedPassword { get; set; }

        [Required(ErrorMessage = "Email jest wymagany")]
        [EmailAddress(ErrorMessage = "Niepoprawny format adresu email")]
        [StringLength(250, ErrorMessage = "Email jest zbyt długi")]
        public string Email { get; set; }
    }
}
