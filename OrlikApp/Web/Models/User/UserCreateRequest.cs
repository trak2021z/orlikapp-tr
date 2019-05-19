using BusinessLayer.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models.User
{
    public class UserCreateRequest : UserBaseRequest
    {
        [Required(ErrorMessage = " Hasło jest wymagane")]
        [StringLength(60, ErrorMessage = "Hasło jest zbyt długie")]
        [MinLength(6, ErrorMessage = "Hasło musi mieć przynajmniej 6 znaków")]
        public string Password { get; set; }
    }
}
