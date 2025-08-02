using CodelineHealthCareCenter.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace CodelineHealthCareCenter.Models
{
    class Service
    {
        //1. class fields ...

        public int ServiceId;
        public string ServiceName;
        public double Price;
        public static int ServiceCount = 0;
        public int ClinicId; // optional, if services are clinic-specific
        public static List<Service> Services = new List<Service>();

        public static IServiceService service; // for ServiceMenu()

        //file path for saving and loading services
        public static string ServicesFilePath = "Services.txt";


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

        public static void AddService(string name, string description, double price) // adds a new service to the list
        {
            var newService = new Service(name, price);
            Services.Add(newService);
            Console.WriteLine($"Service '{name}' added successfully with ID {newService.ServiceId}.");
        }

        public static void UpdateService(int id, string newName, string newDescription, double newPrice) // updates an existing service by its ID
        {
            var service = Services.FirstOrDefault(s => s.ServiceId == id);
            if (service == null)
            {
                Console.WriteLine("Service not found.");
                return;
            }

            service.ServiceName = newName;
            service.Price = newPrice;
            Console.WriteLine("Service updated successfully.");
        }

        public static void DeleteService(int id) // deletes a service by its ID
        {
            var service = Services.FirstOrDefault(s => s.ServiceId == id);
            if (service == null)
            {
                Console.WriteLine("Service not found.");
                return;
            }

            Services.Remove(service);
            Console.WriteLine("Service deleted successfully.");
        }

        public static void GetAllServices() // retrieves and displays all available services
        {
            if (Services.Count == 0)
            {
                Console.WriteLine("No services available.");
                return;
            }

            foreach (var s in Services)
                s.ViewServiceInfo();
        }

        public static void GetServiceById(int id) // retrieves a service by its ID and displays its details
        {
            var service = Services.FirstOrDefault(s => s.ServiceId == id);
            if (service == null)
            {
                Console.WriteLine("Service not found.");
                return;
            }

            service.ViewServiceInfo();
        }

        public static void SaveServicesToFile()
        {
            using StreamWriter writer = new StreamWriter(ServicesFilePath);
            foreach (var service in Service.Services)
            {
                writer.WriteLine($"{service.ServiceId}|{service.ServiceName}|{service.Price} | {service.ClinicId}");
            }
            Console.WriteLine("Service data saved successfully.");
        }

        public static void LoadServicesFromFile()
        {
            ServiceCount = 0;

            if (!File.Exists(ServicesFilePath)) return;

            string[] lines = File.ReadAllLines(ServicesFilePath);
            foreach (var line in lines)
            {
                string[] parts = line.Split('|');
                if (parts.Length < 3) continue;

                Service service = new Service(parts[1], double.Parse(parts[2]))
                {
                    ServiceId = int.Parse(parts[0])
                };
                service.ClinicId = parts.Length > 3 ? int.Parse(parts[3]) : 0; // optional ClinicId
                Services.Add(service);
                if (service.ServiceId > ServiceCount)
                    ServiceCount = service.ServiceId;
            }
            Console.WriteLine("Service data loaded successfully.");
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





