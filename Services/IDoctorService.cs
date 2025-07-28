using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodelineHealthCareCenter.Services
{
    interface IDoctorService
    {
        // Methods for Doctor Service
        void AddDoctor(string username, string password, string email, string specialization);
        void UpdateDoctorDetails(int doctorId, string username, string email, string specialization);
      
        void GetDoctorById(int doctorId);
        void GetDoctorByName(string username);
        void GetAllDoctors();
        void GetDoctorsByBranchDep(int branchId, int departmentId);
        void GetDoctorByEmail(string email);
        void GetDoctorData(int doctorId);
        void GetDoctorByBranchName(string branchName);
        void GetDoctorByDepartmentName(string departmentName);
        void UpdateDoctor(int doctorId, string username, string email, string specialization, bool isActive);
        void GetDoctorDetailsById(int doctorId);
    }
}
