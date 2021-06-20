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
        private readonly SpacesSqlDAO spaceDAO;
        private readonly ReservationSqlDAO reservationDAO;

        int chosenVenue = 0;
        //int chosenSpace = 0;

        public UserInterface(string connectionString)
        {
            venueDAO = new VenueSqlDAO(connectionString);
            categoryDAO = new CategorySqlDAO(connectionString);
            spaceDAO = new SpacesSqlDAO(connectionString);
            reservationDAO = new ReservationSqlDAO(connectionString);
        }




        public void Run()
        {
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
                foreach (Venue venue in venues)
                {
                    Console.WriteLine(venue.VenueOrdinal + ")".PadRight(10) + venue.VenueName.PadRight(40));
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
                            chosenVenue = venueId;
                            Venue selected = venueDAO.VenueDetails(v.VenueID);
                            Console.WriteLine(selected.VenueName);
                            Console.WriteLine("Location: " + selected.CityName + ", " + selected.StateAbbreviation);
                            Console.WriteLine("Categories:");
                            List<string> VenueCategories = categoryDAO.ListCategories(venueId);
                            foreach (string cat in VenueCategories)
                            {
                                Console.Write(cat + " ");
                            }
                            Console.WriteLine();
                            Console.WriteLine(selected.VenueDescription);
                            Console.WriteLine();

                            
                            PrintSubMenu();
                            while (true)
                            {
                                string newCommand = Console.ReadLine();

                                Console.Clear();

                                switch (newCommand.ToLower())
                                {
                                    case "1":
                                        ICollection<Space> spaces = spaceDAO.ListSpaces(chosenVenue);
                                        Console.Clear();
                                        Console.WriteLine("      " + "Name".PadRight(30) + "Open".PadRight(5) + "Close".PadRight(5) + "Daily Rate".PadRight(10) + "Max. Occupancy");
                                        foreach (Space s in spaces)
                                        {
                                            Console.WriteLine("#" + s.SpaceOrdinal.ToString().PadRight(5) + s.Name.PadRight(30) + s.OpenFrom.ToString().PadRight(5) + s.OpenTo.ToString().PadRight(5) + s.DailyRate.ToString("c").PadRight(10) + s.MaxOccupancy);
                                        }
                                        break;

                                    case "2":
                                        ICollection<Reservation> reservations = reservationDAO.ListReservations(chosenVenue);
                                        Console.Clear();
                                        Console.WriteLine("The following reservations exist for this venue:");
                                        foreach (Reservation r in reservations)
                                        {
                                            Console.WriteLine(r.ReservatoinOrdinal +": " +r.SpaceName +"\t" + r.ReservedFor);
                                        }
                                        break;

                                    case "3":
                                        Console.WriteLine("When do you need the space? ");
                                        string reply = Console.ReadLine();
                                        DateTime startDate = DateTime.Parse(reply) ;
                                        Console.WriteLine("How many days will you need the space? ");
                                        reply = Console.ReadLine();
                                        int stayLength = int.Parse(reply);
                                        Console.WriteLine("How many people will be in attendance? ");
                                        reply = Console.ReadLine();
                                        int attendees = int.Parse(reply);

                                        ICollection<Reservation> reservationSpace = (ICollection<Reservation>)reservationDAO.SearchSpace(attendees, stayLength, startDate);
                                        Console.Clear();
                                        Console.WriteLine("The following spaces are available based on your needs:");

                                        foreach(Reservation r in reservationSpace)
                                        {
                                            Console.WriteLine(r.SpaceId + r.SpaceName + )
                                        }
                                        


                                    case "r":
                                        return;

                                    default:
                                        Console.WriteLine("The command provided was not a valid command, please try again.");
                                        break;
                                }

                                PrintSubMenu();
                            } 


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

        private void PrintSubMenu()
        {
            Console.WriteLine("What would you like to do next?");
            Console.WriteLine("1) View Spaces");
            Console.WriteLine("2) Search for Reservation");
            Console.WriteLine("R) Return to Previous Screen");

        }

        private void PrintSpaceMenu()
        {
            Console.WriteLine("What would you like to do next?");
            Console.WriteLine("1) Reserve a space");
            Console.WriteLine("R) Return to previous screen");        
        }



        

        
    }
}
