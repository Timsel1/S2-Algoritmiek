using System;
using System.Collections.Generic;
using System.Text;

namespace Logic
{
    public class Section
    {
        public List<Chair> chairs = new List<Chair>();
        public string Name { get; private set; }
        public int UnoccupiedChairs { get; private set; }

        public Section(string name, int rows, int chairAmount)
        {
            this.Name = name;
            this.chairs = AddChairsToList(rows, chairAmount);
        }

        public override string ToString()
        {
            return $"{Name}";
        }

        public List<Chair> AddChairsToList(int rows, int chairs)
        {
            List<Chair> sectionChairs = new List<Chair>();
            rows = Clamp(rows, 1, 3);
            for (int i = 0; i < rows; i++)
            {
                chairs = Clamp(chairs, 3, 10);
                for (int j = 0; j < chairs; j++)
                {
                    sectionChairs.Add(new Chair(i, j));
                }
            }
            return sectionChairs;
        }

        private static int Clamp(int value, int min, int max)
        {
            return (value < min) ? min : (value > max) ? max : value;
        }

        public int CountUnoccupiedChairs()
        {
            UnoccupiedChairs = 0;
            foreach (var chair in chairs)
            {
                if (!chair.Occupied)
                {
                    UnoccupiedChairs++;
                }
            }
            return UnoccupiedChairs;
        }

        
    }
}
