using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodelineHealthCareCenter.Services
{
    interface IPatientRecordService
    {
        void AddPatientRecord(int patientId, string recordDetails);
        void UpdatePatientRecord(int recordId, string newDetails);
        void DeletePatientRecord(int recordId);
        void GetPatientRecordById(int recordId);
        void GetAllPatientRecords();
        void GetRecordsByPatientId(int patientId);
        void GetRecordsByClinicIdAndDate(int clinicId, DateTime date);
    }
}
