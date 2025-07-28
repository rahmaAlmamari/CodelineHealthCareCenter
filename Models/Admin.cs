using CodelineHealthCareCenter.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodelineHealthCareCenter.Models
{
    class Admin : User
    {
        //1. class fields ...
        public int BranchID;
        public List<Clinic> Clinics = new List<Clinic>();
        //====================================================
        //2. class property ...

        public static int AdminCount { get; private set; }

        //====================================================
        //3. class method ...
        public void AddClinic(Clinic clinic) // adds a clinic to the admin's list of clinics
        {
            Clinics.Add(clinic);
            Console.WriteLine($" Clinic '{clinic.ClinicName}' added to Admin '{UserName}'.");


        }

        public void ViewClinics() // displays all clinics managed by the admin
        {
            Console.WriteLine($"Clinics managed by Admin {UserName}:");
            if (Clinics.Count == 0)
            {
                Console.WriteLine(" No clinics assigned yet.");
            }
            else
            {
                foreach (var clinic in Clinics)
                    clinic.ViewClinicInfo();
            }
        }

        public static void AdminMenu(IAdminService service)
        {
            Additional.WelcomeMessage("Admin Panel"); // to display the welcome message for admin panel

            while (true)
            {
                Console.Clear();
                Console.WriteLine(" ADMIN MANAGEMENT MENU ");
                Console.WriteLine("1. Assign Doctor to Clinic");
                Console.WriteLine("2. Add Service to Clinic");
                Console.WriteLine("3. View Clinic's Doctors");
                Console.WriteLine("4. View Clinic's Services");
                Console.WriteLine("5. Exit");
                Console.Write("Select an option: ");

                string choice = Console.ReadLine();
                Console.WriteLine();

                switch (choice)
                {
                    case "1": // Assign a doctor to a clinic
                        int doctorId = Validation.IntValidation("Doctor ID");
                        int clinicId = Validation.IntValidation("Clinic ID");

                        if (Additional.ConfirmAction("assign the doctor to this clinic"))
                        {
                            service.AssignDoctorToClinic(doctorId, clinicId);
                        }
                        else
                        {
                            Console.WriteLine("Assignment cancelled.");
                        }
                        break;

                    case "2": // Add a service to a clinic
                        int clinicId2 = Validation.IntValidation("Clinic ID");
                        int serviceId = Validation.IntValidation("Service ID");

                        if (Additional.ConfirmAction("add this service to the clinic"))
                        {
                            service.AddClinicService(clinicId2, serviceId);
                        }
                        else
                        {
                            Console.WriteLine(" Adding service cancelled.");
                        }
                        break;

                    case "3": // View all doctors in a clinic
                        int clinicId3 = Validation.IntValidation("Clinic ID to view doctors");
                        service.GetClinicDoctors(clinicId3);
                        break;

                    case "4": // View all services in a clinic
                        int clinicId4 = Validation.IntValidation("Clinic ID to view services");
                        service.GetClinicServices(clinicId4);
                        break;

                    case "5": // Exit the admin menu
                        Console.WriteLine("Exiting Admin Menu...");
                        return;

                    default: // Invalid option
                        Console.WriteLine("Invalid option. Try again.");
                        break;




                }

                Additional.HoldScreen(); // to hold the screen after each operation
            }


        }

        //====================================================
        //4. class constructor ...

        public Admin(string username, string email, int branchId)
            
        {
            BranchID = branchId;
            AdminCount++;
            UserRole = "Doctor";
            UserStatus = "Active";
        }
    }
}
