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
        public static string DepartmentsFilePath = "Departments.txt"; 

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
            Console.WriteLine($"Department '{newDepartment.DepartmentName}' created in Branch ID {newDepartment.BranchId}.");

            // Increment the department count
            DepartmentCount++;
            // Save the new department to file
            //SaveDepartmentsToFile();
            Additional.HoldScreen();
            SuperAdmin.AdminDepartmentMenu();
            

        }

        // Get All Departments
        public static void GetAllDepartments()
        {
            Console.Clear();
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
            Additional.HoldScreen();
            SuperAdmin.AdminDepartmentMenu();
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
            Additional.HoldScreen();
            SuperAdmin.AdminDepartmentMenu();
        }

        // Set Department Active Status
        public static void SetDepartmentActiveStatus(int departmentId, bool isActive)
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

        // Get Department By ID
        public static Department GetDepartmentById(int departmentId)
        {
            var department = BranchDepartment.Departments.FirstOrDefault(d => d.DepartmentId == departmentId);
            if (department != null)
            {
                return department;
            }
            else
            {
                Console.WriteLine($"Department ID {departmentId} not found.");
                return null;
            }

        }


        // Get Department By Name
        public static Department GetDepartmentByName(string departmentName)
        {
            var department = BranchDepartment.Departments.FirstOrDefault(d => d.DepartmentName.Equals(departmentName, StringComparison.OrdinalIgnoreCase));
            if (department != null)
            {
                return department;
            }
            else
            {
                Console.WriteLine($"Department '{departmentName}' not found.");
                return null;
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
            Additional.HoldScreen();
            SuperAdmin.AdminDepartmentMenu();
        }


        // Department Exists Check
        public static bool DepartmentExists(int departmentId)
        {
            return BranchDepartment.Departments.Any(d => d.DepartmentId == departmentId);
        }

        // Check department status
        public static bool IsDepartmentActive(int departmentId)
        {
            var department = BranchDepartment.Departments.FirstOrDefault(d => d.DepartmentId == departmentId);
            if (department != null)
            {
                // Assuming active status is determined by some property, e.g., IsActive
                return true; // Placeholder, replace with actual logic if needed
            }
            else
            {
                Console.WriteLine($"Department ID {departmentId} not found.");
                return false;
            }
        }

        // view All Departments
        public static void ViewAllDepartments()
        {
            
            Console.WriteLine("List of All Departments:");
            if (BranchDepartment.Departments.Count == 0)
            {
                Console.WriteLine("No departments available.");
                return;
            }

            foreach (var department in BranchDepartment.Departments)
            {
                Console.WriteLine($"ID: {department.DepartmentId}, Name: {department.DepartmentName}, Branch ID: {department.BranchId}");
            }
           
        }


        // save department to file
        public static void SaveDepartmentsToFile()
        {
            using (StreamWriter writer = new StreamWriter(DepartmentsFilePath))
            {
                foreach (var department in BranchDepartment.Departments)
                {
                    writer.WriteLine($"{department.DepartmentId}|{department.DepartmentName}|{department.BranchId}");
                }
            }
            Console.WriteLine("Departments saved to file.");

        }

        // Load departments from file
        public static void LoadDepartmentsFromFile()
        {
            if (File.Exists(DepartmentsFilePath))
            {
                BranchDepartment.Departments.Clear(); // Optional: clear current list

                using (StreamReader reader = new StreamReader(DepartmentsFilePath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        var parts = line.Split('|');
                        if (parts.Length == 3)
                        {
                            Department department = new Department
                            {
                                DepartmentId = int.Parse(parts[0]),
                                DepartmentName = parts[1],
                                BranchId = int.Parse(parts[2])
                            };
                            BranchDepartment.Departments.Add(department);

                            // Ensure DepartmentCount is always the max ID
                            if (department.DepartmentId > DepartmentCount)
                                DepartmentCount = department.DepartmentId;
                        }
                    }
                }
                Console.WriteLine("Departments loaded from file.");
            }
            else
            {
                Console.WriteLine("No departments file found.");
            }
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
