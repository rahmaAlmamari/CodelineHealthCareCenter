using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CodelineHealthCareCenter.Models
{
    class Validation
    {
        //1. CharValidation method ...
        public static char CharValidation(string message)
        {
            bool CharFlag;//to handle user char error input ...
            char CharInput = '0';
            do
            {
                try
                {
                    CharFlag = false;
                    Console.WriteLine($"Enter your {message}:");
                    CharInput = char.Parse(Console.ReadLine());
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Your {message} not accepted due to: " + e.Message);
                    CharFlag = true;
                }

            } while (CharFlag);

            //to return tne char input ...
            return CharInput;
        }
        //2. StringNamingValidation method ...
        public static string StringNamingValidation(string message)
        {
            bool StringNamingFlag;//to handle user StringNaming error input ...
            string StringNamingInput = "null";
            do
            {
                StringNamingFlag = false;
                Console.WriteLine($"Enter your {message}:");
                StringNamingInput = Console.ReadLine();
                //to check if StringNamingInput has number or not ...
                bool check_StringNaming = Additional.IsAlpha(StringNamingInput);
                if (check_StringNaming == false)
                {
                    Console.WriteLine($"{message} can not contains number and con not be null ..." +
                                      "please prass enter key to try again");
                    Additional.HoldScreen();//just to hold a second ...
                    StringNamingFlag = true;
                }

            } while (StringNamingFlag);

            //to return tne char input ...
            return StringNamingInput;
        }
        //3. StringValidation method ...
        public static string StringValidation(string message)
        {
            bool StringFlag;//to handle user StringNaming error input ...
            string StringInput = "null";
            do
            {
                StringFlag = false;
                Console.WriteLine($"Enter your {message}:");
                StringInput = Console.ReadLine();
                // Check if StringInput null or empty
                if (string.IsNullOrWhiteSpace(StringInput))
                {
                    Console.WriteLine($"{message} cannot be empty.");
                    Additional.HoldScreen();//just to hoad second ...
                    StringFlag = true;
                }

            } while (StringFlag);

            //to return tne char input ...
            return StringInput;
        }
        //4. DoubleValidation method ...
        public static double DoubleValidation(string message)
        {
            bool DoubleFlag;//to handle user StringNaming error input ...
            double DoubleInput = 0;
            do
            {
                DoubleFlag = false;
                try
                {
                    Console.WriteLine($"Enter your {message}:");
                    DoubleInput = double.Parse(Console.ReadLine());
                }
                catch (Exception e)
                {
                    Console.WriteLine($"{message} not accepted due to " + e.Message);
                    Console.WriteLine("please prass enter key to try again");
                    Additional.HoldScreen();//just to hold a second ...
                    DoubleFlag = true;
                }

            } while (DoubleFlag);
            //to return tne char input ...
            return DoubleInput;
        }
        //5. IntValidation method ...
        public static int IntValidation(string message)
        {
            bool IntFlag;//to handle user StringNaming error input ...
            int IntInput = 0;
            do
            {
                IntFlag = false;
                try
                {
                    Console.WriteLine($"Enter {message}:");
                    IntInput = int.Parse(Console.ReadLine());
                }
                catch (Exception e)
                {
                    Console.WriteLine($"{message} not accepted due to " + e.Message);
                    Additional.HoldScreen();//just to hold a second ...
                    IntFlag = true;
                }

            } while (IntFlag);
            //to return tne char input ...
            return IntInput;
        }
        //6. DateTimeValidation method ...
        public static DateTime DateTimeValidation(string message)
        {
            bool DateTimeFlag; // to handle user DateTime error input
            DateTime DateTimeInput = DateTime.Now;

            do
            {
                DateTimeFlag = false;
                try
                {
                    Console.WriteLine($"Enter your {message} (format: MM/dd/yyyy):");
                    DateTimeInput = DateTime.Parse(Console.ReadLine());

                    //// Check if the date is in the future or today
                    //if (DateTimeInput.Date > DateTime.Now.Date)
                    //{
                    //    Console.WriteLine($"{message} should be a date valid.");
                    //    HoldScreen(); // just to hold a second
                    //    DateTimeFlag = true; // ask user again
                    //}
                }
                catch (Exception e)
                {
                    Console.WriteLine($"{message} not accepted due to: " + e.Message);
                    Additional.HoldScreen(); // just to hold a second
                    DateTimeFlag = true; // ask user again
                }
            } while (DateTimeFlag);

            return DateTimeInput; // Return the validated input
        }
        //7. DateOnlyValidation method ...
        public static DateOnly DateOnlyValidation(string message)
        {
            bool DateTimeFlag; // to handle user DateTime error input
            DateOnly DateTimeInput = DateOnly.FromDateTime(DateTime.Now);

            do
            {
                DateTimeFlag = false;
                try
                {
                    Console.WriteLine($"Enter your {message} (format: MM/dd/yyyy):");
                    DateTimeInput = DateOnly.Parse(Console.ReadLine());

                    //// Check if the date is in the future or today
                    //if (DateTimeInput.Date > DateTime.Now.Date)
                    //{
                    //    Console.WriteLine($"{message} should be a date valid.");
                    //    HoldScreen(); // just to hold a second
                    //    DateTimeFlag = true; // ask user again
                    //}
                }
                catch (Exception e)
                {
                    Console.WriteLine($"{message} not accepted due to: " + e.Message);
                    Additional.HoldScreen(); // just to hold a second
                    DateTimeFlag = true; // ask user again
                }
            } while (DateTimeFlag);

            return DateTimeInput; // Return the validated input
        }
        //8. To read password from the user and validate it ...
        public static string ReadPassword(string message)
        {
            //StringBuilder -> to improve performance when building strings character by character.
            //password -> to store the password input from the user ...
            StringBuilder password = new StringBuilder();
            //ConsoleKeyInfo -> is a structure that stores information
            //about a key press: the key, character, and modifiers (like Shift or Ctrl).
            ConsoleKeyInfo key;

            //To show message to the user to enter password ...
            Console.WriteLine($"Enter your {message} (press Enter when done):");
            do
            {
                //(intercept: true) -> reads a key press without showing it on the screen.
                key = Console.ReadKey(intercept: true);
                //To checks if the user pressed the Backspace key and remove it if so 
                //from the password and delete * from the console.
                if (key.Key == ConsoleKey.Backspace && password.Length > 0)
                {
                    password.Remove(password.Length - 1, 1);
                    Console.Write("\b \b");
                }
                //This filters out non-printable characters, like Ctrl or Alt.
                //If the key is normal characters (letters, digits, etc.)
                //it will enter the (if) and add the key to the password
                else if (!char.IsControl(key.KeyChar))
                {
                    password.Append(key.KeyChar);
                    Console.Write("*");
                }
            }
            //The loop continues until the user presses Enter.
            while (key.Key != ConsoleKey.Enter);

            Console.WriteLine();
            return password.ToString();
        }
        //9. Check if the Password unique or not ...
        public static bool PasswordIsUnique(string password, List<string> list)
        {
            bool IsUnique = true;//it is unique (not exsit in the system) ...
            //to check if password is exist or not (password should be unique) ...
            foreach (var storedHashpassword in list)
            {
                //to call VerifyPasswordPBKDF2 which will hash the password and
                //compare it with the stored hash password ...
                if (VerifyPasswordPBKDF2(password, storedHashpassword))
                {
                    Console.WriteLine("Password is exist in the system.");
                    Additional.HoldScreen();//just to hoad second ...
                    IsUnique = false;
                    return false; // Match found
                }
            }
            return IsUnique; // No match
        }
        //10. To hashed Password ...
        public static string HashPasswordPBKDF2(string password)
        {
            using (var rng = new RNGCryptoServiceProvider())
            // RNGCryptoServiceProvider -> is used to generate a cryptographically strong random number.
            {
                byte[] salt = new byte[16];//to get a random value that makes each hash unique.
                // GetBytes -> fills the specified array with a cryptographically strong random sequence of values.
                rng.GetBytes(salt);

                var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000);//to creates a secure hash using the PBKDF2 algorithm.
                byte[] hash = pbkdf2.GetBytes(20);//to gets the first 20 bytes (160 bits) of the hash.

                byte[] hashBytes = new byte[36];//to creates a final array to store salt + hash.
                Array.Copy(salt, 0, hashBytes, 0, 16);
                Array.Copy(hash, 0, hashBytes, 16, 20);
                //to Copies salt (first 16 bytes) and hash (next 20 bytes) into one array.

                return Convert.ToBase64String(hashBytes);
                //to converts the whole 36-byte array to a Base64 string so
                //it can be stored in a database or file easily.
            }
        }
        //HashPasswordPBKDF2 -> this method hashes the user’s password securely using the
        //PBKDF2 algorithm with a random salt, and returns the result as a Base64 string.

        //what is salt -> a random value added to a password before hashing it.
        //It’s used to make each password hash unique, even if two users have the same password.

        //11. Verify password by comparing hashes
        public static bool VerifyPasswordPBKDF2(string password, string savedHash)
        {
            byte[] hashBytes = Convert.FromBase64String(savedHash);
            //to converts the stored string back into the original 36-byte array
            //(16 bytes salt + 20 bytes hash).

            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);//to extracts the first 16 bytes (the salt).

            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000);
            //to recreates the hash for the input password using the same salt and iteration count.
            byte[] hash = pbkdf2.GetBytes(20);//to gets the expected hash (20 bytes).

            for (int i = 0; i < 20; i++)//to compares each byte of the newly generated
                                        //hash with the one stored after the salt.
            {
                if (hashBytes[i + 16] != hash[i])//to check if any byte is different
                                                 //the password is incorrect.
                    return false;
            }
            return true;//If all bytes match, the password is correct.
        }
        //VerifyPasswordPBKDF2 -> this method verifies a user’s password by:
        // 1. Extracting the original salt from the stored hash
        // 2. Re-hashing the input password using the same salt
        // 3. Comparing both hashes

        //12. EmailValidation method ...
        public static string EmailValidation(string message)
        {
            bool emailFlag;//to handle user email error input ...
            string emailInput = "null";
            do
            {
                emailFlag = false;
                Console.WriteLine($"Enter your {message}:");
                emailInput = Console.ReadLine();
                //to check if emailInput is valid or not ...
                if (!IsValidEmail(emailInput))
                {
                    Console.WriteLine($"{message} is not valid, please try again.");
                    //Additional.HoldScreen();//just to hold a second ...
                    emailFlag = true;
                }
            } while (emailFlag);
            //to return tne char input ...
            return emailInput;
        }

        //13. Email validation using regular expression
        private static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            // Basic but solid regex for general email validation
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, pattern, RegexOptions.IgnoreCase);
        }
        //14. to check if the patient national id exists or not in patient list ...
        public static bool PatientNationalIdExists(string nationalId)
        {
            //to check if the national id exists or not in patient list in branch list in hospital class ...
            foreach (var branch in Hospital.Branches)
            {
                foreach (var patient in branch.Patients)
                {
                    if (patient.UserNationalID == nationalId)
                    {
                        //to get the patient index using national id ...
                        //int patientIndex = branch.Patients.FindIndex(p => p.UserNationalID == nationalId);
                        return true; //if national id exists ...
                    }
                }
            }
            return false; //if national id does not exist ...

        }
        //15. To check if the doctor national id exists or not in doctor list ...
        public static bool DoctorNationalIdExists(string nationalId)
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
        //16. to check if the admin national id exists or not in admin list ...
        public static bool AdminNationalIdExists(string nationalId)
        {
            //to check if the national id exists or not in SuperAdmin list hospital class ...
            foreach (var admin in BranchDepartment.Admins)
            {
                if (admin.UserNationalID == nationalId)
                {
                    return true; //if the national id exists ...
                }
            }
            return false; //if national id does not exist ...
        }
        //17. to check if the super admin national id exists or not in super admin list ...
        public static bool SuperAdminNationalIdExists(string nationalId)
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
        //18. to check if the national id exists or not in UserNationalID ...
        public static bool UserNationalIdExists(string nationalId)
        {
            //to check if the national id exists or not in User list in hospital class ...
            foreach (var id in Hospital.UserNationalID)
            {
                if (id == nationalId)
                {
                    return true; //if national id exists ...
                }
            }
            return false; //if national id does not exist ...
        }
        //19. UserNationalIdValidation method ...
        public static string UserNationalIdValidation()
        {
            bool FlagError = false;
            string nationalId = "null";
            do
            {
                FlagError = false;
                nationalId = Validation.StringValidation("National ID");
                //to check if the national id exists or not ...
                if (Validation.UserNationalIdExists(nationalId))
                {
                    Console.WriteLine("This national ID already exists. Please try again with a different one.");
                    FlagError = true; // Set the flag to true to repeat the loop
                }

            } while (FlagError);
            return nationalId; // Return the valid national ID
        }
        //20. UserPhoneNumberValidation method ...
        public static int UserPhoneNumberValidation()
        {
            bool FalgError = false;
            int value = 0;
            do
            {
                FalgError = false;
                value = Validation.IntValidation("phone number");
                //to check if the phone number is 8 digits or not ...
                if (value < 10000000 || value > 99999999)
                {
                    Console.WriteLine("Phone number must be 8 digits.");
                    FalgError = true; //to handle the error ...
                }
            } while (FalgError);
            return value; // Return the valid phone number
        }
        //21. to check if clinic name exists or not in the clinic list ...
        public static bool ClinicNameExists(string clinicName)
        {
            //to check if the clinic name exists or not in the clinic list in branch department class ...
            foreach (var department in BranchDepartment.Departments)
            {
                foreach (var clinic in department.Clinics)
                {
                    if (clinic.ClinicName.Equals(clinicName, StringComparison.OrdinalIgnoreCase))
                    {
                        return true; //if the clinic name exists ...
                    }
                }
            }
            return false; //if clinic name does not exist ...
        }
        //22. ClinicNameValidation method ...
        public static string ClinicNameValidation()
        {
            bool FlagError = false;
            string clinicName = "null";
            do
            {
                FlagError = false;
                clinicName = Validation.StringValidation("Clinic Name");
                //to check if the clinic name exists or not ...
                if (Validation.ClinicNameExists(clinicName))
                {
                    Console.WriteLine("This clinic name already exists. Please try again with a different one.");
                    FlagError = true; // Set the flag to true to repeat the loop
                }
            } while (FlagError);
            return clinicName; // Return the valid clinic name
        }
        //23. to check if branch id exists or not in the branch list ...
        public static bool BranchIdExists(int branchId)
        {
            //to check if the branch id exists or not in the branch list in hospital class ...
            foreach (var branch in Hospital.Branches)
            {
                if (branch.BranchId == branchId)
                {
                    return true; //if the branch id exists ...
                }
            }
            return false; //if branch id does not exist ...
        }
        //24. BranchIdValidation method ...
        public static int BranchIdValidation()
        {
            bool FlagError = false;
            int branchId = 0;
            do
            {
                FlagError = false;
                branchId = Validation.IntValidation("Branch ID");
                //to check if the branch id exists or not ...
                if (!Validation.BranchIdExists(branchId))
                {
                    Console.WriteLine("This branch ID does not exist. Please try again with a valid one.");
                    FlagError = true; // Set the flag to true to repeat the loop
                }
            } while (FlagError);
            return branchId; // Return the valid branch ID
        }
        //25. to check if the department id exists or not in the department list ...
        public static bool DepartmentIdExists(int departmentId)
        {
            //to check if the department id exists or not in the department list in branch department class ...
            foreach (var department in BranchDepartment.Departments)
            {
                if (department.DepartmentId == departmentId)
                {
                    return true; //if the department id exists ...
                }
            }
            return false; //if department id does not exist ...
        }
        //26. DepartmentIdValidation method ...
        public static int DepartmentIdValidation()
        {
            bool FlagError = false;
            int departmentId = 0;
            do
            {
                FlagError = false;
                departmentId = Validation.IntValidation("Department ID");
                //to check if the department id exists or not ...
                if (!Validation.DepartmentIdExists(departmentId))
                {
                    Console.WriteLine("This department ID does not exist. Please try again with a valid one.");
                    FlagError = true; // Set the flag to true to repeat the loop
                }
            } while (FlagError);
            return departmentId; // Return the valid department ID
        }
        //to list all branchs 
        public static void ListAllBranches()
        {
            Console.WriteLine("List of Branches:");
            if (Hospital.Branches.Count == 0)
            {
                Console.WriteLine("No branches available.");
                return;
            }

            foreach (var branch in Hospital.Branches)
            {
                Console.WriteLine($"ID: {branch.BranchId}, Name: {branch.BranchName}");
            }


        }
        //to list all departments in a branch
        public static void ListDepartmentsInBranch(int branchId)
        {
            Console.WriteLine($"List of Departments in Branch ID {branchId}:");
            var departmentsInBranch = BranchDepartment.Departments.Where(d => d.BranchId == branchId).ToList();
            if (departmentsInBranch.Count == 0)
            {
                Console.WriteLine("No departments found in this branch.");
                return;
            }
            foreach (var department in departmentsInBranch)
            {
                Console.WriteLine($"ID: {department.DepartmentId}, Name: {department.DepartmentName}");
            }
        }
        //to list all deprartments in the system
        public static void ListAllDepartments()
        {
            Console.WriteLine("List of All Departments:");
            if (BranchDepartment.Departments.Count == 0)
            {
                Console.WriteLine("No departments available.");
                return;
            }
            foreach (var department in BranchDepartment.Departments)
            {
                Console.WriteLine($"ID: {department.DepartmentId}, Name: {department.DepartmentName}, Branch ID: {department.BranchId}");
            }
        }
        //to get patient by booking id 
        public static Patient GetPatientByBookingId(int bookingId)
        {
            foreach (var branch in Hospital.Branches)
            {
                foreach (var patient in branch.Patients)
                {
                    foreach(var booking in patient.PatientAppointments)
                    {
                        if (booking.BookingId == bookingId)
                        {
                            return patient; // Return the patient if booking ID matches
                        }
                    }
                 
                }
            }
            return null; // Return null if no patient found with the given booking ID
        }
        //to list all patient who have appointments with a specific doctor based on doctor.bookingId == petient.bookingId
        public static void ListPatientsByDoctorId(int doctorId)
        {
            Console.WriteLine($"List of Patients for Doctor ID {doctorId}:");
            var patientsWithAppointments = BranchDepartment.Doctors
                .Where(d => d.UserId == doctorId)
                .SelectMany(d => d.DoctorAppointments)
                .Select(booking => GetPatientByBookingId(booking.BookingId))
                .Where(patient => patient != null)
                .ToList();
            if (patientsWithAppointments.Count == 0)
            {
                Console.WriteLine("No patients found for this doctor.");
                return;
            }
            foreach (var patient in patientsWithAppointments)
            {
                Console.WriteLine($"Patient National ID: {patient.UserNationalID}, Name: {patient.UserName}");
            }
        }
    }
}
