using System;
using System.Collections.Generic;
using System.Text;

namespace Logic
{
    public class EventLocation
    {
        public bool isAllowedIn { get; private set; }
        public List<Visitor> individualVisitors = new List<Visitor>();
        public List<Visitor> groupVisitors = new List<Visitor>();
    }
}
