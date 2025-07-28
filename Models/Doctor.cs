using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodelineHealthCareCenter.Models
{
    class Doctor : User

    {
        //1. class fields ...
        public int DepartmentId;
        public int ClinicID;
        public string DoctorSpecialization;
        public List<Booking> DoctorAppointments = new List<Booking>();
        public List<PatientRecord> PatientRecords = new List<PatientRecord>();

        private static int doctorCounter = 0;
        //====================================================
        //2. class properity ...

        //====================================================
        //3. class method ...

        public static void DoctorMenu()
        {
            Console.WriteLine("Welcome to Doctor Menu");
            Console.WriteLine("1. View Appointments");
            Console.WriteLine("2. Add Patient Record");
            Console.WriteLine("3. View Patient Records");
            Console.WriteLine("4. Exit");
        }

        //====================================================
        //4. class constructor ...
        public Doctor(string name, string email, string password, int departmentId, int clinicId, string specialization)
        {
            DepartmentId = departmentId;
            ClinicID = clinicId;
            DoctorSpecialization = specialization;
        }
    }
}
