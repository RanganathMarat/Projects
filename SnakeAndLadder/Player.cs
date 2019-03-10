using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeAndLadder
{
    public interface IPlayer
    {
        int GridPosition { set; get; }
    }

    /// <summary>
    /// This class represents the Player of the Snake and Ladder game.
    /// Player is characterized by his position in the game Grid.
    /// </summary>
    public class Player : IPlayer
    {
        /// <summary>
        /// Talks about the position of the player on the Snake and Ladder grid.
        /// </summary>
        public virtual int GridPosition { get; set; }
    }
}
