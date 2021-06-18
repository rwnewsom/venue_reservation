using Capstone.DAL;
using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace Capstone
{
    /// <summary>
    /// This class is responsible for representing the main user interface to the user.
    /// </summary>
    /// <remarks>
    /// ALL Console.ReadLine and WriteLine in this class
    /// NONE in any other class. 
    ///  
    /// The only exceptions to this are:
    /// 1. Error handling in catch blocks
    /// 2. Input helper methods in the CLIHelper.cs file
    /// 3. Things your instructor explicitly says are fine
    /// 
    /// No database calls should exist in classes outside of DAO objects
    /// </remarks>
    public class UserInterface
    {

        const string Command_ListVenues = "1";
        const string Command_Quit = "q";


        private readonly VenueSqlDAO venueDAO;
        private readonly CategorySqlDAO categoryDAO;





        public UserInterface(string connectionString)
        {
            venueDAO = new VenueSqlDAO(connectionString);
        }




        public void Run()
        {
            //Console.WriteLine("Reached the User Interface.");
            PrintHeader();
            PrintMenu();
            while (true)
            {
                string command = Console.ReadLine();

                Console.Clear();

                switch (command.ToLower())
                {
                    case Command_ListVenues:
                        ListVenues();
                        break;


                    case Command_Quit:
                        Console.WriteLine("Thank you for using the reservation utility!");
                        return;

                    default:
                        Console.WriteLine("The command provided was not a valid command, please try again.");
                        break;
                }

                PrintMenu();
            }
        }



        private void ListVenues()
        {

            ICollection<Venue> venues = venueDAO.ListVenues();
            if (venues.Count > 0)
            {
                Console.WriteLine("Which venue would you like to view? ");
                //int count = 1;
                foreach (Venue venue in venues)
                {
                    Console.WriteLine(venue.VenueOrdinal + ")".PadRight(10) + venue.VenueName.PadRight(40));
                    //count++;
                }
                Console.WriteLine("R) Return to previous menu.");
            }
            else
            {
                Console.WriteLine("**** NO RESULTS ****");
            }
            while (true)
            {

                string command = Console.ReadLine().ToLower();
                if (command == "r")
                {
                    return;
                }
                else
                {
                    int i = int.Parse(command);


                    foreach (Venue v in venues)
                    {
                        if (v.VenueOrdinal == i)
                        {
                            int venueId = v.VenueID;
                            Venue selected = venueDAO.VenueDetails(v.VenueID);
                            Console.WriteLine(selected.VenueName);
                            Console.WriteLine("Location: " + selected.CityName + ", " + selected.StateAbbreviation);
                            Console.WriteLine("Categories: " + selected.CategoryName);
                            List<string> VenueCategories = categoryDAO.ListCategories(venueId);
                            foreach (string cat in VenueCategories)
                            {
                                Console.WriteLine(cat);
                            }
                            Console.WriteLine(selected.VenueDescription);
                            Console.WriteLine();

                        }
                    }

                    Console.WriteLine("Invalid Input");
                    break;
                }

            }

        }


        private void PrintHeader()
        {
            Console.WriteLine();
            Console.WriteLine(@"$$$$$$$\                                                              $$\     $$\                           $$\   $$\   $$\     $$\ $$\ $$\   $$\               ");
            Console.WriteLine(@"$$  __$$\                                                             $$ |    \__|                          $$ |  $$ |  $$ |    \__|$$ |\__|  $$ |              ");
            Console.WriteLine(@"$$ |  $$ | $$$$$$\   $$$$$$$\  $$$$$$\   $$$$$$\ $$\    $$\ $$$$$$\ $$$$$$\   $$\  $$$$$$\  $$$$$$$\        $$ |  $$ |$$$$$$\   $$\ $$ |$$\ $$$$$$\   $$\   $$\ ");
            Console.WriteLine(@"$$$$$$$  |$$  __$$\ $$  _____|$$  __$$\ $$  __$$\\$$\  $$  |\____$$\\_$$  _|  $$ |$$  __$$\ $$  __$$\       $$ |  $$ |\_$$  _|  $$ |$$ |$$ |\_$$  _|  $$ |  $$ |");
            Console.WriteLine(@"$$  __$$< $$$$$$$$ |\$$$$$$\  $$$$$$$$ |$$ |  \__|\$$\$$  / $$$$$$$ | $$ |    $$ |$$ /  $$ |$$ |  $$ |      $$ |  $$ |  $$ |    $$ |$$ |$$ |  $$ |    $$ |  $$ |");
            Console.WriteLine(@"$$ |  $$ |$$   ____| \____$$\ $$   ____|$$ |       \$$$  / $$  __$$ | $$ |$$\ $$ |$$ |  $$ |$$ |  $$ |      $$ |  $$ |  $$ |$$\ $$ |$$ |$$ |  $$ |$$\ $$ |  $$ |");
            Console.WriteLine(@"$$ |  $$ |\$$$$$$$\ $$$$$$$  |\$$$$$$$\ $$ |        \$  /  \$$$$$$$ | \$$$$  |$$ |\$$$$$$  |$$ |  $$ |      \$$$$$$  |  \$$$$  |$$ |$$ |$$ |  \$$$$  |\$$$$$$$ |");
            Console.WriteLine(@"\__|  \__| \_______|\_______/  \_______|\__|         \_/    \_______|  \____/ \__| \______/ \__|  \__|       \______/    \____/ \__|\__|\__|   \____/  \____$$ |");
            Console.WriteLine(@"                                                                                                                                                      $$\   $$ |");
            Console.WriteLine(@"                                                                                                                                                      \$$$$$$  |");
            Console.WriteLine(@"                                                                                                                                                       \______/");
            Console.WriteLine();
            Console.WriteLine();
        }

        private void PrintMenu()
        {
            Console.WriteLine("Main Menu - What would you like to do?");
            Console.WriteLine(" 1) List Venues");
            Console.WriteLine(" Q) Quit");
            Console.WriteLine();
        }
    }
}
