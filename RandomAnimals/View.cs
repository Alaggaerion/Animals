/// ETML
/// Auteur      : Sylvain Laydernier
/// Date        : 15.03.2018
/// Description : View class, linked with controller

using System;
using System.Collections.Generic;

namespace RandomAnimals
{
    public class View
    {
        private Controller appController = null;

        /// <summary>
        /// Constructor, create menu when called
        /// </summary>
        /// <param name="anyController"></param>
        public View(Controller anyController)
        {
            this.appController = anyController;
            this.appController.AppView = this;
            Menu();            
        }

        /// <summary>
        /// Write string to console
        /// </summary>
        /// <param name="stringToWrite"></param>
        public void Write(string stringToWrite)
        {
            Console.WriteLine(stringToWrite);
        }

        /// <summary>
        ///  Write list content on Console
        /// </summary>
        /// <param name="listToShow"></param>
        public void WriteSelect(List<string>[] listToShow)
        {
            // Create an header
            Console.WriteLine("------------------------");
            Console.WriteLine("ID\tName\tDateTime");
            Console.WriteLine("------------------------");

            // Write each data contained in the list
            for (int index = 0; index < listToShow[0].Count; index++)
            {
                Console.WriteLine(listToShow[0][index] + "\t" + listToShow[1][index] + "\t" + listToShow[2][index]);
            }
        }

        /// <summary>
        /// Create interactive menu interface and deal with inputs
        /// </summary>
        private void Menu()
        {
            string userInput;
            string animalInput;
            DateTime dateTime;
            int id;
            string newNameInput;

            Console.WriteLine(" -------------------------------------------------------------------");
            Console.WriteLine("¦                  Welcome in the table manager !                  ¦");
            Console.WriteLine(" -------------------------------------------------------------------");

            do
            {
                // Console interface to choose what to do
                Console.WriteLine("Please choose one of the followings settings by pressing the corresponding key :");
                Console.WriteLine("1. Insert an animal");
                Console.WriteLine("2. Update an animal's name");
                Console.WriteLine("3. Update an animal's date and time");
                Console.WriteLine("4. Delete an animal");
                Console.WriteLine("5. Show table content");
                Console.WriteLine("---------------------------------------------------------------");

                // Get user input
                userInput = Console.ReadLine();

                Console.WriteLine("---------------------------------------------------------------");

                // Depending on user's input
                // 1 : open insert new data interface
                // 2 : open update name interface
                // 3 : open update date and time interface
                // 4 : open delete data interface
                // 5 : open select data interface
                // default : error message
                // Then ask user if he wants to continue, if yes, restart again
                switch (userInput)
                {
                    // Input = 1 : open insert new data interface
                    case "1":
                        Console.WriteLine("Please enter new animal's name to insert.");
                        animalInput = Console.ReadLine();
                        dateTime = DateTime.Now;
                        this.appController.Insert(animalInput, dateTime);
                        break;

                    // Input = 2 : open update name interface
                    case "2":
                        Console.WriteLine("Please enter animal's ID which you want to change name.");

                        // Test user's input
                        try
                        {
                            id = Convert.ToInt32(Console.ReadLine());
                        }
                        // Show error message and break case if invalid input
                        catch
                        {
                            Console.WriteLine("Error, you have to enter an integer.");
                            break;
                        }
                        Console.WriteLine("Please enter the new name of animal " + Convert.ToString(id));
                        newNameInput = Console.ReadLine();
                        this.appController.Update(newNameInput, id);
                        break;

                    // Input = 3 : open update date and time interface
                    case "3":
                        Console.WriteLine("Please enter animal's ID which you want to change date and time.");

                        // Test user's input
                        try
                        { 
                            id = Convert.ToInt32(Console.ReadLine());
                        }
                        // Show error message if invalid input
                        catch
                        {
                            Console.WriteLine("Error, you have to enter an integer.");
                            break;
                        }
                        Console.WriteLine("Please enter the new date and time of animal " + Convert.ToString(id)+", format is 'YYYY-MM-DD HH:MM:SS'");
                        
                        // Test user's input
                        try
                        {
                            dateTime = Convert.ToDateTime(Console.ReadLine());
                        }
                        // Show error message if invalid input
                        catch
                        {
                            Console.WriteLine("Error, please use following format : YYYY-MM-DD HH:MM:SS");
                            break;
                        }
                        this.appController.Update(dateTime, id);
                        break;

                    // Input = 4 : open delete data interface
                    case "4":                        
                        Console.WriteLine("Please enter id of the animal you want to delete.");
                        // Test user's input
                        try
                        {
                            id = Convert.ToInt32(Console.ReadLine());
                        }
                        // Show error message if invalid input
                        catch
                        {
                            Console.WriteLine("Error, you have to enter an integer.");
                            break;
                        }
                        this.appController.Delete(id);
                        break;

                    // Input = 5 : open select data interface
                    case "5":                        
                        this.appController.Select();
                        break;

                    // default : error message
                    default:
                        // Error message
                        Console.WriteLine("Please choose an option between 1 and 5");
                        Console.WriteLine("---------------------------------------------------------------");

                        // Set user input to "Y" to continue asking what user want to do
                        userInput = "Y";

                        // Restart loop
                        continue;
                }

                // Console interface to ask user if he wants to continue
                Console.WriteLine("---------------------------------------------------------------");
                Console.WriteLine("Do you want to continue manage the data base? (Y/N)");
                userInput = Console.ReadLine();
                Console.WriteLine("---------------------------------------------------------------");
            } while (userInput == "Y" || userInput == "y"); // While user's input is Y or y, restart loop
        }
    }
}
