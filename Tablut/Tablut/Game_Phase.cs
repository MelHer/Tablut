using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tablut
{
    /// <summary>
    /// Enumeration used to define what is the player doing.
    /// He player can be 'picking' a pawn or 'moving' a selected one.
    /// </summary>
    enum Game_Phase
    {
        picking,
        moving
    }
}
