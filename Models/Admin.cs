using CodelineHealthCareCenter.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;


namespace CodelineHealthCareCenter.Models
{
    class Admin : User, IAdminService
    {
        //====================================================
        //1. class fields ...
        public int BranchID;
        public List<Clinic> Clinics = new List<Clinic>();

        // Shared static lists
        //public static List<Doctor> Doctors = new List<Doctor>();
        public static List<Service> Services = new List<Service>();

        //====================================================
        //2. class properties ...
        public static int AdminCount { get; private set; }

        //====================================================
        //3. class methods ...

        public void AddClinic()
        {
            //Clinics.Add(clinic);
            //Console.WriteLine($" Clinic '{clinic.ClinicName}' added to Admin '{UserName}'.");
            Department.ViewAllDepartments();
            int departmentId = Validation.IntValidation("Department ID to add Clinic to");
            foreach(var department in BranchDepartment.Departments)
            {
                if (department.DepartmentId == departmentId)
                {
                    //to get the branch ID from the department ...
                    int branchId = department.BranchId;
                    //to get the floor ID from the branch ...
                    int floorId = 1; // Assuming a default floor ID, can be modified as needed
                    //to get the room ID from the branch ...
                    int roomId = 1; // Assuming a default room ID, can be modified as needed
                    string clinicName = Validation.StringValidation("Clinic Name");
                    Clinic newClinic = new Clinic(clinicName, departmentId, branchId, floorId, roomId);
                    //newClinic.CreateClinic(departmentId);
                    Clinics.Add(newClinic);
                    Console.WriteLine($"Clinic '{newClinic.ClinicName}' added to Department ID {departmentId}.");
                    return;
                }
            }
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

        public void AssignDoctorToClinic(int doctorId, int clinicId)
        {
            var clinic = Clinics.FirstOrDefault(c => c.ClinicId == clinicId);
            var doctor = BranchDepartment.Doctors.FirstOrDefault(d => d.UserId == doctorId);

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

            if (clinic.Doctors.Any(d => d.UserId == doctorId))
            {
                Console.WriteLine("Doctor already assigned.");
                return;
            }

            clinic.Doctors.Add(doctor);
            Console.WriteLine($"Doctor {doctor.UserName} assigned to Clinic '{clinic.ClinicName}'.");
        }

        public void AddClinicService(int clinicId, int serviceId)
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

        public void GetClinicDoctors(int clinicId)
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

        public void AddClinicSpot(int clinicId, DateTime newSpot)
        {
            var clinic = Clinics.FirstOrDefault(c => c.ClinicId == clinicId);
            if (clinic == null)
            {
                Console.WriteLine("Clinic not found.");
                return;
            }
            if (clinic.ClinicSpots.Contains(newSpot))
            {
                Console.WriteLine("Spot already exists.");
                return;
            }
            clinic.ClinicSpots.Add(newSpot);
            Console.WriteLine($"Spot {newSpot:G} added to Clinic '{clinic.ClinicName}'.");
        }

        public void RemoveClinicSpot(int clinicId, DateTime spotToRemove)
        {
            var clinic = Clinics.FirstOrDefault(c => c.ClinicId == clinicId);
            if (clinic == null)
            {
                Console.WriteLine("Clinic not found.");
                return;
            }
            if (!clinic.ClinicSpots.Remove(spotToRemove))
            {
                Console.WriteLine("Spot not found.");
                return;
            }
            Console.WriteLine($"Spot {spotToRemove:G} removed from Clinic '{clinic.ClinicName}'.");
        }

        public static void SaveToFile(string filePath) 
        {
            using StreamWriter writer = new StreamWriter(filePath);
            foreach (var admin in BranchDepartment.Admins)
            {
                writer.WriteLine($"{admin.UserId}|{admin.UserName}|{admin.UserEmail}|{admin.BranchID}|{admin.UserStatus}|{admin.UserNationalID}");
            }
        }

        public static void LoadFromFile(string filePath)
        {
            BranchDepartment.Admins.Clear();

            if (!File.Exists(filePath)) return;

            string[] lines = File.ReadAllLines(filePath);
            foreach (var line in lines)
            {
                string[] parts = line.Split('|');
                if (parts.Length < 7) continue;

                Admin admin = new Admin(parts[1], parts[2], int.Parse(parts[3]))
                {
                    UserId = int.Parse(parts[0]),
                    UserStatus = parts[4],
                    UserNationalID = parts[5],
                    
                };

                BranchDepartment.Admins.Add(admin);
            }
        }



        public static void AdminMenu()
        {
            Additional.WelcomeMessage("Admin Panel");

            Console.WriteLine("Enter your National ID to continue:");
            string AdminNationalId = Validation.StringValidation("your national ID");

            Admin CurrentAdmin = null;
            foreach (var admin in BranchDepartment.Admins)
            {
                if (admin.UserNationalID == AdminNationalId)
                {
                    CurrentAdmin = admin;
                    break;
                }
            }

            if (CurrentAdmin == null)
            {
                Console.WriteLine("Admin not found.");
                return;
            }

            while (true)
            {
                Console.Clear();
                Console.WriteLine(" ADMIN MANAGEMENT MENU ");
                Console.WriteLine($"Welcome, {CurrentAdmin.UserName}!");
                Console.WriteLine("1. Add Clinic");
                Console.WriteLine("2. Assign Doctor to Clinic");
                Console.WriteLine("3. Add Service to Clinic");
                Console.WriteLine("4. View Clinic's Doctors");
                Console.WriteLine("5. View Clinic's Services");
                Console.WriteLine("6. Add Clinic Spot");
                Console.WriteLine("7. Remove Clinic Spot");
                Console.WriteLine("8. Exit");
                Console.Write("Select an option: ");
                string choice = Console.ReadLine();
                Console.WriteLine();

                switch (choice)
                {
                    case "1":
                        CurrentAdmin.AddClinic();
                        break;
                    case "2":
                        Console.WriteLine("All Doctors:");
                        Doctor.GetAllDoctors();

                        //Doctor.GetAllDoctors();
                        Console.WriteLine("All Clinics:");
                        Clinic.GetAllClinics();
                        CurrentAdmin.ViewClinics();
                        int doctorId = Validation.IntValidation("Doctor ID");
                        int clinicId = Validation.IntValidation("Clinic ID");
                        if (Additional.ConfirmAction("assign the doctor to this clinic"))
                            CurrentAdmin.AssignDoctorToClinic(doctorId, clinicId);
                        else
                            Console.WriteLine("Assignment cancelled.");
                        break;

                    case "3":
                        Console.WriteLine("All Clinics:");
                        CurrentAdmin.ViewClinics();
                        int clinicId2 = Validation.IntValidation("Clinic ID");
                        int serviceId = Validation.IntValidation("Service ID");
                        if (Additional.ConfirmAction("add this service to the clinic"))
                            CurrentAdmin.AddClinicService(clinicId2, serviceId);
                        else
                            Console.WriteLine("Adding service cancelled.");
                        break;

                    case "4":
                        int clinicId3 = Validation.IntValidation("Clinic ID to view doctors");
                        CurrentAdmin.GetClinicDoctors(clinicId3);
                        break;

                    case "5":
                        int clinicId4 = Validation.IntValidation("Clinic ID to view services");
                        CurrentAdmin.GetClinicServices(clinicId4);
                        break;

                    case "6":
                        int cl5 = Validation.IntValidation("Clinic ID to add spot");
                        DateTime newSpot = Validation.DateTimeValidation("New Spot (e.g., MM/dd/yyyy HH:mm)");
                        if (Additional.ConfirmAction($"add spot {newSpot:G} to this clinic"))
                            CurrentAdmin.AddClinicSpot(cl5, newSpot);
                        else
                            Console.WriteLine("Spot addition cancelled.");
                        break;

                    case "7":
                        int cl6 = Validation.IntValidation("Clinic ID to remove spot");
                        DateTime spotToRemove = Validation.DateTimeValidation("Spot to remove (e.g., MM/dd/yyyy HH:mm)");
                        if (Additional.ConfirmAction($"remove spot {spotToRemove:G} from this clinic"))
                            CurrentAdmin.RemoveClinicSpot(cl6, spotToRemove);
                        else
                            Console.WriteLine("Spot removal cancelled.");
                        break;

                    case "8":
                        Console.WriteLine("Exiting Admin Menu...");
                        return;

                    default:
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
