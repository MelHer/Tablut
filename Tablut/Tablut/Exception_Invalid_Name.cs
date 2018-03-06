using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tablut
{
    class Exception_Invalid_Name : Exception
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message"></param>
        public Exception_Invalid_Name(string message) : base(message)
        {}
    }
}
