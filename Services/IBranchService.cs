using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodelineHealthCareCenter.Services
{
    interface IBranchService
    {
        // Methods for Branch Service ...
        void AddBranch();
        void GetAllBranches();
        void GetBranchById(int branchId);
        void GetBranchDetails(int branchId);
        void GetBranchDetailsByBranchName(string branchName);
        void GetBranchName(string branchName);
        void GetBranchStatus(string branchName);
        void UpdateBranch(int branchId, string branchName, string location);
        void DeleteBranch(int branchId);
       
       


    }
}
