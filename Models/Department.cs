using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodelineHealthCareCenter.Models
{
    class Department
    {
        //1. class fields ...
        public int DepartmentId;
        public string DepartmentName;
        public int BranchId;
        public static int DepartmentCount = 0;
        public List<Clinic> Clinics = new List<Clinic>();

        //====================================================
        //2. class properity ...


        //====================================================
        //3. class method ...

        // Create Department
        public static void CreateDepartment(string departmentName, int branchId)
        {
           
            Department newDepartment = new Department
            {
                DepartmentName = departmentName,
                BranchId = branchId
            };
            // add the new department to the list of departments in the branch
            BranchDepartment.Departments.Add(newDepartment); 
            Console.WriteLine($"Department '{newDepartment.DepartmentName}' created with ID {newDepartment.DepartmentId} in Branch ID {newDepartment.BranchId}.");

        }

        // Get All Departments
        public static void GetAllDepartments()
        {
            Console.WriteLine("List of Departments:");
            if (DepartmentCount == 0)
            {
                Console.WriteLine("No departments available.");
                return;
            }
            
            foreach (var department in BranchDepartment.Departments)
            {
                Console.WriteLine($"ID: {department.DepartmentId}, Name: {department.DepartmentName}, Branch ID: {department.BranchId}");
            }
        }


        // Update Department
        public static void UpdateDepartment(int departmentId, string newName)
        {
            var department = BranchDepartment.Departments.FirstOrDefault(d => d.DepartmentId == departmentId);
            if (department != null)
            {
                department.DepartmentName = newName;
                Console.WriteLine($"Department ID {departmentId} updated to '{newName}'.");
            }
            else
            {
                Console.WriteLine($"Department ID {departmentId} not found.");
            }
        }

        // Set Department Active Status
        public static void SetDepartmentStatus(int departmentId, bool isActive)
        {
            var department = BranchDepartment.Departments.FirstOrDefault(d => d.DepartmentId == departmentId);
            if (department != null)
            {
               
                Console.WriteLine($"Department ID {departmentId} status set to {(isActive ? "Active" : "Inactive")}.");
            }
            else
            {
                Console.WriteLine($"Department ID {departmentId} not found.");
            }
        }


        // Get Department Name
        public static string GetDepartmentName(int departmentId)
        {
            var department = BranchDepartment.Departments.FirstOrDefault(d => d.DepartmentId == departmentId);
            if (department != null)
            {
                return department.DepartmentName;
            }
            else
            {
                Console.WriteLine($"Department ID {departmentId} not found.");
                return null;
            }
        }


        // Delete Department

        public static void DeleteDepartment(int departmentId)
        {
            var department = BranchDepartment.Departments.FirstOrDefault(d => d.DepartmentId == departmentId);
            if (department != null)
            {
                BranchDepartment.Departments.Remove(department);
                Console.WriteLine($"Department ID {departmentId} deleted successfully.");
            }
            else
            {
                Console.WriteLine($"Department ID {departmentId} not found.");
            }
        }


        // Department Exists Check
        public static bool DepartmentExists(int departmentId)
        {
            return BranchDepartment.Departments.Any(d => d.DepartmentId == departmentId);
        }


        //====================================================
        //4. class constructor ...
        public Department()
        {
            DepartmentCount++;
            DepartmentId = DepartmentCount;
        }
    }
}
