using System;
using System.Collections.Generic;
using System.Linq;
using CodelineHealthCareCenter.Services;
using System.IO;

namespace CodelineHealthCareCenter.Models
{
    class Clinic : IHasServices
    {
        //====================================================
        //1. class fields ...

        private bool clinicStatus = true;
        private static int clinicCounter = 0;
        //private string location;
        //private decimal price;
        //public static IClinicService service;

        //clinic file path ...
        private static string filePath = "clinics.txt";

        //====================================================
        //2. class properties ...

       // public static List<Clinic> Clinics = new List<Clinic>();
        public int ClinicId { get; set; }
        public string ClinicName { get; set; }
        public int DepartmentId { get; set; }
        public int BranchId { get; set; }
        public int FloorId { get; set; }
        public int RoomId { get; set; }

        public List<Doctor> Doctors { get; set; } = new List<Doctor>();
        public List<DateTime> ClinicSpots { get; set; } = new List<DateTime>();
        public List<Service> Services { get; set; } = new List<Service>();  // <-- IHasServices implementation

        public static int ClinicCount => clinicCounter;
        //public string Location { get => location; set => location = value; }
        //public decimal Price { get => price; set => price = value; }
        public bool ClinicStatus => clinicStatus;

        //====================================================
        //3. class methods ...

        public void SetClinicStatus(bool isActive)
        {
            clinicStatus = isActive;
        }

        public void UpdateClinicDetails(string newName)
        {
            ClinicName = newName;
        }

        public void ViewClinicInfo()
        {
            Console.WriteLine($"\nCLINIC ID: {ClinicId}, NAME: {ClinicName}");
            Console.WriteLine($"Dept: {DepartmentId}, Branch: {BranchId}, Floor: {FloorId}, Room: {RoomId}");
            Console.WriteLine($"Status: {(ClinicStatus ? "Open" : "Closed")}");
            Console.WriteLine($"Doctors: {Doctors.Count}, Appointments: {ClinicSpots.Count}, Services: {Services.Count}");
        }

        public void AddService(Service service)
        {
            if (Services.Any(s => s.ServiceId == service.ServiceId))
            {
                Console.WriteLine("Service already assigned to this clinic.");
                return;
            }

            Services.Add(service);
            Console.WriteLine($"Service '{service.ServiceName}' added to Clinic '{ClinicName}'.");
        }

        public void ViewServices()
        {
            Console.WriteLine($"\nServices assigned to Clinic '{ClinicName}':");
            if (Services.Count == 0)
            {
                Console.WriteLine("No services assigned.");
                return;
            }

            foreach (var service in Services)
                service.ViewServiceInfo();
        }

        public void AddClinicSpot(DateTime spot)
        {
            if (ClinicSpots.Contains(spot))
            {
                Console.WriteLine("This time slot is already available.");
                return;
            }

            ClinicSpots.Add(spot);
            Console.WriteLine($"Time slot {spot:MM/dd/yyyy HH:mm} added to clinic '{ClinicName}'.");
        }


        public static void AddClinic() // adds a new clinic to the list
        {
            //to get clinic name ...
            string clinicName = Validation.ClinicNameValidation();
            //to list all branches ...
            Validation.ListAllBranches();
            //to get branch ID ...
            int branchId = Validation.BranchIdValidation();
            //to list all departments ...
            Validation.ListDepartmentsInBranch(branchId);
            //to get department ID ...
            int departmentId = Validation.DepartmentIdValidation();
            //to add clinic to clinic list ...
            foreach (var department in BranchDepartment.Departments)
            {
                if (department.DepartmentId != departmentId || department.BranchId != branchId) continue;
                // Create and add new clinic
                var newClinic = new Clinic(clinicName, 0, 0) // FloorId and RoomId are set to 0 for now
                {
                    DepartmentId = departmentId,
                    BranchId = branchId
                };
                department.Clinics.Add(newClinic);
                Console.WriteLine($"Clinic '{clinicName}' added successfully to Department ID {department.DepartmentId}.");
                return;
            }


            //foreach (var department in BranchDepartment.Departments)
            //{
            //    if (department.Clinics.Any(c => c.ClinicName.Equals(clinicName, StringComparison.OrdinalIgnoreCase)))
            //    {
            //        Console.WriteLine($"Clinic '{clinicName}' already exists in Department ID {department.DepartmentId}.");
            //        return;
            //    }
            //}
            //var newClinic = new Clinic(clinicName, location, 0, 0, 0, 0, 0);
            //Clinics.Add(newClinic);
            //Console.WriteLine($"Clinic '{clinicName}' added successfully.");
        }

