using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Models.Match
{
    public class MatchSearch
    {
        public long? FieldId { get; private set; }
        public bool OnlyUnconfirmed { get; private set; }
        public bool OnlyOwn { get; private set; }

        public MatchSearch(long? fieldId, bool onlyUnconfirmed = false, bool onlyOwn = false)
        {
            FieldId = fieldId;
            OnlyUnconfirmed = onlyUnconfirmed;
            OnlyOwn = onlyOwn;
        }
    }
}
