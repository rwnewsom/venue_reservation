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

        private const string GetVenueDetails = "SELECT v.name AS 'vname',ct.name AS 'city', st.abbreviation AS 'state', cat.name AS 'catname', v.description AS 'vdesc' " +
            "FROM venue v INNER JOIN city ct ON v.city_id = ct.id INNER JOIN state st ON ct.state_abbreviation = st.abbreviation " +
            "INNER JOIN category_venue cv ON v.id = cv.venue_id INNER JOIN category cat ON cv.category_id = cat.id WHERE v.id = @vid ";

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
                    int ordCount = 1;
                    while (reader.Read())
                    {
                        Venue venue = new Venue();
                        venue.VenueOrdinal = ordCount;
                        venue.VenueID = Convert.ToInt32(reader["id"]);
                        venue.VenueName = Convert.ToString(reader["name"]);
                        venue.CityId = Convert.ToInt32(reader["city_id"]);
                        venue.VenueDescription = Convert.ToString(reader["description"]);
                        venues.Add(venue);
                        ordCount++;
                    }
                }

            }

            catch (SqlException ex)
            {
                Console.WriteLine("error retrieving venues: " + ex.Message);
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
                    command.Parameters.AddWithValue("@vid", requestedVenue);
                    SqlDataReader reader = command.ExecuteReader();                    

                    while (reader.HasRows)
                    {
                        List<string> categories = new List<string>();
                        while (reader.Read())
                        {
                            venue.VenueName = Convert.ToString(reader["vname"]);
                            venue.CityName = Convert.ToString(reader["city"]);
                            venue.StateAbbreviation = Convert.ToString(reader["state"]);
                            venue.VenueDescription = Convert.ToString(reader["vdesc"]);
                            venue.CategoryName = Convert.ToString(reader["catname"]);
                        }
                        reader.NextResult();
                    }
                }

            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            return venue;



        }
    }

}

