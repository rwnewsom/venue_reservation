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
        private readonly DateConverter dateCon = new DateConverter();
        private readonly PrintShop printShop = new PrintShop();

        int chosenVenue = 0;
        int chosenSpace = 0;

        public UserInterface(string connectionString)
        {
            venueDAO = new VenueSqlDAO(connectionString);
            categoryDAO = new CategorySqlDAO(connectionString);
            spaceDAO = new SpacesSqlDAO(connectionString);
            reservationDAO = new ReservationSqlDAO(connectionString);
        }




        public void Run()
        {
            printShop.PrintHeader();
            printShop.PrintMainMenu();
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

                printShop.PrintMainMenu();
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
                            CategorySubMenu(v);
                            return;
                        }
                    }
                    Console.WriteLine("Invalid Input");
                    break;
                }
            }

            void MakeReservation(Venue selected, DateTime startDate, int stayLength, int attendees, ICollection<Reservation> reservationSpace, string userChoice)
            {
                chosenSpace = int.Parse(userChoice);

                foreach (Reservation r in reservationSpace)
                {
                    if (r.SpaceId == chosenSpace)
                    {
                        Console.WriteLine("Who is this reservation for?");

                        string reservationName = Console.ReadLine();
                        DateTime endDate = startDate.AddDays(stayLength);
                        decimal totalCost = r.DailyRate * stayLength;
                        int conNum = reservationDAO.CreateReservation(r.SpaceId, attendees, startDate, endDate, reservationName);
                        Console.WriteLine();
                        Console.WriteLine("Thanks for submitting your reservation! The details for your event are listed below:");
                        Console.WriteLine("Confirmation #: " + conNum);
                        Console.WriteLine("Venue: " + selected.VenueName);
                        Console.WriteLine("Space: " + r.SpaceName);
                        Console.WriteLine("Reserved for: " + reservationName);
                        Console.WriteLine("Attendees: " + attendees);
                        Console.WriteLine("Arrival Date: " + startDate);
                        Console.WriteLine("Departure Date: " + endDate);
                        Console.WriteLine("Total Cost: " + totalCost.ToString("c"));
                    }
                }
            }

            void SearchReservation(out DateTime startDate, out int stayLength, out int attendees, out ICollection<Reservation> reservationSpace)
            {
                Console.WriteLine("When do you need the space? mon/day/year");
                string reply = Console.ReadLine();
                startDate = DateTime.Parse(reply);
                Console.WriteLine("How many days will you need the space? ");
                reply = Console.ReadLine();
                stayLength = int.Parse(reply);
                Console.WriteLine("How many people will be in attendance? ");
                reply = Console.ReadLine();
                attendees = int.Parse(reply);
                reservationSpace = reservationDAO.SearchSpace(attendees, stayLength, startDate, chosenVenue);
                Console.Clear();
                Console.WriteLine("The following spaces are available based on your needs:");
                Console.WriteLine();
                Console.WriteLine("Space #   Name                Daily Rate   Max Occup.   Accessible?   Total Cost");
                foreach (Reservation r in reservationSpace)
                {
                    decimal totalCost = r.DailyRate * stayLength;
                    string adaComp = "";
                    if (r.IsAccessible)
                    {
                        adaComp = "Yes";
                    }
                    else
                    {
                        adaComp = "No";
                    }
                    Console.WriteLine(r.SpaceId.ToString().PadRight(10) + r.SpaceName.PadRight(20) + r.DailyRate.ToString("c").PadRight(13) + r.MaxOccupancy.ToString().PadRight(13) + adaComp.PadRight(14) + totalCost.ToString("c"));
                }
            }

            void ViewMenuSpaces(Venue selected)
            {
                ICollection<Space> spaces = spaceDAO.ListSpaces(chosenVenue);
                Console.Clear();
                Console.WriteLine("     Name                Open   Close   Daily Rate   Max. Occupancy");
                foreach (Space s in spaces)
                {

                    string openFrom = dateCon.FormatDate(s.OpenFrom);
                    string openTo = dateCon.FormatDate(s.OpenTo);
                    Console.WriteLine("#" + s.SpaceOrdinal.ToString().PadRight(5) + s.Name.PadRight(20) + openFrom.PadRight(7) + openTo.PadRight(8) + s.DailyRate.ToString("c").PadRight(13) + s.MaxOccupancy);
                }
                printShop.PrintSpaceMenu();
                string userInput = Console.ReadLine().ToLower();
                if (userInput == "r")
                {
                    Console.Clear();
                    return;
                }

                else if (userInput == "1")
                {
                    SearchReservation(out DateTime startDate, out int stayLength, out int attendees, out ICollection<Reservation> reservationSpace);

                    Console.WriteLine("Which space would you like to reserve (enter 0 to cancel)?");
                    string userChoice = Console.ReadLine();
                    if (userChoice == "0")
                    {
                        return;
                    }
                    else
                    {
                        MakeReservation(selected, startDate, stayLength, attendees, reservationSpace, userChoice);
                    }
                }
                else
                {
                    Console.WriteLine("Invalid Input. Returning to previous menu.");
                }
                return;
            }

            void CategorySubMenu(Venue v)
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


                printShop.PrintSubMenu();
                while (true)
                {
                    string newCommand = Console.ReadLine();

                    Console.Clear();

                    switch (newCommand.ToLower())
                    {
                        case "1":
                            ViewMenuSpaces(selected);
                            return;

                        case "2":
                            ICollection<Reservation> reservations = reservationDAO.ListReservations(chosenVenue);
                            Console.Clear();
                            Console.WriteLine("The following reservations exist for this venue:");
                            foreach (Reservation r in reservations)
                            {
                                Console.WriteLine(r.ReservatoinOrdinal + ": " + r.SpaceName + "\t" + r.ReservedFor);
                            }

                            return;

                        case "r":
                            return;

                        default:
                            Console.WriteLine("The command provided was not a valid command, please try again.");
                            break;
                    }
                }
            }
        }
    }
}
