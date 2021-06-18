using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Models
{
    public class Space
    {
        /// <summary>
        /// Primary auto incrementing id.
        /// </summary>
        public int SpaceId { get; set; }

        /// <summary>
        /// Allows us to re-index from 1 to n alphabetically
        /// </summary>
        public int SpaceOrdinal { get; set; }

        /// <summary>
        /// References the id from the venue table.
        /// </summary>
        public int VenueId { get; set; }

        /// <summary>
        /// The name of the space.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Whether or not this space is handicap accessible.
        /// </summary>
        public bool IsAccessible { get; set; }

        /// <summary>
        /// The month of the year this space is available from. If null, then it is always open.
        /// </summary>
        public int? OpenFrom { get; set; }

        /// <summary>
        /// The month of the year this space is available until. If null, then it is never closed.
        /// </summary>
        public int? OpenTo { get; set; }

        /// <summary>
        /// The daily cost for renting the space.
        /// </summary>
        public Decimal DailyRate { get; set; }

        /// <summary>
        /// The maximum number of people that the space is able to safely support.
        /// </summary>
        public int MaxOccupancy { get; set; }

    }
}
