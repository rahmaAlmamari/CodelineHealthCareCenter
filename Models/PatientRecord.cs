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
        public static List<PatientRecord> Records = new List<PatientRecord>();


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

        public static void AddPatientRecord(int patientId, string recordDetails) // adds a new patient record with the given details and services
        {
            Console.WriteLine("Enter number of services to add:");
            int count = Validation.IntValidation("Service Count");

            List<Service> selectedServices = new List<Service>();
            for (int i = 0; i < count; i++)
            {
                int serviceId = Validation.IntValidation($"Service ID #{i + 1}");
                var service = Service.Services.FirstOrDefault(s => s.ServiceId == serviceId);
                if (service == null)
                {
                    Console.WriteLine("Invalid service ID. Skipping.");
                    continue;
                }
                selectedServices.Add(service);
            }

            var record = new PatientRecord(recordDetails, selectedServices);
            Records.Add(record);
            Console.WriteLine($"Patient record added with ID: {record.PatientRecordId}");
        }

        public static void UpdatePatientRecord(int recordId, string newDetails) // updates the doctor note of an existing patient record by its ID
        {
            var record = Records.FirstOrDefault(r => r.PatientRecordId == recordId);
            if (record == null)
            {
                Console.WriteLine("Record not found.");
                return;
            }

            record.DoctorNote = newDetails;
            Console.WriteLine("Doctor note updated successfully.");
        }

        public static void DeletePatientRecord(int recordId) // deletes a patient record by its ID
        {
            var record = Records.FirstOrDefault(r => r.PatientRecordId == recordId);
            if (record == null)
            {
                Console.WriteLine("Record not found.");
                return;
            }

            Records.Remove(record);
            Console.WriteLine("Patient record deleted successfully.");
        }

        public static void GetPatientRecordById(int recordId) // retrieves a patient record by its ID and displays its details
        {
            var record = Records.FirstOrDefault(r => r.PatientRecordId == recordId);
            if (record == null)
            {
                Console.WriteLine("Record not found.");
                return;
            }
            record.ViewRecordDetails();
        }






        public static void PatientRecordMenu()
        {
            Additional.WelcomeMessage("Patient Record Management");

            while (true)
            {
                Console.Clear();
                Console.WriteLine(" PATIENT RECORD MENU ");
                Console.WriteLine("1. Add Patient Record");
                Console.WriteLine("2. Update Patient Record");
                Console.WriteLine("3. Delete Patient Record");
                Console.WriteLine("4. Get Patient Record by ID");
                Console.WriteLine("5. Get All Patient Records");
                Console.WriteLine("6. Get Records by Patient ID");
                Console.WriteLine("7. Get Records by Clinic ID and Date");
                Console.WriteLine("8. Exit");
                Console.Write("Select an option: ");

                string choice = Console.ReadLine();
                Console.WriteLine();

                switch (choice)
                {
                    case "1": // Add Patient Record
                        int patientId = Validation.IntValidation("Patient ID");
                        string details = Validation.StringValidation("Doctor Notes / Record Details");
                        service.AddPatientRecord(patientId, details);
                        break;

                    case "2": // Update Patient Record
                        int updateId = Validation.IntValidation("Record ID to update");
                        string newNote = Validation.StringValidation("New Doctor Note");
                        service.UpdatePatientRecord(updateId, newNote);
                        break;

                    case "3": // Delete Patient Record 
                        int deleteId = Validation.IntValidation("Record ID to delete");
                        if (Additional.ConfirmAction("delete this patient record"))
                        {
                            service.DeletePatientRecord(deleteId);
                        }
                        else Console.WriteLine("Deletion cancelled.");
                        break;

                    case "4": // Get Patient Record by ID
                        int id = Validation.IntValidation("Record ID");
                        service.GetPatientRecordById(id);
                        break;

                    case "5": // Get All Patient Records
                        service.GetAllPatientRecords();
                        break;

                    case "6": // Get Records by Patient ID
                        int patientSearchId = Validation.IntValidation("Patient ID");
                        service.GetRecordsByPatientId(patientSearchId);
                        break;

                    case "7": // Get Records by Clinic ID and Date
                        int clinicId = Validation.IntValidation("Clinic ID");
                        DateTime date = Validation.DateTimeValidation("Date (MM/dd/yyyy)");
                        service.GetRecordsByClinicIdAndDate(clinicId, date);
                        break;

                    case "8": // Exit
                        Console.WriteLine("Exiting Patient Record Menu...");
                        return;

                    default: // Invalid option
                        Console.WriteLine("Invalid option. Try again.");
                        break;
                }

                Additional.HoldScreen(); // waits for user input before clearing the screen
            }
        }


        //====================================================
        //4. class constructor ...
       public PatientRecord(string doctorNote, List<Service> services)
        {
            PatientRecordCount++;
            PatientRecordId = PatientRecordCount;
            DoctorNote = doctorNote;
            Services = services;
            TotalCost = 0;
            foreach (var s in services)
            {
                TotalCost += s.Price;
            }
        }
    }
}
