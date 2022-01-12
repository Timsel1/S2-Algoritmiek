using System;
using Logic;

namespace VisitorPlacementTool
{
    class Program
    {
        static void Main(string[] args)
        {
            EventLocation eventLocation = new EventLocation(2021, 2, 20, 2021, 1, 20);
            eventLocation.MakeSection("A", 1, 4);
            eventLocation.MakeSection("B", 1, 3);
            eventLocation.MakeSection("C", 1, 3);
            eventLocation.MakeSection("D", 2, 3);
            eventLocation.MakeGroup(0, 9, 2003, 2, 8, "nup", 2020, 9, 9);
            eventLocation.MakeGroup(1, 2, 2002, 9, 8, "yup", 2020, 9, 9);
            eventLocation.MakeVisitorList(2002, 9, 8, "Tim", 2020, 9, 9);
            eventLocation.GetBigGroupSectionList(eventLocation.groups[0]);
            eventLocation.PlaceGroups();
            eventLocation.PlaceVisitors();
            foreach (var item in eventLocation.groupSections)
            {
                Console.WriteLine(item);
            }
            foreach (var item in eventLocation.sections)
            {
                foreach (var item2 in item.chairs)
                {
                    Console.WriteLine(item2);
                }
            }
            Console.ReadLine();
        }
    }
}
