using System;
using System.Collections.Generic;
using System.Text;

namespace Logic
{
    public class Visitor
    {

        
        public DateTime BirthDate { get; private set; }
        public DateTime TicketBuyDate { get; private set; }
        public int Age { get; private set; }
        public bool TicketBought { get; private set; }
        public string Name { get; private set; }
        public bool IsAdult { get; private set; }
        public bool HasChair { get; set; }

        public Visitor(int birthYear, int birthMonth, int birthDay, int buyYear, int buyMonth, int buyDay)
        {
            this.BirthDate = new DateTime(birthYear, birthMonth, birthDay);
            this.TicketBuyDate = new DateTime(buyYear, buyMonth, buyDay);
        }

        public Visitor(int age, string name, bool ticketBought)
        {
            this.Age = age;
            this.Name = name;
            this.TicketBought = ticketBought;
            this.IsAdult = (Age > 17);
        }

        public override string ToString()
        {
            return $"age is {Age}, name is {Name}, ticket bought in time {TicketBought}";
        }

    }
}
