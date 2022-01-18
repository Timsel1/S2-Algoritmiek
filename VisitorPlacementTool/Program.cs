using System;
using Logic;

namespace VisitorPlacementTool
{
    class Program
    {
        static void Main(string[] args)
        {
            //Arrange
            EventLocation eventLocation = new EventLocation(2021, 2, 20, 2021, 1, 20);
            eventLocation.MakeSectionList("A", 1, 4);
            eventLocation.MakeSectionList("B", 1, 3);
            eventLocation.MakeSectionList("C", 1, 3);
            eventLocation.MakeSectionList("D", 2, 3);
            eventLocation.MakeGroup(0, 2, 7);
            eventLocation.MakeGroup(1, 1, 1);
            eventLocation.MakeVisitorList("Tim", 2002, 9, 8, 2020, 9, 9);
            

            //Act
            eventLocation.FullAlgorithm();
            
            //Console.WriteLine(eventLocation.sections[2].UnoccupiedChairs);
            foreach (var item in eventLocation.sections)
            {
                foreach (var item2 in item.chairs)
                {
                    Console.WriteLine(item2);
                }
                Console.WriteLine("--------------------------------------------------");
            }

            Console.ReadLine();

        }

        
    }
}
