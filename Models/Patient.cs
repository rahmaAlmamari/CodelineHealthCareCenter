using CodelineHealthCareCenter.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodelineHealthCareCenter.Models
{
    class Patient : User, IPatientService
    {
        //1. class feilds ...

        public string PatientCity;
        public List<Booking> PatientAppointments = new List<Booking>();
        public List<PatientRecord> PatientRecords = new List<PatientRecord>();
        //patient file path ...
        private static string filePath = "patients.txt";


        //====================================================
        //2. class properity ...

        //====================================================
        //3. class method ...
        //-------------------------------------To add new patient--------------------------------------
        public static void PatientMenu()
        {
            bool Flag = true; //to store the user choice ...
            //to display the patient menu options ...
            do
            {
                char choice = '0'; //to store the user choice ...
                Console.Clear(); //to clear the console ...
                Console.WriteLine("Welcome to Patient Menu");
                Console.WriteLine("1. Book Appointment");
                Console.WriteLine("2. View Appointments");
                Console.WriteLine("3. View Patient Records");
                Console.WriteLine("0. Exit");
                Console.WriteLine("Please select an option: ");
                //to get the user choice ...
                choice = Validation.CharValidation("option");
                //to check the user choice ...
                switch(choice)
                {
                    case '1': //to book an appointment ...
                        Booking.BookAppointment();
                        break;
                    case '2': //to view appointments ...
                        ViewPatientAppointments();
                        break;
                    case '3': //to view patient records ...
                        ViewPatientRecords();
                        break;
                    case '0': //to exit the patient menu ...
                        Flag = false; //to exit the loop ...
                        break;
                    default: //if the user choice is invalid ...
                        Console.WriteLine("Invalid choice. Please try again.");
                        Additional.HoldScreen(); //just to hold the screen ...
                        break;
                }
            } while (Flag);


        }
        //to singUp new patient ...
        public static void SinUp()
        {
            //to get national id and check if it exists or not ...
            string UserNationalID = Validation.UserNationalIdValidation();
            ////to check if the national id exists or not ...
            //if (Validation.UserNationalIdExists(UserNationalID))
            //{
            //    Console.WriteLine("This national ID already exists. Please try again with a different one.");
            //    Additional.HoldScreen();
            //    return; //exit the method if national ID exists ...
            //}
            //to get the user data ...
            string UserName = Validation.StringValidation("user name");
            string UserPassword = Validation.ReadPassword("password");
            string UserEmail = Validation.EmailValidation("email");
            int UserPhoneNumber = Validation.UserPhoneNumberValidation();
            DisplayBranchesCities(); //to display the branches cities in the hospital class ...
            string UserCity = Validation.StringValidation("city");
            //UserRole and UserStatus are set to default values for patient in patient constructor ...
            //to do hashing for the password ...
            string UserPasswordHashed = Validation.HashPasswordPBKDF2(UserPassword);
            //to find the branch index that the patient wants to register in using city ...
            Branch branch = FindBranchByCity(UserCity);
            //int BranchIndex = Hospital.Branches.FindIndex(b => b.BranchCity.Equals(UserCity, StringComparison.OrdinalIgnoreCase));
            //to call the AddPatient method to create a new patient ...
            Patient patientService = new Patient();//this opject just to call the method ...
            patientService.AddPatient(UserName, UserPasswordHashed, UserEmail, UserPhoneNumber, UserNationalID, UserCity, branch);

        }
        //to display city of all branches in the hospital class ...
        public static void DisplayBranchesCities()
        {
            Console.WriteLine("List of Branches Cities:");
            if (Hospital.Branches.Count == 0)
            {
                Console.WriteLine("No branches available in the system.");
                Additional.HoldScreen();
                return;
            }
            foreach (var branch in Hospital.Branches)
            {
                Console.WriteLine($"Branch City: {branch.BranchCity}");
            }
            //Additional.HoldScreen();
        }
        //to find the branch by city ...
        public static Branch FindBranchByCity(string city)
        {
            //to loop through branch list ...
            foreach (var branch in Hospital.Branches)
            {
                if (branch.BranchCity.ToLower() == city.ToLower())
                {
                    return branch; //if branch found ...
                }
            }
            Console.WriteLine("Branch not found in this city.");
            Additional.HoldScreen();
            return null; //if branch not found ...
        }
        //to add a new patient to the branch list in hospital class ...
        public void AddPatient(string username, string password, string email, int phoneNumber, string userNationalID, string city, Branch branch)
        {
            //to create a new patient object ...
            Patient newPatient = new Patient
            {
                UserName = username,
                P_UserPassword = password,
                UserEmail = email,
                UserPhoneNumber = phoneNumber,
                UserNationalID = userNationalID,
                PatientCity = city,
            };
            //to add the new patient to the branch list in hospital class ...
            if (branch != null)
            {
                
                //add the new patient to the branch patients list ...
                branch.Patients.Add(newPatient); //add the new patient to the branch patients list ...
                //to add patient id to hospital UserNationalID list ...
                Hospital.UserNationalID.Add(userNationalID); //to add the user national id to the hospital user national id list ...
                UserCount++; //to increase the user count ...
                Console.WriteLine("Patient added successfully with following details:");
                newPatient.PrintPatientDetails(); //to print the patient details ...
                Additional.HoldScreen();//just to hold the screen ...
            }
            else
            {
                Console.WriteLine("Failed to add patient. Branch not found.");
                Additional.HoldScreen();//just to hold the screen ...
            }
        }
        //to print the patient details ...
        public void PrintPatientDetails()
        {
            Console.WriteLine($"Patient ID: {UserId}");
            Console.WriteLine($"Name: {UserName}");
            Console.WriteLine($"Email: {UserEmail}");
            Console.WriteLine($"Phone Number: {UserPhoneNumber}");
            Console.WriteLine($"National ID: {UserNationalID}");
            Console.WriteLine($"City: {PatientCity}");
            Console.WriteLine($"Role: {UserRole}");
            Console.WriteLine($"Status: {UserStatus}");
        }
        //to view patient appointments ...
        public static void ViewPatientAppointments()
        {
            //to get the patient national id ...
            string userNationalID = Validation.StringValidation("national ID");
            //to find the patient by national id ...
            Patient patient = FindPatientByNationalId(userNationalID);
            if (patient != null)
            {
                Console.WriteLine($"Appointments for Patient {patient.UserName}:");
                if (patient.PatientAppointments.Count == 0)
                {
                    Console.WriteLine("No appointments found for this patient.");
                }
                else
                {
                    foreach (var appointment in patient.PatientAppointments)
                    {
                        //to get doctor name from the appointment ...
                        var doctor = BranchDepartment.Doctors.Find(d => d.UserId == appointment.DoctorId);
                        Console.WriteLine($"Appointment ID: {appointment.BookingId}, Date: {appointment.BookingDateTime}, Doctor: {doctor.UserName}");
                    }
                }
            }
            else
            {
                Console.WriteLine("Patient not found.");
            }
            Additional.HoldScreen(); //just to hold the screen ...
        }
        //to find the patient by national id ...
        public static Patient FindPatientByNationalId(string nationalId)
        {
            //to loop through all branches and their patients ...
            foreach (var branch in Hospital.Branches)
            {
                foreach (var patient in branch.Patients)
                {
                    if (patient.UserNationalID == nationalId)
                    {
                        return patient; //if patient found ...
                    }
                }
            }
            Console.WriteLine("Patient not found with this National ID.");
            Additional.HoldScreen();
            return null; //if patient not found ...
        }
        //to view patient records ...
        public static void ViewPatientRecords()
        {
            //to get the patient national id ...
            string userNationalID = Validation.StringValidation("national ID");
            //to find the patient by national id ...
            Patient patient = FindPatientByNationalId(userNationalID);
            if (patient != null)
            {
                Console.WriteLine($"Records for Patient {patient.UserName}:");
                if (patient.PatientRecords.Count == 0)
                {
                    Console.WriteLine("No records found for this patient.");
                }
                else
                {
                    //foreach (var record in patient.PatientRecords)
                    //{
                    //    Console.WriteLine($"Record ID: {record.RecordId}, Date: {record.RecordDate}, Description: {record.RecordDescription}");
                    //}
                    //PatientRecord.GetRecordsByPatientId(patient.UserId); //to get the records by patient id ...
                }
            }
            else
            {
                Console.WriteLine("Patient not found.");
            }
            Additional.HoldScreen(); //just to hold the screen ...
        }
        //to save patient data to file ...
        public static void SavePatientsToFile()
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (var branch in Hospital.Branches)
                {
                    foreach (var patient in branch.Patients)
                    {
                        writer.WriteLine($"{patient.UserId}|{patient.UserName}|{patient.P_UserPassword}|{patient.UserEmail}|{patient.UserPhoneNumber}|{patient.UserNationalID}|{patient.PatientCity}|{patient.UserRole}|{patient.UserStatus}|{branch.BranchCity}");
                    }
                }
            }

            Console.WriteLine("All patients saved successfully.");
           // Additional.HoldScreen(); //just to hold the screen ...
        }
        //to load patient data from file ...
        public static void LoadPatientsFromFile()
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine("Patient file not found.");
                return;
            }

            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] parts = line.Split('|');
                    if (parts.Length != 10) continue;

                    string userName = parts[1];
                    string password = parts[2];
                    string email = parts[3];
                    int phone = int.Parse(parts[4]);
                    string nationalId = parts[5];
                    string city = parts[6];
                    string role = parts[7];
                    string status = parts[8];
                    string branchCity = parts[9];

                    Branch branch = Patient.FindBranchByCity(branchCity);
                    if (branch != null)
                    {
                        Patient patient = new Patient
                        {
                            UserId = int.Parse(parts[0]),
                            UserName = userName,
                            P_UserPassword = password,
                            UserEmail = email,
                            UserPhoneNumber = phone,
                            UserNationalID = nationalId,
                            PatientCity = city,
                            UserRole = role,
                            UserStatus = status
                        };

                        branch.Patients.Add(patient);
                    }
                }
            }

            Console.WriteLine("Patients loaded successfully.");
            //Additional.HoldScreen(); //just to hold the screen ...
        }
        //to SaveAppointmentsToFile
        public static void SavePatientAppointmentsToFile()
        {
            string path = "PatientAppointments.txt";
            using (StreamWriter writer = new StreamWriter(path))
            {
                foreach (var branch in Hospital.Branches)
                {
                    foreach (var patient in branch.Patients)
                    {
                        foreach (var appointment in patient.PatientAppointments)
                        {
                            writer.WriteLine($"{patient.UserId}|{appointment.BookingId}|{appointment.BookingDateTime:yyyy-MM-dd HH:mm}|{appointment.DoctorId}");
                        }
                    }
                }
            }

            Console.WriteLine("Appointments saved successfully.");
        }
        //to LoadAppointmentsFromFile
        public static void LoadPatientAppointmentsFromFile()
        {
            string path = "PatientAppointments.txt";
            if (!File.Exists(path))
            {
                Console.WriteLine("Appointments file not found.");
                return;
            }

            using (StreamReader reader = new StreamReader(path))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    var parts = line.Split('|');
                    if (parts.Length != 4) continue;

                    int patientId = int.Parse(parts[0]);
                    int bookingId = int.Parse(parts[1]);
                    DateTime bookingDateTime = DateTime.Parse(parts[2]);
                    int doctorId = int.Parse(parts[3]);

                    var patient = FindPatientById(patientId);
                    if (patient != null)
                    {
                        Booking booking = new Booking
                        {
                            BookingId = bookingId,
                            BookingDateTime = bookingDateTime,
                            DoctorId = doctorId
                        };
                        patient.PatientAppointments.Add(booking);
                    }
                }
            }

            Console.WriteLine("Appointments loaded successfully.");
        }

        //to find patient by ID
        public static Patient FindPatientById(int id)
        {
            foreach (var branch in Hospital.Branches)
            {
                foreach (var patient in branch.Patients)
                {
                    if (patient.UserId == id)
                        return patient;
                }
            }
            return null;
        }
        //to SavePatientRecordsToFile
        //public static void SavePatientRecordsToFile()
        //{
        //    string path = "patient_records.txt";
        //    using (StreamWriter writer = new StreamWriter(path))
        //    {
        //        foreach (var branch in Hospital.Branches)
        //        {
        //            foreach (var patient in branch.Patients)
        //            {
        //                foreach (var record in patient.PatientRecords)
        //                {
        //                    writer.WriteLine($"{patient.UserId}|{record.}|{record.RecordDate:yyyy-MM-dd}|{record.RecordDescription}");
        //                }
        //            }
        //        }
        //    }

        //    Console.WriteLine("Patient records saved successfully.");
        //}
        //to LoadPatientRecordsFromFile
        //public static void LoadPatientRecordsFromFile()
        //{
        //    string path = "patient_records.txt";
        //    if (!File.Exists(path))
        //    {
        //        Console.WriteLine("Patient records file not found.");
        //        return;
        //    }

        //    using (StreamReader reader = new StreamReader(path))
        //    {
        //        string line;
        //        while ((line = reader.ReadLine()) != null)
        //        {
        //            var parts = line.Split('|');
        //            if (parts.Length != 4) continue;

        //            int patientId = int.Parse(parts[0]);
        //            int recordId = int.Parse(parts[1]);
        //            DateTime recordDate = DateTime.Parse(parts[2]);
        //            string description = parts[3];

        //            var patient = FindPatientById(patientId);
        //            if (patient != null)
        //            {
        //                PatientRecord record = new PatientRecord
        //                {
        //                    RecordId = recordId,
        //                    RecordDate = recordDate,
        //                    RecordDescription = description
        //                };
        //                patient.PatientRecords.Add(record);
        //            }
        //        }
        //    }

        //    Console.WriteLine("Patient records loaded successfully.");
        //}




        //====================================================
        //4. class constructor ...
        public Patient()
        {
            //to set the default values for the patient ...
            UserRole = "Patient"; //default role for patient ...
            UserStatus = "Active"; //default status for patient ...
        }
    }
}
