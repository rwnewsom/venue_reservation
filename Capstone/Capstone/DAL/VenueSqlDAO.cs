using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using Capstone.Models;

namespace Capstone.DAL
{
    public class VenueSqlDAO
    {
        private readonly string connectionString;
        private const string SqlGetAllVenues = "SELECT * FROM venue ORDER BY name;";
        private const string GetVenueDetails = "SELECT v.name,ct.name AS 'city', st.abbreviation AS 'state', cat.name, v.description FROM venue v INNER JOIN city ct ON v.city_id = ct.id INNER JOIN state st ON ct.state_abbreviation = st.abbreviation INNER JOIN category_venue cv ON v.id = cv.venue_id INNER JOIN category cat ON cv.category_id = cat.id WHERE v.id = @v.id ";
        //SELECT v.name,ct.name AS 'city', st.name AS 'state' FROM venue v INNER JOIN city ct ON v.city_id = ct.id INNER JOIN state st ON ct.state_abbreviation = st.abbreviation WHERE v.id = 1;


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
                       // venue.CityName = Convert.ToString(reader["name"]);
                        //venue.StateAbbreviation = Convert.ToString(reader["st.abbreviation"]);

                        venues.Add(venue);
                    }
                }

            }

            catch (SqlException ex)
            {
                Console.WriteLine("error retrieveing departments: " + ex.Message);
            }
            return venues;
        }
        public Venue VenueDetails(int requestedVenue)
        {

            Venue venue = new Venue();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand command = new SqlCommand(GetVenueDetails, conn);
                    SqlDataReader reader = command.ExecuteReader();
                    command.Parameters.AddWithValue("@v.id", requestedVenue);


                    while (reader.Read())
                    {
                        venue.CityName = Convert.ToString(reader["name"]);
                        venue.StateAbbreviation = Convert.ToString(reader["SqlGetAllVenues"]);
                    }

                }

            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                throw;
            }
            return venue;



        }
    }

}

