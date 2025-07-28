using CodelineHealthCareCenter.Services;
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

        public int DoctorID { get; private set; }
        public static int DoctorCount => doctorCounter;


        //====================================================
        //3. class method ...

        public void ViewDoctorInfo() // displays the doctor's information
        {
            Console.WriteLine($"ID: {DoctorID}, Name: {UserName}, Email: {UserEmail}");
            Console.WriteLine($"Specialization: {DoctorSpecialization}, DeptID: {DepartmentId}, ClinicID: {ClinicID}");
            Console.WriteLine($"Appointments: {DoctorAppointments.Count}, Patient Records: {PatientRecords.Count}");
        }

        public static void DoctorMenu(IDoctorService service)
        {
            Additional.WelcomeMessage("Doctor Management"); 

            while (true) 
            {
                Console.Clear();
                Console.WriteLine(" DOCTOR MANAGEMENT MENU ");
                Console.WriteLine("1. Add Doctor");
                Console.WriteLine("2. Update Doctor");
                Console.WriteLine("3. Get Doctor By ID");
                Console.WriteLine("4. Get Doctor By Name");
                Console.WriteLine("5. Get Doctor By Email");
                Console.WriteLine("6. Get All Doctors");
                Console.WriteLine("7. Get Doctor Data");
                Console.WriteLine("8. Get Doctor By Branch Name");
                Console.WriteLine("9. Get Doctor By Department Name");
                Console.WriteLine("10. Exit");
                Console.Write("Select an option: ");

                string choice = Console.ReadLine();
                Console.WriteLine();

            }


            //====================================================
            //4. class constructor ...

        }
    }
}
