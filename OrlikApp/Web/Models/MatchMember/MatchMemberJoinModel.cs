using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models.MatchMember
{
    public class MatchMemberJoinModel
    {
        public long PlayerId { get; set; }
        public long MatchId { get; set; }
    }
}