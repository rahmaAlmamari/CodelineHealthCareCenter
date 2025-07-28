using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodelineHealthCareCenter.Models
{
    class Patient : User
    {
        //1. class feilds ...

        public string PatientCity;
        public List<Booking> PatientAppointments = new List<Booking>();
        public List<PatientRecord> PatientRecords = new List<PatientRecord>();

        //====================================================
        //2. class properity ...

        //====================================================
        //3. class method ...

        public static void PatientMenu()
        {
            Console.WriteLine("Welcome to Patient Menu");
            Console.WriteLine("1. Book Appointment");
            Console.WriteLine("2. View Appointments");
            Console.WriteLine("3. View Patient Records");
            Console.WriteLine("4. Exit");
        }
        //to singUp new patient ...
        public static void SinUp()
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
            //UserRole and UserStatus are set to default values for patient in patient constructor ...
            //to do hashing for the password ...
            string UserPasswordHashed = Validation.HashPasswordPBKDF2(UserPassword);
            //to find the branch that the patient wants to register in using city ...
            Branch branch = FindBranchByCity(UserCity);
            //to call the AddPatient method to create a new patient ...
            AddPatient(UserName, UserPasswordHashed, UserEmail, P_UserPhoneNumber, UserNationalID, UserCity, branch);

        }
        //to check if the national id exists or not ...
        public static bool NationalIdExists(string nationalId)
        {
            //to check if the national id exists or not in patient list in branch list in hospital class ...
            foreach (var branch in Hospital.Branches)
            {
                foreach (var patient in branch.Patients)
                {
                    if (patient.UserNationalID == nationalId)
                    {
                        return true; //if national id exists ...
                    }
                }
            }
            return false; //if national id does not exist ...

        }
        //to find the branch by city ...
        public static Branch FindBranchByCity(string city)
        {
            //to loop through branch list ...
            foreach (var branch in Hospital.Branches)
            {
                if (branch.BranchCity == city)
                {
                    return branch; //if branch found ...
                }
            }
            Console.WriteLine("Branch not found in this city.");
            Additional.HoldScreen();
            return null; //if branch not found ...
        }
        //to add a new patient to the branch list in hospital class ...
        public static void AddPatient(string username, string password, string email, int phoneNumber, string userNationalID, string city, Branch PationtBranch)
        {
            //to create a new patient object ...
            Patient newPatient = new Patient
            {
                UserName = username,
                P_UserPassword = password,
                UserEmail = email,
                P_UserPhoneNumber = phoneNumber,
                UserNationalID = userNationalID,
                PatientCity = city,
            };
            //to add the new patient to the branch list in hospital class ...
            if (PationtBranch != null)
            {
                PationtBranch.Patients.Add(newPatient);
                UserCount++; //to increase the user count ...
                Console.WriteLine("Patient added successfully.");
                Additional.HoldScreen();//just to hold the screen ...
            }
            else
            {
                Console.WriteLine("Failed to add patient. Branch not found.");
                Additional.HoldScreen();//just to hold the screen ...
            }

        }
        //====================================================
        //4. class constructor ...
        public Patient()
        {
            //to set the default values for the patient ...
            UserRole = "Patient"; //default role for patient ...
            UserStatus = "Active"; //default status for patient ...
        }
    }
}
