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
    public partial class frm_Confirmation : Form
    {
        //Player for sound event
        System.Media.SoundPlayer player;

        public frm_Confirmation(string m_Message)
        {
            InitializeComponent();

            player = new System.Media.SoundPlayer();

            lbl_Confirmation_Message.Text = m_Message;

            //Sound cues
            pic_Confirmation_Confirm.MouseEnter += new System.EventHandler(this.play_Sound_Enter);
            pic_Confirmation_Cancel.MouseEnter += new System.EventHandler(this.play_Sound_Enter);
        }

        /// <summary>
        /// Confirms the previous action from the player
        /// Delete, Reset a profile or leave the game while playing.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pic_Confirmation_Confirm_Click(object sender, EventArgs e)
        {
            play_Sound_Click();
            DialogResult = DialogResult.OK;
        }

        /// <summary>
        /// Cancels the previous action from the player
        /// Delete, Reset a profile or leave the game while playing.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pic_Confirmation_Cancel_Click(object sender, EventArgs e)
        {
            play_Sound_Click();
            DialogResult = DialogResult.Cancel;
        }

        #region Sound_Players
        /// <summary>
        /// Play the click sound for each button.
        /// Called in the click events.
        /// </summary>
        private void play_Sound_Click()
        {
            player.SoundLocation = (@"P:\Tablut\Design\Son\Menu_Click.wav");
            player.Play();
        }

        /// <summary>
        /// Play a sound when the mouse enters the controls.
        /// Applies to the button of the menus.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void play_Sound_Enter(object sender, EventArgs e)
        {
            player.SoundLocation = (@"P:\Tablut\Design\Son\Menu_Move.wav");
            player.Play();
        }
        #endregion Sound_Players
    }
}
