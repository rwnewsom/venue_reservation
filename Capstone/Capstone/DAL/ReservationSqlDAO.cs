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
        private const string SearchSpaceDate = "SELECT r.reservation_id, r.space_id, r.number_of_attendees, r.start_date, r.end_date, r.reserved_for FROM reservation r INNER JOIN space sp ON sp.id = r.space_id";

        private const string ReserveSpace = "";


        public ReservationSqlDAO (string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public ICollection<Reservation> ListReservations()
        {
            List<Reservation> reservations = new List<Reservation>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand command = new SqlCommand(SearchSpaceDate, conn);
                    SqlDataReader reader = command.ExecuteReader();
                    int ordCount = 1;
                    while (reader.Read())
                    {
                        Reservation reservation = new Reservation();
                        reservation.ReservationId = Convert.ToInt32(reader["id"]);
                        reservation.SpaceId = Convert.ToInt32(reader["spaceid"]);
                        reservation.NumberOfAttendees = Convert.ToInt32(reader["attendencenumber"]);
                        reservation.StartDate = Convert.ToDateTime(reader["startdate"]);
                        reservation.EndDate = Convert.ToDateTime(reader["enddate"]);
                        reservation.ReservedFor = Convert.ToString(reader["reservedfor"]);
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
    }
}
