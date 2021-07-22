using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone
{
    class PrintShop
    {
        public void PrintHeader()
        { //TODO: Make this ASCII title slightly less obnoxious and better for smaller screens
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

        public void PrintMainMenu()
        {
            Console.WriteLine("Main Menu - What would you like to do?");
            Console.WriteLine(" 1) List Venues");
            Console.WriteLine(" Q) Quit");
            Console.WriteLine();
        }

        public void PrintSpaceMenu()
        {
            Console.WriteLine("What would you like to do next?");
            Console.WriteLine("1) Reserve a space");
            Console.WriteLine("R) Return to previous screen");
        }

        public void PrintSubMenu()
        {
            Console.WriteLine("What would you like to do next?");
            Console.WriteLine("1) View Spaces");
            Console.WriteLine("2) Search for Reservation");
            Console.WriteLine("R) Return to Previous Screen");

        }

    }
}
