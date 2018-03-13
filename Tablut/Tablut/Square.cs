using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Forms;
using System.Drawing;

namespace Tablut
{
    /// <summary>
    /// Class herited from PictureBox. Simulate the board square.
    /// This can be clicked and it holds an image.
    /// </summary>
    class Square : PictureBox
    {

        public Occupant Occupant { get; private set; }

        /// <summary>
        /// Constructor. Sets all squares as empty.
        /// </summary>
        /// <param name="m_Occupant"></param>
        public Square(Occupant m_Occupant = Occupant.Empty) : base()
        {
            Occupant = m_Occupant;
        }

        /// <summary>
        /// Change the background image of the picture box.
        /// to determine the occupant of the square and update the property.
        /// </summary>
        /// <param name="m_Occupant">Define what is inside the square</param>
        public void change_Image(Occupant m_Occupant)
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
        /// Clear the image of the square and set it as emply.
        /// </summary>
        public void clear_Image()
        {
            this.BackgroundImage = null;
            this.Occupant = Occupant.Empty;
        }
    }
}
