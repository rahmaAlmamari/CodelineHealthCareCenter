using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodelineHealthCareCenter.Services
{
    interface ISuperAdminService
    {
        // Methods for Super Admin Service ...

        void AddDoctor(string username, string password, string email, string specialization);
        void AddAdmin(string username, string password, string email);
        void AssignAdminToBranch(int adminId, int branchId);
        void UpdateDoctor(int doctorId, string username, string email, string specialization);
        void UpdateAdmin(int adminId, string username, string email);
        void DeleteDoctor(int doctorId);
        void DeleteAdmin(int adminId);

        void SetDoctorStatus(int doctorId, bool isActive);
        void SetAdminStatus(int adminId, bool isActive);

    }
}
