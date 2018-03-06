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

        public Occupant occupant { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="m_occupant"></param>
        public Square(Occupant m_occupant = Occupant.Empty) : base()
        {
            occupant = m_occupant;
        }

        /// <summary>
        /// Change the background image of the picture box.
        /// We do it this way because we can work with the background image path
        /// to determine the occupant of the square
        /// </summary>
        /// <param name="path"></param>
        public void change_image(string m_path)
        {
            this.BackgroundImage = Image.FromFile(m_path);

            if (m_path.Contains("Roi"))
            {
                this.occupant = Occupant.King;
            }
            else if (m_path.Contains("Blanc"))
            {
                this.occupant = Occupant.Defender;
            }
            else if (m_path.Contains("Noir"))
            {
                this.occupant = Occupant.Attacker;
            }
            else
            {
                this.occupant = Occupant.Empty;
            }
        }
    }
}
