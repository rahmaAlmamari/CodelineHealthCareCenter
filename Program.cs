using CodelineHealthCareCenter.Models;

namespace CodelineHealthCareCenter
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //to display the welcome message ...
            Additional.WelcomeMessage("Hospital Management");

            //to list the main menu options ...
            bool exitFlag = false;
            do
            {
                Console.Clear();
                Console.WriteLine("1. SuperAdmin");
                Console.WriteLine("2. Admin");
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
                        SuperAdmin.SuperAdminMenu();
                        break;
                    case '2':
                        //to add a new doctor ...
                        Admin.AdminMenu();
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
