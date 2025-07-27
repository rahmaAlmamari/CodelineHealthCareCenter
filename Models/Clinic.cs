using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodelineHealthCareCenter.Models
{
    class Clinic
    {
        //1. class fields ...
        public int ClinicId;
        public string ClinicName;
        public int DepartmentId;
        bool ClinicStatus = true; // true means open, false means closed
        public int FloorId;
        public int RoomId;
        public List<Doctor> Doctors = new List<Doctor>();
        public List<DateTime> ClinicSpote = new List<DateTime>();
        public static int ClinicCount = 0;

        //====================================================
        //2. class properity ...

        //====================================================
        //3. class method ...

        //====================================================
        //4. class constructor ...
        public Clinic()
        {
            ClinicCount++;
            ClinicId = ClinicCount;
        }
    }
}
