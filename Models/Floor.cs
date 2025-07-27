using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodelineHealthCareCenter.Models
{
    class Floor
    {
        //1. class fields ...
        public int FloorId;
        public List<Room> Rooms = new List<Room>();
        public static int FloorCount = 0;


        //====================================================
        //2. class properity ...

        //====================================================
        //3. class method ...

        //====================================================
        //4. class constructor ...
        public Floor()
        {
            FloorCount++;
            FloorId = FloorCount;
        }
    }
}
