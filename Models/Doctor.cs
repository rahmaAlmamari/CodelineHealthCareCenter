using System;
using System.Collections.Generic;
using CodelineHealthCareCenter.Services;


namespace CodelineHealthCareCenter.Models
{
    class Doctor : User
    {
        //====================================================
        //1. class fields ...

        public int DepartmentId;
        public int ClinicID;
        public string DoctorSpecialization;
        public List<Booking> DoctorAppointments = new List<Booking>();
        public List<PatientRecord> PatientRecords = new List<PatientRecord>();
        public List<DateTime> AvailableTimes = new List<DateTime>();

        private static int doctorCounter = 0;
        public static IDoctorService service; // Used for DoctorMenu()
        public static List<Doctor> Doctors = new List<Doctor>(); // Used to store all doctors in the system


        //====================================================
        //2. class properties ...

        public int DoctorID { get; private set; }
        public static int DoctorCount => doctorCounter;

        //====================================================
        //3. class methods ...

        public void ViewDoctorInfo() // Displays basic information about the doctor
        {
            Console.WriteLine($"ID: {DoctorID}, Name: {UserName}, Email: {UserEmail}");
            Console.WriteLine($"Specialization: {DoctorSpecialization}, DeptID: {DepartmentId}, ClinicID: {ClinicID}");
            Console.WriteLine($"Available Times: {AvailableTimes.Count}, Patient Records: {PatientRecords.Count}"); 

            if (AvailableTimes.Count > 0) // Displays available time slots for the doctor
            {
                Console.WriteLine("Available Time Slots:");
                foreach (var time in AvailableTimes)
                    Console.WriteLine($"- {time:G}");
            }
            else // If no available time slots, display a message
            {
                Console.WriteLine("No available time slots.");
            }

        }

        public static void AddDoctor(string username, string password, string email, string specialization) // Adds a new doctor to the system
        {
            var newDoctor = new Doctor(username, email, specialization, 0, 0);
            Doctors.Add(newDoctor);
            Console.WriteLine($"Doctor '{username}' added successfully.");
        }

        public static void UpdateDoctor(int doctorId, string username, string email, string specialization, bool isActive) // Updates an existing doctor's information
        {
            var doctor = Doctors.FirstOrDefault(d => d.DoctorID == doctorId);
            if (doctor == null)
            {
                Console.WriteLine("Doctor not found.");
                return;
            }

            doctor.UserName = username;
            doctor.UserEmail = email;
            doctor.DoctorSpecialization = specialization;
            doctor.UserStatus = isActive ? "Active" : "Inactive";
            Console.WriteLine($"Doctor ID {doctorId} updated successfully.");
        }

        public static void GetDoctorById(int doctorId) // Retrieves a doctor by their ID and displays their information
     
        {
            var doctor = Doctors.FirstOrDefault(d => d.DoctorID == doctorId);
            if (doctor == null)
            {
                Console.WriteLine("Doctor not found.");
                return;
            }

            doctor.ViewDoctorInfo();
        }

        public static void GetDoctorByName(string username) // Retrieves doctors by their username and displays their information
        {
            var matches = Doctors.Where(d => d.UserName.Equals(username, StringComparison.OrdinalIgnoreCase)).ToList();
            if (!matches.Any())
            {
                Console.WriteLine("No doctor found with that name.");
                return;
            }

            foreach (var doc in matches)
                doc.ViewDoctorInfo();
        }

        public static void GetDoctorByEmail(string email) // Retrieves a doctor by their email and displays their information
        {
            var doctor = Doctors.FirstOrDefault(d => d.UserEmail.Equals(email, StringComparison.OrdinalIgnoreCase));
            if (doctor == null)
            {
                Console.WriteLine("No doctor found with that email.");
                return;
            }

            doctor.ViewDoctorInfo();
        }

        public static void GetAllDoctors() // Retrieves and displays all registered doctors in the system
        {
            if (Doctors.Count == 0)
            {
                Console.WriteLine("No doctors registered yet.");
                return;
            }

            foreach (var doctor in Doctors)
                doctor.ViewDoctorInfo();
        }

        public static void GetDoctorData(int doctorId) // Retrieves detailed information about a doctor, including their appointments and patient records
        {
            var doctor = Doctors.FirstOrDefault(d => d.DoctorID == doctorId);
            if (doctor == null)
            {
                Console.WriteLine("Doctor not found.");
                return;
            }

            Console.WriteLine("[Doctor Info]");
            doctor.ViewDoctorInfo();

            Console.WriteLine("\n[Appointments]");
            foreach (var app in doctor.DoctorAppointments)
            {
                Console.WriteLine($"- Booking ID: {app.BookingId}, Date: {app.BookingDate}");
            }

            Console.WriteLine("\n[Patient Records]");
            foreach (var rec in doctor.PatientRecords)
            {
                rec.ViewRecordDetails();
            }
        }

