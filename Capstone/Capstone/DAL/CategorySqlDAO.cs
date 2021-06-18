using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using Capstone.Models;

namespace Capstone.DAL
{
    public class CategorySqlDAO
    {
        private readonly string connectionString;
        private const string SqlGetAllCategories = "SELECT cat.name AS catname FROM category cat INNER JOIN category_venue cv " +
            "ON cv.category_id = cat.id INNER JOIN venue v ON v.id = cv.venue_id WHERE v.id = @vid";

        public CategorySqlDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }
        public List<string> ListCategories(int requestedVenue)
        {
            List<string> categories = new List<string>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand command = new SqlCommand(SqlGetAllCategories, conn);
                    command.Parameters.AddWithValue("@vid", requestedVenue);
                    SqlDataReader reader = command.ExecuteReader();

                    //List<string> categories = new List<string>();

                    while (reader.Read())
                    {
                        string category = Convert.ToString(reader["catname"]);
                        categories.Add(category);

                    }
                }

            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error" + ex.Message);
            }
            return categories;

        }
    }
}
