using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tablut
{
    class Exception_Invalid_Pawn : Exception
    {
         public Exception_Invalid_Pawn(string message) : base(message)
        {}
    }
}
