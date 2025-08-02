using CodelineHealthCareCenter.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace CodelineHealthCareCenter.Models
{
    class User : IUserService, IAuthService
    {
        //1. class feilds ...
        public int UserId;
        public string UserName;
        private string UserPassword;
        public string UserEmail;
        public int UserPhoneNumber;
        public string UserRole;
        public string UserNationalID;
        public string UserStatus;
        public static int UserCount = 0;

        //====================================================
        //2. class properity ...
        //UserPhoneNumber proprity ...
        //public int P_UserPhoneNumber
        //{
        //    get { return UserPhoneNumber; }
        //    set
        //    {
        //        bool FalgError = false; //to handle the error ...
        //        do
        //        {
        //            FalgError = false; //to reset the error flag ...
        //            //to check if the phone number is 8 digits or not ...
        //            if (value.ToString().Length == 8)
        //            {
        //                UserPhoneNumber = value;
        //            }
        //            else
        //            {
        //                Console.WriteLine("Phone number must be 8 digits.");
        //                value = Validation.IntValidation("user phone number");
        //                FalgError = true; //to handle the error ...
        //            }

        //        } while (FalgError);
        //        UserPhoneNumber = value;
        //    }
        //}
        //UserPassword proprity ...
        public string P_UserPassword
        {
            get { return UserPassword; }
            set
            {
                //to do hashing for the password ...
                UserPassword = value;
            }
        }
        //UserNationalId properity 
        //public string P_UserNationalID
        //{
        //    get { return UserNationalID; }
        //    set
        //    {
        //        bool flagError;
        //        //string newId = value;

        //        do
        //        {
        //            flagError = false;
        //            if (Validation.UserNationalIdExists(value))
        //            {
        //                Console.WriteLine("This national ID already exists. Please try again with a different one.");
        //                Additional.HoldScreen();
        //                value = Validation.StringValidation("national ID");
        //                flagError = true;
        //            }
        //        } while (flagError);

        //        UserNationalID = value; 
        //    }
        //}


        //====================================================
        //3. class method ...
        //to login to the system ...
        public void Login()
        {
            string userNationalID = Validation.StringValidation("national ID");
            string userPassword = Validation.ReadPassword("Password");

            try
            {
                // 1. Check Patient login ...
                foreach (var branch in Hospital.Branches)
                {
                    foreach (var patient in branch.Patients)
                    {
                        if (patient.UserNationalID == userNationalID &&
                            Validation.VerifyPasswordPBKDF2(userPassword, patient.P_UserPassword) &&
                            patient.UserStatus == "Active")
                        {
                            Console.WriteLine($"\nWelcome, Patient {patient.UserName}!");
                            Patient.PatientMenu();
                            return;
                        }
                    }
                }

                // 2. Check Doctor login ...
                foreach (var doctor in BranchDepartment.Doctors)
                {
                    if (doctor.UserNationalID == userNationalID &&
                        Validation.VerifyPasswordPBKDF2(userPassword, doctor.P_UserPassword) &&
                        doctor.UserStatus == "Active")
                    {
                        Console.WriteLine($"\nWelcome, Dr. {doctor.UserName}!");
                        Doctor.DoctorMenu(); // Replace with your actual doctor menu ...
                        return;
                    }
                }

                // 3. Check Admin login ...
                foreach (var admin in BranchDepartment.Admins)
                {
                    if (admin.UserNationalID == userNationalID &&
                        Validation.VerifyPasswordPBKDF2(userPassword, admin.P_UserPassword) &&
                        admin.UserStatus == "Active")
                    {
                        Console.WriteLine($"\nWelcome, Admin {admin.UserName}!");
                        Admin.AdminMenu(); // Replace with your actual admin menu ...
                        return;
                    }
                }

                // 4. Check Super Admin login ...
                foreach (var superAdmin in Hospital.SuperAdmins)
                {
                    if (superAdmin.UserNationalID == userNationalID &&
                        Validation.VerifyPasswordPBKDF2(userPassword, superAdmin.P_UserPassword) &&
                        superAdmin.UserStatus == "Active")
                    {
                        Console.WriteLine($"\nWelcome, Super Admin {superAdmin.UserName}!");
                        Additional.HoldScreen();
                        SuperAdmin.SuperAdminMenu(); // Replace with your actual super admin menu ... 
                        return;
                    }
                    else
                    {
                        Console.WriteLine("\nInvalid login credentials. Please try again.");
                        Additional.HoldScreen();
                    }
                }
            }
            catch (Exception ex)
            {
                //if no match found
                Console.WriteLine("\nInvalid login credentials. Please try again.");
                Additional.HoldScreen();
            }


            
        }

        //====================================================
        //4. class constructor ...
        public User()
        {
            UserCount++;
            UserId = UserCount;//to make user id unique ...
        }


    }
}
