using System;

namespace Logic
{
    public class Section
    {
        public string chair { get; private set; }
        public int amounOfChairs { get; private set; }
        private static readonly Random rnd = new Random();

        public Section()
        {
            
        }

        public int GenerateAmountOfChairs(int min, int max)
        {
            amounOfChairs = rnd.Next(min, max);
            return amounOfChairs;
        }



    }
}
