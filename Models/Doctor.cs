using System;
using System.Collections.Generic;
using CodelineHealthCareCenter.Services;
using System.Linq;
using System.IO;






namespace CodelineHealthCareCenter.Models
{
    class Doctor : User
    {
        //====================================================
        //1. class fields ...

        public int DepartmentId;
        public int ClinicID;
        public string DoctorSpecialization;
        public int BranchID;
        public List<Booking> DoctorAppointments = new List<Booking>();
        public List<PatientRecord> PatientRecords = new List<PatientRecord>();
        public List<DateTime> AvailableTimes = new List<DateTime>();

        private static int doctorCounter = 0;
        public static IDoctorService service; // Used for DoctorMenu()
        public static List<Doctor> Doctors = new List<Doctor>(); // Used to store all doctors in the system


        //====================================================
        //2. class properties ...

        //public int DoctorID { get; private set; }
        public static int DoctorCount => doctorCounter;

        //====================================================
        //3. class methods ...

        public void ViewDoctorInfo() // Displays basic information about the doctor
        {
            Console.WriteLine($"ID: {UserId}, Name: {UserName}, Email: {UserEmail}");
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
            var newDoctor = new Doctor(username, email, specialization);
            Doctors.Add(newDoctor);
            Console.WriteLine($"Doctor '{username}' added successfully.");
        }

        public static void UpdateDoctor(int doctorId, string username, string email, string specialization, bool isActive) // Updates an existing doctor's information
        {
            var doctor = Doctors.FirstOrDefault(d => d.UserId == doctorId);
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
            var doctor = Doctors.FirstOrDefault(d => d.UserId == doctorId);
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

            if (BranchDepartment.Doctors.Count == 0)
            {
                Console.WriteLine("No doctors registered yet.");
                return;
            }

            foreach (var doctor in BranchDepartment.Doctors)
            {
                Console.WriteLine($"Doctor ID       : {doctor.UserId}");
                Console.WriteLine($"Name            : {doctor.UserName}");
                Console.WriteLine($"Email           : {doctor.UserEmail}");
                Console.WriteLine($"Phone Number    : {doctor.UserPhoneNumber}");
                Console.WriteLine($"National ID     : {doctor.UserNationalID}");
                Console.WriteLine($"Specialization  : {doctor.DoctorSpecialization}");
                Console.WriteLine($"Status          : {doctor.UserStatus}");
                Console.WriteLine(new string('-', 40));
            }
            //doctor.ViewDoctorInfo();

        }

