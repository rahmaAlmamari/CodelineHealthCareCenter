using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodelineHealthCareCenter.Services
{
    interface IDepartmentService
    {
        // Methods for Department Service ...

        void CreateDepartment(string departmentName, string description);
        void GetAllDepartments();
        void UpdateDepartment(int departmentId, string departmentName, string description);
        void SetDepartmentActiveStatus(int departmentId, bool isActive);
        void GetDepartmentById(int departmentId);
        void GetDepartmentByName(string departmentName);
        void GetDepartmentName(int departmentId);
        void DeleteDepartment(int departmentId);

    }
}
