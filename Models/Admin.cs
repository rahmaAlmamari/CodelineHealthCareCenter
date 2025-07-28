using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodelineHealthCareCenter.Models
{
    class Admin : User
    {
        //1. class fields ...
        public int BranchID;
        public List<Clinic> Clinics = new List<Clinic>();
        //====================================================
        //2. class property ...

        public static int AdminCount { get; private set; }

        //====================================================
        //3. class method ...
        public void AddClinic(Clinic clinic) // adds a clinic to the admin's list of clinics
        {
            Clinics.Add(clinic);
            Console.WriteLine($" Clinic '{clinic.ClinicName}' added to Admin '{Username}'.");


        }

        public void ViewClinics() // displays all clinics managed by the admin
        {
            Console.WriteLine($"Clinics managed by Admin {Username}:");
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

        //====================================================
        //4. class constructor ...
    }
}
