using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone
{
    /// <summary>
    /// This class contains a helper method to convert integer dates to month abbreviations.
    /// </summary>
    public class DateConverter
    {
        public string FormatDate(int? month)
        {
            

            switch (month)
            {
                case 1:
                    return "Jan.";
                case 2:
                    return "Feb.";
                case 3:
                    return "Mar.";
                case 4:
                    return "Apr.";
                case 5:
                    return "May";
                case 6:
                    return "Jun.";
                case 7:
                    return "Jul.";
                case 8:
                    return "Aug.";
                case 9:
                    return "Sep.";
                case 10:
                    return "Oct.";
                case 11:
                    return "Nov.";
                case 12:
                    return "Dec.";
                default:
                    return "";

            }

        }
    }
}
