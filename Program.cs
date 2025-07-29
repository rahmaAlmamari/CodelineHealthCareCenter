using CodelineHealthCareCenter.Models;
using System.Diagnostics;

namespace CodelineHealthCareCenter
{
    
internal class Program
    {
        // to create a test branch for the hospital
        public static Branch testBranch = new Branch("TestBranch", "Muscat", new DateOnly(2025, 7, 28), 1);
        // to create a test floor for the branch
        public static Floor testFloor = new Floor();
        //to create a test department for the branch
        public static Department DepartmentTest = new Department();
        //to create a test clinic for the department
        public static Clinic ClinicTest = new Clinic("head", "muscat", 1, 1, 1, 1, 20);
        //to create a test patient for the branch
        public static Patient PatientTest = new Patient();
        // to create a test doctor for the branch
        public static Doctor DoctorTest = new Doctor("Ali", "Ali@gmail.com", "Head", 1, 1);

        static void Main(string[] args)
        {
            // to add the test branch to the hospital branches
            Hospital.Branches.Add(testBranch);
            // to add the test floor to the branch floors
            testBranch.Floors.Add(testFloor);
            //to add the test department to the branch departments
            DepartmentTest.BranchId = 1;
            DepartmentTest.DepartmentName = "Cardiology";
            BranchDepartment.Departments.Add(DepartmentTest);
            //to add the test clinic to the department clinics
            DepartmentTest.Clinics.Add(ClinicTest);
            //to add the test patient to the branch patients
            PatientTest.UserName = "Rahma";
            PatientTest.P_UserPassword = "123";
            PatientTest.UserNationalID = "11rr22";
            PatientTest.UserEmail = "rahma@gmail.com";
            PatientTest.PatientCity = "Muscat";
            PatientTest.P_UserPhoneNumber = 12345678;
            testBranch.Patients.Add(PatientTest);
            //to add the test doctor to the branch doctors
            BranchDepartment.Doctors.Add(DoctorTest);
            //to display the welcome message ...
            Additional.WelcomeMessage("Hospital Management");
            User x = new User();
            //to list the main menu options ...
            bool exitFlag = false;
            do
            {
                Console.Clear();
                Console.WriteLine("1. LogIn");
                Console.WriteLine("2. SinUp");
                Console.WriteLine("3. Doctor");
                Console.WriteLine("4. Patient");
                Console.WriteLine("0. Exit");
                Console.Write("Please select an option: ");
                //to get the user choice ...
                char choice = Validation.CharValidation("option");
                switch (choice)
                {
                    case '1':
                        //to add a new patient ...
                        x.Login();
                        break;
                    case '2':
                        //to add a new doctor ...
                        //Admin.AdminMenu();
                        Patient.SinUp();
                        break;
                    case '3':
                        //to Booking an appointment ...
                        Doctor.DoctorMenu();
                        break;
                    case '4':
                        //to displaying all appointments ...
                       Patient.PatientMenu();
                        break;
                    case '0':
                        exitFlag = true;
                        Console.WriteLine("Thank you for using the Hotel System. Goodbye!");
                        break;
                    default:
                        Console.WriteLine("Invalid option, please try again.");
                        Additional.HoldScreen();
                        break;
                }
            } while (!exitFlag);
        }
    }
}
