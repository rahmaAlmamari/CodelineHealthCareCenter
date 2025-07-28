using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CodelineHealthCareCenter.Models
{
    class Additional
    {
        //1. To hoad the screen ...
        public static void HoldScreen()
        {
            Console.WriteLine("Press (Enter Kay) to continue");
            Console.ReadLine();
        }
        //2. To check of the string contains something other than letters like number and empty space(this methos return true or false)....
        public static bool IsAlpha(string input)
        {
            return Regex.IsMatch(input, "^[a-zA-Z]+$");
        }

    }
}
