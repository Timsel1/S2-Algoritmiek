﻿using System;
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

        public Group(int groupId, int size, List<Visitor> groupVisitors)
        {
            this.visitors = groupVisitors;
            this.Size = size;
            this.GroupId = groupId;
        }

        public bool GroupHasAdult()
        {
            foreach (var groupMember in visitors)
            {
                if (groupMember.Age >= 18)
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
                if (groupMember.Age > 17)
                {
                    AmountOfAdults++;
                }
            }
            return AmountOfAdults;
        }

        public void PlacePeople(Section section)
        {
            foreach (var visitor in visitors)
            {
                if (!section.SectionFull)
                {
                    section.PlaceVisitors(visitor);
                }
                else
                {
                    break;
                }
            }
        }

        //public int CountChairlessVisitors()
        //{
        //    int chairlessVisitors = 0;
        //    foreach (var groupMember in visitors)
        //    {
        //        if (groupMember.visitorChair == null)
        //        {
        //            chairlessVisitors++;
        //        }
        //    }
        //    return chairlessVisitors;
        //}

    }
}
