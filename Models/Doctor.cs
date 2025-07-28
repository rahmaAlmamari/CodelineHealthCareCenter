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

                switch (choice)
                {
                    case "1": // Add a new doctor
                        string username = Validation.StringNamingValidation("Doctor Username");
                        string password = Validation.ReadPassword("Password");
                        string email = Validation.StringValidation("Email");
                        string specialization = Validation.StringNamingValidation("Specialization");
                        service.AddDoctor(username, password, email, specialization);
                        break;

                    case "2": // Update an existing doctor
                        int updateId = Validation.IntValidation("Doctor ID to update");
                        if (Additional.ConfirmAction("update this doctor"))
                        {
                            string newUsername = Validation.StringNamingValidation("New Username");
                            string newEmail = Validation.StringValidation("New Email");
                            string newSpec = Validation.StringNamingValidation("New Specialization");
                            bool isActive;
                            Console.WriteLine("Is Active (true/false): ");
                            while (!bool.TryParse(Console.ReadLine(), out isActive))
                            {
                                Console.WriteLine("Invalid input. Please enter true or false:");
                            }
                            service.UpdateDoctor(updateId, newUsername, newEmail, newSpec, isActive);
                        }
                        else Console.WriteLine("Update cancelled.");
                        break;

                    case "3": // Get doctor by ID
                        int id = Validation.IntValidation("Doctor ID");
                        service.GetDoctorById(id);
                        break;

                    case "4": // Get doctor by name
                        string docName = Validation.StringNamingValidation("Doctor Name");
                        service.GetDoctorByName(docName);
                        break;

                    case "5": // Get doctor by email
                        string docEmail = Validation.StringValidation("Doctor Email");
                        service.GetDoctorByEmail(docEmail);
                        break;

                    case "6": // Get all doctors
                        service.GetAllDoctors();
                        break;

                    case "7": // Get doctor data
                        int docDataId = Validation.IntValidation("Doctor ID for Data");
                        service.GetDoctorData(docDataId);
                        break;

                    case "8": // Get doctor by branch name
                        string branchName = Validation.StringNamingValidation("Branch Name");
                        service.GetDoctorByBranchName(branchName);
                        break;




                }


                //====================================================
                //4. class constructor ...

            }
        }
    }
}
