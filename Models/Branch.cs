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
        // Add Branch method to add a new branch
        public static void AddBranch()
        {
            Console.Clear();
            Console.WriteLine("Add New Branch");
            // Get branch details from user
            string branchName = Validation.StringValidation("branch name");
            string branchCity = Validation.StringValidation("branch city");
            DateOnly branchEstablishDate = Validation.DateOnlyValidation("branch establish date");
            int hospitalId = Validation.IntValidation("hospital ID");

            // Add branch to the hospital
            Branch.AddBranch(branchName, branchCity, branchEstablishDate, hospitalId); // Call the static method to add the branch

            Console.WriteLine("Branch added successfully.");
            Additional.HoldScreen();
        }

        // Get All Branches method to display all branches in the hospital
        public static void GetAllBranches()
        {
            Console.Clear();
            Console.WriteLine("List of Branches");
            if (Hospital.Branches.Count == 0)
            {
                Console.WriteLine("No branches available in the system.");
                Additional.HoldScreen();
                return;
            }
            foreach (var branch in Hospital.Branches)
            {
                Console.WriteLine($"Branch ID       : {branch.BranchId}");
                Console.WriteLine($"Branch Name     : {branch.BranchName}");
                Console.WriteLine($"Branch City     : {branch.BranchCity}");
                Console.WriteLine($"Establish Date  : {branch.BranchEstablishDate}");
                Console.WriteLine($"Status          : {(branch.BranchStatus ? "Open" : "Closed")}");
                Console.WriteLine(new string('-', 40));
            }
            Additional.HoldScreen();
        }



        // Get Branch By Id
        public static void GetBranchById()
        {
            Console.WriteLine("Enter the Branch ID to view:");
            int branchId = Validation.IntValidation("Branch ID");
            var branch = Hospital.Branches.FirstOrDefault(b => b.BranchId == branchId);
            if (branch == null)
            {
                Console.WriteLine("Branch not found.");
                Additional.HoldScreen();
                return;
            }
            Console.WriteLine($"Branch ID       : {branch.BranchId}");
            Console.WriteLine($"Branch Name     : {branch.BranchName}");
            Console.WriteLine($"Branch City     : {branch.BranchCity}");
            Console.WriteLine($"Establish Date  : {branch.BranchEstablishDate}");
            Console.WriteLine($"Status          : {(branch.BranchStatus ? "Open" : "Closed")}");
            Additional.HoldScreen();
        }

        // Get Branch Details
        public static void GetBranchDetails()
        {
            Console.Clear();
            Console.WriteLine("Branch Details");
            if (Hospital.Branches.Count == 0)
            {
                Console.WriteLine("No branches available in the system.");
                Additional.HoldScreen();
                return;
            }
            foreach (var branch in Hospital.Branches)
            {
                Console.WriteLine($"Branch ID       : {branch.BranchId}");
                Console.WriteLine($"Branch Name     : {branch.BranchName}");
                Console.WriteLine($"Branch City     : {branch.BranchCity}");
                Console.WriteLine($"Establish Date  : {branch.BranchEstablishDate}");
                Console.WriteLine($"Status          : {(branch.BranchStatus ? "Open" : "Closed")}");
                Console.WriteLine(new string('-', 40));
            }
            Additional.HoldScreen();
        }


        // Get Branch Details By BranchName
        public static void GetBranchDetailsByName()
        {
            Console.Clear();
            Console.WriteLine("Enter the Branch Name to view:");
            string branchName = Validation.StringValidation("Branch Name");
            var branch = Hospital.Branches.FirstOrDefault(b => b.BranchName.Equals(branchName, StringComparison.OrdinalIgnoreCase));
            if (branch == null)
            {
                Console.WriteLine("Branch not found.");
                Additional.HoldScreen();
                return;
            }
            Console.WriteLine($"Branch ID       : {branch.BranchId}");
            Console.WriteLine($"Branch Name     : {branch.BranchName}");
            Console.WriteLine($"Branch City     : {branch.BranchCity}");
            Console.WriteLine($"Establish Date  : {branch.BranchEstablishDate}");
            Console.WriteLine($"Status          : {(branch.BranchStatus ? "Open" : "Closed")}");
            Additional.HoldScreen();
        }

        // Get Branch Name
        public static string GetBranchName(int branchId)
        {
            var branch = Hospital.Branches.FirstOrDefault(b => b.BranchId == branchId);
            if (branch != null)
            {
                return branch.BranchName;
            }
            return "Branch not found";
        }


        // Get Branch Status
        public static bool GetBranchStatus(int branchId)
        {
            var branch = Hospital.Branches.FirstOrDefault(b => b.BranchId == branchId);
            if (branch != null)
            {
                return branch.BranchStatus;
            }
            return false; // Return false if branch not found
        }



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
