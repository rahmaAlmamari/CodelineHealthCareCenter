using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodelineHealthCareCenter.Models
{
    class Doctor : User

    {
        //1. class feilds ...
        public int DepartmentId;
        public int ClinicID;
        public string DoctorSpecialization;
        public List<Booking> DoctorAppointments = new List<Booking>();
        public List<PatientRecord> PatientRecords = new List<PatientRecord>();
        //====================================================
        //2. class properity ...

        //====================================================
        //3. class method ...

        //====================================================
        //4. class constructor ...
    }
}
