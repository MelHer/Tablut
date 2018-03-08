using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Forms;
using System.Drawing;

namespace Tablut
{
    class Square : PictureBox
    {

        public Occupant Occupant { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="m_Occupant"></param>
        public Square(Occupant m_Occupant = Occupant.Empty) : base()
        {
            Occupant = m_Occupant;
        }

        /// <summary>
        /// Change the background image of the picture box.
        /// We do it this way because we can work with the background image path
        /// to determine the occupant of the square
        /// </summary>
        /// <param name="path"></param>
        public void change_Image(string m_path)
        {
            this.BackgroundImage = Image.FromFile(m_path);

            if (m_path.Contains("Roi"))
            {
                this.Occupant = Occupant.King;
            }
            else if (m_path.Contains("Blanc"))
            {
                this.Occupant = Occupant.Defender;
            }
            else if (m_path.Contains("Noir"))
            {
                this.Occupant = Occupant.Attacker;
            }
            else
            {
                this.Occupant = Occupant.Empty;
            }
        }

        public void clear_Image()
        {
            this.BackgroundImage = null;
            this.Occupant = Occupant.Empty;
        }
    }
}