        public static void GetAllClinics() // displays all clinics

        {
            //if (Clinics.Count == 0)
            //{
            //    Console.WriteLine("No clinics available.");
            //    return;
            //}
            //foreach (var clinic in Clinics)
            //    clinic.ViewClinicInfo();
            foreach (var department in BranchDepartment.Departments) 
            {
                foreach (var clinic in department.Clinics)
                {
                    clinic.ViewClinicInfo();
                }
            }
        }

        public static void GetClinicById(int clinicId) // retrieves a clinic by its ID
        {
            foreach (var department in BranchDepartment.Departments)
            {
                var clinic = department.Clinics.FirstOrDefault(c => c.ClinicId == clinicId);
                if (clinic == null)
                {
                    Console.WriteLine("Clinic not found.");
                    return;
                }
                clinic.ViewClinicInfo();
                return;
            }
            //var clinic = Clinics.FirstOrDefault(c => c.ClinicId == clinicId);
            //if (clinic == null)
            //    Console.WriteLine("Clinic not found.");
            //else
            //    clinic.ViewClinicInfo();
        }

        public static void GetClinicByName(string clinicName) // retrieves clinics by their name
        {
            foreach (var department in BranchDepartment.Departments)
            {
                var matches = department.Clinics.Where(c => c.ClinicName.Equals(clinicName, StringComparison.OrdinalIgnoreCase)).ToList();
                if (!matches.Any())
                {
                    Console.WriteLine("No clinic found with that name.");
                    return;
                }
                foreach (var clinic in matches)
                    clinic.ViewClinicInfo();
                return;
            }
            //var matches = Clinics.Where(c => c.ClinicName.Equals(clinicName, StringComparison.OrdinalIgnoreCase)).ToList();
            //if (!matches.Any())
            //{
            //    Console.WriteLine("No clinic found with that name.");
            //    return;
            //}
            //foreach (var clinic in matches)
            //    clinic.ViewClinicInfo();
        }

        public static void GetClinicByBranchDep(int branchId, int departmentId) // retrieves clinics by branch and department IDs
        {
            foreach (var department in BranchDepartment.Departments)
            {
                if (department.DepartmentId != departmentId || department.BranchId != branchId) continue;
                if (department.Clinics.Count == 0)
                {
                    Console.WriteLine($"No clinics found in Branch ID {branchId} and Department ID {departmentId}.");
                    return;
                }
                foreach (var clinic in department.Clinics)
                    clinic.ViewClinicInfo();
                return;
            }
            //var matches = Clinics.Where(c => c.BranchId == branchId && c.DepartmentId == departmentId).ToList();
            //if (!matches.Any())
            //{
            //    Console.WriteLine("No clinic found for that branch and department.");
            //    return;
            //}
            //foreach (var clinic in matches)
            //    clinic.ViewClinicInfo();
        }

        public static void UpdateClinicDetails(int clinicId, string name, string loc, decimal price) // updates clinic details
        {
            foreach(var department in BranchDepartment.Departments)
            {
                var clinic = department.Clinics.FirstOrDefault(c => c.ClinicId == clinicId);
                if (clinic == null)
                {
                    Console.WriteLine("Clinic not found.");
                    return;
                }
                clinic.UpdateClinicDetails(name);
                Console.WriteLine($"Clinic ID {clinicId} updated successfully.");
                return;
            }
            //var clinic = Clinics.FirstOrDefault(c => c.ClinicId == clinicId);
            //if (clinic == null)
            //{
            //    Console.WriteLine("Clinic not found.");
            //    return;
            //}
            //clinic.ClinicName = name;
            //clinic.Location = loc;
            //clinic.Price = price;
            //Console.WriteLine($"Clinic ID {clinicId} updated successfully.");
        }

        public static void SetClinicStatus(int clinicId, bool isActive) // sets the status of a clinic
        {
            foreach (var department in BranchDepartment.Departments)
            {
                var clinic = department.Clinics.FirstOrDefault(c => c.ClinicId == clinicId);
                if (clinic == null)
                {
                    Console.WriteLine("Clinic not found.");
                    return;
                }
                clinic.SetClinicStatus(isActive);
                Console.WriteLine($"Clinic '{clinic.ClinicName}' status set to {(isActive ? "Open" : "Closed")}.");
                return;
            }
            //var clinic = Clinics.FirstOrDefault(c => c.ClinicId == clinicId);
            //if (clinic == null)
            //{
            //    Console.WriteLine("Clinic not found.");
            //    return;
            //}
            //clinic.SetClinicStatus(isActive);
            //Console.WriteLine($"Clinic '{clinic.ClinicName}' status set to {(isActive ? "Open" : "Closed")}.");
        }

