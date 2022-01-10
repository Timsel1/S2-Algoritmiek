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
            //eventLocation.MakeSection("D", 1, 3);
            eventLocation.MakeGroup(0, 6, 2003, 9, 8, "nup", 2020, 9, 9);
            eventLocation.MakeVisitorList(2003, 2, 2, "tim", 2020, 9, 9);
            eventLocation.GetBigGroupSectionList(eventLocation.groups[0]);
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
