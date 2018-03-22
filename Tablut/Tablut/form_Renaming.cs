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
    /// <summary>
    /// Form raised to rename a profile.
    /// </summary>
    public partial class frm_Renaming : Form
    {
        /// <summary>
        /// Sound player used when click on button(picture box).
        /// </summary>
        System.Media.SoundPlayer sound_Player; 

        /// <summary>
        /// Connection with database. 
        /// </summary>
        DB_Connect db_link; 

        /// <summary>
        /// Store the name of the current profile we are renaming. 
        /// </summary>
        string current_Name;

        /// <summary>
        /// The profile's new name after validation. 
        /// </summary>
        public string validated_New_Name{ get; private set; }

        /// <summary>
        /// Constructor. Instantiates the sound player and the even handlers.
        /// Create the database connection object.
        /// </summary>
        /// <param name="m_Current_Name"> The old name of the profile. </param>
        public frm_Renaming(string m_Current_Name)
        {
            InitializeComponent();

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
        /// <param name="sender">The button "Rename".</param>
        /// <param name="e">Contains informations about the raised event.</param>
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
                if (ex.Number == 1042 | ex.Number == 0)
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
        /// <param name="sender">The button "Cancel".</param>
        /// <param name="e">Contains informations about the raised event.</param>
        private void pic_Renaming_Cancel_Click(object sender, EventArgs e)
        {
            play_Sound_Click();

            DialogResult = DialogResult.Cancel;
        }

        /// <summary>
        /// Gets the message in parameter and print it on the screen to warn user
        /// that the renaming of the profile failed.
        /// </summary>
        /// <param name="m_Message"> The message of the error displayed to the user.</param>
        private void display_Error_Renaming(string m_Message)
        {
            lbl_Renaming_Fail.Text = m_Message;
            lbl_Renaming_Fail.Visible = true;
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
        /// <param name="e">Contains informations about the raised event.</param>
        private void play_Sound_Enter(object sender, EventArgs e)
        {
            sound_Player = new System.Media.SoundPlayer(Tablut.Properties.Resources.Menu_Move);
            sound_Player.Play();
        }
        #endregion Sound_Players

    }
}
