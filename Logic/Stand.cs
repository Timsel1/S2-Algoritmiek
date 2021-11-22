using System;
using System.Collections.Generic;
using System.Text;

namespace Logic
{
    public class Stand
    {
        public List<Section> sections = new List<Section>();
        public List<string> standLayout = new List<string>();
        
        private int i = 0;

        public enum SectionLetter
        {
            a,
            b,
            c,
            d,
            e
        }

        public void AddSectionsToList(int amountOfSections)
        {
            for (int i = 0; i < amountOfSections; i++)
            {
                sections.Add(new Section());
            }
        }

        public List<string> MakeStandLayout()
        {
            foreach (var section in sections)
            {
                standLayout.Add("Section " + (SectionLetter)i + " has " + sections[i].ToString() + " rows");
                i++;
            }
            return standLayout;
        }

        



    }
}
