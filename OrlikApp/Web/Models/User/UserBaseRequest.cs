using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models.User
{
    public class UserBaseRequest
    {
        [StringLength(60, ErrorMessage = "Imię jest zbyt długie")]
        public string FirstName { get; set; }

        [StringLength(60, ErrorMessage = "Nazwisko jest zbyt długie")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Nazwa użytkownika jest wymagana")]
        [StringLength(60, ErrorMessage = "Login jest zbyt długi")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Email jest wymagany")]
        [EmailAddress(ErrorMessage = "Niepoprawny format adresu email")]
        [StringLength(250, ErrorMessage = "Email jest zbyt długi")]
        public string Email { get; set; }

        public DateTime? BirthDate { get; set; }

        public int? Height { get; set; }

        public int? Weight { get; set; }

        [StringLength(20, ErrorMessage = "Numer telefonu jest zbyt długi")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Rola jest wymagana")]
        public int RoleId { get; set; }

        [StringLength(120, ErrorMessage = "Nazwa ulicy jest zbyt długa")]
        public string Street { get; set; }

        public int? StreetNumber { get; set; }

        [StringLength(120, ErrorMessage = "Numer miasta jest zbyt długa")]
        public string City { get; set; }
    }
}
