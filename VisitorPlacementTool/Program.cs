using System;
using Logic;

namespace VisitorPlacementTool
{
    class Program
    {
        static void Main(string[] args)
        {
            EventLocation eventLocation = new EventLocation(2021, 2, 20, 2021, 1, 20);
            eventLocation.MakeSection("A", 2, 3);
            eventLocation.MakeSection("B", 1, 10);
            eventLocation.MakeSection("C", 2, 9);
            eventLocation.MakeSection("D", 1, 3);
            eventLocation.MakeGroup(0, 30, 2000, 4, 7, "Nop", 2019, 9, 8);
            eventLocation.GetBigGroupSections(eventLocation.groups[0]);
            foreach (var item in eventLocation.groupSections)
            {
                Console.WriteLine(item.Name);
            }
            Console.ReadLine();
        }
    }
}
