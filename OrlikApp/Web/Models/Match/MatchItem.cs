using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Models.Field;
using Web.Models.User;

namespace Web.Models.Match
{
    public class MatchItem
    {
        public long Id { get; set; }
        public string Description { get; set; }
        public string StartDate { get; set; }
        public string EndOfJoiningDate { get; set; }
        public int Minutes { get; set; }
        public int WantedPlayersAmmount { get; set; }
        public int WantedPlayersLeftAmmonut { get; set; }
        public int? PlayersAmmount { get; set; }
        public UserSimpleItem Founder { get; set; }
        public FieldBaseItem Field { get; set; }
        public List<UserSimpleItem> MatchMembers { get; set; }
    }
}
