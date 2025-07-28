using System;
using System.Collections.Generic;
using CodelineHealthCareCenter.Services;
using CodelineHealthCareCenter.Utilities; // for Validation and Additional classes

namespace CodelineHealthCareCenter.Models
{
    class Clinic
    {
        //1. class fields ...

        private bool clinicStatus = true; // true = open, false = closed
        private static int clinicCounter = 0;
        private string location;
        private decimal price;

        //====================================================
        //2. class property ...

        public int ClinicId { get; private set; } 
        public string ClinicName { get; set; }
        public int DepartmentId { get; set; }
        public int FloorId { get; set; }
        public int RoomId { get; set; }

        // list of doctors and clinic spots
        public static List<Doctor> Doctors { get; set; }
        public static List<DateTime> ClinicSpots { get; set; }

        // Static property to track total clinics
        public static int ClinicCount => clinicCounter;
        public string Location { get => location; set => location = value; } 
        public decimal Price { get => price; set => price = value; }
        public bool ClinicStatus => clinicStatus;



        //====================================================
        //3. class method ...
        public void SetClinicStatus(bool isActive) // method to set clinic status
        {
            clinicStatus = isActive;
        }

        public void UpdateClinicDetails(string newName, string newLocation, decimal newPrice) // method to update clinic details
        {
            ClinicName = newName;
            Location = newLocation;
            Price = newPrice;
        }

        public void ViewClinicInfo() // method to view clinic information
        {
            Console.WriteLine($"   ID: {ClinicId}, Name: {ClinicName}, DeptID: {DepartmentId}, BranchID: {BranchId}");
            Console.WriteLine($"   Floor: {FloorId}, Room: {RoomId}, Location: {Location}, Price: ${Price}");
            Console.WriteLine($"   Status: {(ClinicStatus ? "Open" : "Closed")}, Doctors: {Doctors.Count}, TimeSlots: {ClinicSpots.Count}");


        }

        public static void ClinicMenu(IClinicService service) //method to display clinic menu
        {
            Additional.WelcomeMessage("Clinic Management");


        }





        //====================================================
        //4. class constructor ...
        public Clinic()
        {
            ClinicCount++;
            ClinicId = ClinicCount;
            ClinicName = clinicName;
            DepartmentId = departmentId;
            FloorId = floorId;
            RoomId = roomId;

            // Initialize lists
            Doctors = new List<Doctor>();
            ClinicSpots = new List<DateTime>();

        }
    }
}
