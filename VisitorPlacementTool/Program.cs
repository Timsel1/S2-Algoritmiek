using System;
using Logic;

namespace VisitorPlacementTool
{
    class Program
    {
        static void Main(string[] args)
        {
            Stand stand = new Stand();
            Section seection = new Section();
            
            stand.AddSectionsToList(5);
            stand.MakeStandLayout();
            foreach (var item in stand.standLayout)
            {
                Console.WriteLine(item);
            }

            Console.ReadLine();

        }
    }
}
