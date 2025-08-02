using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodelineHealthCareCenter.Models
{
    class BranchDepartment
    {
        //1. class fields ...
        public static List<Department> Departments = new List<Department>();
        public static List<Doctor> Doctors = new List<Doctor>();
        public static List<Admin> Admins = new List<Admin>();
        public int BranchId;

        //====================================================
        //2. class properity ...

        //====================================================
        //3. class method ...


        //Add department to the branch
        public void AddDepartment(Department department)
        {
            Console.Clear();
            // view all Departments
            Console.WriteLine("Add New Department");
            Console.WriteLine("-------------------------------------");
            Console.WriteLine("List of All Department");
            // Display all departments
            Department.GetAllDepartments();
            Console.WriteLine("-------------------------------------");
            Console.WriteLine("List of All Branches");
            // View All Branch
            Branch.GetAllBranches();

            // Select a department to add from the list
            
            int departmentId = Validation.IntValidation("Choose Department ID :");
            int branchId = Validation.IntValidation("Choose Branch ID :");


            // check if the department already exists in the branch
            if (Departments.Any(d => d.DepartmentId == departmentId && d.BranchId == branchId))
            {
                Console.WriteLine($"Department with ID {departmentId} already exists in Branch ID {branchId}.");
                return;
            }

            // Add the department to the branch
            department.DepartmentId = ++ Department.DepartmentCount; // Assuming Department class has a static field for counting departments
            department.BranchId = branchId; // Set the branch ID for the department

            Departments.Add(department);
            Console.WriteLine($"Department '{department.DepartmentName}' added to Branch ID {this.BranchId}.");
        }


        // Add Department by branch
        public void GetDepartmentByBranch(int branchId)
        {
            Console.Clear();
            Console.WriteLine($"List of Departments in Branch ID {branchId}:");
            var departmentsInBranch = Departments.Where(d => d.BranchId == branchId).ToList();
            if (departmentsInBranch.Count == 0)
            {
                Console.WriteLine("No departments found in this branch.");
                return;
            }
            foreach (var department in departmentsInBranch)
            {
                Console.WriteLine($"ID: {department.DepartmentId}, Name: {department.DepartmentName}");
            }
        }

        // Get Branch by department
        public void GetBranchByDepartment(int departmentId)
        {
            Console.Clear();
            Console.WriteLine($"List of Branches for Department ID {departmentId}:");
            var branchesWithDepartment = Departments.Where(d => d.DepartmentId == departmentId).Select(d => d.BranchId).Distinct().ToList();
            if (branchesWithDepartment.Count == 0)
            {
                Console.WriteLine("No branches found for this department.");
                return;
            }
            foreach (var branch in branchesWithDepartment)
            {
                Console.WriteLine($"Branch ID: {branch}");
            }
        }


        // Update Branch Department
        public void UpdateBranchDepartment(int departmentId, string newDepartmentName)
        {
            Console.Clear();
            var department = Departments.FirstOrDefault(d => d.DepartmentId == departmentId);
            if (department == null)
            {
                Console.WriteLine($"Department with ID {departmentId} not found.");
                return;
            }
            department.DepartmentName = newDepartmentName;
            Console.WriteLine($"Department ID {departmentId} updated to '{newDepartmentName}'.");
        }


        // Get Branch Department
        public void GetBranchDep(int departmentId)
        {
            Console.Clear();
            var department = Departments.FirstOrDefault(d => d.DepartmentId == departmentId);
            if (department == null)
            {
                Console.WriteLine($"Department with ID {departmentId} not found.");
                return;
            }
            Console.WriteLine($"Department ID: {department.DepartmentId}");
            Console.WriteLine($"Department Name: {department.DepartmentName}");
            Console.WriteLine($"Branch ID: {department.BranchId}");

        }

        // Get Department By BranchName
        public void GetDepartmentByBranchName(string branchName)
        {
            Console.Clear(); 
            Console.WriteLine($"List of Departments in Branch '{branchName}':");
            // Get banch name form list of branches
            var branch = Hospital.Branches.FirstOrDefault(b => b.BranchName.Equals(branchName, StringComparison.OrdinalIgnoreCase));

            // get all departments in the branch
            if (branch == null)
            {
                Console.WriteLine($"Branch '{branchName}' not found.");
                return;
            }
            var departmentsInBranch = Departments.Where(d => d.BranchId == branch.BranchId).ToList();
            foreach (var department in departmentsInBranch)
            {
                Console.WriteLine($"ID: {department.DepartmentId}, Name: {department.DepartmentName}");
            }

        }

        //====================================================
        //4. class constructor ...

    }
}