        public static void GetDoctorByBranchName(string branchName) // Searches for doctors associated with a specific branch name
        {
            Console.WriteLine($"Searching for doctors in branch '{branchName}'...");
            Console.WriteLine("Functionality to be linked with Branch model.");
        }

          public static void GetDoctorByDepartmentName(string departmentName) // Searches for doctors associated with a specific department name
        {
            Console.WriteLine($"Searching for doctors in department '{departmentName}'...");
            Console.WriteLine("Functionality to be linked with Department model.");
        }

        public static void AddAvailableTime(int doctorId, DateTime availableTime) // Adds an available time slot for a specific doctor
        {
            var doctor = Doctors.FirstOrDefault(d => d.DoctorID == doctorId);
            if (doctor == null)
            {
                Console.WriteLine("Doctor not found.");
                return;
            }
            if (doctor.AvailableTimes.Contains(availableTime))
            {
                Console.WriteLine("This time slot is already available.");
                return;
            }
            doctor.AvailableTimes.Add(availableTime);
            Console.WriteLine($"Available time {availableTime:G} added for Doctor ID {doctorId}.");
        }


        public static void DoctorMenu() // Displays the Doctor Management Menu and handles user input for various doctor-related operations
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
                Console.WriteLine("10. Add Available Time");
                Console.WriteLine("11. Remove Available Time");
                Console.WriteLine("11. Exit");
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

                            Console.Write("Is Active (true/false): ");
                            bool isActive;
                            while (!bool.TryParse(Console.ReadLine(), out isActive))
                            {
                                Console.WriteLine("Invalid input. Please enter true or false:");
                            }

                            service.UpdateDoctor(updateId, newUsername, newEmail, newSpec, isActive);
                        }
                        else Console.WriteLine("Update cancelled.");
                        break;

                    case "3": // Get a doctor by ID
                        int id = Validation.IntValidation("Doctor ID");
                        service.GetDoctorById(id);
                        break;

                    case "4": // Get a doctor by name
                        string docName = Validation.StringNamingValidation("Doctor Name");
                        service.GetDoctorByName(docName);
                        break;

                    case "5": // Get a doctor by email
                        string docEmail = Validation.StringValidation("Doctor Email");
                        service.GetDoctorByEmail(docEmail);
                        break;

                    case "6": // Get all doctors
                        service.GetAllDoctors();
                        break;

                    case "7": // Get doctor data by ID
                        int docDataId = Validation.IntValidation("Doctor ID for Data");
                        service.GetDoctorData(docDataId);
                        break;

                    case "8": // Get doctor by branch name
                        string branchName = Validation.StringNamingValidation("Branch Name");
                        service.GetDoctorByBranchName(branchName);
                        break;

                    case "9": // Get doctor by department name
                        string deptName = Validation.StringNamingValidation("Department Name");
                        service.GetDoctorByDepartmentName(deptName);
                        break;

                        case "10": // Add available time slot for a doctor
                        int doctorId = Validation.IntValidation("Doctor ID to add available time");
                        DateTime availableTime = Validation.DateTimeValidation("Available Time (yyyy-MM-dd HH:mm)");
                        var doctor = Doctors.FirstOrDefault(d => d.DoctorID == doctorId);
                        if (doctor != null)
                        {
                            doctor.AvailableTimes.Add(availableTime);
                            Console.WriteLine($"Available time {availableTime:G} added for Doctor ID {doctorId}.");
                        }
                        else
                        {
                            Console.WriteLine("Doctor not found.");
                        }
                        break;

                        case "11": // Remove available time slot for a doctor
                        int removeDoctorId = Validation.IntValidation("Doctor ID to remove available time");
                        DateTime removeTime = Validation.DateTimeValidation("Available Time to Remove (yyyy-MM-dd HH:mm)");
                        var removeDoctor = Doctors.FirstOrDefault(d => d.DoctorID == removeDoctorId);
                        if (removeDoctor != null)
                        {
                            if (removeDoctor.AvailableTimes.Remove(removeTime))
                            {
                                Console.WriteLine($"Available time {removeTime:G} removed for Doctor ID {removeDoctorId}.");
                            }
                            else
                            {
                                Console.WriteLine("Available time not found.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Doctor not found.");
                        }
                        break;

                    case "12": //   Exit the doctor menu
                    Console.WriteLine("Exiting Doctor Menu...");
                    return;

                    default: // Invalid option
                        Console.WriteLine("Invalid option. Try again.");
                        break;
                }

                Additional.HoldScreen();
            }
        }

        //====================================================
        //4. class constructor ...

        public Doctor(string username, string email, string specialization, int departmentId, int clinicId)
        {
            doctorCounter++;
            DoctorID = doctorCounter;
            DoctorSpecialization = specialization;
            DepartmentId = departmentId;
            ClinicID = clinicId;
            UserName = username;
            UserEmail = email;
            UserRole = "Doctor";
            UserStatus = "Active";

            DoctorAppointments = new List<Booking>();
            PatientRecords = new List<PatientRecord>();
        }
    }
}
