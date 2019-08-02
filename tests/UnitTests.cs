using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using mastermind;

namespace tests
{
    [TestClass]
    public class UnitTests
    {
        [TestMethod]
        public void TestCanSwapElements()
        {
            var list = new List<int>(){ 1, 2, 3, 4 };
            list.Swap(1, 2);
            Assert.IsTrue(3 == list[1] );
            Assert.IsTrue(2 == list[2]);
        }

        [TestMethod]
        public void TestCanRandomizeElements()
        {
            var list = new List<int>() { 1, 2, 3, 4 };
           
            for (int i = 0; i < 5; ++i)
            {
                var copy = new List<int>(list);
                bool equal = true;
                // This test isn't full proof as it is possible
                // to get the same random result 10 times in a row,
                // but it's good enough for the purpose of this program.
                for (int j = 0; j < 10 && equal; ++j)
                {
                    copy.OrderRandom();
                    
                    string result = copy.Select(x => x.ToString()).Aggregate((a, b) => a + b);
                    Console.WriteLine($"Result: {result}");
                    equal = list.SequenceEqual(copy);
                }

                Assert.IsFalse(equal);
            }
        }

        [TestMethod]
        public void TestCanGenerateGameNumber()
        {
            var gameNumber = new Game(null, 1, 6, 4, 10);
            string value = gameNumber.Value;
            Console.WriteLine($"Game Number: {value}");
            Assert.IsTrue(4 == value.Length);
            Assert.IsTrue(Regex.Match(value, "^[1-6]+$").Success);
        }

        [TestMethod]
        public void TestGoodUserInput()
        {
            var gameNumber = new Game(null, 1, 6, 4, 10);
            string errMessage;
            Assert.IsTrue(gameNumber.ValidateInput("2451", out errMessage));
            Assert.IsTrue(gameNumber.ValidateInput("3415", out errMessage));
        }


        [TestMethod]
        public void TestBadUserInputTooLong()
        {
            var gameNumber = new Game(null, 1, 6, 4, 10);
            string errMessage;
            Assert.IsFalse(gameNumber.ValidateInput("24516", out errMessage));
        }


        [TestMethod]
        public void TestBadUserInputTooShort()
        {
            var gameNumber = new Game(null, 1, 6, 4, 10);
            string errMessage;
            Assert.IsFalse(gameNumber.ValidateInput("245", out errMessage));
            Assert.IsFalse(gameNumber.ValidateInput("2457", out errMessage));
        }

        [TestMethod]
        public void TestBadUserInputInvalidRange()
        {
            var gameNumber = new Game(null, 1, 6, 4, 10);
            string errMessage;
            Assert.IsFalse(gameNumber.ValidateInput("2457", out errMessage));
        }

        [TestMethod]
        public void TestPlayGame2314_User1234()
        {
            var game = new Game(new List<int>() {2, 3, 1, 4});
            var result = game.Try("1234", out string hint);
            Console.WriteLine($"Result: {hint}");
            Assert.IsTrue("---+" == hint);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TestPlayGame2314_User1534()
        {
            var game = new Game(new List<int>() { 2, 3, 1, 4 });
            var result = game.Try("1534", out string hint);
            Console.WriteLine($"Result: {hint}");
            Assert.IsTrue("- -+" == hint);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TestPlayGame2314_User2534()
        {
            var game = new Game(new List<int>() { 2, 3, 1, 4 });
            var result = game.Try("2534", out string hint);
            Console.WriteLine($"Result: {hint}");
            Assert.IsTrue("+ -+" == hint);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TestPlayGame2314_User2536()
        {
            var game = new Game(new List<int>() { 2, 3, 1, 4 });
            var result = game.Try("2536", out string hint);
            Console.WriteLine($"Result: {hint}");
            Assert.IsTrue("+ - " == hint);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TestPlayGame6135_User1232()
        {
            var game = new Game(new List<int>() { 6, 1, 3, 5 });
            var result = game.Try("1232", out string hint);
            Console.WriteLine($"Result: {hint}");
            Assert.IsTrue("- + " == hint);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TestPlayGame6135_User3452()
        {
            var game = new Game(new List<int>() { 6, 1, 3, 5 });
            var result = game.Try("3452", out string hint);
            Console.WriteLine($"Result: {hint}");
            Assert.IsTrue("- - " == hint);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TestPlayGame2314_User2314()
        {
            var game = new Game(new List<int>() { 2, 3, 1, 4 });
            var result = game.Try("2314", out string hint);
            Console.WriteLine($"Result: {hint}");
            Assert.IsTrue("++++" == hint);
            Assert.IsTrue(result);
        }



    }
}
