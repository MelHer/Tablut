using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tablut
{
    class Exception_Game_Error : Exception
    {
        public string Title { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message"></param>
        /// <param name="m_Title"></param>
        public Exception_Game_Error(string message, string m_Title) : base(message)
        {
            this.Title = m_Title;
        }
    }
}
