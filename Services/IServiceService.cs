using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodelineHealthCareCenter.Services
{
    interface IServiceService
    {
        void AddService(string name, string description, double price);
        void UpdateService(int id, string newName, string newDescription, double newPrice);
        void DeleteService(int id);
        void GetAllServices();
        void GetServiceById(int id);
    }
}

