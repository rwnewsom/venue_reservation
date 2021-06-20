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

        private const string AvailableSpaces = "SELECT s.id AS 'sid', s.name AS 'name', s.daily_rate AS 'rate' FROM space s WHERE venue_id = @vid AND s.max_occupancy >= @attendees AND s.id NOT IN(SELECT s.id from reservation r JOIN space s on r.space_id = s.id WHERE s.venue_id = @vid AND r.end_date >= @startdate AND r.start_date <= @enddate)";

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
                Console.WriteLine("Error retrieving reservations: " + ex.Message);

            }
            return reservations;
        }
        public ICollection <Reservation> SearchSpace(int attendees, int stayLength, DateTime startDate, int chosenVenue)
        {
            DateTime endDate = startDate.AddDays(stayLength);
            List<Reservation> availableSpaces = new List<Reservation>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    //DateTime initialDate = startDate;
                    //TimeSpan duration = new TimeSpan(stayLength, 0, 0, 0);
                   
                    SqlCommand command = new SqlCommand(AvailableSpaces, conn);
                    command.Parameters.AddWithValue("@vid", chosenVenue);
                    command.Parameters.AddWithValue("@attendees", attendees);
                    command.Parameters.AddWithValue("@enddate", startDate);
                    command.Parameters.AddWithValue("@startdate", endDate);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Reservation reservation = new Reservation();
                        reservation.SpaceId = Convert.ToInt32(reader["sid"]);
                        reservation.SpaceName = Convert.ToString(reader["name"]);
                        reservation.DailyRate = Convert.ToDecimal(reader["rate"]);
                        //reservation.StartDate = Convert.ToDateTime(reader["startdate"]);
                        //reservation.EndDate = Convert.ToDateTime(reader["enddate"]);
                        availableSpaces.Add(reservation);

                    }
                    //reader.NextResult();
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            return availableSpaces;
        }
    }
}
