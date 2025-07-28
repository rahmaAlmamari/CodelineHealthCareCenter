using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodelineHealthCareCenter.Services
{
    interface IClinicService
    {
        // Methods for Clinic Service ...

        void AddClinic(string clinicName, string location);
        void GetAllClinics();
        void GetClinicById(int clinicId);
        void GetClinicByBranchDep(int branchId, int departmentId);
        void GetClinicByName(string clinicName);
        void GetClinicName(int clinicId);
        void GetClinicByBranchName(string branchName);
        void GetClinicByDepartmentId(int departmentId);
        void GetPrice(int clinicId);
        void SetClinicStatus(int clinicId, bool isActive);
        void UpdateClinicDetails(int clinicId, string clinicName, string location, decimal price);
        void DeleteClinic(int clinicId);
    }
}
