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

        //====================================================
        //4. class constructor ...
    }
}
