using System;
using Logic;

namespace VisitorPlacementTool
{
    class Program
    {
        static void Main(string[] args)
        {
            EventLocation eventLocation = new EventLocation(2021, 2, 20, 2021, 1, 20);
            eventLocation.MakeSection("A", 1, 3);
            eventLocation.MakeVisitorList(2002, 9, 9, "jan", 2021, 1, 1);
            eventLocation.MakeVisitorList(2002, 9, 9, "jap", 2021, 2, 1);
            eventLocation.MakeVisitorList(2006, 9, 9, "jak", 2021, 1, 13);
            eventLocation.MakeVisitorList(2002, 9, 9, "jal", 2021, 1, 20);
            eventLocation.MakeVisitorList(2002, 9, 9, "jab", 2021, 1, 18);
            eventLocation.MakeSection("4", 300, 8000);
            eventLocation.ClassifyVisitors();
            foreach (var visitor in eventLocation.sections)
            {
                Console.WriteLine(visitor.chairs.Count);
            }
            Console.ReadLine();
        }
    }
}
