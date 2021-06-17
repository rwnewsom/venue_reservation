-- This file will set up your database before testing
-- Delete all of the data
DELETE FROM project_employee;
DELETE FROM project;
DELETE FROM employee;
DELETE FROM department;

SELECT * FROM department;

-- Insert a fake department
INSERT INTO department (name) VALUES ('Department of Naming Departments');
DECLARE @newDepartmentId int = (SELECT @@IDENTITY);

-- Insert a fake project
INSERT INTO project (name, from_date, to_date) VALUES ('Project Y', '1965-03-01', '2007-08-31');

-- Insert a fake employee
INSERT INTO employee (department_id, first_name, last_name, birth_date, hire_date, job_title)
VALUES (1, 'Newman', 'Roderick', '1977-07-15', '2012-06-01', 'Oxygen Consultant');


--assign fake employee to fake project
INSERT INTO project_employee (project_id, employee_id) VALUES (1, 1);


-- Return the id of the fake department
SELECT @newDepartmentId as newDepartmentId;