        public static void DeleteClinic(int clinicId) // deletes a clinic by its ID
      
        {
            foreach(var department in BranchDepartment.Departments)
            {
                var clinic = department.Clinics.FirstOrDefault(c => c.ClinicId == clinicId);
                if (clinic != null)
                {
                    department.Clinics.Remove(clinic);
                    Console.WriteLine($"Clinic '{clinic.ClinicName}' deleted successfully.");
                    return;
                }
            }
            //var clinic = Clinics.FirstOrDefault(c => c.ClinicId == clinicId);
            //if (clinic == null)
            //{
            //    Console.WriteLine("Clinic not found.");
            //    return;
            //}
            //Clinics.Remove(clinic);
            //Console.WriteLine($"Clinic '{clinic.ClinicName}' deleted successfully.");
        }

        public static void SaveClinicToFile()
        {
            using StreamWriter writer = new StreamWriter(filePath);

            foreach (var department in BranchDepartment.Departments)
            {
                foreach (var clinic in department.Clinics)
                {
                    // Line 1: Basic clinic info
                    writer.WriteLine($"{clinic.ClinicId}|{clinic.ClinicName}|{clinic.DepartmentId}|{clinic.BranchId}|{clinic.FloorId}|{clinic.RoomId}|{clinic.ClinicStatus}");

                    // Line 2: Doctors (IDs only)
                    string doctorLine = "Doctors:" + string.Join(",", clinic.Doctors.Where(d => d != null).Select(d => d.UserId));
                    writer.WriteLine(doctorLine);

                    // Line 3: ClinicSpots (date-time list)
                    string spotsLine = "Spots:" + string.Join(",", clinic.ClinicSpots.Select(dt => dt.ToString("yyyy-MM-dd HH:mm")));
                    writer.WriteLine(spotsLine);

                    // Line 4: Services (IDs only)
                    string serviceLine = "Services:" + string.Join(",", clinic.Services.Where(s => s != null).Select(s => s.ServiceId));
                    writer.WriteLine(serviceLine);
                }
            }

            Console.WriteLine("Clinic data saved successfully.");
        }


        public static void LoadClinicFromFile()
        {
            clinicCounter = 0;

            if (!File.Exists(filePath))
            {
                Console.WriteLine("Clinic data file not found.");
                return;
            }

            string[] lines = File.ReadAllLines(filePath);

            for (int i = 0; i < lines.Length; i += 4)
            {
                string[] parts = lines[i].Split('|');
                if (parts.Length != 7)
                    continue;

                int clinicId = int.Parse(parts[0]);
                string clinicName = parts[1];
                int departmentId = int.Parse(parts[2]);
                int branchId = int.Parse(parts[3]);
                int floorId = int.Parse(parts[4]);
                int roomId = int.Parse(parts[5]);
                bool status = bool.Parse(parts[6]);

                // Create clinic
                Clinic clinic = new Clinic(clinicName, floorId, roomId)
                {
                    ClinicId = clinicId,
                    DepartmentId = departmentId,
                    BranchId = branchId,
                    FloorId = floorId,
                    RoomId = roomId
                };
                clinic.SetClinicStatus(status);

                // Update counter
                if (clinicId > clinicCounter)
                    clinicCounter = clinicId;

                // Line 2: Doctors
                if (i + 1 < lines.Length && lines[i + 1].StartsWith("Doctors:"))
                {
                    var doctorIds = lines[i + 1].Substring(8).Split(',', StringSplitOptions.RemoveEmptyEntries);
                    foreach (var idStr in doctorIds)
                    {
                        if (int.TryParse(idStr, out int doctorId))
                        {
                            var doctor = BranchDepartment.Doctors.FirstOrDefault(d => d.UserId == doctorId);
                            //if (doctor != null)
                                clinic.Doctors.Add(doctor);
                        }
                    }
                }

                // Line 3: Spots
                if (i + 2 < lines.Length && lines[i + 2].StartsWith("Spots:"))
                {
                    var spotStrs = lines[i + 2].Substring(6).Split(',', StringSplitOptions.RemoveEmptyEntries);
                    foreach (var spotStr in spotStrs)
                    {
                        if (DateTime.TryParse(spotStr, out DateTime spot))
                            clinic.ClinicSpots.Add(spot);
                    }
                }

                // Line 4: Services
                if (i + 3 < lines.Length && lines[i + 3].StartsWith("Services:"))
                {
                    var serviceIds = lines[i + 3].Substring(9).Split(',', StringSplitOptions.RemoveEmptyEntries);
                    foreach (var idStr in serviceIds)
                    {
                        if (int.TryParse(idStr, out int serviceId))
                        {
                            var service = Service.Services.FirstOrDefault(s => s.ServiceId == serviceId);
                            if (service != null)
                                clinic.Services.Add(service);
                        }
                    }
                }

                // Add to department
                var department = BranchDepartment.Departments
                    .FirstOrDefault(d => d.DepartmentId == departmentId && d.BranchId == branchId);

                if (department != null)
                {
                    department.Clinics.Add(clinic);
                }
            }

            Console.WriteLine("Clinic data loaded successfully.");
        }



