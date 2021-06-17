using Capstone.DAL;
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
        private readonly string connectionString;

        private readonly VenueDAO venueDAO;

        public UserInterface(string connectionString)
        {
            this.connectionString = connectionString;
            venueDAO = new VenueDAO(connectionString);
        }

        public void Run()
        {
            //Console.WriteLine("Reached the User Interface.");
            PrintHeader();
            Console.ReadLine();
        }
        private void PrintHeader()
        {
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
        }
    }
}
