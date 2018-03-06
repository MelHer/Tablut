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
    public partial class frm_Error : Form
    {
        //Player for sound event
        System.Media.SoundPlayer player;

        public frm_Error(string m_Title, string m_Message)
        {
            InitializeComponent();

            player = new System.Media.SoundPlayer();

            lbl_Error_Title.Text = m_Title;
            lbl_Error_Message.Text = m_Message;

            //Sound cues
            pic_Error_Confirm.MouseEnter += new System.EventHandler(this.play_Sound_Enter);
        }

        /// <summary>
        /// Confirm that the player red the error message and close the message pop up.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pic_Error_Confirm_Click(object sender, EventArgs e)
        {
            play_Sound_Click();

            DialogResult = DialogResult.OK;
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
