using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Models
{
    //merge to venue along w/ state
    public class City
    {
        /// <summary>
        /// Primary auto incrementing id.
        /// </summary>
        public int CityID { get; set; }

        /// <summary>
        /// The name of the city.
        /// </summary>
        public string CityName { get; set; }

        /// <summary>
        /// References the abbreviation from the state table.
        /// </summary>
        public string StateAbbreviation { get; set; }
    }
}
