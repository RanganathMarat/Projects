using SnakeAndLadder;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SnakeAndLadderTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod()]
        public void RollTest()
        {
            int diceCount = Dice.Roll();
            Assert.IsTrue(diceCount > 0 && diceCount < 7, "Dice count should be between 1 and 6");
        }
    }
}
