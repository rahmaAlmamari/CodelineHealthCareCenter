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
      
        bool ClinicStatus = true; // true means open, false means closed
        public static int ClinicCount = 0;

        //====================================================
        //2. class properity ...

        public int ClinicId { get; private set; } 
        public string ClinicName { get; set; }
        public int DepartmentId { get; set; }
        public int FloorId { get; set; }
        public int RoomId { get; set; }

        // list of doctors and clinic spots
        public List<Doctor> Doctors { get; set; }
        public List<DateTime> ClinicSpots { get; set; }






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
