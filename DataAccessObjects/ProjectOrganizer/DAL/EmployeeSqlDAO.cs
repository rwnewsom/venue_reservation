using ProjectOrganizer.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ProjectOrganizer.DAL
{
    public class EmployeeSqlDAO : IEmployeeDAO
    {
        private readonly string connectionString;
        private const string SqlGetAllEmployees = "SELECT * FROM employee";
        //pass the search terms firstname lastname   "UPDATE department SET name = @name WHERE department_id = @id";
        private const string SqlFirstAndLast = "SELECT * FROM employee WHERE last_name LIKE @lastname  AND first_name LIKE @firstname";
        private const string SqlOrphanEmployees = "SELECT * FROM employee e LEFT JOIN project_employee pe ON pe.employee_id = e.employee_id WHERE pe.project_id IS NULL";
        // Single Parameter Constructor
        public EmployeeSqlDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        /// <summary>
        /// Returns a list of all of the employees.
        /// </summary>
        /// <returns>A list of all employees.</returns>
        public ICollection<Employee> GetAllEmployees()
        {
            List<Employee> employees = new List<Employee>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand command = new SqlCommand(SqlGetAllEmployees, conn);

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Employee employee = new Employee();
                        employee.FirstName = Convert.ToString(reader["first_name"]);
                        employee.LastName = Convert.ToString(reader["last_name"]);
                        employee.BirthDate = Convert.ToDateTime(reader["birth_date"]);
                        employee.EmployeeId = Convert.ToInt32(reader["employee_id"]);
                        employee.JobTitle = Convert.ToString(reader["job_title"]);

                        employees.Add(employee);
                    }
                }

            }
            catch (SqlException ex)
            {
                Console.WriteLine("error getting employees: " + ex.Message);
            }

            return employees;
        }

        /// <summary>
        /// Find all employees whose names contain the search strings.
        /// Returned employees names must contain *both* first and last names.
        /// </summary>
        /// <remarks>Be sure to use LIKE for proper search matching.</remarks>
        /// <param name="firstname">The string to search for in the first_name field</param>
        /// <param name="lastname">The string to search for in the last_name field</param>
        /// <returns>A list of employees that matches the search.</returns>
        public ICollection<Employee> Search(string firstname, string lastname)
        {
            List<Employee> employees = new List<Employee>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand command = new SqlCommand(SqlFirstAndLast, conn);
                    command.Parameters.AddWithValue("@lastname", lastname);
                    command.Parameters.AddWithValue("@firstname", firstname);

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Employee employee = new Employee();
                        employee.FirstName = Convert.ToString(reader["first_name"]);
                        employee.LastName = Convert.ToString(reader["last_name"]);
                        employee.BirthDate = Convert.ToDateTime(reader["birth_date"]);
                        employee.EmployeeId = Convert.ToInt32(reader["employee_id"]);
                        employee.JobTitle = Convert.ToString(reader["job_title"]);

                        employees.Add(employee);
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Cannot find employee: " + ex.Message);
            }
            return employees;
        }

        /// <summary>
        /// Gets a list of employees who are not assigned to any active projects.
        /// </summary>
        /// <returns></returns>
        public ICollection<Employee> GetEmployeesWithoutProjects()
        {


            List<Employee> employees = new List<Employee>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand command = new SqlCommand(SqlOrphanEmployees, conn);

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Employee employee = new Employee();
                        employee.FirstName = Convert.ToString(reader["first_name"]);
                        employee.LastName = Convert.ToString(reader["last_name"]);
                        employee.BirthDate = Convert.ToDateTime(reader["birth_date"]);
                        employee.EmployeeId = Convert.ToInt32(reader["employee_id"]);
                        employee.JobTitle = Convert.ToString(reader["job_title"]);

                        employees.Add(employee);
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Cannot find employees without projects: " + ex.Message);
            }

            return employees;

        }

    }
}