        public static void GetDoctorData(int doctorId) // Retrieves detailed information about a doctor, including their appointments and patient records
        {
            var doctor = Doctors.FirstOrDefault(d => d.UserId == doctorId);
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
                Console.WriteLine($"- Booking ID: {app.BookingId}, Date: {app.BookingDateTime}");
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
            var doctor = Doctors.FirstOrDefault(d => d.UserId == doctorId);
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

        public static void RemoveAvailableTime(int doctorId, DateTime availableTime) // Removes an available time slot for a specific doctor
        {
            var doctor = Doctors.FirstOrDefault(d => d.UserId == doctorId);
            if (doctor == null)
            {
                Console.WriteLine("Doctor not found.");
                return;
            }
            if (!doctor.AvailableTimes.Remove(availableTime))
            {
                Console.WriteLine("This time slot is not available.");
                return;
            }
            Console.WriteLine($"Available time {availableTime:G} removed for Doctor ID {doctorId}.");
        }

        public void ViewMyAppointments(Doctor doctor) // Displays the appointments for the current doctor
        {
            Console.WriteLine($"Appointments for Dr. {UserName}:");
            if (doctor.DoctorAppointments.Count == 0)
            {
                Console.WriteLine("No appointments found.");
                return;
            }
            foreach (var app in doctor.DoctorAppointments)
                Console.WriteLine($"- Booking ID: {app.BookingId}, Date: {app.BookingDateTime:G}");
            Additional.HoldScreen();
        }
        

        public void AddPatientRecord(Doctor doctor) // Adds a patient record for a specific patient, including doctor notes and services provided
        {
            //to list all patient who have an appointment with this doctor ...
            Validation.ListPatientsByDoctorId(doctor.UserId);
            //to get the national ID of the patient ...
            string nationalId = Validation.StringValidation("Patient National ID");
            foreach (var branch in Hospital.Branches)
            {
                var patient = branch.Patients.FirstOrDefault(p => p.UserNationalID == nationalId);
                if (patient == null)
                {
                    Console.WriteLine("Patient not found.");
                    return;
                }
                //to get patient appointments ...
                var patientAppointments = patient.PatientAppointments.Where(a => a.DoctorId == doctor.UserId).ToList();
                string note = Validation.StringValidation("Doctor Note");
                //to get total cost of service ...
                double totalCost = 0;
                foreach(var app in doctor.DoctorAppointments)
                {
                    foreach(var PatientAPP in patientAppointments)
                    {
                        if (app.BookingId == PatientAPP.BookingId)
                        {
                            totalCost += app.BookingService.Sum(s => s.Price);
                        }
                    }
                }

                var record = new PatientRecord(patient.UserId, ClinicID, note, totalCost);
                this.PatientRecords.Add(record);
                patient.PatientRecords.Add(record);

                Console.WriteLine("Patient record successfully added.");
            }
        }

        //public static void SaveToFile(string filePath) // save data to file 
        //{
        //    using StreamWriter writer = new StreamWriter(filePath);
        //    foreach (var doc in Doctors)
        //    {
        //        // Save: DoctorID | UserName | Email | Specialization | DepartmentId | ClinicId | Status
        //        writer.WriteLine($"{doc.UserId}|{doc.UserName}|{doc.UserEmail}|{doc.DoctorSpecialization}|{doc.DepartmentId}|{doc.ClinicID}|{doc.UserStatus}");
        //    }
        //}

        //public static void LoadFromFile(string filePath) 
        //{
        //    Doctors.Clear();
        //    doctorCounter = 0;

        //    if (!File.Exists(filePath)) return;

        //    string[] lines = File.ReadAllLines(filePath);
        //    foreach (var line in lines)
        //    {
        //        string[] parts = line.Split('|');
        //        if (parts.Length < 7) continue;

        //        Doctor doctor = new Doctor(
        //            username: parts[1],
        //            email: parts[2],
        //            specialization: parts[3]
        //        );

        //        doctor.UserId = int.Parse(parts[0]); // override auto-incremented ID
        //        doctor.UserStatus = parts[6];

        //        Doctors.Add(doctor);
        //        if (doctor.UserId > doctorCounter)
        //            doctorCounter = doctor.UserId;
        //    }
        //}



        public static void DoctorMenu() // Displays the doctor management menu and handles user input for various doctor-related operations
        {
            Additional.WelcomeMessage("Doctor Panel");

            while (true)
            {
                Console.Clear();
                Console.WriteLine(" DOCTOR MENU ");
                Console.WriteLine("1. View Appointments");
                Console.WriteLine("2. Add Patient Record");
                Console.WriteLine("3. Exit");
                Console.Write("Select an option: ");

                string choice = Console.ReadLine();
                Console.WriteLine();


                switch (choice)
                {
                    case "1": // View Appointments
                        string DoctorNationalId = Validation.StringValidation("Your Doctor national id");
                        var doctor = BranchDepartment.Doctors.FirstOrDefault(d => d.UserNationalID == DoctorNationalId);
                        if (doctor != null)
                            doctor.ViewMyAppointments(doctor);
                        else
                            Console.WriteLine("Doctor not found.");
                        break;

                    case "2": // Add Patient Record
                        string DoctorNationalId2 = Validation.StringValidation("Your Doctor National id");
                        var doctor2 = BranchDepartment.Doctors.FirstOrDefault(d => d.UserNationalID == DoctorNationalId2);
                        if (doctor2 != null)
                            doctor2.AddPatientRecord(doctor2);
                        else
                            Console.WriteLine("Doctor not found.");
                        break;

                    case "3": // Exit
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

        public Doctor(string username, string email, string specialization)
        {
            DoctorSpecialization = specialization;
            UserName = username;
            UserEmail = email;
            UserRole = "Doctor";
            UserStatus = "Active";
            DoctorAppointments = new List<Booking>();
            PatientRecords = new List<PatientRecord>();
        }
    }
}
