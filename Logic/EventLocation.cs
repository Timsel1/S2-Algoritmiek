using System;
using System.Collections.Generic;
using System.Text;

namespace Logic
{
    public class EventLocation
    {
        public bool IsAllowedIn { get; private set; }
        private int ChairlessGroupMembers { get; set; }
        public DateTime EventDate { get; private set; }
        public DateTime TicketSaleEndDate { get; private set; }
        private readonly DateTime zeroTime = new DateTime(1, 1, 1);
        public List<Visitor> visitors = new List<Visitor>();
        public List<Group> groups = new List<Group>();
        public List<Section> sections = new List<Section>();
        public List<Section> groupSections = new List<Section>();

        public EventLocation(int eventYear, int eventMonth, int eventDay, int endSaleYear, int endSaleMonth, int endSaleDay)
        {
            this.EventDate = new DateTime(eventYear, eventMonth, eventDay);
            this.TicketSaleEndDate = new DateTime(endSaleYear, endSaleMonth, endSaleDay);
        }
        #region Make Functions
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
                Visitor eventVisitor = new Visitor(birthYear, birthMonth, birthDay, buyYear, buyMonth, buyDay);
                group.Add(new Visitor(CheckVisitorAge(eventVisitor), name, CheckTicketBoughtInTime(eventVisitor), null));
            }
            groups.Add(new Group(id, size, group));
        }
        #endregion

        #region Check Functions
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
        #endregion

        #region Visitor functions
        public void PlaceVisitors()
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
        #endregion

        #region Group Functions
        public void ClassifyGroups()
        {
            foreach (var group in groups)
            {

            }
        }

        //public bool AllowInGroup(Group group)
        //{
        //    int groupSize = group.visitors.Count;
        //    if (!GroupIsBiggerThanAllSections(groupSize))
        //    {
        //        return true;
        //    }
        //}

        public Section SelectBestSectionForGroup(int chairlessMembers)
        { 
            int chairsLeft = 100;
            int optimalSection = 0;
            for (int i = 0; i < sections.Count; i++)
            {
                int ChairVisitorDiff = chairlessMembers - sections[i].CountUnoccupiedChairs();
                if (ChairVisitorDiff == 0)
                {
                    optimalSection = i;
                    break;
                }
                else if (chairsLeft > ChairVisitorDiff)
                {
                    chairsLeft = ChairVisitorDiff;
                    optimalSection = i;
                }
            }

            return sections[optimalSection];
        }

        public List<Section> GetBigGroupSections(Group group)
        {
            Section section;
            groupSections.Clear();
            groupSections.Add(GetFirstBigGroupSection(group));
            while (ChairlessGroupMembers > 0)
            {
                if (GroupIsBiggerThanAllSections(ChairlessGroupMembers))
                {
                    section = GetOtherBigGroupSections(group);
                    if (!groupSections.Contains(section))
                    {
                        groupSections.Add(section);
                    }
                }
                else
                {
                    section = SelectBestSectionForGroup(ChairlessGroupMembers);
                    if (!groupSections.Contains(section))
                    {
                        groupSections.Add(section);
                    }
                }
            }
            return groupSections;
        }

        public Section GetFirstBigGroupSection(Group group)
        {
            Section bestSection = sections[0];
            int counter = 0;
            foreach (var section in sections)
            {
                int ChairVisitorDiff = group.visitors.Count - section.CountUnoccupiedChairs();
                if (counter == 0)
                {
                    ChairlessGroupMembers = ChairVisitorDiff;
                }
                else if (ChairVisitorDiff < ChairlessGroupMembers)
                {
                    ChairlessGroupMembers = ChairVisitorDiff;
                    bestSection = sections[counter];
                }
                counter++;
            }
            return bestSection;
        }

        public Section GetOtherBigGroupSections(Group group)
        {
            Section bestSection = sections[0];
            int counter = 0;
            foreach (var section in sections)
            {
                int ChairVisitorDiff = ChairlessGroupMembers - section.CountUnoccupiedChairs();
                if (counter == 0)
                {
                    ChairVisitorDiff = ChairlessGroupMembers;
                }
                else if (ChairVisitorDiff < ChairlessGroupMembers)
                {
                    ChairVisitorDiff = ChairlessGroupMembers;
                    bestSection = sections[counter];
                }
                counter++;
            }
            return bestSection;
        }

        public bool GroupIsBiggerThanAllSections(int groupSize)
        {
            bool groupIsBigger = false;
            foreach (var section in sections)
            {
                if (groupSize > section.CountUnoccupiedChairs())
                {
                    groupIsBigger = true;
                }
                else
                {
                    groupIsBigger = false;
                    break;
                }
            }
            return groupIsBigger;
        }
    #endregion
    }
    
}
