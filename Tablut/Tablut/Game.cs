using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tablut
{
    class Game
    {
        public Player Current_Player { get; private set; }
        public Player Attacker { get; private set; }
        public Player Defender { get; private set; }

        /// <summary>
        /// Constructor.
        /// Creates the players aswell and sets the first player.
        /// The first player is always the defender.
        /// </summary>
        /// <param name="m_Attacker_Name"></param>
        /// <param name="m_Defender_Name"></param>
        public Game(string m_Attacker_Name, string m_Defender_Name)
        {
            this.Attacker = new Player(m_Attacker_Name, 16);
            this.Defender = new Player(m_Defender_Name, 9);
            this.Current_Player = Defender;
        }

        /// <summary>
        /// Checks if the selected pawn is owned by the current player.
        /// </summary>
        /// <param name="m_Square"></param>
        /// <returns></returns>
        public void check_Pawn(Square m_Square)
        {
            
        }
    }
}
