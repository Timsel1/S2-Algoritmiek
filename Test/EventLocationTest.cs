using Microsoft.VisualStudio.TestTools.UnitTesting;
using Logic;

namespace Test
{
    [TestClass]
    public class EventLocationTest
    {
        public EventLocation eventLocation = new EventLocation(2021, 12, 9, 2021, 11, 20);
        Visitor visitor = new Visitor("Pim", 2001, 3, 8, 2021, 11, 10);
       
        [TestMethod]
        public void SectionCanBeMadeAndAddedToAList()
        {
            //Arrange

            //Act
            eventLocation.MakeSectionList("A", 3, 5);

            //Assert
            Assert.AreEqual(eventLocation.sections.Count, 1 );
        }

        [TestMethod]
        public void NotEveryoneWillBeAllowedIN()
        {
            //Arrange
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
            eventLocation.MakeSectionList("A", 1, 3);
            eventLocation.MakeVisitorList("Tim", 2003, 2, 8,  2021, 1, 1);
            //Act
            eventLocation.PlaceVisitors();
            //Assert
            Assert.AreEqual(eventLocation.sections[0].chairs[0].visitor, eventLocation.visitors[0]);
        }

        [TestMethod]
        public void OptimalSectionCanBePickedForGroup()
        {
            //Arrange
            eventLocation.MakeSectionList("A", 2, 5);
            eventLocation.MakeSectionList("B", 1, 5);
            eventLocation.MakeSectionList("C", 3, 3);
            eventLocation.MakeSectionList("D", 2, 7);
            eventLocation.MakeSectionList("E", 1, 6);
            eventLocation.MakeGroup(0, 3, 5);

            //Act
            Section section = eventLocation.SelectOptimalSection(eventLocation.groups[0].visitors.Count);

            //Assert
            Assert.AreEqual(section, eventLocation.sections[2]);
        }
    }
}
