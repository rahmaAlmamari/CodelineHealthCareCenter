using CodelineHealthCareCenter.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;


namespace CodelineHealthCareCenter.Models
{
    class PatientRecord
    {
        //====================================================
        //1. class fields ...
        public int PatientRecordId;
        public int PatientId;
        public int ClinicId;
        public DateTime DateCreated;

        public List<Service> Services = new List<Service>();
        public double TotalCost;
        public string DoctorNote;

        public static int PatientRecordCount = 0;
        //public static IPatientRecordService service;
        //public static List<PatientRecord> Records = new List<PatientRecord>();

        //====================================================
        //2. class property ...
        public static int RecordCount => PatientRecordCount;

        //====================================================
        //3. class methods ...

        public void ViewRecordDetails()
        {
            Console.WriteLine($"\nRecord ID: {PatientRecordId}");
            Console.WriteLine($"Patient ID: {PatientId}, Clinic ID: {ClinicId}");
            Console.WriteLine($"Date Created: {DateCreated.ToShortDateString()}");
            Console.WriteLine($"Total Cost: ${TotalCost}");
            Console.WriteLine($"Doctor Note: {DoctorNote}");
            Console.WriteLine($"Services ({Services.Count}):");
            foreach (var s in Services)
                Console.WriteLine($" - {s.ServiceName} (${s.Price})");
        }

        //public static void AddPatientRecord(int patientId, string recordDetails)
        //{
        //    int clinicId = Validation.IntValidation("Clinic ID");

        //    Console.WriteLine("Enter number of services to add:");
        //    int count = Validation.IntValidation("Service Count");

        //    List<Service> selectedServices = new List<Service>();
        //    for (int i = 0; i < count; i++)
        //    {
        //        int serviceId = Validation.IntValidation($"Service ID #{i + 1}");
        //        var service = Service.Services.FirstOrDefault(s => s.ServiceId == serviceId);
        //        if (service == null)
        //        {
        //            Console.WriteLine("Invalid service ID. Skipping.");
        //            continue;
        //        }
        //        selectedServices.Add(service);
        //    }

        //    var record = new PatientRecord(patientId, clinicId, recordDetails, selectedServices);
        //    Records.Add(record);
        //    Console.WriteLine($"Patient record added with ID: {record.PatientRecordId}");
        //}

        //public static void UpdatePatientRecord(int recordId, string newDetails)
        //{
        //    var record = Records.FirstOrDefault(r => r.PatientRecordId == recordId);
        //    if (record == null)
        //    {
        //        Console.WriteLine("Record not found.");
        //        return;
        //    }

        //    record.DoctorNote = newDetails;
        //    Console.WriteLine("Doctor note updated successfully.");
        //}

        //public static void DeletePatientRecord(int recordId)
        //{
        //    var record = Records.FirstOrDefault(r => r.PatientRecordId == recordId);
        //    if (record == null)
        //    {
        //        Console.WriteLine("Record not found.");
        //        return;
        //    }

        //    Records.Remove(record);
        //    Console.WriteLine("Patient record deleted successfully.");
        //}

        //public static void GetPatientRecordById(int recordId)
        //{
        //    var record = Records.FirstOrDefault(r => r.PatientRecordId == recordId);
        //    if (record == null)
        //    {
        //        Console.WriteLine("Record not found.");
        //        return;
        //    }
        //    record.ViewRecordDetails();
        //}

        //public static void GetAllPatientRecords()
        //{
        //    if (Records.Count == 0)
        //    {
        //        Console.WriteLine("No patient records found.");
        //        return;
        //    }
        //    foreach (var record in Records)
        //        record.ViewRecordDetails();
        //}

        //public static void GetRecordsByPatientId(int patientId)
        //{
        //    var results = Records.Where(r => r.PatientId == patientId).ToList();
        //    if (results.Count == 0)
        //    {
        //        Console.WriteLine("No records found for this patient.");
        //        return;
        //    }

        //    foreach (var r in results)
        //        r.ViewRecordDetails();
        //}

        //public static void GetRecordsByClinicIdAndDate(int clinicId, DateTime date)
        //{
        //    var results = Records
        //        .Where(r => r.ClinicId == clinicId && r.DateCreated.Date == date.Date)
        //        .ToList();

        //    if (results.Count == 0)
        //    {
        //        Console.WriteLine("No records found for this clinic on that date.");
        //        return;
        //    }

        //    foreach (var r in results)
        //        r.ViewRecordDetails();
        //}

        //public static void SaveToFile(string filePath)
        //{
        //    using StreamWriter writer = new StreamWriter(filePath);
        //    foreach (var record in Records)
        //    {
        //        string serviceIds = string.Join(",", record.Services.Select(s => s.ServiceId));
        //        writer.WriteLine($"{record.PatientRecordId}|{record.PatientId}|{record.ClinicId}|{record.DateCreated:O}|{record.TotalCost}|{record.DoctorNote}|{serviceIds}");
        //    }
        //}

