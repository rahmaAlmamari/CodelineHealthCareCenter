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

        // Add a new department to the branch
        public static void AddDepartment(Department department)
        {
            Departments.Add(department);
            Console.WriteLine($"Department '{department.DepartmentName}' added to Branch ID {department.BranchId}.");
        }




        //====================================================
        //4. class constructor ...

    }
}
