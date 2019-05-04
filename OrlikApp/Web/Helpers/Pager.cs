﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Helpers
{
    public class Pager
    {
        public int Index { get; set; }
        public int Size { get; set; }
        public int Offset { get { return Index * Size; } }
    }
}
