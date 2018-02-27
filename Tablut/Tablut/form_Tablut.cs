﻿using System;
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

            //profile managment
            pic_Menu.MouseEnter += new System.EventHandler(this.play_Sound_Enter);
            pic_Rename.MouseEnter += new System.EventHandler(this.play_Sound_Enter);
            pic_Reset.MouseEnter += new System.EventHandler(this.play_Sound_Enter);
            pic_Delete.MouseEnter += new System.EventHandler(this.play_Sound_Enter);
        }

        #region Main_Menu
        ////////////////////////////////////
        //            Main Menu           //
        ////////////////////////////////////

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
            play_Sound_Click();

            pnl_Menu.Visible = false;
            pnl_Manage_Profile.Visible = true;

            //Populating the drop down list with profile name
            List<string> profile_Name = new List<String>(db_link.get_Profile_Name());

            foreach (string name in profile_Name)
            {
                cbo_Manage_Profile.Items.Add(name);
            }

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
        #endregion Main_Menu

        #region Profile_Creation
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
                display_Error_Create_Profile(ex.Message);
            }
            catch(Exception ex)
            {
                if(ex.Message == "Unable to connect to any of the specified MySQL hosts.")
                {
                    display_Error_Create_Profile("Connexion à la base de données impossible.");
                }
                else if(ex.Message == "Duplicata du champ '"+txt_Profile_Name.Text+"' pour la clef 'Name_UNIQUE'")
                {
                    display_Error_Create_Profile("Profil avec un nom similaire déjà existant.");
                }

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
        /// Get the message in parameter and print it on the screen to warn user
        /// that the creation of the profile failed.
        /// Called only in the profile creation.
        /// </summary>
        /// <param name="m_Message"></param>
        private void display_Error_Create_Profile(string m_Message)
        {
            lbl_Fail_Create_Profile.Text = m_Message;
            lbl_Succes_Create_Profile.Visible = false;
            lbl_Fail_Create_Profile.Visible = true;
        }
        #endregion Profile_Creation

        #region Profile_Management
        ////////////////////////////////////
        //      Controls Manage Prolfile  //
        ////////////////////////////////////

        /// <summary>
        /// Close the profile managment tab and open the main menu.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pic_Menu_Click(object sender, EventArgs e)
        {
            play_Sound_Click();

            cbo_Manage_Profile.Items.Clear();

            //Reset controls availability
            manage_Profile_Button_Locker(false);

            pnl_Manage_Profile.Visible = false;
            pnl_Menu.Visible = true;
        }

        /// <summary>
        /// Get the actual name to query the database for statistics
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbo_Manage_Profile_Selected_Index_Changed(object sender, EventArgs e)
        {
            manage_Profile_Button_Locker(true);

            //Populating the statistics.
            
        }

        /// <summary>
        /// Allow renaming the selected profile.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmd_Rename_Click(object sender, EventArgs e)
        {
            play_Sound_Click();
        }

        /// <summary>
        /// Resets the stats of the selected profile.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmd_Reset_Click(object sender, EventArgs e)
        {
            play_Sound_Click();
        }

        /// <summary>
        /// Delete the selected profile in the database.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmd_Delete_Click(object sender, EventArgs e)
        {
            play_Sound_Click();
        }

        #endregion Profile_Managment

        #region Sound_Players
        /// <summary>
        /// Play the click sound for each button.
        /// Called in the click events.
        /// </summary>
        private void play_Sound_Click()
        {
            player.SoundLocation = (@"P:\Tablut\Design\SFX\Menu_Click.wav");
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
            player.SoundLocation = (@"P:\Tablut\Design\SFX\Menu_Move.wav");
            player.Play();
        }
        #endregion Sound_Players

        /// <summary>
        /// Enable or disable the profile managment buttons (delete, rename, reset)
        /// Disabled if no profile selected else unlocked.
        /// </summary>
        /// <param name="m_State"></param>
        private void manage_Profile_Button_Locker(bool m_State)
        {
            pic_Rename.Enabled = m_State;
            pic_Reset.Enabled = m_State;
            pic_Delete.Enabled = m_State;

            if (m_State)
            {
                pic_Rename.Image = Image.FromFile(@"P:\Tablut\Design\Bouton\s_btn_Renommer.png");
                pic_Reset.Image = Image.FromFile(@"P:\Tablut\Design\Bouton\s_btn_Reinitialiser.png");
                pic_Delete.Image = Image.FromFile(@"P:\Tablut\Design\Bouton\s_btn_Supprimer.png");
            }
            else
            {
                pic_Rename.Image = Image.FromFile(@"P:\Tablut\Design\Bouton\s_btn_Renommer_Disable.png");
                pic_Reset.Image = Image.FromFile(@"P:\Tablut\Design\Bouton\s_btn_Reinitialiser_Disable.png");
                pic_Delete.Image = Image.FromFile(@"P:\Tablut\Design\Bouton\s_btn_Supprimer_Disable.png");
            }
        }

    }
}