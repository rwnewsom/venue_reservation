using System;
using System.Collections.Generic;
using System.Text;
using Capstone.Models;

namespace Capstone.DAL
{
    public interface IVenueDAO
    {
        //vestigial

        /// <summary>
        /// Returns a list of all venues.
        /// </summary>
        /// <returns></returns>
        ICollection<Venue> ListVenues();



    }
}
