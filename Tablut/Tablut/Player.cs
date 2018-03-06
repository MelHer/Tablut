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

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="m_Name"></param>
        public Player(string m_Name, int m_Pawn_Left)
        {
            this.Name = m_Name;
            this.Total_Moves = 0;
            this.Total_Enemy_Pawn_Eliminated = 0;
            this.Pawn_Left = m_Pawn_Left;
        }


    }
}
