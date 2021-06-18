using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using Capstone.Models;

namespace Capstone.DAL
{
    public class SpacesSqlDAO
    {
        private readonly string connectionString;
        private const string SqlGetAllSpaces = "SELECT sp.name AS 'spname', sp.is_accessible AS 'acc', sp.open_from AS 'openfrom', sp.open_to AS 'opento', sp.daily_rate AS 'rate', sp.max_occupancy AS 'maxocp'" +
            "FROM space sp INNER JOIN venue v ON v.id = sp.venue_id WHERE v.id = @vid";

        /// <summary>
        /// Single parameter constructor
        /// </summary>
        /// <param name="dbConnectionString"></param>
        public SpacesSqlDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public ICollection<Space> ListSpaces(int requestedVenue)
        {
            List<Space> spaces = new List<Space>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand command = new SqlCommand(SqlGetAllSpaces, conn);
                    command.Parameters.AddWithValue("@vid", requestedVenue);
                    SqlDataReader reader = command.ExecuteReader();
                    int spaceOrd = 1;
                    while (reader.Read())
                    {
                        Space space = new Space();
                        space.SpaceOrdinal = spaceOrd;
                        space.Name = Convert.ToString(reader["spname"]);
                        space.IsAccessible = Convert.ToBoolean(reader["acc"]);
                        if (reader["openfrom"] != DBNull.Value)
                        {
                            space.OpenFrom = Convert.ToInt32(reader["openfrom"]);
                        }
                        if(reader["opento"] != DBNull.Value)
                        {
                            space.OpenTo = Convert.ToInt32(reader["opento"]);
                        }                        
                        space.DailyRate = Convert.ToDecimal(reader["rate"]);
                        space.MaxOccupancy = Convert.ToInt32(reader["maxocp"]);
                        spaces.Add(space);
                        spaceOrd++;
                    }
                    
                }

            }
            catch(SqlException ex)
            {
                Console.WriteLine("error retrieving spaces: " + ex.Message);
                
            }
            return spaces;
        }
    }
}
