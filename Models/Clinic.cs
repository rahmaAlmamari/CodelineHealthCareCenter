using System;
using System.Collections.Generic;
using System.Linq;
using CodelineHealthCareCenter.Services;

namespace CodelineHealthCareCenter.Models
{
    class Clinic : IHasServices
    {
        //====================================================
        //1. class fields ...

        private bool clinicStatus = true;
        private static int clinicCounter = 0;
        private string location;
        private decimal price;
        public static IClinicService service;

        //====================================================
        //2. class properties ...

        public static List<Clinic> Clinics = new List<Clinic>();
        public int ClinicId { get; private set; }
        public string ClinicName { get; set; }
        public int DepartmentId { get; set; }
        public int BranchId { get; set; }
        public int FloorId { get; set; }
        public int RoomId { get; set; }

        public List<Doctor> Doctors { get; set; } = new List<Doctor>();
        public List<DateTime> ClinicSpots { get; set; } = new List<DateTime>();
        public List<Service> Services { get; set; } = new List<Service>();  // <-- IHasServices implementation

        public static int ClinicCount => clinicCounter;
        public string Location { get => location; set => location = value; }
        public decimal Price { get => price; set => price = value; }
        public bool ClinicStatus => clinicStatus;

        //====================================================
        //3. class methods ...

        public void SetClinicStatus(bool isActive)
        {
            clinicStatus = isActive;
        }

        public void UpdateClinicDetails(string newName, string newLocation, decimal newPrice)
        {
            ClinicName = newName;
            Location = newLocation;
            Price = newPrice;
        }

        public void ViewClinicInfo()
        {
            Console.WriteLine($"\nCLINIC ID: {ClinicId}, NAME: {ClinicName}");
            Console.WriteLine($"Dept: {DepartmentId}, Branch: {BranchId}, Floor: {FloorId}, Room: {RoomId}");
            Console.WriteLine($"Location: {Location}, Price: ${Price}, Status: {(ClinicStatus ? "Open" : "Closed")}");
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

        public static void AddClinic(string clinicName, string location) // adds a new clinic to the list
        {
            var newClinic = new Clinic(clinicName, location, 0, 0, 0, 0, 0);
            Clinics.Add(newClinic);
            Console.WriteLine($"Clinic '{clinicName}' added successfully.");
        }

        public static void GetAllClinics() // displays all clinics

        {
            if (Clinics.Count == 0)
            {
                Console.WriteLine("No clinics available.");
                return;
            }
            foreach (var clinic in Clinics)
                clinic.ViewClinicInfo();
        }

        public static void GetClinicById(int clinicId) // retrieves a clinic by its ID
        {
            var clinic = Clinics.FirstOrDefault(c => c.ClinicId == clinicId);
            if (clinic == null)
                Console.WriteLine("Clinic not found.");
            else
                clinic.ViewClinicInfo();
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
                        string name = Validation.StringNamingValidation("Clinic Name");
                        string location = Validation.StringValidation("Clinic Location");
                        service.AddClinic(name, location);
                        break;

                    case "2":
                        service.GetAllClinics();
                        break;

                    case "3":
                        int id = Validation.IntValidation("Clinic ID");
                        service.GetClinicById(id);
                        break;

                    case "4":
                        string searchName = Validation.StringNamingValidation("Clinic Name");
                        service.GetClinicByName(searchName);
                        break;

                    case "5":
                        int branchId = Validation.IntValidation("Branch ID");
                        int deptId = Validation.IntValidation("Department ID");
                        service.GetClinicByBranchDep(branchId, deptId);
                        break;

                    case "6":
                        int updateId = Validation.IntValidation("Clinic ID to update");

                        if (Additional.ConfirmAction("update this clinic"))
                        {
                            string newName = Validation.StringNamingValidation("New Clinic Name");
                            string newLocation = Validation.StringValidation("New Location");
                            decimal price = (decimal)Validation.DoubleValidation("New Price");
                            service.UpdateClinicDetails(updateId, newName, newLocation, price);
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
                            service.SetClinicStatus(toggleId, isActive);
                        }
                        else Console.WriteLine("Status change cancelled.");
                        break;

                    case "8":
                        int deleteId = Validation.IntValidation("Clinic ID to delete");

                        if (Additional.ConfirmAction("delete this clinic"))
                        {
                            service.DeleteClinic(deleteId);
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

        public Clinic(string clinicName, string location, int departmentId, int branchId, int floorId, int roomId, decimal price)
        {
            clinicCounter++;
            ClinicId = clinicCounter;
            ClinicName = clinicName;
            Location = location;
            DepartmentId = departmentId;
            BranchId = branchId;
            FloorId = floorId;
            RoomId = roomId;
            Price = price;

            Doctors = new List<Doctor>();
            ClinicSpots = new List<DateTime>();
            Services = new List<Service>();
        }
    }
}
