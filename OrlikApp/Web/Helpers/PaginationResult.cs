using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Helpers
{
    public class PaginationResult<T>
    {
        public IEnumerable<T> Items { get; set; }
        public int RowNumber { get; set; }
    }
}
