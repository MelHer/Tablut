using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tablut
{
    /// <summary>
    /// Class defining the players during the game.
    /// </summary>
    class Player
    {
        /// <summary>
        /// Player's name.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Total of moves done by the player.
        /// </summary>
        public int Total_Moves { get; private set; }

        /// <summary>
        /// The number of enemy pawns he eliminated.
        /// </summary>
        public int Total_Enemy_Pawn_Eliminated { get; private set; }

        /// <summary>
        /// The number of owning pawn he still has.
        /// </summary>
        public int Pawn_Left { get; private set; }

        /// <summary>
        /// His role, he can be 'Attacker' or 'Defender'.
        /// </summary>
        public Player_Role Role { get; private set; }

        /// <summary>
        /// Constructor. Sets the player.
        /// </summary>
        /// <param name="m_Name">The player's name.</param>
        /// <param name="m_Pawn_Left">Initial number of pawn (attack = 12, defence = 9).</param>
        /// <param name="m_Role">The role of the player (Attacker or Defender).</param>
        public Player(string m_Name, int m_Pawn_Left, Player_Role m_Role)
        {
            this.Name = m_Name;
            this.Total_Moves = 0;
            this.Total_Enemy_Pawn_Eliminated = 0;
            this.Pawn_Left = m_Pawn_Left;
            this.Role = m_Role;
        }

        /// <summary>
        /// Adds one move to the moves counter of the current player.
        /// </summary>
        public void add_Moves()
        {
            this.Total_Moves++;
        }

        /// <summary>
        /// Adds the number of pawns eliminated (after a move) to the total of enemy pawn eliminated counter.
        /// </summary>
        /// <param name="m_Eliminated_Number">The number of pawn eliminated.</param>
        public void add_Enemy_Pawn_Eliminated(int m_Eliminated_Number)
        {
            this.Total_Enemy_Pawn_Eliminated += m_Eliminated_Number;
        }

        /// <summary>
        /// Removes the number of owning pawn lost (after a move) to the total owning pawn remaining counter.
        /// </summary>
        /// <param name="m_Pawn_Lost">Number of owning pawn lost.</param>
        public void remove_Pawn(int m_Pawn_Lost)
        {
            this.Pawn_Left -= m_Pawn_Lost;
        }

    }
}
