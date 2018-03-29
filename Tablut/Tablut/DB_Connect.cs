using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MySql.Data.MySqlClient;
using System.Text.RegularExpressions;

namespace Tablut
{
    /// <summary>
    /// This class connects to the MySQL database to interract
    /// with the different profiles. The functions work mainly with
    /// profile's names since they are unique in the database.
    /// </summary>
    class DB_Connect
    {

        /// <summary>
        /// Connection with the database
        /// </summary>
        private MySqlConnection connection;

        /// <summary>
        /// Constructor. Calls the connexion initializing function.
        /// </summary>
        public DB_Connect()
        {
            this.Init_Connecton();
        }

        /// <summary>
        /// Initializes the connexion with the database.
        /// </summary>
        private void Init_Connecton()
        {
            //Creating the connection
            string connection_String = "SERVER=127.0.0.1; DATABASE=tablut; UID=Tablut_User; PASSWORD=T@blutI5gr8";
            this.connection = new MySqlConnection(connection_String);
        }

        /// <summary>
        /// Checks if the name is syntactically correct.
        /// Then adds the profile in the database.
        /// </summary>
        /// <param name="m_Profile_Name">Value from the text box in the profile creation menu.</param>
        public void Add_Profile(string m_Profile_Name)
        {
            //Verifying the syntax of the name
            Regex rgx = new Regex(@"^[a-zA-Z0-9_]+$");
            if (!rgx.IsMatch(m_Profile_Name))
            {
                this.connection.Close();
                throw new Exception_Invalid_Name("Le nom peut contenir les lettres allant de A-Z, a-z et '_'");
            }

            //Checking if connection not already opened
            if (connection != null && connection.State == System.Data.ConnectionState.Closed)
            {
                //Opens the SQL connection
                this.connection.Open();
            }
            
            //Creating the query
            MySqlCommand cmd = this.connection.CreateCommand();
            cmd.CommandText = "INSERT INTO Tablut.profile (Name) VALUES (@m_Name)";

            //Inserting the parameter
            cmd.Parameters.AddWithValue("@m_Name", m_Profile_Name);

            //Executing query
            cmd.ExecuteNonQuery();

            //Closes connection
            this.connection.Close();
        }

        /// <summary>
        /// Returns all the profile's name from the database
        /// </summary>
        public List<string> get_Profile_Name()
        {
            //Checking if connection not already opened
            if (connection != null && connection.State == System.Data.ConnectionState.Closed)
            {
                //Opening the SQL connection
                this.connection.Open();
            }

            //Creating the query
            MySqlCommand cmd = this.connection.CreateCommand();
            cmd.CommandText = "SELECT Name FROM Tablut.Profile ORDER BY Name";

            //To store the result of the querry
            List<string> db_Profile_Name = new List<String>();

            //Executing query and putting result in list
            MySqlDataReader reader;
            reader = cmd.ExecuteReader();
            try
            {
                while (reader.Read())
                {
                    db_Profile_Name.Add(reader.GetString(0));
                }
            }
            finally
            {
                //Closes connections
                reader.Close();
                this.connection.Close();
            }

            return db_Profile_Name;
        }

        /// <summary>
        /// For a given profile, returns all his statistics.
        /// </summary>
        /// <param name="m_Profile_Name">Value from the drop down list in the profile managment menu.</param>
        /// <returns>Int array containing the number of [0] attacks won, [1] attacks lost, [2] defence won, [3] Defence lost.</returns>
        public int[] get_Profile_Stats(string m_Profile_Name)
        {
            //Checking if connection not already opened
            if (connection != null && connection.State == System.Data.ConnectionState.Closed)
            {
                //Opening the SQL connection
                this.connection.Open();
            }

            //Creating the query
            MySqlCommand cmd = this.connection.CreateCommand();
            cmd.CommandText = "SELECT Won_Attack, Lost_Attack, Won_Defence, Lost_Defence FROM Tablut.Profile WHERE Name = @m_Name LIMIT 0,1";

            //Inserting the parameter
            cmd.Parameters.AddWithValue("@m_Name", m_Profile_Name);

            //To store the result of the querry
            int[] db_Profile_Stats = new int[4];

            //Executing query and putting result in list
            MySqlDataReader reader;
            reader = cmd.ExecuteReader();
            try
            {
                if (reader.Read())
                {
                    db_Profile_Stats[0] = reader.GetInt32("Won_Attack");
                    db_Profile_Stats[1] = reader.GetInt32("Lost_Attack");
                    db_Profile_Stats[2] = reader.GetInt32("Won_Defence");
                    db_Profile_Stats[3] = reader.GetInt32("Lost_Defence");
                    
                }
            }
            finally
            {
                //Closes connections
                reader.Close();
                this.connection.Close();
            }

            return db_Profile_Stats;
        }

