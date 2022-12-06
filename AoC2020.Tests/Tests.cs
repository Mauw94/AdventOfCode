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

        [TestMethod]
        public void Day2()
        {
            // arrange
            var day2 = new Day2(2, 2020);

            // act
            var sol1 = day2.SolvePart1();
            var sol2 = day2.SolvePart2();

            // assert
            Assert.AreEqual(2, sol1);
            Assert.AreEqual(1, sol2);
        }

        [TestMethod]
        public void Day3()
        {
            // arrange
            var day3 = new Day3(3, 2020);

            // act
            var sol1 = day3.SolvePart1();
            var sol2 = day3.SolvePart2();

            // assert
            Assert.AreEqual(7, sol1);
            Assert.AreEqual(336, sol2);
        }
    }
}