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

        // SuperAdminMenu
        public static void SuperAdminMenu()
        {
            Console.Clear();
            Console.WriteLine("Welcome to SuperAdminMenu");
            Console.WriteLine("1. Users ( Admins And Doctors )");
            Console.WriteLine("2. Branchs");
            Console.WriteLine("3. Departments");
            Console.WriteLine("0. Exit");
            Console.Write("Please select an option: ");
            //to get the user choice ...
            char choice = Validation.CharValidation("option");
            switch (choice)
            {
                case '1':
                    AdminDoctorUserMenu();
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

        public static void AdminDoctorUserMenu() 
        {
            Console.Clear();
            Console.WriteLine("Users ( Admins And Doctors ) Menu ");
            Console.WriteLine("1. Admin ");
            Console.WriteLine("2. Doctor ");
            Console.WriteLine("0. Exit ");
            Console.WriteLine("Please select an option : ");
            char choice1 = Validation.CharValidation("option");
            switch (choice1)
            {
                case '1':
                    AdminUserMenu();
                    break;

                case '2':
                    break;

                case '0':
                    SuperAdminMenu();
                    Console.WriteLine("Invalid option, please try again.");
                    Additional.HoldScreen();
                    break;
            }
        }
        public static void AdminUserMenu() 
        {
            Console.Clear();
            Console.WriteLine("Users Admin Menu ");
            Console.WriteLine("1. Add New Admin ");
            Console.WriteLine("2. View All Admin ");
            Console.WriteLine("3. Update Admin ");
            Console.WriteLine("4. Delete Admin ");
            Console.WriteLine("0. Exit ");
            Console.WriteLine("Please select an option : ");
            char choice1 = Validation.CharValidation("option");
            switch (choice1)
            {
                case '1':
                    AddAdmin();
                    break;

                case '2':
                    ViewAdmins();
                    break;
                case '3':
                    UpdateAdmin();
                    break;
                case '4':
                    DeleteAdmin();
                    break;

                case '0':
                    AdminDoctorUserMenu();
                    Console.WriteLine("Exiting Admin User Menu.");
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
            AdminUserMenu();
        }


        // Update Admin method to update admin details
        public static void UpdateAdmin()
        {
            Console.Clear();
            Console.WriteLine("Available Admins for Updation");
            ViewAllAdmins();
            Console.WriteLine(new string('-', 40));
            int adminId = Validation.IntValidation("Enter the Admin ID to update:");
            var adminToUpdate = BranchDepartment.Admins.FirstOrDefault(a => a.UserId == adminId);
            if (adminToUpdate == null)
            {
                Console.WriteLine("Admin not found.");
                Additional.HoldScreen();
                return;
            }
            // Update admin details
            // Choose what to update
            Console.WriteLine("What do you want to update?");
            // Method to display admin update options
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Update Admin Information");
                Console.WriteLine("------------------------");
                Console.WriteLine("1. Update Email");
                Console.WriteLine("2. Update Phone Number");
                Console.WriteLine("3. Update National ID");
                Console.WriteLine("4. Update Role");
                Console.WriteLine("5. Update Status");
                Console.WriteLine("0. Exit");

                char choice = Validation.CharValidation("Choose an option: ");

                switch (choice)
                {
                    case '1':
                        adminToUpdate.UserEmail = Validation.EmailValidation("Enter new email: ");
                        break;
                    case '2':
                        adminToUpdate.P_UserPhoneNumber = Validation.IntValidation("Enter new phone number: ");
                        break;
                    case '3':
                        adminToUpdate.UserNationalID = Validation.StringValidation("Enter new national ID: ");
                        break;
                    case '4':
                        adminToUpdate.UserRole = Validation.StringValidation("Enter new role (Admin/SuperAdmin): ");
                        break;
                    case '5':
                        adminToUpdate.UserStatus = Validation.StringValidation("Enter new status (Active/Inactive): ");
                        break;
                    case '0':
                        Console.WriteLine("Exiting update menu...");
                        AdminUserMenu();
                        return;
                    default:
                        Console.WriteLine("Invalid option, please try again.");
                        break;
                }
                Console.Write("Do you want to update anything else? (y/n): ");
                string continueChoice = Console.ReadLine().ToLower();

                if (continueChoice != "y")
                {
                    Console.WriteLine("Returning to Admin Menu...");
                    AdminUserMenu();
                    return;
                }
            }

        }

        // Delete Admin method to delete an admin by ID
        public static void DeleteAdmin()
        {
            Console.Clear();
            Console.WriteLine("Available Admins for Deletion");
            ViewAllAdmins();
            Console.WriteLine(new string('-', 40));
            int adminId = Validation.IntValidation("Enter the Admin ID to delete:");
            var adminToDelete = BranchDepartment.Admins.FirstOrDefault(a => a.UserId == adminId);
            if (adminToDelete == null)
            {
                Console.WriteLine("Admin not found.");
                Additional.HoldScreen();
                return;
            }
            // Confirm deletion
            if (Additional.ConfirmAction("delete this admin"))
            {
                BranchDepartment.Admins.Remove(adminToDelete);
                Console.WriteLine("Admin deleted successfully.");
            }
            else
            {
                Console.WriteLine("Deletion cancelled.");
            }
            Additional.HoldScreen();
            // Return to Admin User Menu
            AdminUserMenu();
        }

        // View Admins method to display all admins in the branch department
        public static void ViewAdmins()
        {
            Console.Clear();
            Console.WriteLine("List of Admins");
            if (BranchDepartment.Admins.Count == 0)
            {
                Console.WriteLine("No admins available in the system.");
                Additional.HoldScreen();
                return;
            }
            foreach (var admin in BranchDepartment.Admins)
            {
                Console.WriteLine($"Admin ID        : {admin.UserId}");
                Console.WriteLine($"Name            : {admin.UserName}");
                Console.WriteLine($"Email           : {admin.UserEmail}");
                Console.WriteLine($"Phone Number    : {admin.P_UserPhoneNumber}");
                Console.WriteLine($"National ID     : {admin.UserNationalID}");
                Console.WriteLine($"Status          : {admin.UserStatus}");
                Console.WriteLine(new string('-', 40));
            }
            Additional.HoldScreen();
            AdminUserMenu();
        }

        // Common Method to View All Admins
        public static void ViewAllAdmins()
        {
            Console.Clear();
            Console.WriteLine("List of All Admins");
            if (BranchDepartment.Admins.Count == 0)
            {
                Console.WriteLine("No admins available in the system.");
                Additional.HoldScreen();
                return;
            }
            foreach (var admin in BranchDepartment.Admins)
            {
                Console.WriteLine($"Admin ID        : {admin.UserId}");
                Console.WriteLine($"Name            : {admin.UserName}");
                Console.WriteLine($"Email           : {admin.UserEmail}");
                Console.WriteLine($"Phone Number    : {admin.P_UserPhoneNumber}");
                Console.WriteLine($"National ID     : {admin.UserNationalID}");
                Console.WriteLine($"Role            : {admin.UserRole}");
                Console.WriteLine($"Status          : {admin.UserStatus}");
                Console.WriteLine(new string('-', 40));
            }
           
        }

        // Additional Method 

        


        //====================================================
        //4. class constructor ...

        public SuperAdmin(string username, string password, string email)
        {
           
            UserRole = "SuperAdmin";
            UserStatus = "Active"; //default status for SuperAdmin  
        }

       
    }
}