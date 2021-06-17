using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Models
{
    public class City
    {
        /// <summary>
        /// The city id.
        /// </summary>
        public int CityID { get; set; }

        /// <summary>
        /// The name of the city.
        /// </summary>
        public string CityName { get; set; }

        /// <summary>
        /// Official two letter abbreviation of state.
        /// </summary>
        public string StateAbbreviation { get; set; }
    }
}
