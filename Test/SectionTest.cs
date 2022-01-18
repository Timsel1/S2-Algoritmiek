using Microsoft.VisualStudio.TestTools.UnitTesting;
using Logic;

namespace Test
{
    [TestClass]
    public class SectionTest
    {
        [TestMethod]
        public void ChairsCanBeAddedToList()
        {
            //arrange
            Section section = new Section("A", 2, 5);

            //act
            section.AddChairsToList(2, 5);

            //assert
            Assert.IsNotNull(section.chairs);
            Assert.AreEqual(section.Name, "A");
            Assert.AreEqual(section.chairs.Count, 10);
        }

        [TestMethod]
        public void AbleToCountUnoccupiedChairs()
        {
            //Arrange
            Section section = new Section("A", 1, 3);
            section.chairs[1].SetChairOccupied();

            //Act
            section.CountUnoccupiedChairs();

            //Assert
            Assert.AreEqual(section.CountUnoccupiedChairs(), 2);
        }
    }
}
