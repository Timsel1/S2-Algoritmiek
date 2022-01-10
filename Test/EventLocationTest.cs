using Microsoft.VisualStudio.TestTools.UnitTesting;
using Logic;

namespace Test
{
    [TestClass]
    public class EventLocationTest
    {
        public EventLocation eventLocation = new EventLocation(2021, 12, 9, 2021, 11, 20);

        [TestMethod]
        public void AgeCanBeCalculated()
        {
            //arrange
            Visitor visitor = new Visitor(2001, 3, 8, 2021, 11, 10);
            eventLocation.visitors.Add(visitor);

            //act
            eventLocation.CheckVisitorAge(visitor);

            //assert
            Assert.AreEqual(eventLocation.CheckVisitorAge(visitor), 20);
        }

        [TestMethod]
        public void CanCalculteIfTicketWasBoughtInTime()
        {
            //Arrange
            Visitor visitor = new Visitor(2001, 3, 8, 2021, 12, 10);

            //Act
            eventLocation.CheckTicketBoughtInTime(visitor);

            //Assert
            Assert.AreEqual(eventLocation.CheckTicketBoughtInTime(visitor), false);
        }

        [TestMethod]
        public void Cools()
        {
            eventLocation.MakeSection("4", 3, 8000);

            Assert.AreEqual(eventLocation.sections.Count, 1);
        }

        [TestMethod]
        public void SectionCanBeMadeAndAddedToAList()
        {
            //Arrange

            //Act
            eventLocation.MakeSection("A", 3, 5);

            //Assert
            Assert.AreEqual(eventLocation.sections.Count, 1 );
        }

        [TestMethod]
        public void NotEveryoneWillBeAllowedIN()
        {
            //Arrange
            Visitor visitor = new Visitor(18, "Tim", false, null);
            bool allow;
            //Act
            allow = eventLocation.AllowIn(visitor);
            //Assert
            Assert.AreEqual(allow, false);
        }

        [TestMethod]
        public void IndividualsCanGetAChair()
        {
            //Arrange
            eventLocation.MakeSection("A", 1, 3);
            eventLocation.MakeVisitorList(2003, 2, 8, "Tim", 2021, 1, 1);
            //Act
            eventLocation.PlaceVisitors();
            //Assert
            Assert.IsNotNull(eventLocation.visitors[0].visitorChair);
        }

        [TestMethod]
        public void OptimalSectionCanBePickedForGroup()
        {
            //Arrange
            eventLocation.MakeSection("A", 2, 5);
            eventLocation.MakeSection("B", 1, 5);
            eventLocation.MakeSection("C", 3, 3);
            eventLocation.MakeSection("D", 2, 7);
            eventLocation.MakeSection("E", 1, 6);
            eventLocation.MakeGroup(0, 14, 2000, 1, 1, "Jan", 2020, 1, 1);

            //Act
            eventLocation.SelectBestSectionForGroup(0);

            //Assert
            Assert.AreEqual(eventLocation.SelectBestSectionForGroup(0), eventLocation.sections[3]);
        }
    }
}
