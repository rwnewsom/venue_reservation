using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using Capstone.Models;

namespace Capstone.DAL
{
    class VenueSqlDAO : IVenueDAO
    {
        private readonly string connectionString;
        private const string SqlGetAllVenues = "SELECT * FROM venue;";

        //Single Parameter Constructor
        public VenueSqlDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        /// <summary>
        /// Returns a list of all the venues.
        /// </summary>
        /// <returns></returns>
        public ICollection<Venue> ListVenues()
        {
            List<Venue> venues = new List<Venue>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand command = new SqlCommand(SqlGetAllVenues, conn);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Venue venue = new Venue();
                        venue.VenueID = Convert.ToInt32(reader["id"]);
                        venue.VenueName = Convert.ToString(reader["name"]);
                        venue.CityId = Convert.ToInt32(reader["city_id"]);
                        venue.VenueDescription = Convert.ToString(reader["description"]);

                        venues.Add(venue);
                    }
                }

            }

            catch(SqlException ex)
            {
                Console.WriteLine("error retrieveing departments: " + ex.Message);
            }
            return venues;

        }
    }
}