        public static void ClinicMenu()
        {
            Additional.WelcomeMessage("Clinic Management");

            while (true)
            {
                Console.Clear();
                Console.WriteLine(" CLINIC MANAGEMENT MENU ");
                Console.WriteLine("1. Add Clinic");
                Console.WriteLine("2. View All Clinics");
                Console.WriteLine("3. Search Clinic by ID");
                Console.WriteLine("4. Search Clinic by Name");
                Console.WriteLine("5. Search Clinic by Branch + Department");
                Console.WriteLine("6. Update Clinic Details");
                Console.WriteLine("7. Toggle Clinic Status");
                Console.WriteLine("8. Delete Clinic");
                Console.WriteLine("9. Exit");
                Console.Write("Select an option: ");

                string choice = Console.ReadLine();
                Console.WriteLine();

                switch (choice)
                {
                    case "1":
                        //string name = Validation.StringNamingValidation("Clinic Name");
                        //string location = Validation.StringValidation("Clinic Location");
                        Clinic.AddClinic();
                        break;

                    case "2":
                        Clinic.GetAllClinics();
                        break;

                    case "3":
                        int id = Validation.IntValidation("Clinic ID");
                        Clinic.GetClinicById(id);
                        break;

                    case "4":
                        string searchName = Validation.StringNamingValidation("Clinic Name");
                        Clinic.GetClinicByName(searchName);
                        break;

                    case "5":
                        int branchId = Validation.IntValidation("Branch ID");
                        int deptId = Validation.IntValidation("Department ID");
                        Clinic.GetClinicByBranchDep(branchId, deptId);
                        break;

                    case "6":
                        int updateId = Validation.IntValidation("Clinic ID to update");

                        if (Additional.ConfirmAction("update this clinic"))
                        {
                            string newName = Validation.StringNamingValidation("New Clinic Name");
                            string newLocation = Validation.StringValidation("New Location");
                            decimal price = (decimal)Validation.DoubleValidation("New Price");
                            Clinic.UpdateClinicDetails(updateId, newName, newLocation, price);
                        }
                        else Console.WriteLine("Update cancelled.");
                        break;

                    case "7":
                        int toggleId = Validation.IntValidation("Clinic ID");

                        if (Additional.ConfirmAction("change this clinic's status"))
                        {
                            Console.WriteLine("Enter Status (true=open, false=closed): ");
                            bool isActive;
                            while (!bool.TryParse(Console.ReadLine(), out isActive))
                            {
                                Console.WriteLine("Invalid input. Please enter 'true' or 'false': ");
                            }
                            Clinic.SetClinicStatus(toggleId, isActive);
                        }
                        else Console.WriteLine("Status change cancelled.");
                        break;

                    case "8":
                        int deleteId = Validation.IntValidation("Clinic ID to delete");

                        if (Additional.ConfirmAction("delete this clinic"))
                        {
                            Clinic.DeleteClinic(deleteId);
                        }
                        else Console.WriteLine("Deletion cancelled.");
                        break;

                    case "9":
                        Console.WriteLine("Exiting Clinic Menu...");
                        return;

                    default:
                        Console.WriteLine("Invalid option. Press Enter to try again.");
                        break;
                }

                Additional.HoldScreen();
            }
        }

        //====================================================
        //4. class constructor ...

        public Clinic(string clinicName, int floorId, int roomId)
        {
            clinicCounter++;
            ClinicId = clinicCounter;
            ClinicName = clinicName;
            //Location = location;
            //DepartmentId = departmentId;
            //BranchId = branchId;
            FloorId = floorId;
            RoomId = roomId;
            //Price = price;

            //Doctors = new List<Doctor>();
            //ClinicSpots = new List<DateTime>();
            //Services = new List<Service>();
        }
    }
}
