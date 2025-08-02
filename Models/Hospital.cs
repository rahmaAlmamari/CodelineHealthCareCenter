using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodelineHealthCareCenter.Models
{
    class Hospital
    {
        static string HospitalUsersNationalIDFilePath = "HospitalUsersNationalID.txt";
        //1. class fields ...

        public static int HospitalId = 1;
        public static string HospitalName = "Codeline Health Care Center";
        public static DateOnly HospitalEstablishDate = new DateOnly(2025, 7, 28);
        public static bool HospitalStatus = true; // true means open, false means closed
        public static int HospitalCount = 0;
        public static List<Branch> Branches = new List<Branch>();
        public static List<SuperAdmin> SuperAdmins = new List<SuperAdmin>();
        public static List<string> UserNationalID = new List<string>();

        //====================================================
        //2. class properity ...

        //====================================================
        //3. class method ...

        //====================================================
        //4. class constructor ...
        public Hospital()
        {

            HospitalId = HospitalId;


        }


        // save the hospital data to a file
        public static void SaveHospitalUsersNationalIDToFile()
        {
            using (StreamWriter writer = new StreamWriter(HospitalUsersNationalIDFilePath))
            {
                foreach (var a in UserNationalID)
                {
                    writer.WriteLine($"{a}");

                }
                Console.WriteLine("Hospital data saved to file.");
            }


        }
        // load the hospital data from a file
        public static void LoadHospitalUsersNationalIDFromFile()
        {
            if (File.Exists(HospitalUsersNationalIDFilePath))
            {
                using (StreamReader reader = new StreamReader(HospitalUsersNationalIDFilePath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        UserNationalID.Add(line);
                    }
                }
                Console.WriteLine("Hospital data loaded from file.");
            }
            else
            {
                Console.WriteLine("Hospital data file not found.");
            }


        }
    }
}
