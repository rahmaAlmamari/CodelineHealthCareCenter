using CodelineHealthCareCenter.Services;
using Microsoft.VisualBasic.FileIO;
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
        public static string DoctorsFilePath = "Doctors.txt";
        public static string AdminsFilePath = "Admins.txt";
        public static string SuperAdminFilePath = "SuperAdmin.txt"; // File path for SuperAdmin data
        //====================================================
        //2. class properity ...

        //====================================================
        //3. class method ...

        // SuperAdminMenu -> Main Menu
        public static void SuperAdminMenu()
        {
            //Branch.LoadBranches();
            //Department.LoadDepartmentsFromFile();
            //LoadDoctorsFromFile();
            //LoadAdminsFromFile();
            //Hospital.LoadHospitalFromFile();

            char choice;
            do
            {
                Console.Clear();
                Console.WriteLine("Welcome to SuperAdminMenu");
                Console.WriteLine("1. Users ( Admins And Doctors )");
                Console.WriteLine("2. Branchs");
                Console.WriteLine("3. Departments");
                Console.WriteLine("0. Exit");
                choice = Validation.CharValidation("option");

                switch (choice)
                {
                    case '1':
                        AdminDoctorUserMenu();
                        break;
                    case '2':
                        AdminBranchMenu();
                        break;
                    case '3':
                        AdminDepartmentMenu();
                        break;
                    case '0':
                        Console.WriteLine("Exiting SuperAdmin Menu.");
                        Additional.HoldScreen();
                        // return to MainMenu in program file
                        Program.ShowMainMenu();
                        //Environment.Exit(0); // Exit the application
                        break; // Exit the SuperAdminMenu
                    default:
                        Console.WriteLine("Invalid option, please try again.");
                        Additional.HoldScreen();
                        break;
                }
            } while (choice != '0');


        }

        // SuperAdmin -> to choose Admin and Doctor Menu 
        public static void AdminDoctorUserMenu()
        {
            Console.Clear();
            Console.WriteLine("Users ( Admins And Doctors ) Menu ");
            Console.WriteLine("1. Admin ");
            Console.WriteLine("2. Doctor ");
            Console.WriteLine("0. Exit ");
            char choice1 = Validation.CharValidation("Please select an option :");
            switch (choice1)
            {
                case '1':
                    AdminUserMenu();
                    break;

                case '2':
                    DoctorUserMenu();
                    break;
                case '0':
                    Console.WriteLine("Exiting Users Menu.");
                    Additional.HoldScreen();
                    SuperAdminMenu();
                    break;
                default:
                    Console.WriteLine("Invalid option, please try again.");
                    Additional.HoldScreen();
                    break;
            }
        }
        // SuperAdmin -> Admin Menu 
        public static void AdminUserMenu()
        {
            
            char choice1;
            do
            {
                Console.Clear();
                Console.WriteLine("Users Admin Menu ");
                Console.WriteLine("1. Add New Admin ");
                Console.WriteLine("2. View All Admin ");
                Console.WriteLine("3. Update Admin ");
                Console.WriteLine("4. Delete Admin ");
                Console.WriteLine("0. Back ");
                choice1 = Validation.CharValidation("Please select an option : ");

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
                        break; 
                    default:
                        Console.WriteLine("Invalid option, please try again.");
                        Additional.HoldScreen();
                        break;
                }
            } while (choice1 != '0');






        }
        // SuperAdmin -> Doctor Menu 
        public static void DoctorUserMenu()
        {
            string choice1;
            do
            {
                Console.Clear();
                Console.WriteLine("Doctor Admin Menu ");
                Console.WriteLine("1. Add New Doctor ");
                Console.WriteLine("2. View All Doctors ");
                Console.WriteLine("3. Update Doctor ");
                Console.WriteLine("4. Delete Doctor ");
                Console.WriteLine("0. Back ");
                choice1 = Validation.StringValidation("Please select an option : ");

                switch (choice1)
                {
                    case "1":
                        AddDoctor();
                        break;
                    case "2":
                        ViewDoctors();
                        break;
                    case "3":
                        UpdateDoctor();
                        break;
                    case "4":
                        DeleteDoctor();
                        break;
                    case "0":
                        AdminDoctorUserMenu();
                        break; 
                    default:
                        Console.WriteLine("Invalid option, please try again.");
                        Additional.HoldScreen();
                        break;
                }

            } while (choice1 != "0");
        }

        // SuperAdmin -> Branch Menu
        public static void AdminBranchMenu()
        {
            Console.Clear();
            Console.WriteLine("Branch Admin Menu ");
            Console.WriteLine("1. Add New Branch ");
            Console.WriteLine("2. View All Branches ");
            Console.WriteLine("3. Update Branch ");
            Console.WriteLine("4. Delete Branch ");
            Console.WriteLine("5. Get Branch By ID ");
            Console.WriteLine("6. Get Branch Details By BranchName");
            Console.WriteLine("0. Exit ");
            string choice1 = Validation.StringValidation("Please select an option : ");
            switch (choice1)
            {
                case "1":
                    Branch.AddBranch();
                    break;
                case "2":
                    Branch.GetAllBranches();
                    break;
                case "3":
                    Branch.UpdateBranch();
                    break;
                case "4":
                    Branch.DeleteBranch();
                    break;
                case "5":
                    Branch.GetBranchById();
                    break;
                case "6":
                    Branch.GetBranchDetailsByBranchName();
                    break;
                case "0":
                   
                    Console.WriteLine("Exiting Branch Admin Menu.");
                    Additional.HoldScreen();
                    SuperAdminMenu();
                    break;
                default:
                    Console.WriteLine("Invalid option, please try again.");
                    Additional.HoldScreen();
                    break;


            }

        }

        // SuperAdmin -> Department Menu
        public static void AdminDepartmentMenu()
        {
            string choice1;
            do
            {
                Console.Clear();
                Console.WriteLine("Department Menu ");
                Console.WriteLine("1. Add New Department ");
                Console.WriteLine("2. View All Departments ");
                Console.WriteLine("3. Update Department ");
                Console.WriteLine("4. Delete Department ");
                Console.WriteLine("0. Back ");
                choice1 = Validation.StringValidation("Please select an option : ");

                switch (choice1)
                {
                    case "1":
                        Console.WriteLine("Adding New Department...");
                        Console.WriteLine("All Branches:");
                        Branch.ViewAllBranch();
                        Console.WriteLine("----------------------------------");
                        int branchId = Validation.IntValidation("Please select a branch to add the department to:");
                        string departmentName = Validation.StringValidation("Enter Department Name:");
                        Department.CreateDepartment(departmentName, branchId);
                        break;

                    case "2":
                        Department.GetAllDepartments();
                        break;

                    case "3":
                        Console.WriteLine("Update Department ");
                        Department.ViewAllDepartments();
                        Console.WriteLine("-----------------------------");
                        int departmentId = Validation.IntValidation("Enter Department ID to update:");
                        if (Department.DepartmentExists(departmentId))
                        {
                            if (Department.IsDepartmentActive(departmentId))
                            {
                                string newDepartmentName = Validation.StringValidation("Enter New Department Name:");
                                Department.UpdateDepartment(departmentId, newDepartmentName);

                            }
                            else
                            {
                                Console.WriteLine("Department is inactive and cannot be updated.");
                                Additional.HoldScreen();
                            }
                        }
                        else
                        {
                            Console.WriteLine("Department not found.");
                            Additional.HoldScreen();
                        }
                        break;

                    case "4":
                        Console.WriteLine("Delete Department ");
                        Department.ViewAllDepartments();
                        Console.WriteLine("-----------------------------");

                        int departmentId1 = Validation.IntValidation("Enter Department ID to delete:");
                        if (Department.DepartmentExists(departmentId1))
                        {
                            Department.DeleteDepartment(departmentId1);
                        }
                        else
                        {
                            Console.WriteLine("Department not found.");
                            Additional.HoldScreen();
                        }
                        break;

                    case "0":
                        Console.WriteLine("Exiting Department Menu.");
                        Additional.HoldScreen();
                        SuperAdminMenu();
                        break;
                        //return; // Back to SuperAdminMenu
                    default:
                        Console.WriteLine("Invalid option, please try again.");
                        Additional.HoldScreen();
                        break;
                }

            } while (choice1 != "0");
        }

      


        public void PrintDoctorDetails()
        {
            Console.WriteLine($"Doctor ID : {UserId}");
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
            Console.WriteLine(new string('-', 40));
            // Get doctor details from user
            // Name
            string name;
            do
            {
                name = Validation.StringNamingValidation("doctor name");
                if (string.IsNullOrWhiteSpace(name))
                {
                    Console.WriteLine("Invalid name. Please try again.");
                }
            } while (string.IsNullOrWhiteSpace(name));

            // Email
            string email;
            do
            {
                email = Validation.EmailValidation("doctor email");
                if (string.IsNullOrWhiteSpace(email))
                {
                    Console.WriteLine("Invalid email. Please try again.");
                }
            } while (string.IsNullOrWhiteSpace(email));

            // Phone Number
            int phoneNumber;
            do
            {
                phoneNumber = Validation.UserPhoneNumberValidation();
                if (phoneNumber.ToString().Length < 8) // Replace with your own rule
                {
                    Console.WriteLine("Invalid phone number. Please try again.");
                }
            } while (phoneNumber.ToString().Length < 8);

            // National ID
            string nationalId;
            do
            {
                nationalId = Validation.UserNationalIdValidation();
                if (string.IsNullOrWhiteSpace(nationalId))
                {
                    Console.WriteLine("Invalid National ID. Please try again.");
                }
            } while (string.IsNullOrWhiteSpace(nationalId));

            // Password
            string password;
            do
            {
                password = Validation.ReadPassword("doctor password");
                if (string.IsNullOrWhiteSpace(password))
                {
                    Console.WriteLine("Password cannot be empty. Please try again.");
                }
            } while (string.IsNullOrWhiteSpace(password));

            // Hash the password after it is valid
            string HashUserPassword = Validation.HashPasswordPBKDF2(password);
            // Display all branches
            Branch.ViewAllBranch();
            int BranchId = Validation.IntValidation("doctor Branch ID :");
            // Check if the branch ID exists
            if (!Branch.BranchIdIsExeist(BranchId))
            {
                Console.WriteLine("Branch Not found !");
                Additional.HoldScreen();
                DoctorUserMenu();
                return;
            }

            //if (Validation.UserNationalIdValidation())
            //{
            //    Console.WriteLine("A doctor with this National ID already exists.");
            //    Additional.HoldScreen();
            //    return;
            //}

            string specialization = Validation.StringValidation("doctor specialization");
           

            // Create a new doctor instance
            Doctor doctor = new Doctor(name, email, specialization);
            doctor.UserName = name;
            doctor.P_UserPassword = HashUserPassword; // Assuming P_UserPassword is a property that handles password hashing
            doctor.UserEmail = email;
            doctor.UserPhoneNumber = phoneNumber;
            doctor.UserNationalID = nationalId;
            doctor.DoctorSpecialization = specialization;
            doctor.UserRole = "Doctor"; // Set the role to Doctor
            doctor.UserStatus = "Active"; // Set the status to Active
            doctor.BranchID = BranchId; // Set the branch ID for the doctor
            // Add the doctor to the List
            BranchDepartment.Doctors.Add(doctor);
            // Add Doctor UserNationalID to UserNationalID
            Hospital.UserNationalID.Add(nationalId);
            SaveDoctorsToFile();
            Console.WriteLine("Doctor added successfully.");
            Additional.HoldScreen();
            DoctorUserMenu();
        }

        // ViewDoctors method to display all doctors in the branch department
        public static void ViewDoctors()
        {
            Console.Clear();
            Console.WriteLine("View Doctors");
            Console.WriteLine(new string('-', 40));
            Console.WriteLine("List of Doctors");
            if (BranchDepartment.Doctors.Count == 0)
            {
                Console.WriteLine("No doctors available in the system.");
                Additional.HoldScreen();
                DoctorUserMenu();
                //return;
            }
            foreach (var doctor in BranchDepartment.Doctors)
            {
                Console.WriteLine($"Doctor ID       : {doctor.UserId}");
                Console.WriteLine($"Name            : {doctor.UserName}");
                Console.WriteLine($"Email           : {doctor.UserEmail}");
                Console.WriteLine($"Phone Number    : {doctor.UserPhoneNumber}");
                Console.WriteLine($"National ID     : {doctor.UserNationalID}");
                Console.WriteLine($"Specialization  : {doctor.DoctorSpecialization}");
                Console.WriteLine($"Status          : {doctor.UserStatus}");
                Console.WriteLine($"Branch ID       : {doctor.BranchID}");
                Console.WriteLine(new string('-', 40));

            }
            Additional.HoldScreen();

            DoctorUserMenu();
        }

        // Update Doctor method to update doctor details

        public static void UpdateDoctor()
        {
            Console.Clear();
            Console.WriteLine("Update Doctors");
            Console.WriteLine(new string('-', 40));
            ViewAllDoctors();
            Console.WriteLine(new string('-', 40));
            int doctorId = Validation.IntValidation("Enter the Doctor ID to update:");
            var doctorToUpdate = BranchDepartment.Doctors.FirstOrDefault(d => d.UserId == doctorId);
            if (doctorToUpdate == null)
            {
                Console.WriteLine("Doctor not found.");
                Additional.HoldScreen();
                DoctorUserMenu();
                //return;
            }


            // Choose what to update
            Console.WriteLine("What do you want to update?");
            // Method to display admin update options
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Update Doctor Information");
                Console.WriteLine("------------------------");
                Console.WriteLine("1. Update Email");
                Console.WriteLine("2. Update Phone Number");
                Console.WriteLine("3. Update Status");
                Console.WriteLine("0. Exit");

                char choice = Validation.CharValidation("Choose an option: ");
                
                switch (choice)
                {
                    case '1':
                        doctorToUpdate.UserEmail = Validation.EmailValidation("Enter new email: ");
                        break;
                    case '2':
                        doctorToUpdate.UserPhoneNumber = Validation.IntValidation("Enter new phone number: ");
                        break;
                    case '3':
                        doctorToUpdate.UserStatus = Validation.StringValidation("Enter new status (Active/Inactive): ");
                       // SetDoctorStatus();
                        break;
                    case '0':
                        Console.WriteLine("Exiting update menu...");
                        if (choice == 0)
                            break;
                        AdminUserMenu();
                        return;
                    default:
                        Console.WriteLine("Invalid option, please try again.");
                        break;
                }
                Console.WriteLine("Doctor details updated successfully.");
                Console.Write("Do you want to update anything else? (y/n): ");
                string continueChoice = Console.ReadLine().ToLower();

                if (continueChoice != "y")
                {
                    Console.WriteLine("Returning to Admin Menu...");
                    DoctorUserMenu();
                    return;
                }
               
                DoctorUserMenu();
            }
        }

        // Delete Doctor method to delete a doctor by ID
        public static void DeleteDoctor()
        {
            Console.Clear();
            Console.WriteLine("Delete Doctors");
            Console.WriteLine(new string('-', 40));
            ViewAllDoctors();
            Console.WriteLine(new string('-', 40));
            int doctorId = Validation.IntValidation("Doctor ID");
            var doctorToDelete = BranchDepartment.Doctors.FirstOrDefault(d => d.UserId == doctorId);
            if (doctorToDelete == null)
            {
                Console.WriteLine("Doctor not found.");
                Additional.HoldScreen();
                DoctorUserMenu();
                //return;
            }
            // Confirm deletion
            if (Additional.ConfirmAction("delete this doctor"))
            {

                BranchDepartment.Doctors.Remove(doctorToDelete);
                // delete UserNationalID from UserNationalID list
                Hospital.UserNationalID.Remove(doctorToDelete.UserNationalID);
                Console.WriteLine("Doctor deleted successfully.");
                DoctorUserMenu();
                Additional.HoldScreen();
            }
            else
            {
                Console.WriteLine("Deletion cancelled.");
                DoctorUserMenu();
                Additional.HoldScreen();

            }
               
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

        // save the doctors to file
        public static void SaveDoctorsToFile()
        {
            using (StreamWriter writer = new StreamWriter(DoctorsFilePath))
            {
                foreach (var doctor in BranchDepartment.Doctors)
                {
                    writer.WriteLine($"{doctor.UserId}|{doctor.UserName}|{doctor.P_UserPassword}|{doctor.UserEmail}|{doctor.UserPhoneNumber}|{doctor.UserNationalID}|{doctor.DoctorSpecialization}|{doctor.UserRole}|{doctor.UserStatus}|{doctor.BranchID}");
                }
            }
            Console.WriteLine("Doctor data saved successfully.");
        }

        // Load Doctors from file
        public static void LoadDoctorsFromFile()
        {
            if (File.Exists(DoctorsFilePath))
            {
                using (StreamReader reader = new StreamReader(DoctorsFilePath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        var parts = line.Split('|');
                        if (parts.Length == 10)
                        {
                            Doctor doctor = new Doctor(parts[1], parts[2], parts[5]);
                            doctor.UserId = int.Parse(parts[0]);
                            doctor.UserName = parts[1];
                            doctor.P_UserPassword = parts[2]; // Assuming P_UserPassword is a property that handles password hashing
                            doctor.UserEmail = parts[3];
                            doctor.UserPhoneNumber = int.Parse(parts[4]);
                            doctor.UserNationalID = parts[5];
                            doctor.DoctorSpecialization = parts[6];
                            doctor.UserRole = parts[7];
                            doctor.UserStatus = parts[8];
                            doctor.BranchID = int.Parse(parts[9]);
                            BranchDepartment.Doctors.Add(doctor);
                        }
                    }
                }
                Console.WriteLine("Doctor data loaded successfully.");
            }
            else
            {
                Console.WriteLine("No doctor data fount!");
            }
        }

        // ADMIN Method

        // Add Admin method to add a new admin
        public static void AddAdmin()
        {
            Console.Clear();
            Console.WriteLine("Add New Admin");
            // Get admin details from user
            //string name = Validation.StringValidation("admin name");
            //string email = Validation.EmailValidation("admin email");
            //int phoneNumber = Validation.UserPhoneNumberValidation();
            //string nationalId = Validation.UserNationalIdValidation();
            //string password = Validation.ReadPassword("admin password");
            //// Validate the password
            //string HashUserPassword = Validation.HashPasswordPBKDF2(password);

            string name;
            do
            {
                name = Validation.StringNamingValidation("admin name");
                if (string.IsNullOrWhiteSpace(name))
                {
                    Console.WriteLine("Invalid name. Please try again.");
                }
            } while (string.IsNullOrWhiteSpace(name));

            // Email validation
            string email;
            do
            {
                email = Validation.EmailValidation("admin email");
                if (string.IsNullOrWhiteSpace(email))
                {
                    Console.WriteLine("Invalid email. Please try again.");
                }
            } while (string.IsNullOrWhiteSpace(email));

            // Phone number validation
            int phoneNumber;
            do
            {
                phoneNumber = Validation.UserPhoneNumberValidation();
                if (phoneNumber.ToString().Length < 8) // or whatever rule you have
                {
                    Console.WriteLine("Invalid phone number. Please try again.");
                }
            } while (phoneNumber.ToString().Length < 8);

            // National ID validation
            string nationalId;
            do
            {
                nationalId = Validation.UserNationalIdValidation();
                if (string.IsNullOrWhiteSpace(nationalId))
                {
                    Console.WriteLine("Invalid National ID. Please try again.");
                }
            } while (string.IsNullOrWhiteSpace(nationalId));

            // Password and hashing
            string password;
            do
            {
                password = Validation.ReadPassword("admin password");
                if (string.IsNullOrWhiteSpace(password))
                {
                    Console.WriteLine("Password cannot be empty. Please try again.");
                }
            } while (string.IsNullOrWhiteSpace(password));

            string HashUserPassword = Validation.HashPasswordPBKDF2(password);
            //Branch.ViewAllBranch();
            //int branchId = Validation.IntValidation("admin Branch ID :");
            //// chech if branch id is available or not 
            //if (!Branch.BranchIdIsExeist(branchId))
            //{
            //    Console.WriteLine("Branch Not found !");
            //    Additional.HoldScreen();
            //}

            int branchId;
            do
            {
                Branch.ViewAllBranch();
                branchId = Validation.IntValidation("admin Branch ID :");

                if (!Branch.BranchIdIsExeist(branchId))
                {
                    Console.WriteLine("Branch not found! Please try again.");
                    Additional.HoldScreen();
                    Console.Clear();
                }

            } while (!Branch.BranchIdIsExeist(branchId));

            // Check if the national ID already exists
            //if (Validation.UserNationalIdExists(nationalId))
            //{
            //    Console.WriteLine("An admin with this National ID already exists.");
            //    Additional.HoldScreen();
            //    return;
            //}
            // Create a new admin instance
            Admin admin = new Admin(name, email, 0);
            admin.UserName = name;
            admin.P_UserPassword = HashUserPassword;
            admin.UserEmail = email;
            admin.UserPhoneNumber = phoneNumber;
            admin.UserNationalID = nationalId;
            admin.UserRole = "Admin"; // Set the role to Admin
            admin.UserStatus = "Active"; // Set the status to Active
            admin.BranchID = branchId;
            // Add the admin to the List
            BranchDepartment.Admins.Add(admin);
            // Add Admin UserNationalID to UserNationalID
            Hospital.UserNationalID.Add(nationalId);
            SaveAdminsToFile();
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
                AdminUserMenu();
                //return;
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
                Console.WriteLine("3. Update Status");
                Console.WriteLine("0. Exit");

                char choice = Validation.CharValidation("Choose an option: ");

                switch (choice)
                {
                    case '1':
                        adminToUpdate.UserEmail = Validation.EmailValidation("Enter new email: ");
                        break;
                    case '2':
                        adminToUpdate.UserPhoneNumber = Validation.UserPhoneNumberValidation();
                        break;
                    case '3':
                        //adminToUpdate.UserStatus = Validation.StringValidation("Enter new status (Active/Inactive): ");
                        //if (adminToUpdate.UserStatus.ToLower() != "active" && adminToUpdate.UserStatus.ToLower() != "inactive")
                        //{
                        //    Console.WriteLine("Invalid status. Please enter 'Active' or 'Inactive'.");
                        //    // do while loop to ask for status again
                        //    while (true)
                        //    {
                        //        adminToUpdate.UserStatus = Validation.StringValidation("Enter new status (Active/Inactive): ");
                        //        if (adminToUpdate.UserStatus.ToLower() == "active" || adminToUpdate.UserStatus.ToLower() == "inactive")
                        //            break; // Valid status, exit the loop
                        //        Console.WriteLine("Invalid status. Please enter 'Active' or 'Inactive'.");
                        //    }
                        //    continue; // Go back to the menu
                        //}
                        //}
                        
                        do
                        {
                            adminToUpdate.UserStatus = Validation.StringValidation("Enter new status (Active/Inactive): ");
                            if (adminToUpdate.UserStatus.ToLower() == "active" || adminToUpdate.UserStatus.ToLower() == "inactive")
                                break; // Valid status, exit the loop
                            Console.WriteLine("Invalid status. Please enter 'Active' or 'Inactive'.");
                        } while (true);

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
                AdminUserMenu();
                //return;
            }
            // Confirm deletion
            if (Additional.ConfirmAction("delete this admin"))
            {
                BranchDepartment.Admins.Remove(adminToDelete);
                // delete UserNationalID from UserNationalID list
                Hospital.UserNationalID.Remove(adminToDelete.UserNationalID);
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
                AdminUserMenu();
                //return;
            }
            foreach (var admin in BranchDepartment.Admins)
            {
                Console.WriteLine($"Admin ID        : {admin.UserId}");
                Console.WriteLine($"Name            : {admin.UserName}");
                Console.WriteLine($"Email           : {admin.UserEmail}");
                Console.WriteLine($"Phone Number    : {admin.UserPhoneNumber}");
                Console.WriteLine($"National ID     : {admin.UserNationalID}");
                Console.WriteLine($"Status          : {admin.UserStatus}");
                Console.WriteLine($"Branch ID       : {admin.BranchID}");
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
                AdminUserMenu();

                //return;
            }
            foreach (var admin in BranchDepartment.Admins)
            {
                Console.WriteLine($"Admin ID        : {admin.UserId}");
                Console.WriteLine($"Name            : {admin.UserName}");
                Console.WriteLine($"Status          : {admin.UserStatus}");
                Console.WriteLine($"Branch ID       : {admin.BranchID}");

                Console.WriteLine(new string('-', 40));
            }

        }

        // save admins to file
        public static void SaveAdminsToFile()
        {
            using (StreamWriter writer = new StreamWriter(AdminsFilePath))
            {
                foreach (var admin in BranchDepartment.Admins)
                {
                    writer.WriteLine($"{admin.UserId}|{admin.UserName}|{admin.P_UserPassword}|{admin.UserEmail}|{admin.UserPhoneNumber}|{admin.UserNationalID}|{admin.UserRole}|{admin.UserStatus}|{admin.BranchID}");
                }
            }
            Console.WriteLine("Admin data saved successfully.");
        }

        // Load Admins from file
        public static void LoadAdminsFromFile()
        {
            if (File.Exists(AdminsFilePath))
            {
                using (StreamReader reader = new StreamReader(AdminsFilePath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        var parts = line.Split('|');
                        if (parts.Length == 9)
                        {
                            Admin admin = new Admin(parts[1], parts[2], int.Parse(parts[8]));
                            admin.UserId = int.Parse(parts[0]);
                            admin.UserName = parts[1];
                            admin.P_UserPassword = parts[2];
                            admin.UserEmail = parts[3];
                            admin.UserPhoneNumber = int.Parse(parts[4]);
                            admin.UserNationalID = parts[5];
                            admin.UserRole = parts[6];
                            admin.UserStatus = parts[7];
                            admin.BranchID = int.Parse(parts[8]);
                            BranchDepartment.Admins.Add(admin);
                        }
                    }
                }
                Console.WriteLine("Admin data loaded successfully.");
                //foreach (var admin in BranchDepartment.Admins)
                //{
                //    Console.WriteLine($"Super Admin ID: {admin.UserId}, Name: {admin.UserName},Password: {admin.P_UserPassword}, Email: {admin.UserEmail}, Phone: {admin.UserPhoneNumber}, National ID: {admin.UserNationalID}, Role: {admin.UserRole}, Status: {admin.UserStatus}|{admin.BranchID}");
                //}
            }
            else
            {
                Console.WriteLine("No admin data fount!");
            }
        }


        // BRANCH Methods ...






        // Additional Method 


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

        //====================================================
        //4. class constructor ...

        public SuperAdmin(string username, string password, string email)
        {

            UserRole = "SuperAdmin";
            UserStatus = "Active"; //default status for SuperAdmin  
        }


        // view all Doctor
        public static void ViewAllDoctors()
        {
            Console.WriteLine("List of All Doctors");
            if (BranchDepartment.Doctors.Count == 0)
            {
                Console.WriteLine("No doctors available in the system.");
                Additional.HoldScreen();
                DoctorUserMenu();
                //return;
            }

            foreach (var doctor in BranchDepartment.Doctors)
            {
                Console.WriteLine($"Doctor ID       : {doctor.UserId}");
                Console.WriteLine($"Name            : {doctor.UserName}");
                Console.WriteLine($"Specialization  : {doctor.DoctorSpecialization}");
                Console.WriteLine($"Status          : {doctor.UserStatus}");
                Console.WriteLine($"Branch ID       : {doctor.BranchID}");
                Console.WriteLine(new string('-', 40));
            }
           
        }


        // Save SuperAdmin to file
        public static void SaveSuperAdminToFile()
        {
            using (StreamWriter writer = new StreamWriter(SuperAdminFilePath))
            {
                foreach (var superAdmin in Hospital.SuperAdmins)
                {
                    writer.WriteLine($"{superAdmin.UserId}|{superAdmin.UserName}|{superAdmin.P_UserPassword}|{superAdmin.UserEmail}|{superAdmin.UserPhoneNumber}|{superAdmin.UserRole}|{superAdmin.UserNationalID}|{superAdmin.UserStatus}");
                }
            }
            Console.WriteLine("Super admin data saved successfully.");
        }
        // Load SuperAdmin from file
        public static void LoadSuperAdminFromFile()
        {
            if (File.Exists(SuperAdminFilePath))
            {
                using (StreamReader reader = new StreamReader(SuperAdminFilePath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        var parts = line.Split('|');
                        if (parts.Length == 8)
                        {
                            SuperAdmin superAdmin = new SuperAdmin(parts[1], parts[2], parts[3]);
                            superAdmin.UserId = int.Parse(parts[0]);
                            superAdmin.UserName = parts[1];
                            superAdmin.P_UserPassword = parts[2]; // Assuming P_UserPassword is a property that handles password hashing
                            superAdmin.UserEmail = parts[3];
                            superAdmin.UserPhoneNumber = int.Parse(parts[4]);
                            superAdmin.UserRole = parts[5];
                            superAdmin.UserNationalID = parts[6];
                            superAdmin.UserStatus = parts[7];
                            Hospital.SuperAdmins.Add(superAdmin);
                        }
                    }
                }
                Console.WriteLine("Super admin data loaded successfully.");
                //foreach (var superAdmin in Hospital.SuperAdmins)
                //{
                //    Console.WriteLine($"Super Admin ID: {superAdmin.UserId}, Name: {superAdmin.UserName},Password: {superAdmin.P_UserPassword}, Email: {superAdmin.UserEmail}, Phone: {superAdmin.UserPhoneNumber}, National ID: {superAdmin.UserNationalID}, Role: {superAdmin.UserRole}, Status: {superAdmin.UserStatus}");
                //}
            }
            else
            {
                Console.WriteLine("No super admin data fount!");
            }
        }



    }
}