        /// <summary>
        /// Deletes the given profile in the database.
        /// </summary>
        /// <param name="m_Profile_Name">Value from the drop down list in the profile managment menu.</param>
        public void Remove_Profile(string m_Profile_Name)
        {
            //Checking if connection not already opened
            if (connection != null && connection.State == System.Data.ConnectionState.Closed)
            {
                //Opening the SQL connection
                this.connection.Open();
            }

            //Creating the query
            MySqlCommand cmd = this.connection.CreateCommand();
            cmd.CommandText = "DELETE FROM Tablut.profile WHERE Name = (@m_Name)";

            //Inserting the parameter
            cmd.Parameters.AddWithValue("@m_Name", m_Profile_Name);

            //Executes query
            cmd.ExecuteNonQuery();

            //Closes connection
            this.connection.Close();
        }

        /// <summary>
        /// Reset to 0 all statistics of the given profile.
        /// </summary>
        /// <param name="m_Profile_Name">Value from the drop down list in the profile managment menu.</param>
        public void Reset_Profile(string m_Profile_Name)
        {
            //Checking if connection not already opened
            if (connection != null && connection.State == System.Data.ConnectionState.Closed)
            {
                //Opening the SQL connection
                this.connection.Open();
            }

            //Creating the query
            MySqlCommand cmd = this.connection.CreateCommand();
            cmd.CommandText = "UPDATE Tablut.profile SET Won_Attack=0, Lost_Attack=0, Won_Defence=0, Lost_Defence=0 WHERE Name = (@m_Name)";

            //Inserting the parameter
            cmd.Parameters.AddWithValue("@m_Name", m_Profile_Name);

            //Executes query
            cmd.ExecuteNonQuery();

            //Closes connection
            this.connection.Close();
        }

        /// <summary>
        /// Renames the given profile.
        /// </summary>
        /// <param name="m_New_Name"> Value from the text box in the renaming form. (New name)</param>
        /// <param name="m_Profile_Name">Value from the drop down list in the profile managment menu. (Old name)</param>
        public void Rename_Profile(string m_New_Name, string m_Profile_Name)
        {
            //Verifying the syntax of the name
            Regex rgx = new Regex(@"^[a-zA-Z0-9_]+$");
            if (!rgx.IsMatch(m_New_Name))
            {
                this.connection.Close();
                throw new Exception_Invalid_Name("Le nom peut contenir les lettres allant de A-Z, a-z et '_'");
            }

            //Checking if connection not already opened
            if (connection != null && connection.State == System.Data.ConnectionState.Closed)
            {
                //Opening the SQL connection
                this.connection.Open();
            }

            //Creating the query
            MySqlCommand cmd = this.connection.CreateCommand();
            cmd.CommandText = "UPDATE Tablut.profile SET Name = (@m_New_Name) WHERE Name=(@m_Name)";

            //Inserting the parameter
            cmd.Parameters.AddWithValue("@m_New_Name", m_New_Name);
            cmd.Parameters.AddWithValue("@m_Name", m_Profile_Name);

            //Executes query
            cmd.ExecuteNonQuery();

            //Closes connection
            this.connection.Close();
        }

        /// <summary>
        /// Adds a victory to the given player for his previous role.
        /// </summary>
        /// <param name="m_Profile_Name">The winner of the game</param>
        /// <param name="m_Role">His role: attacker or defender</param>
        public void Add_Victory(string m_Profile_Name, Player_Role m_Role)
        {
            //Checking if connection not already opened
            if (connection != null && connection.State == System.Data.ConnectionState.Closed)
            {
                //Opening the SQL connection
                this.connection.Open();
            }

            //Creating the querry
            MySqlCommand cmd = this.connection.CreateCommand();

            if (m_Role == Player_Role.Attacker)
            {
                cmd.CommandText = "UPDATE Tablut.profile SET Won_Attack = Won_Attack + 1 WHERE Name=(@m_Name)";
            }
            else
            {
                cmd.CommandText = "UPDATE Tablut.profile SET Won_Defence = Won_Defence + 1 WHERE Name=(@m_Name)";
            }

            //Inserting the parameter
            cmd.Parameters.AddWithValue("@m_Name", m_Profile_Name);
            
            //Executes query
            cmd.ExecuteNonQuery();

            //Closes connection
            this.connection.Close();
        }

        /// <summary>
        /// Adds a defeat to the given player for his previous role.
        /// </summary>
        /// <param name="m_Profile_Name">The loser of the game</param>
        /// <param name="m_Role">His role: attacker or defender</param>
        public void Add_Defeat(string m_Profile_Name, Player_Role m_Role)
        {
            //Checking if connection not already opened
            if (connection != null && connection.State == System.Data.ConnectionState.Closed)
            {
                //Opening the SQL connection
                this.connection.Open();
            }

            //Creating the querry
            MySqlCommand cmd = this.connection.CreateCommand();

            if (m_Role == Player_Role.Attacker)
            {
                cmd.CommandText = "UPDATE Tablut.profile SET Lost_Attack = Lost_Attack + 1 WHERE Name=(@m_Name)";
            }
            else
            {
                cmd.CommandText = "UPDATE Tablut.profile SET Lost_Defence = Lost_Defence + 1 WHERE Name=(@m_Name)";
            }

            //Inserting the parameter
            cmd.Parameters.AddWithValue("@m_Name", m_Profile_Name);

            //Executes query
            cmd.ExecuteNonQuery();

            //Closes connection
            this.connection.Close();
        }
    }
}
