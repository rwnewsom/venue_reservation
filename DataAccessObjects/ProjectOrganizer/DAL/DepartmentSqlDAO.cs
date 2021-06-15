using ProjectOrganizer.Models;
using System;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace ProjectOrganizer.DAL
{
    public class DepartmentSqlDAO : IDepartmentDAO
    {
        private readonly string connectionString;
        private const string SqlGetAllDepts = "SELECT * FROM department";
        private const string SqlInsert = "INSERT INTO department (name) VALUES (@name); SELECT @@IDENTITY;";
        private const string SqlUpdateDept = "UPDATE department(name) SET @updatedName WHERE @departmentId";

        // Single Parameter Constructor
        public DepartmentSqlDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        /// <summary>
        /// Returns a list of all of the departments.
        /// </summary>
        /// <returns></returns>
        public ICollection<Department> GetDepartments()
        {
            List<Department> departments = new List<Department>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand command = new SqlCommand(SqlGetAllDepts, conn);

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Department department = new Department();
                        department.Name = Convert.ToString(reader["name"]);
                        department.Id = Convert.ToInt32(reader["department_id"]);

                        departments.Add(department);
                    }
                }

            }

            catch (SqlException ex)
            {
                Console.WriteLine("error getting departments: " + ex.Message);
            }

            return departments;
            

        }

        /// <summary>
        /// Creates a new department.
        /// </summary>
        /// <param name="newDepartment">The department object.</param>
        /// <returns>The id of the new department (if successful).</returns>
        public int CreateDepartment(Department newDepartment)
        { //throw new NotImplementedException();
            try
            {
                using (SqlConnection conn = new SqlConnection(this.connectionString))
                {
                    conn.Open();
                    SqlCommand command = new SqlCommand(SqlInsert, conn);
                    command.Parameters.AddWithValue("@name", newDepartment.Name);

                    //get the ID of the department we created
                    int id = Convert.ToInt32(command.ExecuteScalar());
                    return id;
                }

            }
            catch (SqlException ex)
            {
                Console.WriteLine("Could not Create Department: " + ex.Message);
                throw;
                return -1;
            }

        }

        /// <summary>
        /// Updates an existing department.
        /// </summary>
        /// <param name="updatedDepartment">The department object.</param>
        /// <returns>True, if successful.</returns>
        public bool UpdateDepartment(Department updatedDepartment)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(this.connectionString))
                {
                    conn.Open();
                    SqlCommand command = new SqlCommand(SqlUpdateDept, conn);

                    //update dept name
                    command.Parameters.AddWithValue("@departmentId", updatedDepartment.Name);
                    return true;

                }

            }
            catch(SqlException ex)
            {
                Console.WriteLine("Could not update department " + ex.Message);
                return false;
            }
        }

    }
}
