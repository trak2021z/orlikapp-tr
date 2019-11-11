using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Models.Match
{
    public class OccupiedFieldModel
    {
        public bool IsOccupied { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public OccupiedFieldModel()
        {
            IsOccupied = false;
        }
    }
}
