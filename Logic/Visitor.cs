using System;
using System.Collections.Generic;
using System.Text;

namespace Logic
{
    public class Visitor
    {


        public DateTime BirthDate { get; private set; }
        public DateTime TicketBuyDate { get; private set; }
        private readonly DateTime zeroTime = new DateTime(1, 1, 1);
        public int Age { get; private set; }
        public bool TicketBought { get; private set; }
        public string Name { get; private set; }
        public int GroupID { get; private set; }
        public bool HasChair { get; private set; }

        public Visitor(string name, int birthYear, int birthMonth, int birthDay, int buyYear, int buyMonth, int buyDay)
        {
            this.BirthDate = new DateTime(birthYear, birthMonth, birthDay);
            this.TicketBuyDate = new DateTime(buyYear, buyMonth, buyDay);
            this.Name = name;
        }

        public Visitor(int groupID, int birthYear, int birthMonth, int birthDay, int buyYear, int buyMonth, int buyDay)
        {
            this.BirthDate = new DateTime(birthYear, birthMonth, birthDay);
            this.TicketBuyDate = new DateTime(buyYear, buyMonth, buyDay);
            this.GroupID = groupID;
        }

        public override string ToString()
        {
            if (Name != null)
            {
                return $"age is {Age}, name is {Name}, ticket bought in time {TicketBought}, and has chair {HasChair}";
            }
            return $"age is {Age}, Group: {GroupID}, ticket bought in time {TicketBought}, and has chair {HasChair}";

        }

        public void GiveVisitorAChair()
        {
            HasChair = true;
        }

        public void CalculateVisitorAge(DateTime eventDate)
        {
            TimeSpan timeSpan = eventDate.Subtract(BirthDate);
            Age = (zeroTime + timeSpan).Year - 1;
        }

        public void CalculateTicketBoughtInTime(DateTime ticketSaleEnd)
        {
            var diffOfDates = ticketSaleEnd.Subtract(TicketBuyDate);
            TicketBought = (diffOfDates.Days >= 0);
        }
    }
}
