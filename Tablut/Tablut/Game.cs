using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tablut
{
    /// <summary>
    /// Class containing all the game logic.
    /// </summary>
    class Game
    {
        /// <summary>
        /// The player who has to play.
        /// </summary>
        public Player Current_Player { get; private set; }

        /// <summary>
        /// The attacker player object.
        /// </summary>
        public Player Attacker { get; private set; }

        /// <summary>
        /// The defender player object.
        /// </summary>
        public Player Defender { get; private set; }

        /// <summary>
        /// Store the game phase of the current player.
        /// </summary>
        public Game_Phase Phase { get; private set; }

        /// <summary>
        /// Store the selected pawn.
        /// </summary>
        public Square selected_Pawn { get; private set; }

        /// <summary>
        /// Is the game over ?
        /// </summary>
        public bool Over { get; set; }

        /// <summary>
        /// List containing all possible square to move the selected pawn.
        /// </summary>
        private List<string> possible_Move; 

        /// <summary>
        /// Constructor. It creates the two players and sets the game.
        /// The first player is always the defender.
        /// </summary>
        /// <param name="m_Attacker_Name"> Attacker's name from player selection attacker drop down list.</param>
        /// <param name="m_Defender_Name"> Defender's name from player selection defender drop down list.</param>
        public Game(string m_Attacker_Name, string m_Defender_Name)
        {
            this.Attacker = new Player(m_Attacker_Name, 16, Player_Role.Attacker);
            this.Defender = new Player(m_Defender_Name, 9, Player_Role.Defender);
            this.Current_Player = Defender;
            this.Phase = Game_Phase.picking;
            this.Over = false;
        }

        /// <summary>
        /// Checks if the selected square contains a pawn and if the pawn is owned by the current player.
        /// </summary>
        /// <param name="m_Square">The clicked square.</param>
        /// <param name="m_board">Dictionnary containing the actual board state.</param>
        /// <returns> All the possible movements possible for the selected square </returns>
        public List<string> Pawn_Click(Square m_Square, Dictionary<string, Square> m_board)
        {
            possible_Move = new List<string>();

            //If the player doesn't own the pawn, exit without changing game phase.
            if (m_Square.Occupant == Occupant.King || m_Square.Occupant == Occupant.Defender)
            {
                if(Current_Player.Role != Player_Role.Defender)
                {
                    return possible_Move;
                }
            }
            else if(m_Square.Occupant == Occupant.Attacker)
            {
                if(Current_Player.Role != Player_Role.Attacker)
                {
                    return possible_Move;
                }
            }
            else
            {
                return possible_Move;
            }

            this.selected_Pawn = m_Square;

            //Stores the position of the square from 0 to 8
            int column = int.Parse(m_Square.Name.Substring(m_Square.Name.Length - 2,1));
            int row = int.Parse(m_Square.Name.Substring(m_Square.Name.Length - 1, 1));

            #region Getting available squares
            //Getting the available squares on top
            if(row > 0)
            {
                for(int row_num = row-1; row_num >= 0; row_num--)
                {
                    if(m_board[column+""+row_num].Occupant == Occupant.Empty)
                    {
                        possible_Move.Add(m_board[column + "" + row_num].Name);
                    }
                    else
                    {
                        //We exit the loop because a pawn can't jump over another.
                        break;
                    }
                }
            }
            //Getting the available squares on right
            if (column < 8)
            {
                for (int column_num = column + 1; column_num <= 8; column_num++)
                {
                    if (m_board[column_num + "" + row].Occupant == Occupant.Empty)
                    {
                        possible_Move.Add(m_board[column_num + "" + row].Name);
                    }
                    else
                    {
                        //We exit the loop because a pawn can't jump over another.
                        break;
                    }
                }
            }
            //Getting the available squares on bottom
            if (row < 8)
            {
                for (int row_num = row + 1; row_num <= 8; row_num++)
                {
                    if (m_board[column + "" + row_num].Occupant == Occupant.Empty)
                    {
                        possible_Move.Add(m_board[column + "" + row_num].Name);
                    }
                    else
                    {
                        //We exit the loop because a pawn can't jump over another.
                        break;
                    }
                }
            }
            //Getting the available squares on left
            if (column > 0)
            {
                for (int column_num = column - 1; column_num >= 0; column_num--)
                {
                    if (m_board[column_num + "" + row].Occupant == Occupant.Empty)
                    {
                        possible_Move.Add(m_board[column_num + "" + row].Name);
                    }
                    else
                    {
                        //We exit the loop because a pawn can't jump over another.
                        break;
                    }
                }
            }
            #endregion Getting available squares

            //If the selected pawn can move, change game phase.
            if(possible_Move.Count() > 0)
            {
                this.Phase = Game_Phase.moving;

                //Only the king can go on the middle square
                if(this.selected_Pawn.Occupant != Occupant.King && possible_Move.Contains("44"))
                {
                    possible_Move.Remove("44");
                }
            }

            return possible_Move;
        }

        /// <summary>
        /// Checks if the player clicks again on the selected pawn, then reset the possible moves.
        /// Then if the player clicks on a possible moves, allow the move.
        /// </summary>
        /// <param name="m_Square"> The clicked square.</param>
        /// <param name="m_Possible_Move"> The list containing all the possible moves.</param>
        /// <returns>Return an string explaining to the view how to update the board
        /// reset: the selected pawn is clicked again. The player can change the pawn he wants to move.
        /// move: the plaver clicked a valid square.
        /// invalide: the player clicked a wrong square.
        /// </returns>
        public string Square_Click(Square m_Square, List<string> m_Possible_Move)
        {
            if(m_Square == selected_Pawn)
            {
                this.Phase = Game_Phase.picking;

                return "reset";
            }
            else if(m_Square.Occupant == Occupant.Empty && m_Possible_Move.Contains(m_Square.Name))
            {
                if(Current_Player.Role == Player_Role.Attacker)
                {
                    Attacker.add_Moves();
                }
                else
                {
                    Defender.add_Moves();
                }

                //Change the game phase
                this.Phase = Game_Phase.picking;

                return "move";
            }

            return "invalid";
        }

        /// <summary>
        /// Checks if any pawn must be eliminated.
        /// </summary>
        /// <param name="m_Square">The clicked square (new position of a moved pawn). </param>
        /// <param name="m_board">Dictionnary containing the actual board state.</param>
        /// <returns> The list of the eliminated pawns.</returns>
        public List<String> search_Eliminated_Pawn(Square m_Square, Dictionary<string, Square> m_board)
        {
            List<string> eliminated_Pawn = new List<string>();

            //Store the position of the square.
            int column = int.Parse(m_Square.Name.Substring(m_Square.Name.Length - 2, 1));
            int row = int.Parse(m_Square.Name.Substring(m_Square.Name.Length - 1, 1));

            #region Elimination conditions
            if(Current_Player.Role == Player_Role.Attacker)
            {
                //checks if there is any adjacent enemy pawn and if the elimination condition if filled.
                //(Surrounded by two pawns vertically or horizontally)
                //Top
                if (row > 1)
                {
                    if (m_board[column + "" + (row - 1)].Occupant == Occupant.Defender && m_board[column + "" + (row - 2)].Occupant == Occupant.Attacker)
                    {
                        eliminated_Pawn.Add(m_board[column + "" + (row - 1)].Name);
                    }
                }
                //Right
                if (column < 7)
                {
                    if (m_board[(column+1) + "" + row].Occupant == Occupant.Defender && m_board[(column+2) + "" + row ].Occupant == Occupant.Attacker)
                    {
                        eliminated_Pawn.Add(m_board[(column + 1) + "" + row].Name);
                    }
                }
                //Bottom
                if (row < 7)
                {
                    if (m_board[column + "" + (row + 1)].Occupant == Occupant.Defender && m_board[column + "" + (row + 2)].Occupant == Occupant.Attacker)
                    {
                        eliminated_Pawn.Add(m_board[column + "" + (row + 1)].Name);
                    }
                }
                //Left
                if (column > 1)
                {
                    if (m_board[(column - 1) + "" + row].Occupant == Occupant.Defender && m_board[(column - 2) + "" + row].Occupant == Occupant.Attacker)
                    {
                        eliminated_Pawn.Add(m_board[(column - 1) + "" + row].Name);
                    }
                }

                //Checking if the King is captured (surrounded by 4 attacker)

                //Getting king positions
                string king_Key = m_board.FirstOrDefault(x => x.Value.Occupant == Occupant.King).Key;
                column = int.Parse(m_board[king_Key].Name.Substring(m_board[king_Key].Name.Length - 2, 1));
                row = int.Parse(m_board[king_Key].Name.Substring(m_board[king_Key].Name.Length - 1, 1));

                //If the king has a sufficient space around him to be surrounded
                if(column > 0 && column < 8 && row > 0 && row < 8)
                {
                    //Checking top && bottom
                    if(m_board[column+""+(row+1)].Occupant == Occupant.Attacker && m_board[column + "" + (row - 1)].Occupant == Occupant.Attacker)
                    {
                        //Checking right and left
                        if(m_board[(column + 1)+""+row].Occupant == Occupant.Attacker && m_board[(column - 1) + "" + row].Occupant == Occupant.Attacker)
                        {
                            eliminated_Pawn.Add(m_board[column + "" + row].Name);

                            this.Over = true;
                        }
                    }
                }
            }
            else if(Current_Player.Role == Player_Role.Defender)
            {
                //checks if there is any adjacent enemy pawn and if the elimination condition if filled
                //Top
                if (row > 1)
                {
                    if (m_board[column + "" + (row - 1)].Occupant == Occupant.Attacker && (m_board[column + "" + (row - 2)].Occupant == Occupant.Defender|| m_board[column + "" + (row - 2)].Occupant == Occupant.King))
                    {
                        eliminated_Pawn.Add(m_board[column + "" + (row - 1)].Name);
                    }
                }
                //Right
                if (column < 7)
                {
                    if (m_board[(column + 1) + "" + row].Occupant == Occupant.Attacker && (m_board[(column + 2) + "" + row].Occupant == Occupant.Defender || m_board[(column + 2) + "" + row].Occupant == Occupant.King))
                    {
                        eliminated_Pawn.Add(m_board[(column + 1) + "" + row].Name);
                    }
                }
                //Bottom
                if (row < 7)
                {
                    if (m_board[column + "" + (row + 1)].Occupant == Occupant.Attacker && (m_board[column + "" + (row + 2)].Occupant == Occupant.Defender || m_board[column + "" + (row + 2)].Occupant == Occupant.King))
                    {
                        eliminated_Pawn.Add(m_board[column + "" + (row + 1)].Name);
                    }
                }
                //Left
                if (column > 1)
                {
                    if (m_board[(column - 1) + "" + row].Occupant == Occupant.Attacker && (m_board[(column - 2) + "" + row].Occupant == Occupant.Defender || m_board[(column - 2) + "" + row].Occupant == Occupant.King))
                    {
                        eliminated_Pawn.Add(m_board[(column - 1) + "" + row].Name);
                    }
                }
            }
            #endregion Elimination conditions

            //Update players statistics
            if(eliminated_Pawn.Count > 0)
            {
                Current_Player.add_Enemy_Pawn_Eliminated(eliminated_Pawn.Count);

                if(Current_Player.Role == Player_Role.Attacker)
                {
                    Defender.remove_Pawn(eliminated_Pawn.Count);
                }
                else
                {
                    Attacker.remove_Pawn(eliminated_Pawn.Count);
                }
            }
  
            return eliminated_Pawn;
        }

        /// <summary>
        /// Checks if the game is over.
        /// </summary>
        /// <param name="m_board"> Dictionnary containing the actual board state.</param>
        /// <returns> if false the games keep going.</returns>
        public bool is_Over(Dictionary<string, Square> m_board)
        {
            //Over == if king is eliminated. It is set in the 'search_Eliminated_Pawn' function.
            if (Over)
            {
                return true;
            }

            //If the attacker doesn't have pawn left
            if (Defender.Pawn_Left <= 0)
            {
                return true;
            }

            //if the king succed escaping
            //Getting king positions
            string king_Key = m_board.FirstOrDefault(x => x.Value.Occupant == Occupant.King).Key;
            string king_Pos = m_board[king_Key].Name.Substring(m_board[king_Key].Name.Length - 2, 2);
            
            if (king_Pos == "00" || king_Pos == "80" || king_Pos == "08" || king_Pos == "88")
            {
                return true;
            }

            //End of turn, change current player
            if (this.Current_Player.Role == Player_Role.Attacker)
            {
                this.Current_Player = this.Defender;
            }
            else
            {
                this.Current_Player = this.Attacker;
            }

            return false;
        }
    }
}
