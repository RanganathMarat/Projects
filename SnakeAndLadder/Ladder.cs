using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeAndLadder
{
    /// <summary>
    /// This class represents the Ladder in the Snake and Ladder game.
    /// The ladder is characterized by its position in the Game grid in terms
    /// of where its lowest rung is and where its highes rung is.
    /// </summary>
    public class Ladder
    {
        #region Private members
        private int _lowestRung;
        private int _highestRung;
        #endregion // Private members
        public Ladder (int lowestRung, int highestRung)
        {
            _lowestRung = lowestRung;
            _highestRung = highestRung;
        }
        /// <summary>
        /// A ladder escalates a Player by changing the grid position
        /// to where its highest rung is.
        /// </summary>
        /// <param name="player"></param><see cref="Player"/> 
        public void Escalate(IPlayer player)
        {
            player.GridPosition = _highestRung;
        }
    }
}
