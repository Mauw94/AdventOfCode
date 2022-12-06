namespace AoC2022.Tests
{
    [TestClass]
    public class Tests
    {
        [TestMethod]
        public void Day1()
        {
            // arrange
            var day1 = new Day1(1, 2022);

            // act
            var solution1 = day1.SolvePart1();
            var solution2 = day1.SolvePart2();

            // assert
            Assert.AreEqual(24000, solution1);
            Assert.AreEqual(45000, solution2);
        }

        [TestMethod]
        public void Day2()
        {
            // arrange
            var day2 = new Day2(2, 2022);

            // act
            var solution1 = day2.SolvePart1();
            var solution2 = day2.SolvePart2();

            // assert
            Assert.AreEqual(15, solution1);
            Assert.AreEqual(12, solution2);
        }

        [TestMethod]
        public void Day3()
        {
            // arrange
            var day3 = new Day3(3, 2022);

            // act
            var solution1 = day3.SolvePart1();
            var solution2 = day3.SolvePart2();

            // assert
            Assert.AreEqual(157, solution1);
            Assert.AreEqual(70, solution2);
        }

        [TestMethod]
        public void Day4()
        {
            // arrange
            var day4 = new Day4(4, 2022);

            // act
            var sol1 = day4.SolvePart1();
            var sol2 = day4.SolvePart2();

            // assert
            Assert.AreEqual(2, sol1);
            Assert.AreEqual(4, sol2);
        }

        [TestMethod]
        public void Day5()
        {
            // arrange
            var day5 = new Day5(5, 2022);

            // act
            var sol1 = day5.SolvePart1();
            var sol2 = day5.SolvePart2();

            // assert
            Assert.AreEqual("CMZ", sol1);
            Assert.AreEqual("MCD", sol2);
        }

        [TestMethod]
        public void Day6()
        {
            // arrange
            var day6 = new Day6(6, 2022);

            // act
            var sol1 = day6.SolvePart1();
            var sol2 = day6.SolvePart2();

            // assert
            Assert.AreEqual(11, sol1);
            Assert.AreEqual(26, sol2);
        }
    }
}