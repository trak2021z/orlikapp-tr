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
        public string DayName
        {
            get
            {
                string dayName = string.Empty;
                switch ((int)Day)
                {
                    case 0:
                        dayName = "Niedziela";
                        break;
                    case 1:
                        dayName = "Poniedziałek";
                        break;
                    case 2:
                        dayName = "Wtorek";
                        break;
                    case 3:
                        dayName = "Środa";
                        break;
                    case 4:
                        dayName = "Czwartek";
                        break;
                    case 5:
                        dayName = "Piątek";
                        break;
                    case 6:
                        dayName = "Sobota";
                        break;
                }

                return dayName;
            }
        }

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