        //public static void LoadFromFile(string filePath)
        //{
        //    //Records.Clear();
        //    PatientRecordCount = 0;

        //    if (!File.Exists(filePath)) return;

        //    string[] lines = File.ReadAllLines(filePath);
        //    foreach (var line in lines)
        //    {
        //        string[] parts = line.Split('|');
        //        if (parts.Length < 7) continue;

        //        int recordId = int.Parse(parts[0]);
        //        int patientId = int.Parse(parts[1]);
        //        int clinicId = int.Parse(parts[2]);
        //        DateTime dateCreated = DateTime.Parse(parts[3]);
        //        double totalCost = double.Parse(parts[4]);
        //        string doctorNote = parts[5];
        //        string[] serviceIds = parts[6].Split(',');

        //        List<Service> services = new List<Service>();
        //        foreach (var sid in serviceIds)
        //        {
        //            if (int.TryParse(sid, out int parsedId))
        //            {
        //                var svc = Service.Services.FirstOrDefault(s => s.ServiceId == parsedId);
        //                if (svc != null) services.Add(svc);
        //            }
        //        }

        //        var record = new PatientRecord(patientId, clinicId, doctorNote, services)
        //        {
        //            PatientRecordId = recordId,
        //            DateCreated = dateCreated,
        //            TotalCost = totalCost
        //        };

        //        //Records.Add(record);
        //        if (record.PatientRecordId > PatientRecordCount)
        //            PatientRecordCount = record.PatientRecordId;
        //    }
        //}

        //public static void PatientRecordMenu()
        //{
        //    Additional.WelcomeMessage("Patient Record Management");

        //    while (true)
        //    {
        //        Console.Clear();
        //        Console.WriteLine(" PATIENT RECORD MENU ");
        //        Console.WriteLine("1. Add Patient Record");
        //        Console.WriteLine("2. Update Patient Record");
        //        Console.WriteLine("3. Delete Patient Record");
        //        Console.WriteLine("4. Get Patient Record by ID");
        //        Console.WriteLine("5. Get All Patient Records");
        //        Console.WriteLine("6. Get Records by Patient ID");
        //        Console.WriteLine("7. Get Records by Clinic ID and Date");
        //        Console.WriteLine("8. Exit");
        //        Console.Write("Select an option: ");

        //        string choice = Console.ReadLine();
        //        Console.WriteLine();

        //        switch (choice)
        //        {
        //            case "1":
        //                int patientId = Validation.IntValidation("Patient ID");
        //                string details = Validation.StringValidation("Doctor Notes / Record Details");
        //                service.AddPatientRecord(patientId, details);
        //                break;

        //            case "2":
        //                int updateId = Validation.IntValidation("Record ID to update");
        //                string newNote = Validation.StringValidation("New Doctor Note");
        //                service.UpdatePatientRecord(updateId, newNote);
        //                break;

        //            case "3":
        //                int deleteId = Validation.IntValidation("Record ID to delete");
        //                if (Additional.ConfirmAction("delete this patient record"))
        //                    service.DeletePatientRecord(deleteId);
        //                else
        //                    Console.WriteLine("Deletion cancelled.");
        //                break;

        //            case "4":
        //                int id = Validation.IntValidation("Record ID");
        //                service.GetPatientRecordById(id);
        //                break;

        //            case "5":
        //                service.GetAllPatientRecords();
        //                break;

        //            case "6":
        //                int searchPatientId = Validation.IntValidation("Patient ID");
        //                service.GetRecordsByPatientId(searchPatientId);
        //                break;

        //            case "7":
        //                int searchClinicId = Validation.IntValidation("Clinic ID");
        //                DateTime date = Validation.DateTimeValidation("Date (MM/dd/yyyy)");
        //                service.GetRecordsByClinicIdAndDate(searchClinicId, date);
        //                break;

        //            case "8":
        //                Console.WriteLine("Exiting Patient Record Menu...");
        //                return;

        //            default:
        //                Console.WriteLine("Invalid option. Try again.");
        //                break;
        //        }

        //        Additional.HoldScreen();
        //    }
        //}

        //====================================================
        //4. class constructor ...
        public PatientRecord(int patientId, int clinicId, string doctorNote, double totolCost)
        {
            PatientRecordCount++;
            PatientRecordId = PatientRecordCount;
            PatientId = patientId;
            ClinicId = clinicId;
            DoctorNote = doctorNote;
            //Services = services;
            TotalCost = totolCost;
            DateCreated = DateTime.Now;
        }
    }
}
