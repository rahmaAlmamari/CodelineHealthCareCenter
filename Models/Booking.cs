using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodelineHealthCareCenter.Models
{
    class Booking
    {
        //1. class feilds ...

        public int BookingId;
        public DateTime BookingDateTime;
        public int ClinicId;
        public static int BookingCount = 0;


        //====================================================
        //2. class properity ...

        //====================================================
        //3. class method ...
        //to create a new book appointment ...
        public static void BookAppointment()
        {
            //to list all departments ...
            Department.GetAllDepartments();
            int departmentId = Validation.IntValidation("Department ID");
            //to list all clinics in the selected department ...
            GetAllClinicsByDepartmentId(departmentId);
            int clinicId = Validation.IntValidation("Clinic ID");
            //to get the clinic by id ...
        }
        //to GetAllClinicsByDepartmentId ...
        public static void GetAllClinicsByDepartmentId(int departmentId)
        {
            Console.WriteLine("List of Clini in the Selected Department:");
            if (Clinic.ClinicCount == 0)
            {
                Console.WriteLine("No clinic available.");
                return;
            }

            foreach (var department in BranchDepartment.Departments)
            {
                if(department.DepartmentId == departmentId)
                {
                    foreach (var clinic in department.Clinics)
                    {
                        Console.WriteLine($"ID: {clinic.ClinicId}, Name: {clinic.ClinicName}, Department ID: {clinic.DepartmentId}");
                    }
                }
            }

        }
        //to get all spot by clinic id ...
        public static void GetAllSpotsByClinicId(int clinicId)
        {
            Console.WriteLine("List of Spots in the Selected Clinic:");
            if (Clinic.ClinicSpots.Count == 0)
            {
                Console.WriteLine("No spots available.");
                return;
            }


        }
        //====================================================
        //4. class constructor ...
        public Booking()
        {
            BookingCount++;
            BookingId = BookingCount;
        }
    }
}
