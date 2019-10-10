using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BusinessLayer.Entities
{
    public class MatchMember
    {
        [Required]
        public DateTime JoiningDate { get; set; }

        public long MatchId { get; set; }

        public Match Match { get; set; }

        public long PlayerId { get; set; }

        public User Player { get; set; }
    }
}
