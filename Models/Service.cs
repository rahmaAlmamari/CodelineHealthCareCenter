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
        public double ServiceCost;
        public static int ServiceCount = 0;
        public static IServiceService service; // for ServiceMenu()

        //====================================================
        //2. class properties ...

        public static int TotalServices => ServiceCount;

        //====================================================
        //3. class method ...
        public void ViewServiceInfo() // displays the details of the service
        {
            Console.WriteLine($"Service ID: {ServiceId}");
            Console.WriteLine($"Name      : {ServiceName}");
            Console.WriteLine($"Cost      : ${ServiceCost}");
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




        //====================================================
        //4. class constructor ...

        public Service()
        {
            ServiceCount++;
            ServiceId = ServiceCount;

        }
    }
}
