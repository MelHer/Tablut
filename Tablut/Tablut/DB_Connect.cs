using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MySql.Data.MySqlClient;
using System.Text.RegularExpressions;

namespace Tablut
{
    class DB_Connect
    {
        private MySqlConnection connection;

        //Constructor
        public DB_Connect()
        {
            this.Init_Connecton();
        }

        /// <summary>
        /// Method to open a connection with our Database.
        /// </summary>
        private void Init_Connecton()
        {
            //Creating the connection
            string connection_String = "SERVER=127.0.0.1; DATABASE=tablut; UID=root; PASSWORD=";
            this.connection = new MySqlConnection(connection_String);
        }

        /// <summary>
        /// Check if the name is syntactically correct.
        /// Then add the profile in the database.
        /// </summary>
        /// <param name="m_Profile_Name"></param>
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
                //Opening the SQL connection
                this.connection.Open();
            }

            //Creating the querry
            MySqlCommand cmd = this.connection.CreateCommand();
            cmd.CommandText = "INSERT INTO profile (Name) VALUES (@m_Name)";

            //Inserting the parameter
            cmd.Parameters.AddWithValue("@m_Name", m_Profile_Name);

            //Execute query
            cmd.ExecuteNonQuery();

            //Close connection
            this.connection.Close();
        }

        /// <summary>
        /// Return all the profile's name from the database
        /// </summary>
        public List<string> get_Profile_Name()
        {
            //Checking if connection not already opened
            if (connection != null && connection.State == System.Data.ConnectionState.Closed)
            {
                //Opening the SQL connection
                this.connection.Open();
            }

            //Creating the querry
            MySqlCommand cmd = this.connection.CreateCommand();
            cmd.CommandText = "SELECT Name FROM Tablut.Profile ORDER BY Name";

            //To store the result of the querry
            List<string> db_Name = new List<String>();

            //Executing query and putting result in list
            MySqlDataReader reader;
            reader = cmd.ExecuteReader();
            try
            {
                while (reader.Read())
                {
                    db_Name.Add(reader.GetString(0));
                }
            }
            finally
            {
                //Close connections
                reader.Close();
                this.connection.Close();
            }

            return db_Name;
        }
    }
}
