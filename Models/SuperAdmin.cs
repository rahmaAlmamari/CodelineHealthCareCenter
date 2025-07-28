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
                    Console.WriteLine("Add New Doctor");



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
                    Console.WriteLine("Viewing doctors...");
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

       
        public void AddDoctor(string username, string password, string email, string specialization)
        {

            //to get national id and check if it exists or not ...
            string UserNationalID = Validation.StringValidation("national ID");
            //to check if the national id exists or not ...
            if (NationalIdExists(UserNationalID))
            {
                Console.WriteLine("This national ID already exists. Please try again with a different one.");
                Additional.HoldScreen();
                return; //exit the method if national ID exists ...
            }
            //to get the user data ...
            string UserName = Validation.StringValidation("user name");
            string UserPassword = Validation.ReadPassword("password");
            string UserEmail = Validation.EmailValidation("email");
            int P_UserPhoneNumber = Validation.IntValidation("phone number");
            string UserCity = Validation.StringValidation("city");


            //UserRole and UserStatus are set to default values for SuperAdmin in SuperAdmin constructor ...
            //to do hashing for the password ...
            string UserPasswordHashed = Validation.HashPasswordPBKDF2(UserPassword);


            Console.Write("Enter Specialization: ");
            string spec = Validation.StringValidation("specialization");
            // Create a new doctor instance
            Doctor newDoctor = new Doctor(username, email, password, 0, 0, specialization);

            // Add the new doctor to the list
            Clinic.Doctors.Add(newDoctor);
            Console.WriteLine($"Doctor {username} added successfully with specialization: {specialization}");
            // Confirm the action



            Additional.ConfirmAction("add a new doctor");
            Additional.HoldScreen();
        }

        //to check if the national id exists or not ...

        public bool NationalIdExists(string nationalId)
        {
            //to check if the national id exists or not in SuperAdmin list hospital class ...

            foreach (var superAdmin in Hospital.SuperAdmins)
            {
                if (superAdmin.UserNationalID == nationalId)
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
            UserName = username;
            P_UserPassword = password;
            UserEmail = email;

            UserRole = "SuperAdmin";
            UserStatus = "Active"; //default status for SuperAdmin  
        }

       
    }
}