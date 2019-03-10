using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeAndLadder
{
    /// <summary>
    /// This class represents the snake in the Snake and Ladder game.
    /// A snake is characterized by his poistion in the Grid in terms of where 
    /// its head lies and where its tail lies.    

    /// </summary>
    public class Snake
    {
        #region Private members

        private int _head;
        private int _tail;
        #endregion // Private members

        /// <summary>
        /// Snakes are created against there head and tail positions
        /// </summary>
        /// <param name="head"></param>
        /// <param name="tail"></param>
        public Snake(int head, int tail)
        {
            _head = head;
            _tail = tail;
        }
        /// <summary>
        /// The snakes bite players. As a consequence the position of the player
        /// drops to where the tail of the sna
        /// </summary>
        /// <param name="player"/><see cref="Player"/>
        public void Bite(IPlayer player)
        {
            Debug.Assert(_head == player.GridPosition);
            player.GridPosition = _tail;
        }   

    }
}
