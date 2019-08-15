using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models.User
{
    public class UserUpdateResponse
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public DateTime? BirthDate { get; set; }
        public int? Number { get; set; }
        public int? Height { get; set; }
        public int? Weight { get; set; }
        public bool? IsRightFooted { get; set; }
        public string PhoneNumber { get; set; }
        public int RoleId { get; set; }
        public string Street { get; set; }
        public int? StreetNumber { get; set; }
        public string City { get; set; }
    }
}
