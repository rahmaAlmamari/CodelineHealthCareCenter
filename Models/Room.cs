using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodelineHealthCareCenter.Models
{
    class Room
    {
        //1. class fields ...

        public int RoomId;
        public static int RoomCount = 0;
        public bool RoomStatus = true; // true means available, false means occupied

        //====================================================
        //2. class properity ...

        //====================================================
        //3. class method ...

        //====================================================
        //4. class constructor ...
        public Room()
        {
            RoomCount++;
            RoomId = RoomCount;
            
        }
    }
}
