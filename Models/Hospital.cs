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

        public int HospitalId;
        public string HospitalName;
        public DateOnly HospitalEstablishDate;
        public bool HospitalStatus = true; // true means open, false means closed
        public static int HospitalCount = 0;
        public static List<Branch> Branches = new List<Branch>();
        public static List<SuperAdmin> SuperAdmins = new List<SuperAdmin>();

        //====================================================
        //2. class properity ...

        //====================================================
        //3. class method ...

        //====================================================
        //4. class constructor ...
        public Hospital()
        {
            HospitalCount++;
            HospitalId = HospitalCount;
          
        }

    }
}
