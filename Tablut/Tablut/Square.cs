using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Forms;

namespace Tablut
{
    class Square : PictureBox
    {
        public Occupant occupant { get; private set; }

        public Square(Occupant m_occupant = Occupant.Empty) : base()
        {
            occupant = m_occupant;
        }
    }
}
