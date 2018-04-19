using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Forms;
using System.Drawing;

namespace Tablut
{
    /// <summary>
    /// Simulate the board squares. One object equal one square.
    /// This can be clicked and it holds an image.
    /// </summary>
    class Square : PictureBox
    {

        /// <summary>
        /// Defines the content of the square.
        /// </summary>
        public Occupant Occupant { get; private set; }

        /// <summary>
        /// Constructor. Sets all squares as empty.
        /// </summary>
        public Square() : base()
        {
            this.Occupant = Occupant.Empty;
        }

        /// <summary>
        /// Change the background image of the picture box.
        /// The occupant what image is in the square.
        /// Change his state.
        /// </summary>
        /// <param name="m_Occupant">Define what will be inside the square.</param>
        public void Change_Image(Occupant m_Occupant)
        {
            if (m_Occupant == Occupant.King)
            {
                this.BackgroundImage = Tablut.Properties.Resources.white_Pawn_King;
                this.Occupant = Occupant.King;
            }
            else if (m_Occupant == Occupant.Defender)
            {
                this.BackgroundImage = Tablut.Properties.Resources.white_Pawn;
                this.Occupant = Occupant.Defender;
            }
            else if (m_Occupant == Occupant.Attacker)
            {
                this.BackgroundImage = Tablut.Properties.Resources.black_Pawn;
                this.Occupant = Occupant.Attacker;
            }
            else if(m_Occupant == Occupant.Empty)
            {
                this.BackgroundImage = Tablut.Properties.Resources.square_Highlight;
                this.Occupant = Occupant.Empty;
            }
        }

        /// <summary>
        /// Clears the image of the square and sets it as empty.
        /// </summary>
        public void Clear_Image()
        {
            this.BackgroundImage = null;
            this.Occupant = Occupant.Empty;
        }
    }
}
