using System;
using System.Collections.Generic;
using System.Text;

namespace Logic
{
    public class Chair
    {
#nullable enable
        public int ChairNumber { get; private set; }
        public int RowNumber { get; private set; }
        public bool Occupied { get; set; }
        public Visitor visitor { get; private set; }

        public Chair(int rowNmbr, int chairNmbr)
        {
            this.ChairNumber = chairNmbr;
            this.RowNumber = rowNmbr;
        }

        public override string ToString()
        {
            return $"{RowNumber + 1}-{ChairNumber + 1} {visitor}";
        }

        public Visitor GetVisitor(Visitor chairVisitor)
        {
            return visitor = chairVisitor;
        }

        public void SetChairOccupation()
        {
            Occupied = (visitor != null);
        }
#nullable disable
    }
}
