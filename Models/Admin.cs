using CodelineHealthCareCenter.Services;
using System;
using System.Collections.Generic;

namespace CodelineHealthCareCenter.Models
{
    class Admin : User
    {
        //====================================================
        //1. class fields ...
        public int BranchID;
        public List<Clinic> Clinics = new List<Clinic>();

        // Shared static lists
        public static List<Doctor> Doctors = new List<Doctor>();
        public static List<Service> Services = new List<Service>();

        public static IAdminService service; // new static service field

        //====================================================
        //2. class properties ...
        public static int AdminCount { get; private set; }

        //====================================================
        //3. class methods ...

        public void AddClinic(Clinic clinic)
        {
            Clinics.Add(clinic);
            Console.WriteLine($" Clinic '{clinic.ClinicName}' added to Admin '{UserName}'.");
        }

        public void ViewClinics()
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

        public void AssignDoctorToClinic(int doctorId, int clinicId) // assigns a doctor to a clinic by their IDs
        {
            var clinic = Clinics.FirstOrDefault(c => c.ClinicId == clinicId);
            var doctor = Doctors.FirstOrDefault(d => d.DoctorID == doctorId);

            if (clinic == null)
            {
                Console.WriteLine("Clinic not found.");
                return;
            }

            if (doctor == null)
            {
                Console.WriteLine("Doctor not found.");
                return;
            }

            if (clinic.Doctors.Any(d => d.DoctorID == doctorId))
            {
                Console.WriteLine("Doctor already assigned.");
                return;
            }

            clinic.Doctors.Add(doctor);
            Console.WriteLine($"Doctor {doctor.UserName} assigned to Clinic '{clinic.ClinicName}'.");
        }

        public void AddClinicService(int clinicId, int serviceId) // adds a service to a clinic by their IDs
        {
            var clinic = Clinics.FirstOrDefault(c => c.ClinicId == clinicId);
            var service = Services.FirstOrDefault(s => s.ServiceId == serviceId);

            if (clinic == null || service == null)
            {
                Console.WriteLine("Clinic or Service not found.");
                return;
            }

            if (clinic is IHasServices clinicWithServices)
            {
                if (clinicWithServices.Services.Any(s => s.ServiceId == serviceId))
                {
                    Console.WriteLine("Service already added.");
                    return;
                }

                clinicWithServices.Services.Add(service);
                Console.WriteLine($"Service '{service.ServiceName}' added to Clinic '{clinic.ClinicName}'.");
            }
            else
            {
                Console.WriteLine("Clinic must implement IHasServices.");
            }
        }

        public void GetClinicDoctors(int clinicId) // retrieves and displays all doctors assigned to a specific clinic by its ID
        {
            var clinic = Clinics.FirstOrDefault(c => c.ClinicId == clinicId);

            if (clinic == null)
            {
                Console.WriteLine("Clinic not found.");
                return;
            }

            Console.WriteLine($"Doctors in Clinic '{clinic.ClinicName}':");
            if (clinic.Doctors.Count == 0)
                Console.WriteLine("No doctors assigned.");
            else
                clinic.Doctors.ForEach(d => d.ViewDoctorInfo());
        }

        public void GetClinicServices(int clinicId) // retrieves and displays all services assigned to a specific clinic by its ID
        {
            var clinic = Clinics.FirstOrDefault(c => c.ClinicId == clinicId);

            if (clinic == null)
            {
                Console.WriteLine("Clinic not found.");
                return;
            }

            if (clinic is IHasServices clinicWithServices)
            {
                Console.WriteLine($"Services in Clinic '{clinic.ClinicName}':");
                if (clinicWithServices.Services.Count == 0)
                    Console.WriteLine("No services assigned.");
                else
                    clinicWithServices.Services.ForEach(s => s.ViewServiceInfo());
            }
            else
            {
                Console.WriteLine("Clinic does not support services.");
            }
        }



        public static void AdminMenu() // displays the admin management menu and handles user input
        {

            Additional.WelcomeMessage("Admin Panel");

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
                        else Console.WriteLine("Assignment cancelled.");
                        break;

                    case "2": // Add a service to a clinic
                        int clinicId2 = Validation.IntValidation("Clinic ID");
                        int serviceId = Validation.IntValidation("Service ID");

                        if (Additional.ConfirmAction("add this service to the clinic"))
                        {
                            service.AddClinicService(clinicId2, serviceId);
                        }
                        else Console.WriteLine("Adding service cancelled.");
                        break;

                    case "3": // View doctors in a clinic
                        int clinicId3 = Validation.IntValidation("Clinic ID to view doctors");
                        service.GetClinicDoctors(clinicId3);
                        break;

                    case "4": // View services in a clinic
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

                Additional.HoldScreen();
            }


        }



        //====================================================
        //4. class constructor ...

        public Admin(string username, string email, int branchId)
        {
            BranchID = branchId;
            AdminCount++;
            UserName = username;
            UserEmail = email;
            UserRole = "Admin";
            UserStatus = "Active";
        }

    }
}
