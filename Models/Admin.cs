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

        //public void AddClinic()
        //{
        //    //Clinics.Add(clinic);
        //    //Console.WriteLine($" Clinic '{clinic.ClinicName}' added to Admin '{UserName}'.");
        //    Department.ViewAllDepartments();
        //    int departmentId = Validation.IntValidation("Department ID to add Clinic to");
        //    foreach(var department in BranchDepartment.Departments)
        //    {
        //        if (department.DepartmentId == departmentId)
        //        {
        //            //to get the branch ID from the department ...
        //            int branchId = department.BranchId;
        //            //to get the floor ID from the branch ...
        //            int floorId = 1; // Assuming a default floor ID, can be modified as needed
        //            //to get the room ID from the branch ...
        //            int roomId = 1; // Assuming a default room ID, can be modified as needed
        //            string clinicName = Validation.StringValidation("Clinic Name");
        //            //Clinic newClinic = new Clinic(clinicName, departmentId, branchId, floorId, roomId);
        //            //newClinic.CreateClinic(departmentId);
        //            Clinics.Add(newClinic);
        //            Console.WriteLine($"Clinic '{newClinic.ClinicName}' added to Department ID {departmentId}.");
        //            return;
        //        }
        //    }
        //}

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
            foreach (var department in BranchDepartment.Departments)
            {
                //to check if clinic exists ...
                foreach (var clinic in department.Clinics)
                {
                    if (clinic.ClinicId == clinicId)
                    {
                        //to check if doctor exists ...
                        foreach (var doctor in BranchDepartment.Doctors)
                        {
                            if (doctor.UserId == doctorId)
                            {
                                //to check if the doctor is already assigned to the clinic ...
                                //var existingDoctor = clinic.Doctors.FirstOrDefault(d => d.UserId == doctorId);
                                //if (existingDoctor != null)
                                //{
                                //    Console.WriteLine($"Doctor '{doctor.UserName}' is already assigned to Clinic '{clinic.ClinicName}'.");
                                //    return;
                                //}
                                //to assign the doctor to the clinic ...
                                clinic.Doctors.Add(doctor);
                                //to add the clinic id to the doctor ...
                                doctor.ClinicID = clinicId; // Assuming Doctor class has a ClinicID property
                                Console.WriteLine($"Doctor '{doctor.UserName}' assigned to Clinic '{clinic.ClinicName}'.");
                                return;
                            }
                        }
                        Console.WriteLine("Doctor not found.");
                        return;
                    }
                }
                Console.WriteLine("Clinic not found.");
            }

        }

        public void AddClinicService()
        {
            //to list all clinics ...
            Clinic.GetAllClinics();
            int clinicId = Validation.IntValidation("Clinic ID to add service to");
            //to get service details ...
            string serviceName = Validation.StringValidation("Service Name");
            double servicePrice = Validation.DoubleValidation("Service Price");
            Service newService = new Service(serviceName, servicePrice)
            {
                ClinicId = clinicId
            };
            //for adding the service to the clinic ...
            foreach (var department in BranchDepartment.Departments)
            {
                foreach (var clinic in department.Clinics)
                {
                    if (clinic.ClinicId == clinicId)
                    {
                        foreach (var existingService in clinic.Services)
                        {
                            if (existingService.ServiceName.Equals(newService.ServiceName, StringComparison.OrdinalIgnoreCase))
                            {
                                Console.WriteLine("Service already exists in this clinic.");
                                return;
                            }
                        }
                        clinic.Services.Add(newService);
                        Service.Services.Add(newService); // Add to the static list
                        Console.WriteLine($"Service '{newService.ServiceName}' added to Clinic '{clinic.ClinicName}'.");
                        return;
                    }
                }
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

            //if (clinic is IHasServices clinicWithServices)
            //{
            //    Console.WriteLine($"Services in Clinic '{clinic.ClinicName}':");
            //    if (clinicWithServices.Services.Count == 0)
            //        Console.WriteLine("No services assigned.");
            //    else
            //        clinicWithServices.Services.ForEach(s => s.ViewServiceInfo());
            //}
            //else
            //{
            //    Console.WriteLine("Clinic does not support services.");
            //}

            //to list all services in the clinic ...
            Console.WriteLine($"Services in Clinic '{clinic.ClinicName}':");
            foreach(var department in BranchDepartment.Departments)
            {
               foreach(var my_clinic in department.Clinics)
                {
                    if (my_clinic.ClinicId == clinicId)
                    {
                        if (my_clinic.Services.Count == 0)
                        {
                            Console.WriteLine("No services assigned.");
                        }
                        else
                        {
                            foreach (var service in my_clinic.Services)
                            {
                                service.ViewServiceInfo();
                            }
                        }
                        return;
                    }
                }
            }
        }

        public void AddClinicSpot(int clinicId, DateTime newSpot)
        {
            //var clinic = Clinics.FirstOrDefault(c => c.ClinicId == clinicId);
            //if (clinic == null)
            //{
            //    Console.WriteLine("Clinic not found.");
            //    return;
            //}
            //if (clinic.ClinicSpots.Contains(newSpot))
            //{
            //    Console.WriteLine("Spot already exists.");
            //    return;
            //}
            //clinic.ClinicSpots.Add(newSpot);
            //Console.WriteLine($"Spot {newSpot:G} added to Clinic '{clinic.ClinicName}'.");
            foreach(var department in BranchDepartment.Departments)
            {
                foreach (var clinic in department.Clinics)
                {
                    if (clinic.ClinicId == clinicId)
                    {
                        if (clinic.ClinicSpots.Contains(newSpot))
                        {
                            Console.WriteLine("Spot already exists.");
                            return;
                        }
                        clinic.ClinicSpots.Add(newSpot);
                        Console.WriteLine($"Spot {newSpot:G} added to Clinic '{clinic.ClinicName}'.");
                        return;
                    }
                }
                Console.WriteLine("Clinic not found in the specified department.");
            }
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
                Console.WriteLine("2. Add Add Clinic Spot");
                Console.WriteLine("3. Add Service");
                Console.WriteLine("4. Assign Doctor to Clinic");
                Console.WriteLine("5. View Clinic");
                //Console.WriteLine("6. View Clinic's Doctors");
                //Console.WriteLine("7. View Clinic's Services");
                //Console.WriteLine("8. Remove Clinic Spot");
                Console.WriteLine("0. Exit");
                Console.Write("Select an option: ");
                string choice = Console.ReadLine();
                Console.WriteLine();

                switch (choice)
                {
                    case "1":
                        Clinic.AddClinic();
                        break;
                    case "2":
                        //to list all clinics ...
                        Console.WriteLine("List of All Clinics:");
                        Clinic.GetAllClinics();
                        int cl5 = Validation.IntValidation("Clinic ID to add spot");
                        DateTime newSpot = Validation.DateTimeValidation("New Spot (e.g., MM/dd/yyyy HH:mm)");
                        if (Additional.ConfirmAction($"add spot {newSpot:G} to this clinic"))
                            CurrentAdmin.AddClinicSpot(cl5, newSpot);
                        else
                            Console.WriteLine("Spot addition cancelled.");
                        break;
                    case "3":
                        CurrentAdmin.AddClinicService();
                        break;
                    case "4":
                        Console.WriteLine("All Doctors:");
                        Doctor.GetAllDoctors();
                        Console.WriteLine("All Clinics:");
                        Clinic.GetAllClinics();
                        int doctorId = Validation.IntValidation("Doctor ID");
                        int clinicId = Validation.IntValidation("Clinic ID");
                        if (Additional.ConfirmAction("assign the doctor to this clinic"))
                            CurrentAdmin.AssignDoctorToClinic(doctorId, clinicId);
                        else
                            Console.WriteLine("Assignment cancelled.");
                        break;

                    case "5":
                        //to list all clinics ...
                        Console.WriteLine("List of All Clinics:");
                        Clinic.GetAllClinics();
                        break;
                    //case "6":
                    //    int clinicId3 = Validation.IntValidation("Clinic ID to view doctors");
                    //    CurrentAdmin.GetClinicDoctors(clinicId3);
                    //    break;

                    //case "7":
                    //    //to list all clinics ...
                    //    Console.WriteLine("List of All Clinics:");
                    //    if (CurrentAdmin.Clinics.Count == 0)
                    //    {
                    //        Console.WriteLine("No clinics available.");
                    //        break;
                    //    }
                    //    CurrentAdmin.Clinics.ForEach(c => c.ViewClinicInfo());
                    //    int clinicId4 = Validation.IntValidation("Clinic ID to view services");
                    //    CurrentAdmin.GetClinicServices(clinicId4);
                    //    break;



                    //case "8":
                    //    //to list all clinics ...
                    //    Console.WriteLine("List of All Clinics:");
                    //    if (CurrentAdmin.Clinics.Count == 0)
                    //    {
                    //        Console.WriteLine("No clinics available.");
                    //        break;
                    //    }
                    //    CurrentAdmin.Clinics.ForEach(c => c.ViewClinicInfo());
                    //    int cl6 = Validation.IntValidation("Clinic ID to remove spot");
                    //    //to get all spot in the clinic ...
                    //    Console.WriteLine("Available Spots in this Clinic:");
                    //    var clinic = CurrentAdmin.Clinics.FirstOrDefault(c => c.ClinicId == cl6);
                    //    if (clinic == null || clinic.ClinicSpots.Count == 0)
                    //    {
                    //        Console.WriteLine("No spots available in this clinic.");
                    //        break;
                    //    }
                    //    foreach (var spot in clinic.ClinicSpots)
                    //    {
                    //        Console.WriteLine($"- {spot:G}");
                    //    }
                    //    //to get the spot to remove ...
                    //    DateTime spotToRemove = Validation.DateTimeValidation("Spot to remove (e.g., MM/dd/yyyy HH:mm)");
                    //    if (Additional.ConfirmAction($"remove spot {spotToRemove:G} from this clinic"))
                    //        CurrentAdmin.RemoveClinicSpot(cl6, spotToRemove);
                    //    else
                    //        Console.WriteLine("Spot removal cancelled.");
                    //    break;

                    case "0":
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
