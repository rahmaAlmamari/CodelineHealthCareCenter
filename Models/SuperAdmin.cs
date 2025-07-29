using CodelineHealthCareCenter.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodelineHealthCareCenter.Models
{
    class SuperAdmin : User, ISuperAdminService
    {
        //1. class feilds ...
        public int HospitalId;

        //====================================================
        //2. class properity ...

        //====================================================
        //3. class method ...
        public static void SuperAdminMenu()
        {

            Console.WriteLine("Welcome to SuperAdminMenu");
            Console.WriteLine("1. Add Doctor");
            Console.WriteLine("2. Add Admin");
            Console.WriteLine("3. Assign Admin To Branch");
            Console.WriteLine("4. Update Doctor");
            Console.WriteLine("5. Delete Doctor");
            Console.WriteLine("6. View Doctors");
            Console.WriteLine("7. View Admins");
            Console.WriteLine("8. Update Admin");
            Console.WriteLine("9. Delete Admin");
            Console.WriteLine("4. Exit");
            Console.Write("Please select an option: ");
            //to get the user choice ...
            char choice = Validation.CharValidation("option");
            switch (choice)
            {
                case '1':
                    Console.WriteLine("Add Branch");
                   



                    break;
                case '2':
                    //to add a new admin ...
                    Console.WriteLine("Adding a new admin...");
                    
                    break;
                case '3':
                    //to assign admin to branch ...
                    Console.WriteLine("Assigning admin to branch...");
                    break;
                case '4':
                    //to update doctor ...
                    Console.WriteLine("Updating doctor...");
                    break;
                case '5':
                    //to delete doctor ...
                    Console.WriteLine("Deleting doctor...");
                    break;
                case '6':
                    //to view doctors ...
                    //ViewDoctors();
                    Branch.AddBranch();
                    break;
                case '7':
                    //to view admins ...
                    Console.WriteLine("Viewing admins...");
                    break;
                case '8':
                    //to update admin ...
                    Console.WriteLine("Updating admin...");
                    break;
                case '9':
                    //to delete admin ...
                    Console.WriteLine("Deleting admin...");
                    break;
                case '0':
                    Console.WriteLine("Exiting SuperAdmin Menu.");
                    break;
                default:
                    Console.WriteLine("Invalid option, please try again.");
                    Additional.HoldScreen();
                    break;
            }


        }

       
        public static bool NationalIdExists(string nationalId)
        {
            //to check if the national id exists or not in SuperAdmin list hospital class ...  

            foreach (var doctor in BranchDepartment.Doctors)
            {
                if (doctor.UserNationalID == nationalId)
                {
                    return true; //if the national id exists ...  
                }
            }
            return false; //if national id does not exist ...  
        }
       

        public void PrintDoctorDetails()
        {
            Console.WriteLine($"Doctor ID : { UserId}");
            Console.WriteLine($"Doctor Name : {UserName}");
            Console.WriteLine($"Doctor Email : {UserEmail}");
            Console.WriteLine($"Doctor Phone Number : {UserPhoneNumber}");
            Console.WriteLine($"Doctor National ID : {UserNationalID}");
            // Console.WriteLine($"Doctor Specialization : {D_Specialization}");



        }

        // DOCTOR Methods
        // Add Doctor method to add a new doctor
        public static void AddDoctor()
        {
            Console.Clear();
            Console.WriteLine("Add New Doctor");
            // Get doctor details from user
            string name = Validation.StringValidation("doctor name");
            string email = Validation.EmailValidation("doctor email");
            int phoneNumber = Validation.IntValidation("doctor phone number");
            string nationalId = Validation.StringValidation("doctor national ID");
            string specialization = Validation.StringValidation("doctor specialization");
            // Check if the national ID already exists
            if (NationalIdExists(nationalId))
            {
                Console.WriteLine("A doctor with this National ID already exists.");
                Additional.HoldScreen();
                return;
            }

            
            // Create a new doctor instance
            Doctor doctor = new Doctor(name, email, specialization, 0, 0); 
            doctor.UserName = name;
            doctor.UserEmail = email;
            doctor.P_UserPhoneNumber = phoneNumber;
            doctor.UserNationalID = nationalId;
            doctor.UserRole = "Doctor"; // Set the role to Doctor
            doctor.UserStatus = "Active"; // Set the status to Active
            // Add the doctor to the List
            BranchDepartment.Doctors.Add(doctor);
            Console.WriteLine("Doctor added successfully.");
            Additional.HoldScreen();

        }

        // ViewDoctors method to display all doctors in the branch department
        public static void ViewDoctors()
        {
            Console.Clear();
            Console.WriteLine("List of Doctors");

            if (BranchDepartment.Doctors.Count == 0)
            {
                Console.WriteLine("No doctors available in the system.");
                Additional.HoldScreen();
                return;
            }

            foreach (var doctor in BranchDepartment.Doctors)
            {
                Console.WriteLine($"Doctor ID        : {doctor.UserId}");
                Console.WriteLine($"Name            : {doctor.UserName}");
                Console.WriteLine($"Email           : {doctor.UserEmail}");
                Console.WriteLine($"Phone Number    : {doctor.UserPhoneNumber}");
                Console.WriteLine($"National ID     : {doctor.UserNationalID}");
                Console.WriteLine($"Specialization  : {doctor.DoctorSpecialization}");
                Console.WriteLine($"Status          : {doctor.UserStatus}");
                Console.WriteLine(new string('-', 40));
            }

            Additional.HoldScreen();
        }

        // Update Doctor method to update doctor details

        public static void UpdateDoctor()
        {
            Console.WriteLine("Enter the Doctor ID to update:");
            int doctorId = Validation.IntValidation("Doctor ID");
            var doctorToUpdate = BranchDepartment.Doctors.FirstOrDefault(d => d.UserId == doctorId);
            if (doctorToUpdate == null)
            {
                Console.WriteLine("Doctor not found.");
                Additional.HoldScreen();
                return;
            }
            // Update doctor details
            doctorToUpdate.UserName = Validation.StringValidation("new name");
            doctorToUpdate.UserEmail = Validation.EmailValidation("new email");
            doctorToUpdate.P_UserPhoneNumber = Validation.IntValidation("new phone number");
            doctorToUpdate.DoctorSpecialization = Validation.StringValidation("new specialization");
            Console.WriteLine("Doctor details updated successfully.");
            Additional.HoldScreen();
        }

        // Delete Doctor method to delete a doctor by ID
        public static void DeleteDoctor()
        {
            Console.WriteLine("Enter the Doctor ID to delete:");
            int doctorId = Validation.IntValidation("Doctor ID");
            var doctorToDelete = BranchDepartment.Doctors.FirstOrDefault(d => d.UserId == doctorId);
            if (doctorToDelete == null)
            {
                Console.WriteLine("Doctor not found.");
                Additional.HoldScreen();
                return;
            }
            // Confirm deletion
            if (Additional.ConfirmAction("delete this doctor"))
            {
                BranchDepartment.Doctors.Remove(doctorToDelete);
                Console.WriteLine("Doctor deleted successfully.");
            }
            else
            {
                Console.WriteLine("Deletion cancelled.");
            }
            Additional.HoldScreen();
        }


        // Set Doctor Status method to set the status of a doctor
        public static void SetDoctorStatus()
        {
            Console.WriteLine("Enter the Doctor ID to set status:");
            int doctorId = Validation.IntValidation("Doctor ID");
            var doctorToUpdate = BranchDepartment.Doctors.FirstOrDefault(d => d.UserId == doctorId);
            if (doctorToUpdate == null)
            {
                Console.WriteLine("Doctor not found.");
                Additional.HoldScreen();
                return;
            }
            // Set the status of the doctor
            doctorToUpdate.UserStatus = Validation.StringValidation("new status (Active/Inactive)");
            Console.WriteLine("Doctor status updated successfully.");
            Additional.HoldScreen();
        }

        // ADMIN Method

        // Add Admin method to add a new admin
        public static void AddAdmin()
        {
            Console.Clear();
            Console.WriteLine("Add New Admin");
            // Get admin details from user
            string name = Validation.StringValidation("admin name");
            string email = Validation.EmailValidation("admin email");
            int phoneNumber = Validation.IntValidation("admin phone number");
            string nationalId = Validation.StringValidation("admin national ID");
            // Check if the national ID already exists
            if (NationalIdExists(nationalId))
            {
                Console.WriteLine("An admin with this National ID already exists.");
                Additional.HoldScreen();
                return;
            }
            // Create a new admin instance
            Admin admin = new Admin(name, email, 0);
            admin.UserName = name;
            admin.UserEmail = email;
            admin.P_UserPhoneNumber = phoneNumber;
            admin.UserNationalID = nationalId;
            admin.UserRole = "Admin"; // Set the role to Admin
            admin.UserStatus = "Active"; // Set the status to Active
            // Add the admin to the List
            BranchDepartment.Admins.Add(admin);
            Console.WriteLine("Admin added successfully.");
            Additional.HoldScreen();
        }


        // Update Admin method to update admin details
        public static void UpdateAdmin()
        {
            Console.WriteLine("Enter the Admin ID to update:");
            int adminId = Validation.IntValidation("Admin ID");
            var adminToUpdate = BranchDepartment.Admins.FirstOrDefault(a => a.UserId == adminId);
            if (adminToUpdate == null)
            {
                Console.WriteLine("Admin not found.");
                Additional.HoldScreen();
                return;
            }
            // Update admin details
            adminToUpdate.UserName = Validation.StringValidation("new name");
            adminToUpdate.UserEmail = Validation.EmailValidation("new email");
            adminToUpdate.P_UserPhoneNumber = Validation.IntValidation("new phone number");
            Console.WriteLine("Admin details updated successfully.");
            Additional.HoldScreen();
        }




        //====================================================
        //4. class constructor ...

        public SuperAdmin(string username, string password, string email)
        {
           
            UserRole = "SuperAdmin";
            UserStatus = "Active"; //default status for SuperAdmin  
        }

       
    }
}