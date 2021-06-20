using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using Capstone.Models;

namespace Capstone.DAL
{
    class ReservationSqlDAO
    {
        private readonly string connectionString;
        //Build a list of reservations overlapping requested date.
        //Deny request unless list is null or empty.
        private const string SearchSpaceDate = "SELECT r.reservation_id AS 'id', r.space_id AS 'spaceid', r.number_of_attendees AS 'attendencenumber', r.start_date AS 'startdate', r.end_date AS 'enddate', r.reserved_for AS 'reservedfor', v.id AS 'vid', sp.name AS 'sp_name' FROM reservation r INNER JOIN space sp ON sp.id = r.space_id INNER JOIN venue v ON sp.venue_id = v.id WHERE v.id = @vid"; 

        private const string ReserveSpace = "SELECT s.id, @s.daily_rate = dailyrate, @s.max_occupancy = max FROM reservation r JOIN space s on r.space_id = s.id WHERE s.venue_id = 1 AND r.end_date >= '2021-06-21' AND r.start_date <= '2021-06-16' SELECT v.name, ct.name AS 'city', st.name  AS 'state' FROM venue v INNER JOIN city ct ON v.city_id = ct.id INNER JOIN state st ON ct.state_abbreviation = st.abbreviation WHERE v.id = @vid; @@IDENTITY";

        public ReservationSqlDAO (string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public ICollection<Reservation> ListReservations(int chosenVenue)
        {
            List<Reservation> reservations = new List<Reservation>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand command = new SqlCommand(SearchSpaceDate, conn);
                    command.Parameters.AddWithValue("@vid", chosenVenue);
                    SqlDataReader reader = command.ExecuteReader();
                    int ordCount = 1;
                    while (reader.Read())
                    {
                        
                        Reservation reservation = new Reservation();
                        reservation.ReservatoinOrdinal = ordCount;
                        reservation.ReservationId = Convert.ToInt32(reader["id"]);
                        reservation.SpaceId = Convert.ToInt32(reader["spaceid"]);
                        reservation.NumberOfAttendees = Convert.ToInt32(reader["attendencenumber"]);
                        reservation.StartDate = Convert.ToDateTime(reader["startdate"]);
                        reservation.EndDate = Convert.ToDateTime(reader["enddate"]);
                        reservation.ReservedFor = Convert.ToString(reader["reservedfor"]);
                        reservation.VenueID = Convert.ToInt32(reader["vid"]);
                        reservation.SpaceName = Convert.ToString(reader["sp_name"]);
                        reservations.Add(reservation);
                        ordCount++;
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error retreiving reservations: " + ex.Message);

            }
            return reservations;
        }
        public Reservation SearchSpace(int attendees, int stayLength, DateTime startDate)
        {
            Reservation reservation = new Reservation();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand command = new SqlCommand(ReserveSpace, conn);
                    //command.Parameters.AddWithValue("@vid", attendees);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        reservation.SpaceId = Convert.ToInt32(reader["sid"]);
                        reservation.SpaceName = Convert.ToString(reader["name"]);
                        reservation.StartDate = Convert.ToDateTime(reader["startdate"]);
                        reservation.EndDate = Convert.ToDateTime(reader["enddate"]);
                        

                    }
                    reader.NextResult();
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            return reservation;
        }
    }
}
