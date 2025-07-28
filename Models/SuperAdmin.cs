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
                    AddBranch();
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

        // Add Doctor method to add a new doctor
        //public static void AddDoctor()
        //{
        //    Console.Clear();
        //    Console.WriteLine("Add New Doctor");
        //    // Get doctor details from user
        //    string name = Validation.StringValidation("doctor name");
        //    string email = Validation.EmailValidation("doctor email");
        //    int phoneNumber = Validation.IntValidation("doctor phone number");
        //    string nationalId = Validation.StringValidation("doctor national ID");
        //    string specialization = Validation.StringValidation("doctor specialization");
        //    // View departments and get the department ID from user
        //    Console.WriteLine("Available Departments:");
        //    //Department.ViewDepartments(); // Assuming this method displays available departments
            
        //    int departmentId = Validation.IntValidation("Enter the Department ID for the doctor:");

        //    // Check if the department ID is valid
        //    if (!BranchDepartment.Departments.Any(d => d.DepartmentId == departmentId))
        //    {
        //        Console.WriteLine("Invalid Department ID. Please try again.");
        //        Additional.HoldScreen();
        //        return;
        //    }

        //    // view Branches and get the branch ID from user
        //    Console.WriteLine("Available Branches:");
        //    //BranchDepartment.ViewBranches(); // Assuming this method displays available branches
        //    int branchId = Validation.IntValidation("Enter the Branch ID for the doctor:");

        //    // Check if the branch ID is valid
        //    if (!BranchDepartment.Branches.Any(b => b.BranchId == branchId))
        //    {
        //        Console.WriteLine("Invalid Branch ID. Please try again.");
        //        Additional.HoldScreen();
        //        return;
        //    }



        //    // Check if the national ID already exists
        //    if (NationalIdExists(nationalId))
        //    {
        //        Console.WriteLine("A doctor with this National ID already exists.");
        //        Additional.HoldScreen();
        //        return;
        //    }
        //    // Create a new doctor instance
        //    Doctor newDoctor = new Doctor();
        //    newDoctor.UserName = name;
        //    newDoctor.UserEmail = email;
        //    newDoctor.P_UserPhoneNumber = phoneNumber;
        //    newDoctor.UserNationalID = nationalId;
        //    newDoctor.DoctorSpecialization = specialization;
        //    newDoctor.UserRole = "Doctor"; // Set the role to Doctor
        //    newDoctor.UserStatus = "Active"; // Set the status to Active

            
            
        //    BranchDepartment.Doctors.Add(newDoctor);
        //    Console.WriteLine("Doctor added successfully.");
        //    Additional.HoldScreen();
        //}


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

        //===================

        // Add Branch method to add a new branch
        public static void AddBranch()
        {
            Console.Clear();
            Console.WriteLine("Add New Branch");
            // Get branch details from user
            string branchName = Validation.StringValidation("branch name");
            string branchCity = Validation.StringValidation("branch city");
            DateOnly branchEstablishDate = Validation.DateOnlyValidation("branch establish date");
            int hospitalId = Validation.IntValidation("hospital ID");

            // Add branch to the hospital
            Branch.AddBranch(branchName, branchCity, branchEstablishDate, hospitalId); // Call the static method to add the branch

            Console.WriteLine("Branch added successfully.");
            Additional.HoldScreen();
        }

        // Get All Branches method to display all branches in the hospital
        public static void GetAllBranches()
        {
            Console.Clear();
            Console.WriteLine("List of Branches");
            if (Hospital.Branches.Count == 0)
            {
                Console.WriteLine("No branches available in the system.");
                Additional.HoldScreen();
                return;
            }
            foreach (var branch in Hospital.Branches)
            {
                Console.WriteLine($"Branch ID       : {branch.BranchId}");
                Console.WriteLine($"Branch Name     : {branch.BranchName}");
                Console.WriteLine($"Branch City     : {branch.BranchCity}");
                Console.WriteLine($"Establish Date  : {branch.BranchEstablishDate}");
                Console.WriteLine($"Status          : {(branch.BranchStatus ? "Open" : "Closed")}");
                Console.WriteLine(new string('-', 40));
            }
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