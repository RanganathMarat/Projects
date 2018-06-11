using System;

namespace SnakeAndLadder
{
    /// <summary>
    /// This call essentially implies the DICE.
    /// </summary>
    public static class Dice
    {
        /// <summary>
        /// This method essentially rolls the dice and returns the outcome of the roll.
        /// The dice roll is always a number between 1 and 6. 
        /// </summary>
        /// <returns></returns>
        public static int Roll()
        {
            return new Random().Next(1, 6);
        }
    }
}
