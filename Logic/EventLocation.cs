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
            visitors.Add(new Visitor(CheckVisitorAge(EventVisitor), name, CheckTicketBoughtInTime(EventVisitor)));
        }

        public void MakeGroup(int id, int size, int birthYear, int birthMonth, int birthDay, string name, int buyYear, int buyMonth, int buyDay)
        {
            List<Visitor> group = new List<Visitor>();
            for (int i = 0; i < size; i++)
            {
                Visitor eventVisitor = new Visitor(birthYear, birthMonth, birthDay, buyYear, buyMonth, buyDay);
                group.Add(new Visitor(CheckVisitorAge(eventVisitor), name, CheckTicketBoughtInTime(eventVisitor)));
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
            groupSections.Clear();
            foreach (var visitor in visitors)
            {
                if (AllowIn(visitor))
                {
                    CoupleChairAndVisitor(SelectBestSection(1), visitor);
                }
            }
        }

        private void CoupleChairAndVisitor(Section section, Visitor visitor)
        {
            section.CoupleChairAndIndividualVisitor(visitor);
        }
        #endregion

        #region Group Algorithm
        public void PlaceGroups()
        {
            foreach (var group in groups)
            {
                groupSections.Clear();
                GetBigGroupSectionList(group);
                if (AllowInGroup(group))
                {
                    PlaceGroupMembers(group);
                }
            }
        }

        public void PlaceGroupMembers(Group group)
        {
            foreach (var section in groupSections)
            {
                group.PlacePeople(section);
            }
        }

        public int CountAllowableGroupMembers(Group group)
        {
            int members = 0;
            foreach (var visitor in group.visitors)
            {
                if (visitor.TicketBought)
                {
                    members++;
                }
            }
            return members;
        }
        #endregion

        #region Select Group Section Functions
        public Section SelectBestSection(int visitorAmount)
        {
            int chairsLeft = 100;
            Section optimalSection = sections[0];
            foreach (var section in sections)
            {
                int ChairVisitorDiff = section.CountUnoccupiedChairs() - visitorAmount;
                if (ChairVisitorDiff < chairsLeft && ChairVisitorDiff > -1 && !groupSections.Contains(section))
                {
                    chairsLeft = ChairVisitorDiff;
                    optimalSection = section;
                }
            }
            ChairlessGroupMembers = 0;
            return optimalSection;
        }

        public Section GetBigGroupSection(int chairlessVisitors)
        {
            Section bestSection = sections[0];
            int counter = 0;
            foreach (var section in sections)
            {
                int ChairVisitorDiff = chairlessVisitors - section.CountUnoccupiedChairs();
                if (counter == 0 && !groupSections.Contains(sections[counter]))
                {
                    ChairlessGroupMembers = ChairVisitorDiff;
                }
                else if (ChairVisitorDiff < ChairlessGroupMembers && !groupSections.Contains(sections[counter]))
                {
                    ChairlessGroupMembers = ChairVisitorDiff;
                    bestSection = section;
                }
                counter++;
            }
            return bestSection;
        }

        public List<Section> GetBigGroupSectionList(Group group)
        {
            ChairlessGroupMembers = CountAllowableGroupMembers(group);
            while (ChairlessGroupMembers > 0)
            {
                if (GroupIsBiggerThanAllSections(ChairlessGroupMembers))
                {
                    groupSections.Add(GetBigGroupSection(ChairlessGroupMembers));
                }
                else
                {
                    groupSections.Add(SelectBestSection(ChairlessGroupMembers));
                }
            }
            return groupSections;
        }
        #endregion

        #region Group Bools
        public bool AllowInGroup(Group group)
        {
            return groupSections.Count <= group.CountAmountOfAdults();
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


