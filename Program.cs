using CodelineHealthCareCenter.Models;
using System.Diagnostics;
using System.Xml.Linq;

namespace CodelineHealthCareCenter
{
    
internal class Program
    {
        ////to create a test hospital
        ////public static Hospital HospitalTest = new Hospital();
        ////to create a test super admin for the hospital
        ////public static string p_sa = Validation.HashPasswordPBKDF2(Validation.ReadPassword("sa password"));
        //public static SuperAdmin SuperAdminTest = new SuperAdmin("", "", "");
        //// to create a test branch for the hospital
        //public static Branch testBranch = new Branch("TestBranch", "Muscat", new DateOnly(2025, 7, 28), 1);
        //// to create a test floor for the branch
        //public static Floor testFloor = new Floor();
        ////to create a room for the floor
        //public static Room testRoom = new Room();
        ////to create a test admin for the branch
        //public static Admin AdminTest = new Admin("Ahmed", "Ahmed@gmail.com", 1);
        ////to create a test department for the branch
        //public static Department DepartmentTest = new Department();
        ////to create a test clinic for the department
        //public static Clinic ClinicTest = new Clinic("head", "muscat", 1, 1, 1, 1, 20);
        ////to create a test patient for the branch
        //public static Patient PatientTest = new Patient();
        //// to create a test doctor for the branch
        //public static Doctor DoctorTest = new Doctor("Ali", "Ali@gmail.com", "Head", 1, 1);
        ////to create a test service for the clinic
        //public static Service ServiceTest = new Service("S1", 200);


        static void Main(string[] args)
        {
            //to display the welcome message ...
            Additional.WelcomeMessage("Hospital Management");


            //to add the test super admin to the hospital super admins
            //SuperAdminTest.HospitalId = 1; // Assigning HospitalId to the SuperAdmin
            //SuperAdminTest.UserNationalID = "11sa22";
            //SuperAdminTest.UserPhoneNumber = 12345678;
            //SuperAdminTest.P_UserPassword = Validation.HashPasswordPBKDF2(Validation.ReadPassword("super admin password"));
            //SuperAdminTest.UserName = "Karim";
            //SuperAdminTest.UserEmail = "K@gmail.com";


            //Hospital.SuperAdmins.Add(SuperAdminTest);
            //// to add the test branch to the hospital branches
            //Hospital.Branches.Add(testBranch);
            //// to add the test floor to the branch floors
            //testBranch.Floors.Add(testFloor);
            //// to add the test room to the floor rooms
            //testFloor.Rooms.Add(testRoom);
            //// to add the test admin to the branch admins
            //AdminTest.UserNationalID = "11aa22";
            //AdminTest.UserPhoneNumber = 12345678;
            //AdminTest.P_UserPassword = Validation.HashPasswordPBKDF2(Validation.ReadPassword("admin password"));
            //BranchDepartment.Admins.Add(AdminTest);
            ////to add the test department to the branch departments
            //DepartmentTest.BranchId = 1;
            //DepartmentTest.DepartmentName = "Cardiology";
            //BranchDepartment.Departments.Add(DepartmentTest);
            ////to add the test clinic to the department clinics
            //DepartmentTest.Clinics.Add(ClinicTest);
            ////to add the test patient to the branch patients
            //PatientTest.UserName = "Rahma";
            //PatientTest.P_UserPassword = Validation.HashPasswordPBKDF2(Validation.ReadPassword("patient password"));
            //PatientTest.UserNationalID = "11pp22";
            //PatientTest.UserEmail = "rahma@gmail.com";
            //PatientTest.PatientCity = "Muscat";
            //PatientTest.UserPhoneNumber = 12345678;
            //testBranch.Patients.Add(PatientTest);
            ////to add the test doctor to the branch doctors
            //DoctorTest.UserPhoneNumber = 12345678;
            //DoctorTest.UserNationalID = "11dd22";
            //DoctorTest.P_UserPassword = Validation.HashPasswordPBKDF2(Validation.ReadPassword("doctor password"));
            //BranchDepartment.Doctors.Add(DoctorTest);
            ////to add the test service to the clinic services
            //ServiceTest.ClinicId = 1; // Assigning ClinicId to the Service
            //ClinicTest.Services.Add(ServiceTest);
            //Service.Services.Add(ServiceTest); // Add the service to the static Services list
            ////to add the test spot time for clinic
            //ClinicTest.ClinicSpots.Add(DateTime.Parse("07/30/2025 14:30"));
            ////to add all test national IDs to the Hospital UserNational Id list
            //Hospital.UserNationalID.Add(SuperAdminTest.UserNationalID);
            //Hospital.UserNationalID.Add(AdminTest.UserNationalID);
            //Hospital.UserNationalID.Add(PatientTest.UserNationalID);
            //Hospital.UserNationalID.Add(DoctorTest.UserNationalID);

            
            //-----------------------------------------------------------------------------------

            //to load hospital users national id data from file ...
            Hospital.LoadHospitalUsersNationalIDFromFile();
            //to load branches data from file ...
            Branch.LoadBranches();
            //to load departments data from file ...
            Department.LoadDepartmentsFromFile();
            //to load services data from file ...
            Service.LoadServicesFromFile();
            //to load clinics data from file ...
            Clinic.LoadClinicFromFile();
            //to load super admins data to file ...
            SuperAdmin.LoadSuperAdminFromFile();
            //to load doctors data from file ...
            SuperAdmin.LoadDoctorsFromFile();
            //to load admins data from file ...
            SuperAdmin.LoadAdminsFromFile();
            //to load patient data from file ...
            Patient.LoadPatientsFromFile();
            Patient.LoadPatientAppointmentsFromFile(); // Load patient appointments from file
            Console.ReadLine();//just to hold the screen ...
            //to display the main menu options ...
            ShowMainMenu();
        }

        //to show main menu options ...
        public static void ShowMainMenu()
        {
            User x = new User();
            //to list the main menu options ...
            bool exitFlag = false;
            do
            {
                Console.Clear();
                Console.WriteLine("1. LogIn");
                Console.WriteLine("2. SinUp");
                Console.WriteLine("0. Exit");
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
                        //to save hospital user national id data to file ...
                        Hospital.SaveHospitalUsersNationalIDToFile();
                        //to save branches data to file ...
                        Branch.SaveBranches();
                        //to save departments data to file ...
                        Department.SaveDepartmentsToFile();
                        //to save services data to file ...
                        Service.SaveServicesToFile();
                        //to save clinics data to file ...
                        Clinic.SaveClinicToFile();
                        //to save super admins data to file ...
                        SuperAdmin.SaveSuperAdminToFile();
                        //to save admins data to file ...
                        SuperAdmin.SaveAdminsToFile();
                        //to save doctors data to file ...
                        SuperAdmin.SaveDoctorsToFile();
                        //to save patient data to file ...
                        Patient.SavePatientsToFile();
                        Patient.SavePatientAppointmentsToFile(); // Save patient appointments to file
                        Console.ReadLine();//just to hold the screen ... 

                        Console.WriteLine("Thank you for using the Hotel System. Goodbye!");
                        Environment.Exit(0); // Exit the application
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
