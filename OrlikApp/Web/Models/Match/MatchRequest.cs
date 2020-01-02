using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Web.Models.Helpers.Attributes;

namespace Web.Models.Match
{
    public class MatchRequest
    {
        [MaxLength(256, ErrorMessage = "Opis nie może zawierać więcej niż 256 znaków")]
        public string Description { get; set; }

        [Required]
        [FutureDate(ErrorMessage = "Wymagana data z przyszłości")]
        [DateNotEarlierThan("EndOfJoiningDate", 
            ErrorMessage = "Data startu nie może być wcześniejsza niż data zapisów")]
        public DateTime? StartDate { get; set; }

        [Required]
        [FutureDate(ErrorMessage = "Wymagana data z przyszłości")]
        public DateTime? EndOfJoiningDate { get; set; }

        [Required]
        [Range(30, 120, ErrorMessage = "Mecz może trwać od 30 do 120 minut")]
        public int? Minutes { get; set; }

        [Required]
        public int? WantedPlayersAmmount { get; set; }

        public int? PlayersAmmount { get; set; }

        [Required]
        public long? FieldId { get; set; }
    }
}
