using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodelineHealthCareCenter.Models
{
    class Patient : User
    {
        //1. class feilds ...

        public string City;
        public List<Booking> PatientAppointments = new List<Booking>();
        public List<PatientRecord> PatientRecords = new List<PatientRecord>();

        //====================================================
        //2. class properity ...

        //====================================================
        //3. class method ...

        public static void PatientMenu()
        {
            Console.WriteLine("Welcome to Patient Menu");
            Console.WriteLine("1. Book Appointment");
            Console.WriteLine("2. View Appointments");
            Console.WriteLine("3. View Patient Records");
            Console.WriteLine("4. Exit");
        }

        //====================================================
        //4. class constructor ...
    }
}
