using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using MySql.Data.MySqlClient;

namespace Tablut
{
    public partial class frm_Renaming : Form
    {

        //Player for sound event
        System.Media.SoundPlayer player;

        //Connection with database
        DB_Connect db_link;

        //Store the name of the current profile we are renaming
        string current_Name;

        public string validated_New_Name{ get; private set; }

        public frm_Renaming(string m_Current_Name)
        {
            InitializeComponent();

            player = new System.Media.SoundPlayer();

            db_link = new DB_Connect();

            current_Name = m_Current_Name;

            //Sound cues
            pic_Renaming_Confirm.MouseEnter += new System.EventHandler(this.play_Sound_Enter);
            pic_Renaming_Cancel.MouseEnter += new System.EventHandler(this.play_Sound_Enter);
        }

        /// <summary>
        /// Confirm the renaming.
        /// After checking if the name is correct, submits the new name to the database.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pic_Renaming_Confirm_Click(object sender, EventArgs e)
        {
            play_Sound_Click();

            try
            {
                db_link.Rename_Profile(txt_Renaming_Profile_Name.Text, current_Name);

                validated_New_Name = txt_Renaming_Profile_Name.Text;

                DialogResult = DialogResult.OK;
            }
            catch (Exception_Invalid_Name ex)
            {
                display_Error_Renaming(ex.Message);
            }
            catch (MySqlException ex)
            {
                if (ex.Number == 1042)
                {
                    display_Error_Renaming("Connexion à la base de données impossible.");
                }
                else if (ex.Number == 1062)
                {
                    display_Error_Renaming("Profil avec un nom similaire déjà existant.");
                }

            }
        }

        /// <summary>
        /// Cancels the renaming.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pic_Renaming_Cancel_Click(object sender, EventArgs e)
        {
            play_Sound_Click();

            DialogResult = DialogResult.Cancel;
        }

        /// <summary>
        /// Gets the message in parameter and print it on the screen to warn user
        /// that the renaming of the profile failed.
        /// </summary>
        /// <param name="m_Message"></param>
        private void display_Error_Renaming(string m_Message)
        {
            lbl_Fail_Renaming.Text = m_Message;
            lbl_Fail_Renaming.Visible = true;
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
