using CodelineHealthCareCenter.Services;
using System;
using System.Collections.Generic;

namespace CodelineHealthCareCenter.Models
{
    class Admin : User
    {
        //====================================================
        //1. class fields ...
        public int BranchID;
        public List<Clinic> Clinics = new List<Clinic>();
        public static IAdminService service; // new static service field

        //====================================================
        //2. class properties ...
        public static int AdminCount { get; private set; }

        //====================================================
        //3. class methods ...

        public void AddClinic(Clinic clinic)
        {
            Clinics.Add(clinic);
            Console.WriteLine($" Clinic '{clinic.ClinicName}' added to Admin '{UserName}'.");
        }

        public void ViewClinics()
        {
            Console.WriteLine($"Clinics managed by Admin {UserName}:");
            if (Clinics.Count == 0)
            {
                Console.WriteLine(" No clinics assigned yet.");
            }
            else
            {
                foreach (var clinic in Clinics)
                    clinic.ViewClinicInfo();
            }
        }


    }
}
