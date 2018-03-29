/// ETML
/// Auteur      : Sylvain Laydernier
/// Date        : 15.03.2018
/// Description : Model class, linked with controller

using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace RandomAnimals
{
    public class Model
    {
        private Controller appController;
        private MySqlConnection connection;
        private string serverLocation;
        private string databaseName;
        private string uID;
        private string password;
        private string message;
        public string Message
        {
            get
            {
                return message;
            }

            set
            {
                message = value;
            }
        }

        /// <summary>
        /// Constructor, call Initialize() to create connection string
        /// and SetRandomData() to insert random data in DB
        /// </summary>
        /// <param name="anyController">application controller</param>
        public Model(Controller anyController)
        {
            appController = anyController;
            appController.AppModel = this;
            Initialize();
            appController.SetRandomData();
        }

        /// <summary>
        /// Initialize values and create connection string and a MySqlConnection object
        /// </summary>
        private void Initialize()
        {
            serverLocation = "localhost";
            databaseName = "db_animals";
            uID = "root";
            password = "root";
            string connectionString = "SERVER=" + serverLocation + ";" + "DATABASE=" +
            databaseName + ";" + "UID=" + uID + ";" + "PASSWORD=" + password + ";";
            connection = new MySqlConnection(connectionString);
        }

        /// <summary>
        /// open connection to database
        /// </summary>
        /// <returns>is db connected value</returns>
        private bool OpenConnection()
        {
            // Try to connect to DB
            try
            {
                connection.Open();
                message = "Successfully connected to the Data Base.";
                return true;
            }
            // Catch connection error
            catch (MySqlException exception)
            {
                // Error handler, get different message for most common error numbers
                // 0        : Cannot connect to server
                // 1045     : Invalid user name and/or password
                // default  : For all others errors
                switch (exception.Number)
                {
                    case 0:
                        message = "Cannot connect to server, please contact administrator";
                        break;

                    case 1045:
                        message = "Invalid username/password, please try again";
                        break;

                    default:
                        message = exception.Message;
                        break;
                }
                return false;
            }
        }

        /// <summary>
        /// Close connection
        /// </summary>
        /// <returns>is connection closed value</returns>
        private bool CloseConnection()
        {
            // Try to close connection
            try
            {
                connection.Close();
                return true;
            }
            // Catch error
            catch (MySqlException exception)
            {
                message = exception.Message;
                return false;
            }
        }

        /// <summary>
        /// Insert data into t_animals and set status message
        /// </summary>
        /// <param name="name">data to insert in t_animals"</param>
        /// <returns>message operation success or failed</returns>
        public void Insert(string name, DateTime dateTime)
        {
            string query = "INSERT INTO t_animals(aniName,aniDateTime) VALUES (@aniName, @aniDateTime)";

            // Open connection, If connection fails, return error message
            if (this.OpenConnection() == true)
            {
                // create command with query and connection
                MySqlCommand cmd = new MySqlCommand(query, connection);

                // Define parameters and set its values
                cmd.Parameters.AddWithValue("@aniName", name);
                cmd.Parameters.AddWithValue("@aniDateTime", dateTime);

                // Execute command
                cmd.ExecuteNonQuery();

                // if connection closed successfully, message = success
                if (this.CloseConnection() == true)
                {
                    message = "Data inserted successfully !";
                }
                // else message = Error
                else
                {
                    message = "Error";
                }
            }
        }

        /// <summary>
        /// Update name data in t_animals and set status message
        /// </summary>
        /// <param name="newValue">new name to update</param>
        /// <param name="idToUpdate">id of the data to update</param>
        public void Update(string newValue, int idToUpdate)
        {
            string query = "UPDATE t_animals SET aniName = @newValue WHERE idAnimals=@idAnimals";

            // Open connection
            if (this.OpenConnection() == true)
            {
                // create command with query and connection
                MySqlCommand cmd = new MySqlCommand(query, connection);

                // Define parameter and set its value
                cmd.Parameters.AddWithValue("@newValue", newValue);
                cmd.Parameters.AddWithValue("@idAnimals", idToUpdate);

                // Execute command
                cmd.ExecuteNonQuery();

                // if connection closed successfully, message = success
                if (this.CloseConnection() == true)
                {
                    message = "Name updated successfully !";
                }
                // else message = error
                else
                {
                    message = "Error";
                }
            }
        }

        /// <summary>
        /// Update datetime data in t_animals and set status message
        /// </summary>
        /// <param name="newValue">new name to update</param>
        /// <param name="idToUpdate">id of the data to update</param>
        public void Update(DateTime newValue, int idToUpdate)
        {
            string query = "UPDATE t_animals SET aniDateTime = @newValue WHERE idAnimals=@idAnimals";

            // Open connection
            if (this.OpenConnection() == true)
            {
                // create command with query and connection
                MySqlCommand cmd = new MySqlCommand(query, connection);

                // Define parameter and set its value
                cmd.Parameters.AddWithValue("@newValue", newValue);
                cmd.Parameters.AddWithValue("@idAnimals", idToUpdate);

                // Execute command
                cmd.ExecuteNonQuery();

                // if connection closed successfully, message = success
                if (this.CloseConnection() == true)
                {
                    message = "Date and time updated successfully !";
                }
                // else message = error                  
                else
                {
                    message = "Error";
                }
            }
        }

        /// <summary>
        /// Delete statement, set status message
        /// </summary>
        /// <param name="idToDelete">id of the data to delete"</param>
        public void Delete(int idToDelete)
        {
            string query = "DELETE FROM t_animals WHERE idAnimals=@idAnimals";

            // Open connection
            if (this.OpenConnection() == true)
            {
                // create command with query and connection
                MySqlCommand cmd = new MySqlCommand(query, connection);

                // Define parameter and set its value
                cmd.Parameters.AddWithValue("@idAnimals", idToDelete);

                // Execute command
                cmd.ExecuteNonQuery();

                // if connection closed successfully, message = success
                if (this.CloseConnection() == true)
                {
                    message = "Data deleted successfully !";
                }
                // else message = error                   
                else
                {
                    message = "Error";
                }
            }
        }

        /// <summary>
        /// Select statement
        /// </summary>
        /// <returns></returns>
        public List<string>[] Select()
        {
            string query = "SELECT * FROM t_animals";

            // Create a list to store results
            List<string>[] fields = new List<string>[3];
            fields[0] = new List<string>();
            fields[1] = new List<string>();
            fields[2] = new List<string>();

            // Open connection, enter if statement when success
            if (this.OpenConnection() == true)
            {
                // Create command
                MySqlCommand cmd = new MySqlCommand(query, connection);

                // Create data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    fields[0].Add(Convert.ToString(dataReader["idAnimals"]));
                    fields[1].Add(Convert.ToString(dataReader["aniName"]));
                    fields[2].Add(Convert.ToString(dataReader["aniDateTime"]));
                }

                // close Data Reader
                dataReader.Close();

                // if connection closed successfully, message = success
                if (this.CloseConnection() == true)
                {
                    message = "Animals";
                }
                // else message = error   
                else
                {
                    message = "Error";
                }

                // Return list to display
                return fields;
            }
            // set error message and return empty list when connection failed
            else
            {
                message = "Connection to data base failed";
                return fields;
            }
        }

        /// <summary>
        /// Set random data in table
        /// </summary>
        public void SetRandomData()
        {
            // initializing animals table
            string[] animals = new string[]
            {
                    "ant",
                    "baboon",
                    "badger",
                    "bat",
                    "bear",
                    "camel",
                    "cat",
                    "crab",
                    "crow",
                    "dog",
                    "dolphin",
                    "fish",
                    "fox",
                    "goat",
                    "gorilla",
                    "lizard",
                    "monkey",
                    "panda",
                    "raccoon",
                    "shark",
                    "tiger",
                    "whale",
                    "zebra"
            };

            // Entries number to create
            const int CHOICES_NBR = 15;

            const int MIN_ARRAY_INDICE = 0;
            const int EXCLUSIVE_UPPER_BOUND = 23;
            const int MIN_YEAR = 2018;
            const int MAX_YEAR_EXCLUSIVE = 2023;
            const int MIN_MONTH = 1;
            const int MAX_MONTH_EXCLUSIVE = 13;
            const int MIN_DAYS = 1;
            const int MIN_HOUR = 0;
            const int MAX_HOUR_EXCLUSIVE = 24;
            const int MIN_MINUTE = 0;
            const int MAX_MINUTE_EXCLUSIVE = 60;
            const int MIN_SECOND = 0;
            const int MAX_SECOND_EXCLUSIVE = 60; ;
            int year;
            int month;
            int days;
            Random rand = new Random();

            // Create X random entries in db, X being CHOICES_NBR
            for (int count = 0; count < CHOICES_NBR; count++)
            {
                // Create random year
                year = rand.Next(MIN_YEAR, MAX_YEAR_EXCLUSIVE);

                // Create random month
                month = rand.Next(MIN_MONTH, MAX_MONTH_EXCLUSIVE);

                // Create random day considering year and month (+1 is for the exclusive limit)
                days = rand.Next(MIN_DAYS, DateTime.DaysInMonth(year, month) + 1);

                // Create date time with randoms date created and the newly created random hour (HH:MM:SS)
                DateTime randomDateTime = new DateTime(year, month, days, rand.Next(MIN_HOUR, MAX_HOUR_EXCLUSIVE), rand.Next(MIN_MINUTE, MAX_MINUTE_EXCLUSIVE), rand.Next(MIN_SECOND, MAX_SECOND_EXCLUSIVE));

                // Select a random animal
                string animal = animals[rand.Next(MIN_ARRAY_INDICE, EXCLUSIVE_UPPER_BOUND)];

                // Insert the random data created
                Insert(animal, randomDateTime);
            }
        }           
    }
}
