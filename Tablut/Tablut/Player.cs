using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tablut
{
    class Player
    {
        public string Name { get; private set; }

        public int Total_Moves { get; private set; }

        public int Total_Enemy_Pawn_Eliminated { get; private set; }

        public int Pawn_Left { get; private set; }

        public Occupant role { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="m_Name"></param>
        public Player(string m_Name, int m_Pawn_Left, Occupant m_Role)
        {
            this.Name = m_Name;
            this.Total_Moves = 0;
            this.Total_Enemy_Pawn_Eliminated = 0;
            this.Pawn_Left = m_Pawn_Left;
            this.role = m_Role;
        }

        /// <summary>
        /// Adds one move to the moves counter of the current player
        /// </summary>
        public void add_Moves()
        {
            this.Total_Moves++;
        }

        /// <summary>
        /// Adds one to the total count of enemy pawn eliminated
        /// </summary>
        /// <param name="m_Eliminated_Number"></param>
        public void add_Enemy_Pawn_Eliminated(int m_Eliminated_Number)
        {
            this.Total_Enemy_Pawn_Eliminated+= m_Eliminated_Number;
        }

        /// <summary>
        /// Removes one to the total owning pawn remaining counter
        /// </summary>
        /// <param name="m_Pawn_Lost"></param>
        public void remove_Pawn(int m_Pawn_Lost)
        {
            this.Pawn_Left-= m_Pawn_Lost;
        }

    }
}
