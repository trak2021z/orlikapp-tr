using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models.Match
{
    public class MatchCreateResponse
    {
        public long Id { get; set; }
        public bool IsConfirmed { get; set; }
    }
}
