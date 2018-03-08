using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tablut
{
    class Game
    {
        public Player Current_Player { get; private set; }
        public Player Attacker { get; private set; }
        public Player Defender { get; private set; }
        public Game_Phase Phase { get; private set; }
        public Square selected_Pawn { get; private set; }
        public bool Over { get; set; }

        private List<string> possible_Move;

        /// <summary>
        /// Constructor.
        /// Creates the players aswell and sets the first player.
        /// The first player is always the defender.
        /// </summary>
        /// <param name="m_Attacker_Name"></param>
        /// <param name="m_Defender_Name"></param>
        public Game(string m_Attacker_Name, string m_Defender_Name)
        {
            this.Attacker = new Player(m_Attacker_Name, 16, Occupant.Attacker);
            this.Defender = new Player(m_Defender_Name, 9, Occupant.Defender);
            this.Current_Player = Defender;
            this.Phase = Game_Phase.picking;
            this.Over = false;
        }

        /// <summary>
        /// Checks if the selected pawn is owned by the current player.
        /// Then returns all the possible movements to display them.
        /// </summary>
        /// <param name="m_Square"></param>
        /// <param name="m_board"></param>
        /// <returns></returns>
        public List<string> Pawn_Click(Square m_Square, Dictionary<string, Square> m_board)
        {
            possible_Move = new List<string>();

            //If the player doesn't own the pawn, exit without changing game phase.
            //m_Square.occupant != Current_Player.role
            if (m_Square.Occupant == Occupant.King || m_Square.Occupant == Occupant.Defender)
            {
                if(Current_Player.role != Occupant.Defender)
                {
                    return possible_Move;
                }
            }
            else if(m_Square.Occupant == Occupant.Attacker)
            {
                if(Current_Player.role != Occupant.Attacker)
                {
                    return possible_Move;
                }
            }
            else
            {
                return possible_Move;
            }

            this.selected_Pawn = m_Square;

            //Store the position of the square
            int column = int.Parse(m_Square.Name.Substring(m_Square.Name.Length - 2,1));
            int row = int.Parse(m_Square.Name.Substring(m_Square.Name.Length - 1, 1));

            #region Getting available squares
            //Getting the available squares on top
            if(row > 0)
            {
                for(int row_num = row-1; row_num >= 0; row_num--)
                {
                    if(m_board["SQ"+column+""+row_num].Occupant == Occupant.Empty)
                    {
                        possible_Move.Add(m_board["SQ" + column + "" + row_num].Name);
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
                    if (m_board["SQ" + column_num + "" + row].Occupant == Occupant.Empty)
                    {
                        possible_Move.Add(m_board["SQ" + column_num + "" + row].Name);
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
                    if (m_board["SQ" + column + "" + row_num].Occupant == Occupant.Empty)
                    {
                        possible_Move.Add(m_board["SQ" + column + "" + row_num].Name);
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
                    if (m_board["SQ" + column_num + "" + row].Occupant == Occupant.Empty)
                    {
                        possible_Move.Add(m_board["SQ" + column_num + "" + row].Name);
                    }
                    else
                    {
                        //We exit the loop because a pawn can't jump over another.
                        break;
                    }
                }
            }
            #endregion Getting available squares

            //If the selected pawn can't move, doesn't change game phase.
            if(possible_Move.Count() > 0)
            {
                this.Phase = Game_Phase.moving;

                //Only the king can go on the middle square
                if(this.selected_Pawn.Occupant != Occupant.King && possible_Move.Contains("SQ44"))
                {
                    possible_Move.Remove("SQ44");
                }
            }

            return possible_Move;
        }

        /// <summary>
        /// Checks if the player clicks again on the selected pawn, then reset the possible moves.
        /// Then if the player clicks on a possible moves, allow the move.
        /// </summary>
        /// <param name="m_Square"></param>
        /// <param name="m_Possible_Move"></param>
        /// <returns></returns>
        public string Square_Click(Square m_Square, List<string> m_Possible_Move)
        {
            if(m_Square == selected_Pawn)
            {
                this.Phase = Game_Phase.picking;

                return "reset";
            }
            else if(m_Square.Occupant == Occupant.Empty && m_Possible_Move.Contains(m_Square.Name))
            {
                if(Current_Player.role == Occupant.Attacker)
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
        /// <param name="m_Square"></param>
        /// <param name="m_board"></param>
        /// <returns></returns>
        public List<String> search_Eliminated_Pawn(Square m_Square, Dictionary<string, Square> m_board)
        {
            List<string> eliminated_Pawn = new List<string>();

            //Store the position of the square
            int column = int.Parse(m_Square.Name.Substring(m_Square.Name.Length - 2, 1));
            int row = int.Parse(m_Square.Name.Substring(m_Square.Name.Length - 1, 1));

            #region Elimination conditions
            if(Current_Player.role == Occupant.Attacker)
            {
                //checks if there is any adjacent enemy pawn and if the elimination condition if filled.
                //(Surrounded by two pawn vertically or horizontally)
                //Top
                if (row > 1)
                {
                    if (m_board["SQ" + column + "" + (row - 1)].Occupant == Occupant.Defender && m_board["SQ" + column + "" + (row - 2)].Occupant == Occupant.Attacker)
                    {
                        eliminated_Pawn.Add(m_board["SQ" + column + "" + (row - 1)].Name);
                    }
                }
                //Right
                if (column < 7)
                {
                    if (m_board["SQ" + (column+1) + "" + row].Occupant == Occupant.Defender && m_board["SQ" + (column+2) + "" + row ].Occupant == Occupant.Attacker)
                    {
                        eliminated_Pawn.Add(m_board["SQ" + (column + 1) + "" + row].Name);
                    }
                }
                //Bottom
                if (row < 7)
                {
                    if (m_board["SQ" + column + "" + (row + 1)].Occupant == Occupant.Defender && m_board["SQ" + column + "" + (row + 2)].Occupant == Occupant.Attacker)
                    {
                        eliminated_Pawn.Add(m_board["SQ" + column + "" + (row + 1)].Name);
                    }
                }
                //Left
                if (column > 1)
                {
                    if (m_board["SQ" + (column - 1) + "" + row].Occupant == Occupant.Defender && m_board["SQ" + (column - 2) + "" + row].Occupant == Occupant.Attacker)
                    {
                        eliminated_Pawn.Add(m_board["SQ" + (column - 1) + "" + row].Name);
                    }
                }

                //Checking if the King is captured (surrounded by 4 attacker)

                //Getting king positions
                string king_Key = m_board.FirstOrDefault(x => x.Value.Occupant == Occupant.King).Key;
                column = int.Parse(m_board[king_Key].Name.Substring(m_board[king_Key].Name.Length - 2, 1));
                row = int.Parse(m_board[king_Key].Name.Substring(m_board[king_Key].Name.Length - 1, 1));

                //If the king has a sufficient space around him
                if(column > 0 && column < 8 && row > 0 && row < 8)
                {
                    //Checking top && bottom
                    if(m_board["SQ"+column+""+(row+1)].Occupant == Occupant.Attacker && m_board["SQ" + column + "" + (row - 1)].Occupant == Occupant.Attacker)
                    {
                        //Checking right and left
                        if(m_board["SQ"+(column + 1)+""+row].Occupant == Occupant.Attacker && m_board["SQ" + (column - 1) + "" + row].Occupant == Occupant.Attacker)
                        {
                            eliminated_Pawn.Add(m_board["SQ" + column + "" + row].Name);

                            this.Over = true;
                        }
                    }
                }
            }
            else if(Current_Player.role == Occupant.Defender)
            {
                //checks if there is any adjacent enemy pawn and if the elimination condition if filled
                //Top
                if (row > 1)
                {
                    if (m_board["SQ" + column + "" + (row - 1)].Occupant == Occupant.Attacker && m_board["SQ" + column + "" + (row - 2)].Occupant == Occupant.Defender)
                    {
                        eliminated_Pawn.Add(m_board["SQ" + column + "" + (row - 1)].Name);
                    }
                }
                //Right
                if (column < 7)
                {
                    if (m_board["SQ" + (column + 1) + "" + row].Occupant == Occupant.Attacker && m_board["SQ" + (column + 2) + "" + row].Occupant == Occupant.Defender)
                    {
                        eliminated_Pawn.Add(m_board["SQ" + (column + 1) + "" + row].Name);
                    }
                }
                //Bottom
                if (row < 7)
                {
                    if (m_board["SQ" + column + "" + (row + 1)].Occupant == Occupant.Attacker && m_board["SQ" + column + "" + (row + 2)].Occupant == Occupant.Defender)
                    {
                        eliminated_Pawn.Add(m_board["SQ" + column + "" + (row + 1)].Name);
                    }
                }
                //Left
                if (column > 1)
                {
                    if (m_board["SQ" + (column - 1) + "" + row].Occupant == Occupant.Attacker && m_board["SQ" + (column - 2) + "" + row].Occupant == Occupant.Defender)
                    {
                        eliminated_Pawn.Add(m_board["SQ" + (column - 1) + "" + row].Name);
                    }
                }
            }
            #endregion Elimination conditions

            //Update players statistics
            if(eliminated_Pawn.Count > 0)
            {
                Current_Player.add_Enemy_Pawn_Eliminated(eliminated_Pawn.Count);

                if(Current_Player.role == Occupant.Attacker)
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
        /// If a name (the winner's one) is returned, the game is over.
        /// </summary>
        /// <param name="m_board"></param>
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
            if (this.Current_Player.role == Occupant.Attacker)
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
