using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLayer.Helpers.Pagination
{
    public class Pager
    {
        public int Index { get; set; }
        public int Size { get; set; }
        public int Offset { get { return (Index - 1) * Size; } }

        public Pager()
        {

        }

        public Pager(Pager pager)
        {
            Index = (Index <= 0) ? 1 : Index;
            Size = (Size <= 0) ? 10 : Size;
        }

        public Pager(int index, int size)
        {
            Index = (index <= 0) ? 1 : index;
            Size = (size <= 0) ? 10 : size;
        }
    }
}
