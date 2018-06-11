using System;
using TechTalk.SpecFlow;
using SnakeAndLadder;
using Rhino.Mocks;

namespace Specs_SpecFlow_SnakeAndLadderAcceptanceTests
{
    [Binding]
    public class SnakeAndLadderFeatureSteps
    {
        private IPlayer mockPlayer = null;
        private int tail;
        [Given]
        public void GivenIHaveThePlayerWhoLandedAtTheSnakeHead()
        {
            tail = 12;
            mockPlayer = MockRepository.GenerateMock<IPlayer>();
            mockPlayer.Expect(player => player.GridPosition = 12);
        }
        
        [When]
        public void WhenTheSnakeBites()
        {
            Snake snake = new Snake(24, tail);
            snake.Bite(mockPlayer);
        }
        
        [Then]
        public void ThenThePlayerPositionsEndsUpToBeTheSnakeTail()
        {
            mockPlayer.VerifyAllExpectations();
        }
    }
}
