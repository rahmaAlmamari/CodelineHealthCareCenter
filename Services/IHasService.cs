using CodelineHealthCareCenter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodelineHealthCareCenter.Services
{
    interface IHasService
    {
        List<Service> Services { get; set; }
    }
}
