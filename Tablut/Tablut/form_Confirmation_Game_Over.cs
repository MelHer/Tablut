using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Tablut
{
    /// <summary>
    /// Form raised at the end of a game before the statistics screen
    /// to let the players see the final board state before exiting.
    /// </summary>
    public partial class frm_Confirmation_Game_Over : Form
    {
        /// <summary>
        /// Constructor. Sets the form message.
        /// </summary>
        /// <param name="m_Message">Message saying who is the winner.</param>
        public frm_Confirmation_Game_Over(string m_Message)
        {
            InitializeComponent();

            this.lbl_Confirmation_Game_Over_Message.Text = m_Message;
        }

        /// <summary>
        /// Confirms leaving the game.
        /// </summary>
        /// <param name="sender">The "finish" button.</param>
        /// <param name="e">Contains informations about the raised event.</param>
        private void pic_Confirmation_Game_Over_Finish_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }
    }
}
