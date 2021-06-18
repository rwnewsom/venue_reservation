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
        private const string SqlGetAllCategories = "SELECT * FROM category";
        public CategorySqlDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }
        public List<string> ListCategories()
        {
            List<string> categories = new List<string>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand command = new SqlCommand(SqlGetAllCategories, conn);
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
