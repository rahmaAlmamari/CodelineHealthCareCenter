using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodelineHealthCareCenter.Models
{
    class Department
    {
        //1. class fields ...
        public int DepartmentId;
        public string DepartmentName;
        public int BranchId;
        public static int DepartmentCount = 0;
        public List<Clinic> Clinics = new List<Clinic>();

        //====================================================
        //2. class properity ...

        //====================================================
        //3. class method ...

        //====================================================
        //4. class constructor ...
    }
}
