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
        public static IAdminService service; // new static service field

        
        public static List<Doctor> Doctors = new List<Doctor>();
        public static List<Service> Services = new List<Service>();


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

        public void AssignDoctorToClinic(int doctorId, int clinicId) // assigns a doctor to a clinic
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
                Console.WriteLine("Doctor is already assigned to this clinic.");
                return;
            }

            clinic.Doctors.Add(doctor);
            Console.WriteLine($"Doctor {doctor.UserName} assigned to Clinic '{clinic.ClinicName}'.");
        }

        public void AddClinicService(int clinicId, int serviceId) // adds a service to a clinic
        {
            var clinic = Clinics.FirstOrDefault(c => c.ClinicId == clinicId);
            var service = Services.FirstOrDefault(s => s.ServiceId == serviceId);

            if (clinic == null)
            {
                Console.WriteLine("Clinic not found.");
                return;
            }

            if (service == null)
            {
                Console.WriteLine("Service not found.");
                return;
            }

            if (clinic is IHasServices clinicWithServices)
            {
                if (clinicWithServices.Services.Any(s => s.ServiceId == serviceId))
                {
                    Console.WriteLine("Service already assigned to this clinic.");
                    return;
                }

                clinicWithServices.Services.Add(service);
                Console.WriteLine($"Service '{service.ServiceName}' added to Clinic '{clinic.ClinicName}'.");
            }
            else
            {
                Console.WriteLine("Clinic does not support services. Add a List<Service> to Clinic.");
            }
        }

        public void GetClinicDoctors(int clinicId) // retrieves and displays doctors assigned to a specific clinic
        {
            var clinic = Clinics.FirstOrDefault(c => c.ClinicId == clinicId);

            if (clinic == null)
            {
                Console.WriteLine("Clinic not found.");
                return;
            }

            Console.WriteLine($"Doctors assigned to Clinic '{clinic.ClinicName}':");
            if (clinic.Doctors.Count == 0)
            {
                Console.WriteLine("No doctors assigned.");
                return;
            }

            foreach (var doctor in clinic.Doctors)
                doctor.ViewDoctorInfo();
        }

        public void GetClinicServices(int clinicId)
        {
            var clinic = Clinics.FirstOrDefault(c => c.ClinicId == clinicId);

            if (clinic == null)
            {
                Console.WriteLine("Clinic not found.");
                return;
            }

            if (clinic is IHasServices clinicWithServices)
            {
                Console.WriteLine($"Services assigned to Clinic '{clinic.ClinicName}':");

                if (clinicWithServices.Services.Count == 0)
                {
                    Console.WriteLine("No services assigned.");
                    return;
                }

                foreach (var service in clinicWithServices.Services)
                    service.ViewServiceInfo();
            }
            else
            {
                Console.WriteLine("This clinic doesn't support service listing.");
            }
        }





        //====================================================
        //4. class constructor ...

        public Admin(string username, string email, int branchId)
        {
            BranchID = branchId;
            AdminCount++;
            UserRole = "Admin";
            UserStatus = "Active";
        }

    }
}
