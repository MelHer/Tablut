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
    /// Form shown when the user has to confirm an important action
    /// like deleting, renaming, reseting a profile or leaving a game while playing.
    /// </summary>
    public partial class frm_Confirmation : Form
    {
        /// <summary>
        /// Sound player used when a button is clicked or overriden (picture box).
        /// </summary>
        private System.Media.SoundPlayer sound_Player;

        /// <summary>
        /// Constructor. Displays the confirmation message and instantiates
        /// the event handlers.
        /// </summary>
        /// <param name="m_Message"></param>
        public frm_Confirmation(string m_Message)
        {
            InitializeComponent();

            lbl_Confirmation_Message.Text = m_Message;

            //Sound cues
            pic_Confirmation_Confirm.MouseEnter += new System.EventHandler(this.play_Sound_Enter);
            pic_Confirmation_Cancel.MouseEnter += new System.EventHandler(this.play_Sound_Enter);
        }

        /// <summary>
        /// Confirms the previous action of the player.
        /// </summary>
        /// <param name="sender">The "Confirmation" button.</param>
        /// <param name="e">Contains informations about the raised "click" event.</param>
        private void pic_Confirmation_Confirm_Click(object sender, EventArgs e)
        {
            play_Sound_Click();
            DialogResult = DialogResult.OK;
        }

        /// <summary>
        /// Cancels the previous action of the player.
        /// </summary>
        /// <param name="sender">The "Cancel" button.</param>
        /// <param name="e">Contains informations about the raised "click" event.</param>
        private void pic_Confirmation_Cancel_Click(object sender, EventArgs e)
        {
            play_Sound_Click();
            DialogResult = DialogResult.Cancel;
        }

        #region Sound_Players
        /// <summary>
        /// Instantiates the sound player.
        /// Plays the click sound for each button clicked.
        /// </summary>
        private void play_Sound_Click()
        {
            sound_Player = new System.Media.SoundPlayer(Tablut.Properties.Resources.Menu_Click);
            sound_Player.Play();
        }

        /// <summary>
        /// Instantiates the sound player.
        /// Plays a sound when the mouse enters a button.
        /// </summary>
        /// <param name="sender">The button that called the function.</param>
        /// <param name="e">Contains informations about the raised "mouse enter" event.</param>
        private void play_Sound_Enter(object sender, EventArgs e)
        {
            sound_Player = new System.Media.SoundPlayer(Tablut.Properties.Resources.Menu_Move);
            sound_Player.Play();
        }
        #endregion Sound_Players
    }
}
