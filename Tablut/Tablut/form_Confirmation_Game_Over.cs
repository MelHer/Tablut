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
        /// Sound player used when click on button(picture box).
        /// </summary>
        private System.Media.SoundPlayer sound_Player;

        /// <summary>
        /// Constructor. Sets the form message.
        /// </summary>
        /// <param name="m_Message">Message saying who is the winner.</param>
        public frm_Confirmation_Game_Over(string m_Message)
        {
            InitializeComponent();

            this.lbl_Confirmation_Game_Over_Message.Text = m_Message;

            //Initializing event

            //Sound cues
            pic_Confirmation_Game_Over_Finish.MouseEnter += new System.EventHandler(this.play_Sound_Enter);
        }

        /// <summary>
        /// Confirms leaving the game.
        /// </summary>
        /// <param name="sender">The "finish" button.</param>
        /// <param name="e">Contains informations about the raised "click" event.</param>
        private void pic_Confirmation_Game_Over_Finish_Click(object sender, EventArgs e)
        {
            play_Sound_Click();

            DialogResult = DialogResult.OK;
        }

        #region Sound_Players
        /// <summary>
        /// Plays the click sound for each button.
        /// </summary>
        private void play_Sound_Click()
        {
            sound_Player = new System.Media.SoundPlayer(Tablut.Properties.Resources.Menu_Click);
            sound_Player.Play();
        }

        /// <summary>
        /// Plays a sound when the mouse enters the controls.
        /// It applies only to the buttons.
        /// </summary>
        /// <param name="sender">The control that called the function.</param>
        /// <param name="e">Contains informations about the raised "mouse enter" event.</param>
        private void play_Sound_Enter(object sender, EventArgs e)
        {
            sound_Player = new System.Media.SoundPlayer(Tablut.Properties.Resources.Menu_Move);
            sound_Player.Play();
        }
        #endregion Sound_Player
    }
}
