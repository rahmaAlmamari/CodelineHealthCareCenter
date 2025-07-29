using CodelineHealthCareCenter.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodelineHealthCareCenter.Models
{
    class PatientRecord
    {
        //1. class feilds ...
        public int PatientRecordId;

        public List<Service> Services = new List<Service>();
        public double TotalCost;
        public string DoctorNote;
        public static int PatientRecordCount = 0;
        public static IPatientRecordService service; // to support menu without parameters


        //====================================================
        //2. class properity ...

        //====================================================
        //3. class method ...

        //====================================================
        //4. class constructor ...
        public PatientRecord()
        {
            PatientRecordCount++;
            PatientRecordId = PatientRecordCount;
        }
    }
}
