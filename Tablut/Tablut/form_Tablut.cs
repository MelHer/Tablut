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
    /// Class updating the view. Listend to the diffrent event raised
    /// by the form controls.
    /// </summary>
    public partial class frm_Tablut : Form
    {
        System.Media.SoundPlayer sound_Player;  /*!< sound player used when click on button(picture box).*/

        DB_Connect db_link;  /*!< Connection with database. */

        Game game; /*!< Contains the movement algorithms and the player, created at every beginning of a game.*/

        Dictionary<string, Square> board; /*!< Contains all the squares of the board. */

        List<string> possible_Move; /*!< Contains the name of all highlighted squares.  */

        /// <summary>
        /// Constructor. Instantiates the sound player and the even handlers.
        /// Create the database connection object.
        /// </summary>
        public frm_Tablut()
        {
            InitializeComponent();   

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
        /// <param name="sender">The button in main menu "Create profile".</param>
        /// <param name="e">Contains informations about the raised event.</param>
        private void pic_Create_Profile_Click(object sender, EventArgs e)
        {
            play_Sound_Click();

            pnl_Menu.Visible = false;
            pnl_Create_Profile.Visible = true;
        }

        /// <summary>
        /// Closes the menu and open the profile managment
        /// </summary>
        /// <param name="sender"> The button in main menu "Manage profile".</param>
        /// <param name="e">Contains informations about the raised event.</param>
        private void pic_Manage_Profile_Click(object sender, EventArgs e)
        {
            play_Sound_Click();

            pnl_Menu.Visible = false;
            pnl_Manage_Profile.Visible = true;

            populate_Profile_List();
        }

        /// <summary>
        /// Closes the menu and open the profile selection screen.
        /// </summary>
        /// <param name="sender"> The button in main menu "Play".</param>
        /// <param name="e">Contains informations about the raised event.</param>
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
        /// <param name="sender"> The button in main menu "Play".</param>
        /// <param name="e">Contains informations about the raised event.</param>
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
        /// <param name="sender">The button in profile creation "Create".</param>
        /// <param name="e">Contains informations about the raised event.</param>
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
        /// Closes the profile creation and return to the menu.
        /// </summary>
        /// <param name="sender">The button in profile creation "Cancel".</param>
        /// <param name="e">Contains informations about the raised event.</param>
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
        /// <param name="m_Message">The message display to th user about the error</param>
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
        /// Closes the profile managment tab and opens the main menu.
        /// </summary>
        /// <param name="sender">The button in profile managment "Menu".</param>
        /// <param name="e">Contains informations about the raised event.</param>
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
        /// <param name="sender">The drop down list to chose a profile in the profile managment menu.</param>
        /// <param name="e">Contains informations about the raised event.</param>
        private void cbo_Manage_Profile_Selected_Index_Changed(object sender, EventArgs e)
        {
            manage_Profile_Button_Locker(true);

            if(!(cbo_Manage_Profile.SelectedIndex == -1))
            {
                populate_Stat_Board();
            }
        }

        /// <summary>
        /// Allows to rename the selected profile.
        /// </summary>
        /// <param name="sender">The button in profile managment "Rename".</param>
        /// <param name="e">Contains informations about the raised event.<param>
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
        /// <param name="sender">The button in profile managment "Reset".</param>
        /// <param name="e">Contains informations about the raised event.</param>
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
        /// <param name="sender">The button in profile managment "Delete".</param>
        /// <param name="e">Contains informations about the raised event.</param>
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
        /// Populates the drop down list (of the profile managment menu) with every name of the profiles.
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
        /// selected profile in the managment profile menu.
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
        /// Called when the menu is loaded or a profile is deleted;
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
        /// Enables or disables the profile managment buttons (delete, rename, reset)
        /// Disabled if no profile selected else unlocked.
        /// </summary>
        /// <param name="m_State">To unlock set as true.</param>
        private void manage_Profile_Button_Locker(bool m_State)
        {
            pic_Rename.Enabled = m_State;
            pic_Reset.Enabled = m_State;
            pic_Delete.Enabled = m_State;

            if (m_State)
            {
                pic_Rename.Image = Tablut.Properties.Resources.s_btn_Rename;
                pic_Reset.Image = Tablut.Properties.Resources.s_btn_Reset;
                pic_Delete.Image = Tablut.Properties.Resources.s_btn_Delete;
            }
            else
            {
                pic_Rename.Image = Tablut.Properties.Resources.s_btn_Rename_Disable;
                pic_Reset.Image = Tablut.Properties.Resources.s_btn_Reset_Disable;
                pic_Delete.Image = Tablut.Properties.Resources.s_btn_Delete_Disable;
            }
        }
        #endregion Profile_Managment

        #region Player_Selection
        ////////////////////////////////////
        //        Player Selection        //
        ////////////////////////////////////

        /// <summary>
        /// Launches the game when both profiles are selected and set the game.
        /// </summary>
        /// <param name="sender">The button in the player selection menu "Start".</param>
        /// <param name="e">Contains informations about the raised event.</param>
        private void pic_Start_Player_Selection_Click(object sender, EventArgs e)
        {
            play_Sound_Click();

            //Creates the games and the player objects.
            game = new Game(cbo_Player_Selection_Attack.SelectedItem.ToString(), cbo_Player_Selection_Defence.SelectedItem.ToString());

            //Indicates the player round.
            lbl_Current_Player.Text = game.Defender.Name;

            //Populating the board with the pawns
            board = new Dictionary<string, Square>();

            for(int row = 0; row <= 8; row++)
            {
                for(int column = 0; column <= 8; column++)
                {
                    board.Add(column + "" + row, new Square());
                    board[column + "" + row].Name = column + "" + row;
                    board[column + "" + row].Width = tlp_Board.Width / 9;
                    board[column + "" + row].Height = tlp_Board.Width / 9;
                    board[column + "" + row].Click += new System.EventHandler(this.square_Click);

                    tlp_Board.Controls.Add(board[column + "" + row], column, row);
                }
            }

            //Putting images in the initial state of the board
            //Attackers (Black pawns)
            board["30"].change_Image(Occupant.Attacker);
            board["30"].change_Image(Occupant.Attacker);
            board["40"].change_Image(Occupant.Attacker);
            board["50"].change_Image(Occupant.Attacker);
            board["41"].change_Image(Occupant.Attacker);
            board["03"].change_Image(Occupant.Attacker);
            board["83"].change_Image(Occupant.Attacker);
            board["04"].change_Image(Occupant.Attacker);
            board["14"].change_Image(Occupant.Attacker);
            board["74"].change_Image(Occupant.Attacker);
            board["84"].change_Image(Occupant.Attacker);
            board["05"].change_Image(Occupant.Attacker);
            board["85"].change_Image(Occupant.Attacker);
            board["47"].change_Image(Occupant.Attacker);
            board["38"].change_Image(Occupant.Attacker);
            board["48"].change_Image(Occupant.Attacker);
            board["58"].change_Image(Occupant.Attacker);

            //Defenders (White pawns)
            board["42"].change_Image(Occupant.Defender);
            board["43"].change_Image(Occupant.Defender);
            board["24"].change_Image(Occupant.Defender);
            board["34"].change_Image(Occupant.Defender);
            board["44"].change_Image(Occupant.King);
            board["54"].change_Image(Occupant.Defender);
            board["64"].change_Image(Occupant.Defender);
            board["45"].change_Image(Occupant.Defender);
            board["46"].change_Image(Occupant.Defender);

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
        /// <param name="sender">The button in the player selection menu "Cancel".</param>
        /// <param name="e">Contains informations about the raised event.</param>
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
            pic_Start_Player_Selection.Image = Tablut.Properties.Resources.btn_Start_Disable;
        }

        /// <summary>
        /// Select a profile to be the attacker
        /// and check if "Launch" button can be unlocked
        /// </summary>
        /// <param name="sender">The drop down list to select the attacker profile.</param>
        /// <param name="e">Contains informations about the raised event.</param>
        private void cbo_Player_Selection_Attack_SelectedIndexChanged(object sender, EventArgs e)
        {
            profile_Selection_Checker();
        }

        /// <summary>
        /// Select a profile to be the defender
        /// and check if "Launch" button can be unlocked
        /// </summary>
        /// <param name="sender">The drop down list to select the attacker profile.</param>
        /// <param name="e">Contains informations about the raised event.</param>
        private void cbo_Player_Selection_Defence_SelectedIndexChanged(object sender, EventArgs e)
        {
            profile_Selection_Checker();
        }

        /// <summary>
        /// Check if selected profiles are diffrent.
        /// Else "Start" button keeps disabled and
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
                        pic_Start_Player_Selection.Image = Tablut.Properties.Resources.btn_Start_Disable;
                    }
                }
                else
                {
                    lbl_Player_Selection_Fail.Visible = false;

                    //Enables the "Launch" button.
                    pic_Start_Player_Selection.Enabled = true;
                    pic_Start_Player_Selection.Image = Tablut.Properties.Resources.btn_Start;

                }
            }
        }
        #endregion Player_Selection

        #region Game
        ////////////////////////////////////
        //              Game              //
        ////////////////////////////////////

        /// <summary>
        /// Updates were the pawn are on the board.
        /// Calls the diffrents game checker of the game class
        /// like "should some pawn be destroyed ?" or "Is the movement valid ?" 
        /// </summary>
        /// <param name="sender">A clicked square of the board</param>
        /// <param name="e">Contains informations about the raised event.</param>
        private void square_Click(object sender, EventArgs e)
        {
            
            if(game.Phase == Game_Phase.picking)
            {
                //Will Contain all the possible square where the player
                //can move his selected pawn.
                possible_Move = new List<string>();

                possible_Move = game.Pawn_Click((Square)sender, board);

                foreach(String square_Name in possible_Move)
                {
                    board[square_Name].change_Image(Occupant.Empty);
                }
            }
            else if(game.Phase == Game_Phase.moving)
            {
                string result = game.Square_Click((Square)sender, possible_Move);

                if(result != "invalid")
                {
                    //resets possible moves indications. 
                    foreach (String square_Name in possible_Move)
                    {
                        board[square_Name].clear_Image();
                    }
                }
                
                if(result == "move")
                {
                    //Update the board after the move
                    if(game.Current_Player.Role == Player_Role.Attacker)
                    {
                        ((Square)sender).change_Image(Occupant.Attacker);
                    }
                    else
                    {
                        if (game.selected_Pawn.Occupant == Occupant.King)
                        {
                            ((Square)sender).change_Image(Occupant.King);
                        }
                        else
                        {
                            ((Square)sender).change_Image(Occupant.Defender);
                        }
                    }
                    //Resets the pawn ancient location
                    board[game.selected_Pawn.Name].clear_Image();

                    List<string> pawn_To_Remove = new List<string>(game.search_Eliminated_Pawn((Square)sender, board));

                    foreach (string pawn_Name in pawn_To_Remove)
                    {
                        board[pawn_Name].clear_Image();
                    }

                    if (game.is_Over(board))
                    {
                        //Updating victory banner and database stats
                        if(game.Current_Player.Role == Player_Role.Attacker)
                        {
                            lbl_Game_Over_Winner.Text = "Victoire des attaquants";
                            db_link.Add_Victory(game.Attacker.Name, game.Attacker.Role);
                            db_link.Add_Defeat(game.Defender.Name, game.Defender.Role);
                        }
                        else
                        {
                            lbl_Game_Over_Winner.Text = "Victoire des défenseurs";
                            db_link.Add_Victory(game.Defender.Name, game.Defender.Role);
                            db_link.Add_Defeat(game.Attacker.Name, game.Attacker.Role);
                        }

                        //Updating stats
                        lbl_Game_Over_Num_Move_Attack.Text = game.Attacker.Total_Moves.ToString();
                        lbl_Game_Over_Num_Elimination_Attack.Text = game.Attacker.Total_Enemy_Pawn_Eliminated.ToString();
                        lbl_Game_Over_Num_Move_Defence.Text = game.Defender.Total_Moves.ToString();
                        lbl_Game_Over_Num_Elimination_Defence.Text = game.Defender.Total_Enemy_Pawn_Eliminated.ToString();

                        pnl_Game.Visible = false;
                        pnl_Game_Over.Visible = true;

                        reset_Game();
                    }

                    //Change player name who has to play
                    lbl_Current_Player.Text = game.Current_Player.Name;
                }
            }
        }

        /// <summary>
        /// Leaves the game, doesn't save anything and brings back the main menu after raising confirmation form.
        /// </summary>
        /// <param name="sender">The button in game "Menu".</param>
        /// <param name="e">Contains informations about the raised event.</param>
        private void pic_Game_Menu_Click(object sender, EventArgs e)
        {
            play_Sound_Click();

            frm_Confirmation confirmation = new frm_Confirmation("Voulez-vous vraiment retourner au menu principal? Aucun changement ne sera enregistré.");

            if (confirmation.ShowDialog(this) == DialogResult.OK)
            {
                pnl_Game.Visible = false;
                pnl_Menu.Visible = true;

                reset_Game();
            }

            confirmation.Dispose();
        }

        /// <summary>
        /// Leaves the game without saving and close the application after raising confirmation form.
        /// </summary>
        /// <param name="sender">The button in game "Menu".</param>
        /// <param name="e">Contains informations about the raised event.</param>
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

        /// <summary>
        /// Resets the game (for a new game) after returning to the menu
        /// or ending a game.
        /// </summary>
        private void reset_Game()
        {
            board.Clear();
            tlp_Board.Controls.Clear();
        }

        #endregion Game

        #region Game_Over
        ////////////////////////////////////
        //           Game Over            //
        ////////////////////////////////////

        /// <summary>
        /// Leaves the game over screen and returns to the main menu.
        /// </summary>
        /// <param name="sender">The button in game over screen "Menu".</param>
        /// <param name="e">Contains informations about the raised event.</param>
        private void pic_Game_Over_Menu_Click(object sender, EventArgs e)
        {
            pnl_Game_Over.Visible = false;
            pnl_Menu.Visible = true;
        }

        #endregion Game_Over

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
