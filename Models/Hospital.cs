using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodelineHealthCareCenter.Models
{
    class Hospital
    {
        //1. class fields ...

        public int HospitalId = 1;
        public string HospitalName = "Codeline Health Care Center";
        public DateOnly HospitalEstablishDate = new DateOnly(2025, 7, 28);
        public bool HospitalStatus = true; // true means open, false means closed
        public static int HospitalCount = 0;
        public static List<Branch> Branches = new List<Branch>();
        public static List<SuperAdmin> SuperAdmins = new List<SuperAdmin>();
        public static List<string> UserNationalID = new List<string>();

        //====================================================
        //2. class properity ...

        //====================================================
        //3. class method ...

        //====================================================
        //4. class constructor ...
        public Hospital()
        {

            HospitalId = HospitalId;


        }
    }
}
