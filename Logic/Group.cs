using System;
using System.Collections.Generic;
using System.Text;

namespace Logic
{
    public class Group
    {
        public int Size { get; private set; }
        public int GroupId { get; private set; }
        public int AmountOfAdults { get; private set; }
        public bool HasAdult { get; private set; }
        public List<Visitor> visitors = new List<Visitor>();

        public Group(int groupId)
        {
            this.Size = visitors.Count;
            this.GroupId = groupId;
        }

        public void MakeAdults(int amount, DateTime eventDate, DateTime ticketSaleEnd)
        {
            for (int i = 0; i < amount; i++)
            {
                Visitor visitor = new Visitor(GroupId, 1998, 9, 9, 2021, 1, 10);
                visitor.CalculateVisitorAge(eventDate);
                visitor.CalculateTicketBoughtInTime(ticketSaleEnd);
                visitors.Add(visitor);
            }
        }

        public void MakeKid(int amount, DateTime eventDate, DateTime ticketSaleEnd)
        {
            for(int i = 0; i < amount; i++)
            {
                Visitor visitor = new Visitor(GroupId, 2015, 9, 9, 2021, 1, 10);
                visitor.CalculateVisitorAge(eventDate);
                visitor.CalculateTicketBoughtInTime(ticketSaleEnd);
                visitors.Add(visitor);
            }
        }

        public bool GroupHasAdult()
        {
            foreach (var groupMember in visitors)
            {
                if (groupMember.Age >= 12)
                {
                    HasAdult = true;
                    break;
                }
            }
            return HasAdult;
        }

        public int CountAmountOfAdults()
        {
            AmountOfAdults = 0;
            foreach (var groupMember in visitors)
            {
                if (groupMember.Age >= 12)
                {
                    AmountOfAdults++;
                }
            }
            return AmountOfAdults;
        }
    }
}
