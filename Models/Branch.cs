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
        public static List<Floor> Floors = new List<Floor>();
        public static List<Patient> Patients = new List<Patient>();
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



        

        // Constructor to initialize a new branch with specific details
        public Branch(string branchName, string branchCity, DateOnly branchEstablishDate, int hospitalId)
        {
            BranchCount++;
            BranchId = BranchCount;
            BranchName = branchName;
            BranchCity = branchCity;
            BranchEstablishDate = branchEstablishDate;
            HospitalId = hospitalId;
        }

        // Add a new branch to the hospital
        public static void AddBranch(string branchName, string branchCity, DateOnly branchEstablishDate, int hospitalId)
        {
            Branch newBranch = new Branch(branchName, branchCity, branchEstablishDate, hospitalId);
            Hospital.Branches.Add(newBranch);
            Console.WriteLine($"Branch {newBranch.BranchName} added successfully.");
        }




    }
}
