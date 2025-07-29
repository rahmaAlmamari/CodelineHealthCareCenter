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
        //1. class fields ...
        public int PatientRecordId;

        public List<Service> Services = new List<Service>();
        public double TotalCost;
        public string DoctorNote;
        public static int PatientRecordCount = 0;
        public static IPatientRecordService service; // to support menu without parameters


        //====================================================
        //2. class property ...

        public static int RecordCount => PatientRecordCount;

        //====================================================
        //3. class method ...

        public void ViewRecordDetails() // displays the details of the patient record
        {
            Console.WriteLine($"Record ID: {PatientRecordId}");
            Console.WriteLine($"Total Cost: ${TotalCost}");
            Console.WriteLine($"Doctor Note: {DoctorNote}");
            Console.WriteLine($"Services ({Services.Count}):");
            foreach (var s in Services)
                Console.WriteLine($" - {s.ServiceName} (${s.Price})");
        }

        
        //====================================================
        //4. class constructor ...
        public PatientRecord()
        {
            PatientRecordCount++;
            PatientRecordId = PatientRecordCount;
        }
    }
}
