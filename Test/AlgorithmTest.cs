using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Logic;

namespace Test
{
    [TestClass]
    public class AlgorithmTest
    {


        [TestMethod]
        public void TestFullAlgortihm()
        {
            //Arrange
            EventLocation eventLocation = new EventLocation(2021, 2, 20, 2021, 1, 20);
            
            eventLocation.MakeSection("A", 1, 4);
            eventLocation.MakeSection("B", 1, 3);
            eventLocation.MakeSection("C", 1, 3);
            eventLocation.MakeSection("D", 2, 3);

            eventLocation.MakeGroup(0, 9, 2003, 2, 8, "nup", 2020, 9, 9);
            eventLocation.MakeGroup(1, 2, 2002, 9, 8, "yup", 2020, 9, 9);
            
            eventLocation.MakeVisitorList(2002, 9, 8, "Tim", 2020, 9, 9);

            

            //Act
            eventLocation.FullAlgorithm();

            //Assert
            Assert.AreEqual(4, eventLocation.sections[0].chairs.Count);
            Assert.AreEqual(4, eventLocation.sections[0].UnoccupiedChairs);

            Assert.AreEqual(3, eventLocation.sections[1].chairs.Count);
            Assert.AreEqual(0, eventLocation.sections[1].UnoccupiedChairs);

            Assert.AreEqual(3, eventLocation.sections[2].chairs.Count);
            Assert.AreEqual(0, eventLocation.sections[2].UnoccupiedChairs);

            Assert.AreEqual(6, eventLocation.sections[3].chairs.Count);
            Assert.AreEqual(0, eventLocation.sections[3].UnoccupiedChairs);

            Assert.AreEqual(4, eventLocation.sections.Count);

            foreach (var visitor in eventLocation.visitors)
            {
                Assert.AreEqual(true, eventLocation.AllowIn(visitor));
                Assert.AreEqual(true, visitor.HasChair);
            }
            Assert.AreEqual(1, eventLocation.visitors.Count);
        }


    }

}

