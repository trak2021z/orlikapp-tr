using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Models.Match
{
    public class MatchSearch
    {
        public long? FieldId { get; private set; }

        public MatchSearch(long? fieldId)
        {
            FieldId = fieldId;
        }
    }
}
