using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodelineHealthCareCenter.Services
{
    interface IBranchDepartmentService
    {
        // Methods for Branch Department Service ...
        void AddDepartmentToBranch(int branchId, int departmentId);
        void GetDepartmentsByBranch(int branchId);
        void GetBranchesByDepartment(int departmentId);
        void UpdateBranchDepartment(int branchId, int departmentId, string newDepartmentName);
        void GetBranchDep(int branchId, int departmentId);
        void GetDepartmentByBranchName(string BranchName);
    }
}
