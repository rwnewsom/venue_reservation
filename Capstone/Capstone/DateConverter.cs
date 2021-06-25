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


            return month switch
            {
                1 => "Jan.",
                2 => "Feb.",
                3 => "Mar.",
                4 => "Apr.",
                5 => "May",
                6 => "Jun.",
                7 => "Jul.",
                8 => "Aug.",
                9 => "Sep.",
                10 => "Oct.",
                11 => "Nov.",
                12 => "Dec.",
                _ => "",
            };
        }
    }
}
