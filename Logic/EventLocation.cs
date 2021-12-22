using System;
using System.Collections.Generic;
using System.Text;

namespace Logic
{
    public class EventLocation
    {
        public bool IsAllowedIn { get; private set; }
        public DateTime EventDate { get; private set; }
        public DateTime TicketSaleEndDate { get; private set; }
        private readonly DateTime zeroTime = new DateTime(1, 1, 1);
        public List<Visitor> visitors = new List<Visitor>();
        public List<Group> groups = new List<Group>();
        public List<Section> sections = new List<Section>();
        public List<Section> optimalSections = new List<Section>();

        public EventLocation(int eventYear, int eventMonth, int eventDay, int endSaleYear, int endSaleMonth, int endSaleDay)
        {
            this.EventDate = new DateTime(eventYear, eventMonth, eventDay);
            this.TicketSaleEndDate = new DateTime(endSaleYear, endSaleMonth, endSaleDay);
        }

#nullable enable
        public void MakeSection(string name, int rows, int chairs)
        {
            sections.Add(new Section(name, rows, chairs));
        }

        public void MakeVisitorList(int birthYear, int birthMonth, int birthDay, string name, int buyYear, int buyMonth, int buyDay)
        {
            Visitor EventVisitor = new Visitor(birthYear, birthMonth, birthDay, buyYear, buyMonth, buyDay);
            visitors.Add(new Visitor(CheckVisitorAge(EventVisitor), name, CheckTicketBoughtInTime(EventVisitor), null));
        }

        public void MakeGroup(int id, int size, int birthYear, int birthMonth, int birthDay, string name, int buyYear, int buyMonth, int buyDay)
        {
            List<Visitor> group = new List<Visitor>();
            for (int i = 0; i < size; i++)
            {
                Visitor eventVisitor = new Visitor(birthYear + 1, birthMonth, birthDay, buyYear, buyMonth, buyDay + i);
                group.Add(new Visitor(CheckVisitorAge(eventVisitor), name, CheckTicketBoughtInTime(eventVisitor), null));
            }
            groups.Add(new Group(id, size, group));
        }

        public int CheckVisitorAge(Visitor visitor)
        {
            TimeSpan timeSpan = EventDate - visitor.BirthDate;
            return (zeroTime + timeSpan).Year - 1;
        }

        public bool CheckTicketBoughtInTime(Visitor visitor)
        {
            var diffOfDates = TicketSaleEndDate.Subtract(visitor.TicketBuyDate);
            return (diffOfDates.Days >= 0);
        }

        public bool AllowIn(Visitor visitor)
        {
            return (visitor.Age >= 18 && visitor.TicketBought);
        }

        public void ClassifyVisitors()
        {
            foreach (var visitor in visitors)
            {
                foreach (var section in sections)
                {
                    if (visitor.visitorChair != null)
                    {
                        break;
                    }
                    foreach (var chair in section.chairs)
                    {
                        if (AllowIn(visitor) && !chair.Occupied)
                        {
                            visitor.visitorChair = chair;
                            chair.Occupied = true;
                            break;
                        }
                    }
                }
            }
        }

        public void ClassifyGroups()
        {
            int listNmbr = 0;
            foreach (var group in groups)
            {
                if (group.GroupHasAdult())
                {
                    SelectBestSectionForGroup(listNmbr);
                }
                listNmbr++;
            }
        }

        public Section SelectBestSectionForGroup(int groupId)
        {
            int chairsLeft = 100;
            int optimalSection = 0;
            for (int i = 0; i < sections.Count; i++)
            {
                int ChairVisitorDiff = sections[i].CountUnoccupiedChairs() - groups[groupId].visitors.Count;
                if (ChairVisitorDiff == 0)
                {
                    optimalSection = i;
                    break; 
                }
                else if(chairsLeft > ChairVisitorDiff && ChairVisitorDiff > 0)
                {
                    chairsLeft = ChairVisitorDiff;
                    optimalSection = i;
                }
            }
            return sections[optimalSection];
        }

    }
}
