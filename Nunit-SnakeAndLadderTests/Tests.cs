using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SnakeAndLadder;
using Rhino.Mocks;

namespace Nunit_SnakeAndLadderTests
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void TestDiceRoll()
        {
            var rollCount = Dice.Roll();
            Assert.IsTrue(rollCount>0 && rollCount<7, "The Roll should always be between 1 and 6.");
        }
        [Test]
        public void TestSnakeBite()
        {
            // Arrange 
            var mockPlayer = MockRepository.GenerateMock<IPlayer>();
            var tail = 12;
            mockPlayer.Expect(player => player.GridPosition = tail);
            // Act
            Snake snake = new Snake(24, tail); // SUT
            snake.Bite(mockPlayer);
            //Assert
            mockPlayer.VerifyAllExpectations();
        }

        [Test]
        public void TestLadderEscalation()
        {
            var mockPlayer = MockRepository.GenerateMock<IPlayer>();
            var highestRung = 20;
            mockPlayer.Expect(player => player.GridPosition = highestRung);
            Ladder ladder = new Ladder(10, highestRung); // SUT
            ladder.Escalate(mockPlayer);
            mockPlayer.VerifyAllExpectations();
        }
    }
}
