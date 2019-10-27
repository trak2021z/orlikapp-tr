using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLayer.Entities
{
    public class WorkingTime
    {
        [Key]
        public long Id { get; set; }

        public DayOfWeek Day { get; set; }

        [Column(TypeName = "time(0)")]
        public TimeSpan OpenHour { get; set; }

        [Column(TypeName = "time(0)")]
        public TimeSpan CloseHour { get; set; }

        public long FieldId { get; set; }

        public Field Field { get; set; }

        #region NOT MAPPED
        public override bool Equals(object obj)
        {
            if (obj is WorkingTime comparedWorkingTime)
            {
                if (comparedWorkingTime.Day == Day
                    && comparedWorkingTime.OpenHour == OpenHour
                    && comparedWorkingTime.CloseHour == CloseHour)
                {
                    return true;
                }
            }

            return base.Equals(obj);
        }
        #endregion
    }
}
