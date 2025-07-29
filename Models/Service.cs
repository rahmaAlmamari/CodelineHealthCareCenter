using CodelineHealthCareCenter.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodelineHealthCareCenter.Models
{
    class Service
    {
        //1. class fields ...

        public int ServiceId;
        public string ServiceName;
        public double Price;
        public static int ServiceCount = 0;

        public static List<Service> Services = new List<Service>();

        public static IServiceService service; // for ServiceMenu()

        // ===================================================
        //2. class properties ...

        public static int TotalServices => ServiceCount;

        //====================================================
        //3. class method ...
        public void ViewServiceInfo() // displays the details of the service
        {
            Console.WriteLine($"Service ID: {ServiceId}");
            Console.WriteLine($"Name      : {ServiceName}");
            Console.WriteLine($"Cost      : ${Price}");
        }

        public static void ServiceMenu()
        {
            Additional.WelcomeMessage("Service Management");

            while (true)
            {
                Console.Clear();
                Console.WriteLine(" SERVICE MANAGEMENT MENU ");
                Console.WriteLine("1. Add Service");
                Console.WriteLine("2. Update Service");
                Console.WriteLine("3. Delete Service");
                Console.WriteLine("4. View All Services");
                Console.WriteLine("5. View Service by ID");
                Console.WriteLine("6. Exit");
                Console.Write("Select an option: ");

                string choice = Console.ReadLine();
                Console.WriteLine();

                switch (choice)
                {
                    case "1": // Add service
                        string name = Validation.StringNamingValidation("Service Name");
                        string desc = Validation.StringValidation("Service Description");
                        double cost = Validation.DoubleValidation("Service Price");
                        service.AddService(name, desc, cost);
                        break;


                    case "2": // Update service
                        int updateId = Validation.IntValidation("Service ID to update");

                        if (Additional.ConfirmAction("update this service"))
                        {
                            string newName = Validation.StringNamingValidation("New Service Name");
                            string newDesc = Validation.StringValidation("New Description");
                            double newPrice = Validation.DoubleValidation("New Price");
                            service.UpdateService(updateId, newName, newDesc, newPrice);
                        }
                        else Console.WriteLine("Update cancelled.");
                        break;

                    case "3": // Delete service
                        int deleteId = Validation.IntValidation("Service ID to delete");

                        if (Additional.ConfirmAction("delete this service"))
                        {
                            service.DeleteService(deleteId);
                        }
                        else Console.WriteLine("Deletion cancelled.");
                        break;

                    case "4": // View all
                        service.GetAllServices();
                        break;

                    case "5": // View by ID
                        int id = Validation.IntValidation("Service ID");
                        service.GetServiceById(id);
                        break;

                    case "6": // Exit
                        Console.WriteLine("Exiting Service Menu...");
                        return;

                    default: // Invalid option
                        Console.WriteLine("Invalid option. Try again.");
                        break;


                }

                Additional.HoldScreen(); // holds the screen after each operation
            }
        }




        //====================================================
        //4. class constructor ...

        public Service(string name, double price)
        {
            ServiceCount++;
            ServiceId = ServiceCount;
            ServiceName = name;
            Price = price;
        }
    }
}
