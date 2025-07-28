using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodelineHealthCareCenter.Services
{
    interface IAdminService
    {
        // Methods for Admin Service ...

        void AssignDoctorToClinic(int doctorId, int clinicId);
        void AddClinicService(int clinicId, int serviceId);
        void GetClinicServices(int clinicId);
        void GetClinicDoctors(int clinicId);
    }
}
