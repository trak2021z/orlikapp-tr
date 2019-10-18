using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Web.Models.WorkingTime;

namespace Web.Models.Field
{
    public class FieldCreateRequest
    {
        public int? Length { get; set; }

        public int? Width { get; set; }

        [StringLength(512, ErrorMessage = "Opis jest zbyt długi")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Ulica jest wymagana")]
        [StringLength(128, ErrorMessage = "Nazwa ulicy jest zbyt długa")]
        public string Street { get; set; }

        public int? StreetNumber { get; set; }

        [Required(ErrorMessage = "Nazwa miasta jest wymagana")]
        [StringLength(128, ErrorMessage = "Nazwa miasta jest zbyt długa")]
        public string City { get; set; }

        public long? KeeperId { get; set; }

        [Required(ErrorMessage = "Typ boiska jest wymagany")]
        public long? TypeId { get; set; }

        public List<WorkingTimeRequest> WorkingTime { get; set; }
    }
}
