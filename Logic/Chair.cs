using System;
using System.Collections.Generic;
using System.Text;

namespace Logic
{
    public class Chair
    {
        public int ChairNumber { get; private set; }
        public int RowNumber { get; private set; }
        public bool Occupied { get; set; }

        public Chair(int rowNmbr, int chairNmbr, bool taken)
        {
            this.ChairNumber = chairNmbr;
            this.RowNumber = rowNmbr;
            this.Occupied = taken;
        }

        public override string ToString()
        {
            return $"{RowNumber + 1}-{ChairNumber + 1}";
        }

    }
}
