using Capstone.DAL;
using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Text;

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
                        //Console.WriteLine("Not Implemented Yet");
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
            //ICollection<Department> departments = departmentDAO.GetDepartments();
            ICollection<Venue> venues = venueDAO.ListVenues();
            if (venues.Count > 0)
            {
                foreach (Venue venue in venues)
                {
                    Console.WriteLine(venue.VenueID.ToString().PadRight(10) + venue.VenueName.PadRight(40));
                }
            }
            else
            {
                Console.WriteLine("**** NO RESULTS ****");
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
