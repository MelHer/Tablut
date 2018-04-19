using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tablut
{
    /// <summary>
    /// This exception is returned when the player tries to create or rename
    /// a profile with an invalid syntax (other than: A-Z a-z _ 0-9).
    /// </summary>
    class Exception_Invalid_Name : Exception
    {
        /// <summary>
        /// Create the exception.
        /// </summary>
        /// <param name="message">Message describing the error.</param>
        public Exception_Invalid_Name(string message) : base(message)
        {}
    }
}
