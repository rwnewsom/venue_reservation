using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Models
{
    public class Venue
    {
        /// <summary>
        /// Primary auto incrementing id.
        /// </summary>
        public int VenueID { get; set; }

        /// <summary>
        /// The venue name.
        /// </summary>
        public string VenueName { get; set; }

        /// <summary>
        /// References the id from the city table.
        /// </summary>
        public int CityId { get; set; }

        /// <summary>
        /// The description of the venue.
        /// </summary>
        public string VenueDescription { get; set; }

        public string CityName { get; set; }
        public string StateAbbreviation { get; set; }
    }
}
