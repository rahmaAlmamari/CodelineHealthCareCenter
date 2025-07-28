using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodelineHealthCareCenter.Models
{
    class Admin : User
    {
        //1. class feilds ...
        public int BranchID;
        public List<Clinic> Clinics = new List<Clinic>();
        //====================================================
        //2. class properity ...

        //====================================================
        //3. class method ...
        public static void AdminMenu()
        {
            Console.WriteLine("Welcome to Admin Menu");
            Console.WriteLine("1. Add Clinic");
            Console.WriteLine("2. View Clinics");
            Console.WriteLine("3. Update Clinic");
            Console.WriteLine("4. Delete Clinic");
            Console.WriteLine("5. Exit");
        }

        //====================================================
        //4. class constructor ...

    }
}
