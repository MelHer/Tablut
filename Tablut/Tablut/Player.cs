using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tablut
{
    /// <summary>
    /// Defines the players during a game.
    /// </summary>
    class Player
    {
        public string Name { get; private set; } /*!< Player's name */

        public int Total_Moves { get; private set; } /*!< Total moves done by the player */

        public int Total_Enemy_Pawn_Eliminated { get; private set; } /*!< The number of enemy pawns eliminated */

        public int Pawn_Left { get; private set; } /*!< The number of owning pawn remaining */

        public Player_Role Role { get; private set; } /*!< The player role, attacker or defender */

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="m_Name">The player's name</param>
        /// <param name="m_Pawn_Left">Initial number of pawn (attack = 12, defence = 9)</param>
        /// <param name="m_Role">The role of the player (attacker or defender)</param>
        public Player(string m_Name, int m_Pawn_Left, Player_Role m_Role)
        {
            this.Name = m_Name;
            this.Total_Moves = 0;
            this.Total_Enemy_Pawn_Eliminated = 0;
            this.Pawn_Left = m_Pawn_Left;
            this.Role = m_Role;
        }

        /// <summary>
        /// Adds one move to the moves counter of the current player
        /// </summary>
        public void add_Moves()
        {
            this.Total_Moves++;
        }

        /// <summary>
        /// Adds the number of pawn eliminated (after a move) to the total of enemy pawn counter eliminated
        /// </summary>
        /// <param name="m_Eliminated_Number">The number of pawn eliminated</param>
        public void add_Enemy_Pawn_Eliminated(int m_Eliminated_Number)
        {
            this.Total_Enemy_Pawn_Eliminated += m_Eliminated_Number;
        }

        /// <summary>
        /// Removes the number of owning pawn lost (after a move) to the total owning pawn remaining counter
        /// </summary>
        /// <param name="m_Pawn_Lost">Number of owning pawn lost</param>
        public void remove_Pawn(int m_Pawn_Lost)
        {
            this.Pawn_Left -= m_Pawn_Lost;
        }

    }
}
