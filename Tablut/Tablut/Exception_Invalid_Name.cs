using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tablut
{
    class Exception_Invalid_Name : Exception
    {
        public Exception_Invalid_Name(string message) : base(message)
        {}
    }
}
