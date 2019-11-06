using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BusinessLayer.Entities
{
    public class Match
    {
        [Key]
        public long Id { get; set; }

        [Column(TypeName = "nvarchar(256)")]
        public string Descrition { get; set; }

        [Required]
        [Column(TypeName = "smalldatetime")]
        public DateTime StartDate { get; set; }

        [Required]
        [Column(TypeName = "smalldatetime")]
        public DateTime EndOfJoiningDate { get; set; }

        public int? Minutes { get; set; }

        [Required]
        public int WantedPlayersAmmount { get; set; }

        public int? PlayersAmmount { get; set; }

        [Required]
        public bool IsConfirmed { get; set; }

        [Required]
        public long FieldId { get; set; }

        [Required]
        public Field Field { get; set; }

        public long FounderId { get; set; }

        public User Founder { get; set; }

        public List<MatchMember> MatchMembers { get; set; }

    }
}
