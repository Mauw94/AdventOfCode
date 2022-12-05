namespace AoC2020.Tests
{
    [TestClass]
    public class Tests
    {
        [TestMethod]
        public void Day1()
        {
            // arrange
            var day1 = new Day1(1, 2020);

            // act
            var sol1 = day1.SolvePart1();
            var sol2 = day1.SolvePart2();

            // assert
            Assert.AreEqual(514579, sol1);
            Assert.AreEqual(241861950, sol2);
        }
    }
}