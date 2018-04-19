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
    /// Class updating the view. Listens for the diffrent events raised
    /// by the form controls. Displays the menus and the game. This is the main
    /// form of the application.
    /// </summary>
    public partial class frm_Tablut : Form
    {
        /// <summary>
        /// Sound player used when a button is clicked or overriden (picture box).
        /// </summary>
        private System.Media.SoundPlayer sound_Player;

        /// <summary>
        /// Music player to play sound while in the menus or in game.
        /// </summary>
        private System.Windows.Media.MediaPlayer music_Player;

        /// <summary>
        /// Gets the execution folder.
        /// </summary>
        private string root_Location = AppDomain.CurrentDomain.BaseDirectory;

        /// <summary>
        /// Stores the path to the diffrent sounds.
        /// </summary>
        private string sound_Path;

        /// <summary>
        /// If false, the music player will play music menu.
        /// </summary>
        private bool is_In_Game;

        /// <summary>
        /// Connection with database.
        /// </summary>
        private DB_Connect db_Link;

        /// <summary>
        /// Contains the movement algorithm and the player objects, created at every beginning of a game.
        /// </summary>
        private Game game;

        /// <summary>
        /// Contains all the squares of the board. 
        /// </summary>
        private Dictionary<string, Square> board;

        /// <summary>
        /// Contains the name of all highlighted squares.  Those are possible movements. 
        /// </summary>
        private List<string> possible_Move;

        /// <summary>
        /// Constructor. Instantiates the database connection and the event handlers.
        /// Instantiates the music player aswell and gets the sounds location then launch the first music.
        /// </summary>
        public frm_Tablut()
        {
            InitializeComponent();   

            db_Link = new DB_Connect();


            //Background sound
            is_In_Game = false;
            music_Player = new System.Windows.Media.MediaPlayer();
            music_Player.MediaEnded += new System.EventHandler(this.sound_Ended);
            sound_Path = System.IO.Path.Combine(root_Location, @"Content\Menu_Fantasy.WAV");
            music_Player.Open(new Uri(sound_Path));

            music_Player.Play();

            //Initializing event

            //Sound cues
            //menu
            pic_Menu_Create_Profile.MouseEnter += new System.EventHandler(this.play_Sound_Enter);
            pic_Menu_Manage_Profile.MouseEnter += new System.EventHandler(this.play_Sound_Enter);
            pic_Menu_Play.MouseEnter += new System.EventHandler(this.play_Sound_Enter);
            pic_Menu_Close.MouseEnter += new System.EventHandler(this.play_Sound_Enter);

            //profile creation
            pic_Create_Profile_Create.MouseEnter += new System.EventHandler(this.play_Sound_Enter);
            pic_Create_Profile_Cancel.MouseEnter += new System.EventHandler(this.play_Sound_Enter);

            //profile managment
            pic_Manage_Profile_Menu.MouseEnter += new System.EventHandler(this.play_Sound_Enter);
            pic_Manage_Profile_Rename.MouseEnter += new System.EventHandler(this.play_Sound_Enter);
            pic_Manage_Profile_Reset.MouseEnter += new System.EventHandler(this.play_Sound_Enter);
            pic_Manage_Profile_Delete.MouseEnter += new System.EventHandler(this.play_Sound_Enter);

            //Player selection
            pic_Player_Selection_Start.MouseEnter += new System.EventHandler(this.play_Sound_Enter);
            pic_Player_Selection_Cancel.MouseEnter += new System.EventHandler(this.play_Sound_Enter);

            //Game
            pic_Game_Menu.MouseEnter += new System.EventHandler(this.play_Sound_Enter);
            pic_Game_Close.MouseEnter += new System.EventHandler(this.play_Sound_Enter);

            //Game over
            pic_Game_Over_Menu.MouseEnter += new System.EventHandler(this.play_Sound_Enter);

            //Sound control
            pic_Speaker.MouseEnter += new System.EventHandler(this.play_Sound_Enter);
        }

        #region Main_Menu
        ////////////////////////////////////
        //            Main Menu           //
        ////////////////////////////////////

        /// <summary>
        /// Closes the main menu and open the profile creation.
        /// </summary>
        /// <param name="sender">The "Create profile" button in main menu.</param>
        /// <param name="e">Contains informations about the raised "click" event.</param>
        private void pic_Menu_Create_Profile_Click(object sender, EventArgs e)
        {
            play_Sound_Click();

            pnl_Menu.Visible = false;
            pnl_Create_Profile.Visible = true;
        }

        /// <summary>
        /// Closes the main menu and open the profile managment.
        /// </summary>
        /// <param name="sender"> The "Manage profile" button in main menu.</param>
        /// <param name="e">Contains informations about the raised "click" event.</param>
        private void pic_Menu_Manage_Profile_Click(object sender, EventArgs e)
        {
            play_Sound_Click();

            pnl_Menu.Visible = false;
            pnl_Manage_Profile.Visible = true;

            populate_Profile_List();
        }

        /// <summary>
        /// Closes the main menu and open the profiles selection screen.
        /// </summary>
        /// <param name="sender"> The "Play" button in main menu.</param>
        /// <param name="e">Contains informations about the raised "click" event.</param>
        private void pic_Menu_Play_Click(object sender, EventArgs e)
        {
            play_Sound_Click();

            pnl_Menu.Visible = false;
            pnl_Play_Profile_Selection.Visible = true;

            //populating both combo box in the player selection
            try
            {
                List<string> profile_Name = new List<String>(db_Link.get_Profile_Name());

                if (lbl_Player_Selection_Fail.Visible == true)
                {
                    lbl_Player_Selection_Fail.Visible = false;
                }

                foreach (string name in profile_Name)
                {
                    cbo_Player_Selection_Attack.Items.Add(name);
                    cbo_Player_Selection_Defence.Items.Add(name);
                }
            }
            catch (MySqlException ex)
            {
                if (ex.Number == 1042 | ex.Number == 0)
                {
                    lbl_Player_Selection_Fail.Text = "Connexion à la base de données impossible.";
                    lbl_Player_Selection_Fail.Visible = true;
                }
            }
        }

        /// <summary>
        /// Exits the application.
        /// </summary>
        /// <param name="sender"> The "Leave" button in main menu.</param>
        /// <param name="e">Contains informations about the raised "click" event.</param>
        private void pic_Menu_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion Main_Menu

        #region Profile_Creation
        ////////////////////////////////////
        //         Create Prolfile        //
        ////////////////////////////////////

        /// <summary>
        /// Sends a profile creation request to the DB_Connect object.
        /// </summary>
        /// <param name="sender">The "Create" button in profile creation.</param>
        /// <param name="e">Contains informations about the raised "click" event.</param>
        private void pic_Create_Profile_Create_Click(object sender, EventArgs e)
        {
            play_Sound_Click();

            try
            {
                db_Link.Add_Profile(txt_Profile_Creation_Name.Text);
                txt_Profile_Creation_Name.Text = "";

                lbl_Create_Profile_Fail.Visible = false;
                lbl_Create_Profile_Success.Visible = true;
            }
            catch(Exception_Invalid_Name ex)
            {
                display_Error_Create_Profile(ex.Message);
            }
            catch(MySqlException ex)
            {
                if(ex.Number == 1042 | ex.Number == 0)
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
        /// Closes the profile creation and returns to the main menu.
        /// </summary>
        /// <param name="sender">The "Cancel" button in profile creation.</param>
        /// <param name="e">Contains informations about the raised "click" event.</param>
        private void pic_Create_Profile_Cancel_Click(object sender, EventArgs e)
        {
            play_Sound_Click();

            txt_Profile_Creation_Name.Text = "";

            lbl_Create_Profile_Fail.Visible = false; //If visible, will be hidden when re-opening this menu.
            lbl_Create_Profile_Success.Visible = false;

            pnl_Create_Profile.Visible = false;
            pnl_Menu.Visible = true;
        }

        /// <summary>
        /// Gets the message in parameter and print it on the screen to warn user
        /// that the creation of the profile failed.
        /// Called only in the profile creation.
        /// </summary>
        /// <param name="m_Message">The message to display to the user about the error.</param>
        private void display_Error_Create_Profile(string m_Message)
        {
            lbl_Create_Profile_Fail.Text = m_Message;
            lbl_Create_Profile_Success.Visible = false;
            lbl_Create_Profile_Fail.Visible = true;
        }
        #endregion Profile_Creation

        #region Profile_Management
        ////////////////////////////////////
        //      Controls Manage Prolfile  //
        ////////////////////////////////////

        /// <summary>
        /// Closes the profile managment and opens the main menu.
        /// </summary>
        /// <param name="sender">The "Menu" button in profile managment.</param>
        /// <param name="e">Contains informations about the raised "click" event.</param>
        private void pic_Manage_Profile_Menu_Click(object sender, EventArgs e)
        {
            play_Sound_Click();

            cbo_Manage_Profile_Name.Items.Clear();

            //Reset controls availability
            manage_Profile_Button_Locker(false);

            if(lbl_Manage_Profile_Fail.Visible == true)
            {
                lbl_Manage_Profile_Fail.Visible = false;
            }

            reset_Stat_Board();

            pnl_Manage_Profile.Visible = false;
            pnl_Menu.Visible = true;
        }

        /// <summary>
        /// Gets the name of the selected profile to query the database for statistics.
        /// </summary>
        /// <param name="sender">The drop down list to chose a profile in the profile managment menu.</param>
        /// <param name="e">Contains informations about the raised "index changed" event.</param>
        private void cbo_Manage_Profile_Name_Selected_Index_Changed(object sender, EventArgs e)
        {
            manage_Profile_Button_Locker(true);

            if(!(cbo_Manage_Profile_Name.SelectedIndex == -1))
            {
                populate_Stat_Board();
            }
        }

        /// <summary>
        /// Allows to rename the selected profile.
        /// </summary>
        /// <param name="sender">The "Rename" button in profile managment.</param>
        /// <param name="e">Contains informations about the raised "click" event.<param>
        private void pic_Manage_Profile_Rename_Click(object sender, EventArgs e)
        {
            play_Sound_Click();

            frm_Renaming renaming = new frm_Renaming(cbo_Manage_Profile_Name.SelectedItem.ToString());

            if (renaming.ShowDialog(this) == DialogResult.OK)
            {
                //Reloads the list and selects the modified profile
                cbo_Manage_Profile_Name.Items.Clear();
                populate_Profile_List();
                cbo_Manage_Profile_Name.SelectedIndex = cbo_Manage_Profile_Name.FindStringExact(renaming.validated_New_Name);
            }

            renaming.Dispose();
        }

        /// <summary>
        /// Resets the stats of the selected profile.
        /// </summary>
        /// <param name="sender">The "Reset" button in profile managment.</param>
        /// <param name="e">Contains informations about the raised "click" event.</param>
        private void pic_Manage_Profile_Reset_Click(object sender, EventArgs e)
        {
            play_Sound_Click();

            frm_Confirmation confirmation = new frm_Confirmation("Voulez vous vraiment réinitialiser les statistiques de: \n" + cbo_Manage_Profile_Name.SelectedItem.ToString());

            try
            {

                if (confirmation.ShowDialog(this) == DialogResult.OK)
                {
                    db_Link.Reset_Profile(cbo_Manage_Profile_Name.SelectedItem.ToString());

                    reset_Stat_Board();
                    populate_Stat_Board();

                    //Hides error.
                    if(lbl_Manage_Profile_Fail.Visible == true)
                    {
                        lbl_Manage_Profile_Fail.Visible = false;
                    }
                }
            }
            catch (MySqlException ex)
            {
                if (ex.Number == 1042 | ex.Number == 0)
                {
                    lbl_Manage_Profile_Fail.Visible = true;
                }
            }
            confirmation.Dispose();
        }

        /// <summary>
        /// Deletes the selected profile from the database.
        /// </summary>
        /// <param name="sender">The "Delete" button in profile managment.</param>
        /// <param name="e">Contains informations about the raised "click" event.</param>
        private void pic_Manage_Profile_Delete_Click(object sender, EventArgs e)
        {
            play_Sound_Click();

            frm_Confirmation confirmation = new frm_Confirmation("Voulez vous vraiment supprimer le profil: \n"+cbo_Manage_Profile_Name.SelectedItem.ToString());

            try
            {
                if (confirmation.ShowDialog(this) == DialogResult.OK)
                {
                    db_Link.Remove_Profile(cbo_Manage_Profile_Name.SelectedItem.ToString());

                    cbo_Manage_Profile_Name.Items.Clear();
                    populate_Profile_List();
                    reset_Stat_Board();

                    //Hides error
                    if (lbl_Manage_Profile_Fail.Visible == true)
                    {
                        lbl_Manage_Profile_Fail.Visible = false;
                    }
                }

            }
            catch (MySqlException ex)
            {
                if (ex.Number == 1042 | ex.Number == 0)
                {
                    lbl_Manage_Profile_Fail.Visible = true;
                }
            }
            confirmation.Dispose();
        }

        /// <summary>
        /// Populates the drop down list from the profile managment menu with every profile names.
        /// </summary>
        private void populate_Profile_List()
        {
            try
            {
                List<string> profile_Name = new List<String>(db_Link.get_Profile_Name());

                foreach (string name in profile_Name)
                {
                    cbo_Manage_Profile_Name.Items.Add(name);
                }
            }
            catch (MySqlException ex)
            {
                if (ex.Number == 1042 | ex.Number == 0)
                {
                    lbl_Manage_Profile_Fail.Visible = true;
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
            db_Link.get_Profile_Stats(cbo_Manage_Profile_Name.SelectedItem.ToString()).CopyTo(statistics, 0);

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
            pic_Manage_Profile_Rename.Enabled = m_State;
            pic_Manage_Profile_Reset.Enabled = m_State;
            pic_Manage_Profile_Delete.Enabled = m_State;

            if (m_State)
            {
                pic_Manage_Profile_Rename.Image = Tablut.Properties.Resources.s_btn_Rename;
                pic_Manage_Profile_Reset.Image = Tablut.Properties.Resources.s_btn_Reset;
                pic_Manage_Profile_Delete.Image = Tablut.Properties.Resources.s_btn_Delete;
            }
            else
            {
                pic_Manage_Profile_Rename.Image = Tablut.Properties.Resources.s_btn_Rename_Disable;
                pic_Manage_Profile_Reset.Image = Tablut.Properties.Resources.s_btn_Reset_Disable;
                pic_Manage_Profile_Delete.Image = Tablut.Properties.Resources.s_btn_Delete_Disable;
            }
        }
        #endregion Profile_Managment

        #region Player_Selection
        ////////////////////////////////////
        //        Player Selection        //
        ////////////////////////////////////

        /// <summary>
        /// Launches the game when both profiles are selected and sets the game.
        /// Sets the board, the game and the players.
        /// </summary>
        /// <param name="sender">The "Start" button in the player selection menu.</param>
        /// <param name="e">Contains informations about the raised "click" event.</param>
        private void pic_Player_Selection_Start_Click(object sender, EventArgs e)
        {
            play_Sound_Click();

            //Creates the games and the player objects.
            game = new Game(cbo_Player_Selection_Attack.SelectedItem.ToString(), cbo_Player_Selection_Defence.SelectedItem.ToString());

            //Indicates the player round.
            lbl_Game_Current_Player.Text = game.Defender.Name;

            //Populating the board with the pawns
            board = new Dictionary<string, Square>();

            for(int row = 0; row <= 8; row++)
            {
                for(int column = 0; column <= 8; column++)
                {
                    board.Add(column + "" + row, new Square());
                    board[column + "" + row].Name = column + "" + row;
                    board[column + "" + row].Width = tlp_Game_Board.Width / 9;
                    board[column + "" + row].Height = tlp_Game_Board.Width / 9;
                    board[column + "" + row].Click += new System.EventHandler(this.square_Click);

                    tlp_Game_Board.Controls.Add(board[column + "" + row], column, row);
                }
            }

            //Putting images in the initial state of the board
            //Attackers (Black pawns)
            board["30"].Change_Image(Occupant.Attacker);
            board["30"].Change_Image(Occupant.Attacker);
            board["40"].Change_Image(Occupant.Attacker);
            board["50"].Change_Image(Occupant.Attacker);
            board["41"].Change_Image(Occupant.Attacker);
            board["03"].Change_Image(Occupant.Attacker);
            board["83"].Change_Image(Occupant.Attacker);
            board["04"].Change_Image(Occupant.Attacker);
            board["14"].Change_Image(Occupant.Attacker);
            board["74"].Change_Image(Occupant.Attacker);
            board["84"].Change_Image(Occupant.Attacker);
            board["05"].Change_Image(Occupant.Attacker);
            board["85"].Change_Image(Occupant.Attacker);
            board["47"].Change_Image(Occupant.Attacker);
            board["38"].Change_Image(Occupant.Attacker);
            board["48"].Change_Image(Occupant.Attacker);
            board["58"].Change_Image(Occupant.Attacker);

            //Defenders (White pawns)
            board["42"].Change_Image(Occupant.Defender);
            board["43"].Change_Image(Occupant.Defender);
            board["24"].Change_Image(Occupant.Defender);
            board["34"].Change_Image(Occupant.Defender);
            board["44"].Change_Image(Occupant.King);
            board["54"].Change_Image(Occupant.Defender);
            board["64"].Change_Image(Occupant.Defender);
            board["45"].Change_Image(Occupant.Defender);
            board["46"].Change_Image(Occupant.Defender);

            //Reseting combo box
            cbo_Player_Selection_Attack.Items.Clear();
            cbo_Player_Selection_Defence.Items.Clear();

            pnl_Play_Profile_Selection.Visible = false;
            pnl_Game.Visible = true;

            is_In_Game = true;
            sound_Ended(music_Player, EventArgs.Empty); //Force sound switching

            pic_Player_Selection_Start.Enabled = false;
            pic_Player_Selection_Start.Image = Tablut.Properties.Resources.btn_Start_Disable;
        }

        /// <summary>
        /// Exits the player selection and returns to the main menu.
        /// </summary>
        /// <param name="sender">The "Cancel" button in the player selection menu.</param>
        /// <param name="e">Contains informations about the raised click event.</param>
        private void pic_Player_Selection_Cancel_Click(object sender, EventArgs e)
        {
            play_Sound_Click();

            //Reseting combo box
            cbo_Player_Selection_Attack.Items.Clear();
            cbo_Player_Selection_Defence.Items.Clear();

            pnl_Play_Profile_Selection.Visible = false;
            pnl_Menu.Visible = true;

            //Resets start button and lists
            pic_Player_Selection_Start.Enabled = false;
            pic_Player_Selection_Start.Image = Tablut.Properties.Resources.btn_Start_Disable;
        }

        /// <summary>
        /// Selects a profile to be the attacker
        /// and checks if "Launch" button can be unlocked.
        /// </summary>
        /// <param name="sender">The drop down list to select the attacker profile.</param>
        /// <param name="e">Contains informations about the raised "index changed" event.</param>
        private void cbo_Player_Selection_Attack_Selected_Index_Changed(object sender, EventArgs e)
        {
            profile_Selection_Checker();
        }

        /// <summary>
        /// Selects a profile to be the defender
        /// and checks if "Launch" button can be unlocked.
        /// </summary>
        /// <param name="sender">The drop down list to select the attacker profile.</param>
        /// <param name="e">Contains informations about the raised "index changed" event.</param>
        private void cbo_Player_Selection_Defence_Selected_Index_Changed(object sender, EventArgs e)
        {
            profile_Selection_Checker();
        }

        /// <summary>
        /// Checks if selected profiles are diffrent.
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

                    if(pic_Player_Selection_Start.Enabled == true)
                    {
                        pic_Player_Selection_Start.Enabled = false;
                        pic_Player_Selection_Start.Image = Tablut.Properties.Resources.btn_Start_Disable;
                    }
                }
                else
                {
                    lbl_Player_Selection_Fail.Visible = false;

                    //Enables the "Launch" button.
                    pic_Player_Selection_Start.Enabled = true;
                    pic_Player_Selection_Start.Image = Tablut.Properties.Resources.btn_Start;

                }
            }
        }
        #endregion Player_Selection

        #region Game
        ////////////////////////////////////
        //              Game              //
        ////////////////////////////////////

        /// <summary>
        /// Updates were the pawns are on the board.
        /// Calls the diffrents game checker of the game class
        /// like "should some pawns be destroyed ?" or "Is this movement valid ?" 
        /// </summary>
        /// <param name="sender">A clicked square of the board</param>
        /// <param name="e">Contains informations about the raised "click" event.</param>
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
                    board[square_Name].Change_Image(Occupant.Empty);
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
                        board[square_Name].Clear_Image();
                    }
                }
                
                if(result == "move")
                {
                    //Update the board after the move
                    if(game.Current_Player.Role == Player_Role.Attacker)
                    {
                        ((Square)sender).Change_Image(Occupant.Attacker);
                    }
                    else
                    {
                        if (game.selected_Pawn.Occupant == Occupant.King)
                        {
                            ((Square)sender).Change_Image(Occupant.King);
                        }
                        else
                        {
                            ((Square)sender).Change_Image(Occupant.Defender);
                        }
                    }
                    //Resets the pawn ancient location
                    board[game.selected_Pawn.Name].Clear_Image();

                    List<string> pawn_To_Remove = new List<string>(game.search_Eliminated_Pawn((Square)sender, board));

                    foreach (string pawn_Name in pawn_To_Remove)
                    {
                        board[pawn_Name].Clear_Image();
                    }

                    if (game.is_Over(board))
                    {

                        frm_Confirmation_Game_Over game_Over_Confirmation;

                        try
                        {
                            //Updating victory banner and database stats
                            if (game.Current_Player.Role == Player_Role.Attacker)
                            {
                                game_Over_Confirmation = new frm_Confirmation_Game_Over("Victoire des attaquants");

                                if (game_Over_Confirmation.ShowDialog(this) == DialogResult.OK)
                                {
                                    lbl_Game_Over_Winner.Text = "Victoire des attaquants";
                                    db_Link.Add_Victory(game.Attacker.Name, game.Attacker.Role);
                                    db_Link.Add_Defeat(game.Defender.Name, game.Defender.Role);
                                }
                            }
                            else
                            {
                                game_Over_Confirmation = new frm_Confirmation_Game_Over("Victoire des défenseurs");

                                if (game_Over_Confirmation.ShowDialog(this) == DialogResult.OK)
                                {
                                    lbl_Game_Over_Winner.Text = "Victoire des défenseurs";
                                    db_Link.Add_Victory(game.Defender.Name, game.Defender.Role);
                                    db_Link.Add_Defeat(game.Attacker.Name, game.Attacker.Role);
                                }
                            }

                            game_Over_Confirmation.Dispose();
                        }
                        catch (MySqlException ex)
                        {
                            if (ex.Number == 1042 | ex.Number == 0)
                            {
                               MessageBox.Show("Connexion à la base de données impossible.\nLes profils n'ont pas été modifiés.","Erreur");
                            }
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
                    lbl_Game_Current_Player.Text = game.Current_Player.Name;
                }
            }
        }

        /// <summary>
        /// Leaves the game, doesn't save anything and brings back the main menu after raising confirmation form.
        /// </summary>
        /// <param name="sender">The "Menu" button in game.</param>
        /// <param name="e">Contains informations about the raised "click" event.</param>
        private void pic_Game_Menu_Click(object sender, EventArgs e)
        {
            play_Sound_Click();

            frm_Confirmation confirmation = new frm_Confirmation("Voulez-vous vraiment retourner au menu principal? Aucun changement ne sera enregistré.");

            if (confirmation.ShowDialog(this) == DialogResult.OK)
            {
                pnl_Game.Visible = false;
                pnl_Menu.Visible = true;

                reset_Game();

                is_In_Game = false;
                sound_Ended(music_Player, EventArgs.Empty); //Force sound switching
            }

            confirmation.Dispose();
        }

        /// <summary>
        /// Leaves the game without saving and close the application after raising confirmation form.
        /// </summary>
        /// <param name="sender">The "Leave" button in game.</param>
        /// <param name="e">Contains informations about the raised "click" event.</param>
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
        /// Resets the game for a new game after returning to the menu
        /// or ending a game.
        /// </summary>
        private void reset_Game()
        {
            board.Clear();
            tlp_Game_Board.Controls.Clear();
        }

        #endregion Game

        #region Game_Over
        ////////////////////////////////////
        //           Game Over            //
        ////////////////////////////////////

        /// <summary>
        /// Leaves the game over screen and returns to the main menu.
        /// </summary>
        /// <param name="sender">The  "Menu" button in game over screen.</param>
        /// <param name="e">Contains informations about the raised "click" event.</param>
        private void pic_Game_Over_Menu_Click(object sender, EventArgs e)
        {
            play_Sound_Click();

            pnl_Game_Over.Visible = false;
            pnl_Menu.Visible = true;

            is_In_Game = false;
            sound_Ended(music_Player, EventArgs.Empty); //Force sound switching
        }

        #endregion Game_Over

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
        /// <param name="sender">The control that called the function.</param>
        /// <param name="e">Contains informations about the raised "mouse enter" event.</param>
        private void play_Sound_Enter(object sender, EventArgs e)
        {
            sound_Player = new System.Media.SoundPlayer(Tablut.Properties.Resources.Menu_Move);
            sound_Player.Play();
        }

        /// <summary>
        /// Switch the game sounds between menu or in game.
        /// </summary>
        /// <param name="sender">The music_Player object</param>
        /// <param name="e">Details about the event raised</param>
        private void sound_Ended(object sender, EventArgs e)
        {
            if (is_In_Game == false)
            {
                sound_Path = System.IO.Path.Combine(root_Location, @"Content\Menu_Fantasy.wav");
            }
            else
            {
                sound_Path = game_Sound_Switch();
            }
            music_Player.Open(new Uri(sound_Path));
            music_Player.Play();
        }

        /// <summary>
        /// Gets a random game track.
        /// </summary>
        /// <returns>Returns the path of the track to play.</returns>
        private string game_Sound_Switch()
        {
            Random random = new Random();
            int track = random.Next(1, 3);
            string track_Path = "";

            switch (track)
            {
                case 1:
                    track_Path = System.IO.Path.Combine(root_Location, @"Content\IG_Nordic_Landscape.wav");
                    break;
                case 2:
                    track_Path = System.IO.Path.Combine(root_Location, @"Content\IG_Nordic_Title.wav");
                    break;
                case 3:
                    track_Path = System.IO.Path.Combine(root_Location, @"Content\IG_Village.wav");
                    break;
            }
            return track_Path;
        }

        /// <summary>
        /// Mute the sound or unmute.
        /// </summary>
        /// <param name="sender">The "Speaker" button on the bottom right corner of the application.</param>
        /// <param name="e">Details about the Click event</param>
        private void pic_Speaker_Click(object sender, EventArgs e)
        {
            play_Sound_Click();

            if (music_Player.Volume > 0)
            {
                music_Player.Volume = 0;
                pic_Speaker.BackgroundImage = Tablut.Properties.Resources.loudspeaker_Mute;
            }
            else
            {
                music_Player.Volume = 0.5;
                pic_Speaker.BackgroundImage = Tablut.Properties.Resources.loudspeaker;
            }
        }
        #endregion Sound_Player
    }
}
