using System;
using System.Collections.Generic;
using CodelineHealthCareCenter.Services;
//using CodelineHealthCareCenter.Utilities;

namespace CodelineHealthCareCenter.Models
{
    class Clinic
    {
        //====================================================
        //1. class fields ...

        private bool clinicStatus = true; // true = open, false = closed
        private static int clinicCounter = 0;

        private string location;
        private decimal price;

        //====================================================
        //2. class properties ...

        public int ClinicId { get; private set; }
        public string ClinicName { get; set; }
        public int DepartmentId { get; set; }
        public int BranchId { get; set; }
        public int FloorId { get; set; }
        public int RoomId { get; set; }

        // List of doctors associated with this clinic
        public List<Doctor> Doctors { get; set; }
        public List<DateTime> ClinicSpots { get; set; }

        // Static property to get the total number of clinics created
        public static int ClinicCount => clinicCounter;
        public string Location { get => location; set => location = value; }
        public decimal Price { get => price; set => price = value; }
        public bool ClinicStatus => clinicStatus;

        //====================================================
        //3. class methods ...

        public void SetClinicStatus(bool isActive) // sets the clinic status to open or closed
        {
            clinicStatus = isActive;
        }

        public void UpdateClinicDetails(string newName, string newLocation, decimal newPrice) // updates the clinic details
        {
            ClinicName = newName;
            Location = newLocation;
            Price = newPrice;
        }

        public void ViewClinicInfo() // displays the clinic information
        {
            Console.WriteLine($"   ID: {ClinicId}, Name: {ClinicName}, DeptID: {DepartmentId}, BranchID: {BranchId}");
            Console.WriteLine($"   Floor: {FloorId}, Room: {RoomId}, Location: {Location}, Price: ${Price}");
            Console.WriteLine($"   Status: {(ClinicStatus ? "Open" : "Closed")}, Doctors: {Doctors.Count}, TimeSlots: {ClinicSpots.Count}");
        }

        public static void ClinicMenu(IClinicService service) // displays the clinic management menu and handles user input
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
                    case "1": // adds a new clinic
                        string name = Validation.StringNamingValidation("Clinic Name");
                        string location = Validation.StringValidation("Clinic Location");
                        service.AddClinic(name, location);
                        break;

                    case "2": // displays all clinics
                        service.GetAllClinics();
                        break;

                    case "3": // searches for a clinic by ID
                        int id = Validation.IntValidation("Clinic ID");
                        service.GetClinicById(id);
                        break;

                    case "4": // searches for a clinic by name
                        string searchName = Validation.StringNamingValidation("Clinic Name");
                        service.GetClinicByName(searchName);
                        break;

                    case "5": // searches for clinics by branch and department
                        int branchId = Validation.IntValidation("Branch ID");
                        int deptId = Validation.IntValidation("Department ID");
                        service.GetClinicByBranchDep(branchId, deptId);
                        break;

                    case "6": // updates clinic details
                        int updateId = Validation.IntValidation("Clinic ID to update");

                        if (Additional.ConfirmAction("update this clinic"))
                        {
                            string newName = Validation.StringNamingValidation("New Clinic Name");
                            string newLocation = Validation.StringValidation("New Location");
                            decimal price = (decimal)Validation.DoubleValidation("New Price");
                            service.UpdateClinicDetails(updateId, newName, newLocation, price);
                        }
                        else
                        {
                            Console.WriteLine("Update cancelled.");
                        }
                        break;

                    case "7": // toggles the clinic status (open/closed)
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
                        else
                        {
                            Console.WriteLine("Status change cancelled.");
                        }
                        break;

                    case "8": // deletes a clinic
                        int deleteId = Validation.IntValidation("Clinic ID to delete");

                        if (Additional.ConfirmAction("delete this clinic"))
                        {
                            service.DeleteClinic(deleteId);
                        }
                        else
                        {
                            Console.WriteLine("Deletion cancelled.");
                        }
                        break;

                    case "9": // exits the clinic management menu
                        Console.WriteLine("Exiting Clinic Menu...");
                        return;

                    default: // invalid option
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
        }
    }
}
