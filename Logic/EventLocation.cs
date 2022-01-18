using System;
using System.Collections.Generic;
using System.Text;

namespace Logic
{
    public class EventLocation
    {
        private int ChairlessGroupMembers { get; set; }
        public DateTime EventDate { get; private set; }
        public DateTime TicketSaleEndDate { get; private set; }
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
        public void MakeSectionList(string name, int rows, int chairs)
        {
            sections.Add(new Section(name, rows, chairs));
        }

        public void MakeVisitorList(string name, int birthYear, int birthMonth, int birthDay, int buyYear, int buyMonth, int buyDay)
        {
            Visitor EventVisitor = new Visitor(name, birthYear, birthMonth, birthDay, buyYear, buyMonth, buyDay);
            EventVisitor.CalculateVisitorAge(EventDate);
            EventVisitor.CalculateTicketBoughtInTime(TicketSaleEndDate);
            visitors.Add(EventVisitor);
        }

        public void MakeGroup(int id, int amountOfAdults, int amountOfChildren)
        {
            Group group = new Group(id);
            group.MakeAdults(amountOfAdults, EventDate, TicketSaleEndDate);
            group.MakeKid(amountOfChildren, EventDate, TicketSaleEndDate);
            groups.Add(group);
        }
        #endregion

        #region Group Functions
        public List<Section> GetOptimalSectionListForGroup(Group group)
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
                    groupSections.Add(SelectOptimalSection(ChairlessGroupMembers));
                }
            }
            return groupSections;
        }

        public void PlaceAdultInEachSection(Group group)
        {
            foreach (var section in groupSections)
            {
                foreach (var visitor in group.visitors)
                {
                    if (visitor.Age > 12 && !visitor.HasChair)
                    {
                        section.CoupleChairAndVisitor(visitor);
                        break;
                    }
                }
            }
        }
        #endregion

        #region Select Group Section Functions
        public Section GetBigGroupSection(int chairlessVisitors)
        {
            Section bestSection = sections[0];
            ChairlessGroupMembers = 100;
            foreach (var section in sections)
            {
                int ChairVisitorDiff = chairlessVisitors - section.CountUnoccupiedChairs();
                if (ChairVisitorDiff < ChairlessGroupMembers && !groupSections.Contains(section))
                {
                    ChairlessGroupMembers = ChairVisitorDiff;
                    bestSection = section;
                }
            }
            return bestSection;
        }

        public Section SelectOptimalSection(int visitorAmount)
        {
            int chairsLeft = 100;
            Section optimalSection = sections[0];
            foreach (var section in sections)
            {
                int ChairVisitorDiff = section.CountUnoccupiedChairs() - visitorAmount;
                if (ChairVisitorDiff < chairsLeft && ChairVisitorDiff >= 0 && !groupSections.Contains(section))
                {
                    chairsLeft = ChairVisitorDiff;
                    optimalSection = section;
                }
            }
            ChairlessGroupMembers = 0;
            return optimalSection;
        }
        #endregion

        #region Count Functions

        private int CountAllowableGroupMembers(Group group)
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

        public int CountAssignedChairsOccupation()
        {
            int freeChairs = 0;
            foreach (var section in sections)
            {
                freeChairs = section.CountUnoccupiedChairs();
            }
            return freeChairs;
        }
        #endregion

        #region Bool Functions
        public bool AllowIn(Visitor visitor)
        {
            return (visitor.Age >= 12 && visitor.TicketBought);
        }

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

        #region Full Algorithm

        public void PlaceGroups()
        {
            foreach (var group in groups)
            {
                groupSections.Clear();
                GetOptimalSectionListForGroup(group);
                if (AllowInGroup(group))
                {
                    PlaceAdultInEachSection(group);
                    foreach (var section in groupSections)
                    {
                        if (!section.SectionFull)
                        {
                            foreach (var visitor in group.visitors)
                            {
                                section.PlaceVisitors(visitor);
                            }

                        }
                    }
                }
            }
        }

        public void PlaceVisitors()
        {
            groupSections.Clear();
            foreach (var visitor in visitors)
            {
                if (AllowIn(visitor))
                {
                    Section optimalSection = SelectOptimalSection(1);
                    optimalSection.CoupleChairAndVisitor(visitor);
                }
            }
        }

        public void FullAlgorithm()
        {
            PlaceGroups();
            PlaceVisitors();
            CountAssignedChairsOccupation();
        }


        #endregion
    }
}


