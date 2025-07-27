using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodelineHealthCareCenter.Models
{
    class Booking
    {
        //1. class feilds ...

        public int BookingId;
        public DateTime BookingDateTime;
        public int ClinicId;
        public static int BookingCount = 0;


        //====================================================
        //2. class properity ...

        //====================================================
        //3. class method ...

        //====================================================
        //4. class constructor ...
        public Booking()
        {
            BookingCount++;
            BookingId = BookingCount;
        }
    }
}
