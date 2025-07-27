using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodelineHealthCareCenter.Models
{
    class Branch
    {
        //1. class fields ...
        public int BranchId;
        public string BranchName;
        public string BranchCity;
        public DateOnly BranchEstablishDate;
        public bool BranchStatus = true; // true means open, false means closed
        public static int BranchCount = 0;
        public List<Floor> Floors = new List<Floor>();
        public int HospitalId;

        //====================================================
        //2. class properity ...

        //====================================================
        //3. class method ...

        //====================================================
        //4. class constructor ...
        public Branch()
        {
            BranchCount++;
            BranchId = BranchCount;
        }
    }
}
