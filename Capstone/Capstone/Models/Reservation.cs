using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Models
{
    public class Reservation
    {
        /// <summary>
        /// Primary auto incrementing id. This can be used for displaying the confirmation number.
        /// </summary>
        public int ReservationId { get; set; }

        /// <summary>
        /// The id of the space that has been reserved.
        /// </summary>
        public int SpaceId { get; set; }

        /// <summary>
        /// The number of people that will be attending the event.
        /// </summary>
        public int NumberOfAttendees { get; set; }

        /// <summary>
        /// The date the space will first be needed on
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// The last date the space will be needed on.
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// The name of the person or organization this reservation is for.
        /// </summary>
        public string ReservedFor { get; set; }


    }
}
