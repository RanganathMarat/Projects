using System;
using System.Security.Cryptography.X509Certificates;
using NUnit.Framework;
using TechTalk.SpecFlow;
using SnakeAndLadder;
using Rhino.Mocks;

namespace Specs_SpecFlow_SnakeAndLadderAcceptanceTests
{
    [Binding]
    public class SnakeAndLadderFeatureSteps
    {
        private Player _mockPlayer = null;
        private const int SnakeTail = 12;
        private const int SnakeHead = 24;

        [Given]
        public void GivenIHaveThePlayerWhoLandedAtTheSnakeHead()
        {
            _mockPlayer = MockRepository.GenerateMock<Player>();
            _mockPlayer.Stub(x => x.GridPosition).PropertyBehavior();
            //_mockPlayer.Expect(player => player.GridPosition = SnakeTail);
        }

        [When]
        public void WhenTheSnakeBites()
        {
            Snake snake = new Snake(SnakeHead, SnakeTail);
            _mockPlayer.GridPosition = 24;
            snake.Bite(_mockPlayer);
        }

        [Then]
        public void ThenThePlayerPositionsEndsUpToBeTheSnakeTail()
        {
            Assert.AreEqual(_mockPlayer.GridPosition, SnakeTail);
        }
    }
}
