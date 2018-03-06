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

            //Player selection
            pic_Start_Player_Selection.MouseEnter += new System.EventHandler(this.play_Sound_Enter);
            pic_Cancel_Player_Selection.MouseEnter += new System.EventHandler(this.play_Sound_Enter);

            //Game
            pic_Game_Menu.MouseEnter += new System.EventHandler(this.play_Sound_Enter);
            pic_Game_Close.MouseEnter += new System.EventHandler(this.play_Sound_Enter);
        }

        #region Main_Menu
        ////////////////////////////////////
        //            Main Menu           //
        ////////////////////////////////////

        /// <summary>
        /// Closes the menu and open the profile creation.
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
        /// Closes the menu and open the profile managment
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pic_Manage_Profile_Click(object sender, EventArgs e)
        {
            play_Sound_Click();

            pnl_Menu.Visible = false;
            pnl_Manage_Profile.Visible = true;

            populate_Profile_List();
        }

        //Close the menu and launch a game
        private void pic_Play_Click(object sender, EventArgs e)
        {
            play_Sound_Click();

            pnl_Menu.Visible = false;
            pnl_Play_Profile_Selection.Visible = true;

            //populating both combo box in the player selection
            try
            {
                List<string> profile_Name = new List<String>(db_link.get_Profile_Name());

                foreach (string name in profile_Name)
                {
                    cbo_Player_Selection_Attack.Items.Add(name);
                    cbo_Player_Selection_Defence.Items.Add(name);
                }
            }
            catch (MySqlException ex)
            {
                if (ex.Number == 1042)
                {
                    lbl_Player_Selection_Fail.Text = "Connexion à la base de données impossible.";
                    lbl_Player_Selection_Fail.Visible = true;
                }
            }
        }

        /// <summary>
        /// Exits the application.
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
        /// Sends a profile creation request to the DB_Connect object.
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
            catch(MySqlException ex)
            {
                if(ex.Number == 1042)
                {
                    display_Error_Create_Profile("Connexion à la base de données impossible.");
                }
                else if(ex.Number == 1062)
                {
                    display_Error_Create_Profile("Profil avec un nom similaire déjà existant.");
                }

            }
        }

        /// <summary>
        /// Closes the profile creation and return to the menu
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
        /// Gets the message in parameter and print it on the screen to warn user
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
        /// Closes the profile managment tab and open the main menu.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pic_Menu_Click(object sender, EventArgs e)
        {
            play_Sound_Click();

            cbo_Manage_Profile.Items.Clear();

            //Reset controls availability
            manage_Profile_Button_Locker(false);

            if(lbl_Managment_Fail.Visible == true)
            {
                lbl_Managment_Fail.Visible = false;
            }

            reset_Stat_Board();

            pnl_Manage_Profile.Visible = false;
            pnl_Menu.Visible = true;
        }

        /// <summary>
        /// Gets the actual name to query the database for statistics
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbo_Manage_Profile_Selected_Index_Changed(object sender, EventArgs e)
        {
            manage_Profile_Button_Locker(true);

            if(!(cbo_Manage_Profile.SelectedIndex == -1))
            {
                populate_Stat_Board();
            }
        }

        /// <summary>
        /// Allows renaming the selected profile.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmd_Rename_Click(object sender, EventArgs e)
        {
            play_Sound_Click();

            frm_Renaming renaming = new frm_Renaming(cbo_Manage_Profile.SelectedItem.ToString());

            if (renaming.ShowDialog(this) == DialogResult.OK)
            {
                //Reloads the list and selects the modified profile
                cbo_Manage_Profile.Items.Clear();
                populate_Profile_List();
                cbo_Manage_Profile.SelectedIndex = cbo_Manage_Profile.FindStringExact(renaming.validated_New_Name);
            }

            renaming.Dispose();
        }

        /// <summary>
        /// Resets the stats of the selected profile.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmd_Reset_Click(object sender, EventArgs e)
        {
            play_Sound_Click();

            frm_Confirmation confirmation = new frm_Confirmation("Voulez vous vraiment réinitialiser les statistiques de: \n" + cbo_Manage_Profile.SelectedItem.ToString());

            if (confirmation.ShowDialog(this) == DialogResult.OK)
            {
                db_link.Reset_Profile(cbo_Manage_Profile.SelectedItem.ToString());

                reset_Stat_Board();
                populate_Stat_Board();
            }

            confirmation.Dispose();
        }

        /// <summary>
        /// Deletes the selected profile in the database.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmd_Delete_Click(object sender, EventArgs e)
        {
            play_Sound_Click();

            frm_Confirmation confirmation = new frm_Confirmation("Voulez vous vraiment supprimer le profil: \n"+cbo_Manage_Profile.SelectedItem.ToString());
            

            if (confirmation.ShowDialog(this) == DialogResult.OK)
            {
                db_link.Remove_Profile(cbo_Manage_Profile.SelectedItem.ToString());

                cbo_Manage_Profile.Items.Clear();
                populate_Profile_List();
                reset_Stat_Board();
            }

            confirmation.Dispose();
        }

        /// <summary>
        /// Populates the drop down list containing each name of the profiles
        /// in the profile managment menu.
        /// </summary>
        private void populate_Profile_List()
        {
            try
            {
                List<string> profile_Name = new List<String>(db_link.get_Profile_Name());

                foreach (string name in profile_Name)
                {
                    cbo_Manage_Profile.Items.Add(name);
                }
            }
            catch (MySqlException ex)
            {
                if (ex.Number == 1042)
                {
                    lbl_Managment_Fail.Visible = true;
                }
            }
        }

        /// <summary>
        /// Populates the zone which shows the statistics of the
        /// selected profile in the managment profile menu
        /// </summary>
        private void populate_Stat_Board()
        {
   
            int[] statistics = new int[4];
            db_link.get_Profile_Stats(cbo_Manage_Profile.SelectedItem.ToString()).CopyTo(statistics, 0);

            lbl_Num_Attack_Victories.Text = statistics[0].ToString();
            lbl_Num_Attack_Loses.Text = statistics[1].ToString();
            lbl_Num_Defence_Victories.Text = statistics[2].ToString();
            lbl_Num_Defence_Loses.Text = statistics[3].ToString();

            lbl_Num_Games_Played.Text = (statistics[0] + statistics[1] + statistics[2] + statistics[3]).ToString();
            lbl_Num_Global_Victories.Text = (statistics[0] + statistics[2]).ToString();
           
        }

        /// <summary>
        /// Resets the statistics in the profile managment menu.
        /// Called when the menu is loaded, a profile is deleted;
        /// </summary>
        private void reset_Stat_Board()
        {
            lbl_Num_Global_Victories.Text = "";
            lbl_Num_Games_Played.Text = "";
            lbl_Num_Attack_Victories.Text = "";
            lbl_Num_Attack_Loses.Text = "";
            lbl_Num_Defence_Victories.Text = "";
            lbl_Num_Defence_Loses.Text = "";
        }

        /// <summary>
        /// Enables or disable the profile managment buttons (delete, rename, reset)
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
        #endregion Profile_Managment

        #region Player_Selection
        ////////////////////////////////////
        //        Player Selection        //
        ////////////////////////////////////

        /// <summary>
        /// Launches the game when both profiles are selected.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pic_Start_Player_Selection_Click(object sender, EventArgs e)
        {
            play_Sound_Click();

            //Creates the games and the player objects.
            Game game = new Game(cbo_Player_Selection_Attack.SelectedItem.ToString(), cbo_Player_Selection_Defence.SelectedItem.ToString());

            //Indicates the player round.
            lbl_Current_Player.Text = game.Defender.Name;

            //Populating the board with the pawns
            Dictionary<string, Square> board = new Dictionary<string, Square>();

            for(int row = 0; row <= 8; row++)
            {
                for(int column = 0; column <= 8; column++)
                {
                    board.Add("SQ" + column + "" + row, new Square());
                    board["SQ" + column + "" + row].Name = "SQ" + column + "" + row;
                    board["SQ" + column + "" + row].Width = tlp_Board.Width / 9;
                    board["SQ" + column + "" + row].Height = tlp_Board.Width / 9;
                    board["SQ" + column + "" + row].Click += new System.EventHandler(this.square_Click);

                    tlp_Board.Controls.Add(board["SQ" + column + "" + row], column, row);
                }
            }

            //Putting images in the initial state of the board
            //Attackers (Black pawns)
            board["SQ30"].change_image(@"P:\Tablut\Design\Pion\Pion_Noir.png");
            board["SQ30"].change_image(@"P:\Tablut\Design\Pion\Pion_Noir.png");
            board["SQ40"].change_image(@"P:\Tablut\Design\Pion\Pion_Noir.png");
            board["SQ50"].change_image(@"P:\Tablut\Design\Pion\Pion_Noir.png");
            board["SQ41"].change_image(@"P:\Tablut\Design\Pion\Pion_Noir.png");
            board["SQ03"].change_image(@"P:\Tablut\Design\Pion\Pion_Noir.png");
            board["SQ83"].change_image(@"P:\Tablut\Design\Pion\Pion_Noir.png");
            board["SQ04"].change_image(@"P:\Tablut\Design\Pion\Pion_Noir.png");
            board["SQ14"].change_image(@"P:\Tablut\Design\Pion\Pion_Noir.png");
            board["SQ74"].change_image(@"P:\Tablut\Design\Pion\Pion_Noir.png");
            board["SQ84"].change_image(@"P:\Tablut\Design\Pion\Pion_Noir.png");
            board["SQ05"].change_image(@"P:\Tablut\Design\Pion\Pion_Noir.png");
            board["SQ85"].change_image(@"P:\Tablut\Design\Pion\Pion_Noir.png");
            board["SQ47"].change_image(@"P:\Tablut\Design\Pion\Pion_Noir.png");
            board["SQ38"].change_image(@"P:\Tablut\Design\Pion\Pion_Noir.png");
            board["SQ48"].change_image(@"P:\Tablut\Design\Pion\Pion_Noir.png");
            board["SQ58"].change_image(@"P:\Tablut\Design\Pion\Pion_Noir.png");

            //Defenders (White pawns)
            board["SQ42"].change_image(@"P:\Tablut\Design\Pion\Pion_Blanc.png");
            board["SQ43"].change_image(@"P:\Tablut\Design\Pion\Pion_Blanc.png");
            board["SQ24"].change_image(@"P:\Tablut\Design\Pion\Pion_Blanc.png");
            board["SQ34"].change_image(@"P:\Tablut\Design\Pion\Pion_Blanc.png");
            board["SQ44"].change_image(@"P:\Tablut\Design\Pion\Pion_Blanc_Roi.png");
            board["SQ54"].change_image(@"P:\Tablut\Design\Pion\Pion_Blanc.png");
            board["SQ64"].change_image(@"P:\Tablut\Design\Pion\Pion_Blanc.png");
            board["SQ45"].change_image(@"P:\Tablut\Design\Pion\Pion_Blanc.png");
            board["SQ46"].change_image(@"P:\Tablut\Design\Pion\Pion_Blanc.png");

            //Reseting combo box
            cbo_Player_Selection_Attack.Items.Clear();
            cbo_Player_Selection_Defence.Items.Clear();

            //Can potentially include a loading screen because it may takes a few sec to generate the board.
            pnl_Play_Profile_Selection.Visible = false;
            pnl_Game.Visible = true;
        }

        /// <summary>
        /// Exits the player selection and return to the main menu.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pic_Cancel_Player_Selection_Click(object sender, EventArgs e)
        {
            play_Sound_Click();

            //Reseting combo box
            cbo_Player_Selection_Attack.Items.Clear();
            cbo_Player_Selection_Defence.Items.Clear();

            pnl_Play_Profile_Selection.Visible = false;
            pnl_Menu.Visible = true;

            //Reset start button and lists
            pic_Start_Player_Selection.Enabled = false;
            pic_Start_Player_Selection.Image = Image.FromFile(@"P:\Tablut\Design\Bouton\btn_Demarrer_Disable.png");
        }

        /// <summary>
        /// Removes the selected profile from the defence list
        /// and check if "Launch" button can be unlocked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbo_Player_Selection_Attack_SelectedIndexChanged(object sender, EventArgs e)
        {
            profile_Selection_Checker();
        }

        /// <summary>
        /// Removes the selected profile the attack list
        /// and check if "Launch" button can be unlocked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbo_Player_Selection_Defence_SelectedIndexChanged(object sender, EventArgs e)
        {
            profile_Selection_Checker();
        }

        /// <summary>
        /// Check if both profile are diffrent.
        /// Else "Launch" button keep disabled and
        /// print an error message.
        /// </summary>
        private void profile_Selection_Checker()
        {
            //Cant be true as long as both combo box don't have a value select.
            if (!(cbo_Player_Selection_Attack.SelectedIndex == -1) && !(cbo_Player_Selection_Defence.SelectedIndex == -1))
            {
                if (cbo_Player_Selection_Attack.SelectedItem == cbo_Player_Selection_Defence.SelectedItem)
                {
                    lbl_Player_Selection_Fail.Text = "Veuillez choisir 2 profils différents pour lancer une partie.";
                    lbl_Player_Selection_Fail.Visible = true;

                    //To center the label
                    lbl_Player_Selection_Fail.Left = (this.ClientSize.Width - lbl_Player_Selection_Fail.Width) / 2;

                    if(pic_Start_Player_Selection.Enabled == true)
                    {
                        pic_Start_Player_Selection.Enabled = false;
                        pic_Start_Player_Selection.Image = Image.FromFile(@"P:\Tablut\Design\Bouton\btn_Demarrer_Disable.png");
                    }
                }
                else
                {
                    lbl_Player_Selection_Fail.Visible = false;

                    //Enables the "Launch" button.
                    pic_Start_Player_Selection.Enabled = true;
                    pic_Start_Player_Selection.Image = Image.FromFile(@"P:\Tablut\Design\Bouton\btn_Demarrer.png");

                }
            }
        }
        #endregion Player_Selection

        #region Game
        ////////////////////////////////////
        //              Game              //
        ////////////////////////////////////

        //TODO Game phase, select move...
        private void square_Click(object sender, EventArgs e)
        {
            try
            {
                Console.WriteLine(((Square)sender).occupant.ToString());
            }
            catch (Exception_Game_Error ex)
            {

            }
        }

        /// <summary>
        /// Leaves the game, doesn't save anything and brings back the main menu (after confirmation)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pic_Game_Menu_Click(object sender, EventArgs e)
        {
            play_Sound_Click();

            frm_Confirmation confirmation = new frm_Confirmation("Voulez-vous vraiment retourner au menu principal? Aucun changement ne sera enregistré.");

            if (confirmation.ShowDialog(this) == DialogResult.OK)
            {
                //TODO reset game

                pnl_Game.Visible = false;
                pnl_Menu.Visible = true;
            }

            confirmation.Dispose();
        }

        /// <summary>
        /// Leaves the game without saving and close the application (after confirmation)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pic_Game_Close_Click(object sender, EventArgs e)
        {
            play_Sound_Click();

            frm_Confirmation confirmation = new frm_Confirmation("Voulez-vous vraiment quitter le jeu? Aucun changement ne sera enregistré.");

            if (confirmation.ShowDialog(this) == DialogResult.OK)
            {
                Close();
            }

            confirmation.Dispose();

        }
        #endregion Game

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
