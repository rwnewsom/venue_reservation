using ProjectOrganizer.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
namespace ProjectOrganizer.DAL
{
    public class ProjectSqlDAO : IProjectDAO
    {
        private readonly string connectionString;
        private const string ReturnProjects = "SELECT * FROM employee";

        // Single Parameter Constructor
        public ProjectSqlDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        /// <summary>
        /// Returns all projects.
        /// </summary>
        /// <returns></returns>
        public ICollection<Project> GetAllProjects()
        {
            // throw new NotImplementedException();

            List<Project> projects = new List<Project>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand command = new SqlCommand(ReturnProjects, conn);

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Project project = new Project();
                        project.ProjectId = Convert.ToInt32(reader["project_id"]);
                        project.Name = Convert.ToString(reader["name"]);
                        project.StartDate = Convert.ToDateTime(reader["from_date"]);
                        project.EndDate = Convert.ToDateTime(reader["to_date"]);


                        projects.Add(project);
                    }
                }

            }
            catch (SqlException ex)
            {
                Console.WriteLine("error getting projects: " + ex.Message);
            }

            return projects;
        }

        /// <summary>
        /// Assigns an employee to a project using their IDs.
        /// </summary>
        /// <param name="projectId">The project's id.</param>
        /// <param name="employeeId">The employee's id.</param>
        /// <returns>If it was successful.</returns>
        public bool AssignEmployeeToProject(int projectId, int employeeId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Removes an employee from a project.
        /// </summary>
        /// <param name="projectId">The project's id.</param>
        /// <param name="employeeId">The employee's id.</param>
        /// <returns>If it was successful.</returns>
        public bool RemoveEmployeeFromProject(int projectId, int employeeId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Creates a new project.
        /// </summary>
        /// <param name="newProject">The new project object.</param>
        /// <returns>The new id of the project.</returns>
        public int CreateProject(Project newProject)
        {
            throw new NotImplementedException();
        }

    }
}
