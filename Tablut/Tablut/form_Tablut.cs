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
    public partial class frm_Tablut : Form
    {
        //Player for sound event
        System.Media.SoundPlayer player;

        //Connection with database
        DB_Connect db_link;

        public frm_Tablut()
        {
            InitializeComponent();

            player = new System.Media.SoundPlayer();

            db_link = new DB_Connect();

            //Initializing event

            //Sound cues
            //menu
            pic_Create_Profile.MouseEnter += new System.EventHandler(this.play_Sound_Enter);
            pic_Manage_Profile.MouseEnter += new System.EventHandler(this.play_Sound_Enter);
            pic_Play.MouseEnter += new System.EventHandler(this.play_Sound_Enter);
            pic_Close.MouseEnter += new System.EventHandler(this.play_Sound_Enter);

            //profile creation
            pic_Create.MouseEnter += new System.EventHandler(this.play_Sound_Enter);
            pic_Cancel_Create_Profile.MouseEnter += new System.EventHandler(this.play_Sound_Enter);
        }

        /// <summary>
        /// Close the menu and open the profile creation.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pic_Create_Profile_Click(object sender, EventArgs e)
        {
            play_Sound_Click();

            pnl_Menu.Visible = false;
            pnl_Create_Profile.Visible = true;
        }

        /// <summary>
        /// Close the menu and open the profile managment
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pic_Manage_Profile_Click(object sender, EventArgs e)
        {

        }

        //Close the menu and launch a game
        private void pic_Play_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Exit the application.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pic_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        ////////////////////////////////////
        //      Controls Create Prolfile  //
        ////////////////////////////////////

        /// <summary>
        /// Send a profile creation request to the DB_Connect object.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pic_Create_Click(object sender, EventArgs e)
        {
            play_Sound_Click();

            try
            {
                db_link.Add_Profile(txt_Profile_Name.Text);
                txt_Profile_Name.Text = "";

                lbl_Fail_Create_Profile.Visible = false;
                lbl_Succes_Create_Profile.Visible = true;
            }
            catch(Exception_Invalid_Name ex)
            {
                lbl_Fail_Create_Profile.Text = ex.Message;
                lbl_Succes_Create_Profile.Visible = false;
                lbl_Fail_Create_Profile.Visible = true;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Close the profile creation and return to the menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pic_Cancel_Create_Profile_Click(object sender, EventArgs e)
        {
            play_Sound_Click();

            txt_Profile_Name.Text = "";

            lbl_Fail_Create_Profile.Visible = false; //If visible, will be hidden when re-opening this menu.
            lbl_Succes_Create_Profile.Visible = false;

            pnl_Create_Profile.Visible = false;
            pnl_Menu.Visible = true;
        }

        /// <summary>
        /// Play the click sound for each button.
        /// Called in the click events
        /// </summary>
        private void play_Sound_Click()
        {
            player.SoundLocation = (@"P:\Tablut\Design\SFX\Menu_Click.wav");
            player.Play();
        }

        /// <summary>
        /// Play a sound when the mouse enters the controls.
        /// Applies to the button of the menus
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void play_Sound_Enter(object sender, EventArgs e)
        {
            player.SoundLocation = (@"P:\Tablut\Design\SFX\Menu_Move.wav");
            player.Play();
        }
    }
}
