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
        public List<Patient> Patients = new List<Patient>();
        public int HospitalId;

        static string BranchesFilePath = "Branch.txt";

        //====================================================
        //2. class properity ...
        public int P_BranchId
        {
            get { return BranchId; }
            set
            {
                if (value > 0)
                {
                    BranchId = value;
                }
                else
                {
                    throw new ArgumentException("Branch ID must be a positive integer.");
                }
            }
        }
        public string P_BranchName
        {
            get { return BranchName; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    BranchName = value;
                }
                else
                {
                    throw new ArgumentException("Branch name cannot be empty.");
                }
            }
        }
        public string P_BranchCity
        {
            get { return BranchCity; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    BranchCity = value;
                }
                else
                {
                    throw new ArgumentException("Branch city cannot be empty.");
                }
            }
        }
        public DateOnly P_BranchEstablishDate
        {
            get { return BranchEstablishDate; }
            set
            {
                if (value != default)
                {
                    BranchEstablishDate = value;
                }
                else
                {
                    throw new ArgumentException("Branch establish date cannot be empty.");
                }
            }
        }

        //====================================================
        //3. class method ...
        // Add Branch method to add a new branch
        public static void AddBranch()
        {
            //Console.Clear();
            //Console.WriteLine("Add New Branch");
            //string branchName = Validation.StringValidation("Enter Branch Name");
            //string branchCity = Validation.StringValidation("Enter Branch City");
            //DateOnly branchEstablishDate = Validation.DateValidation("Enter Branch Establish Date (YYYY-MM-DD)");
            //int hospitalId = Validation.IntValidation("Enter Hospital ID");
            //// Create a new branch and add it to the hospital
            //Branch newBranch = new Branch(branchName, branchCity, branchEstablishDate, hospitalId);
            //Hospital.Branches.Add(newBranch);
            //Console.WriteLine($"Branch {newBranch.BranchName} added successfully.");
            //Additional.HoldScreen();
            //SuperAdmin.AdminBranchMenu();

         
            // Load existing branches from file
            Console.Clear();
            Console.WriteLine("Add New Branch");
            Console.WriteLine(new string('-', 40));
            // Get branch details from user
            //string branchName = Validation.StringNamingValidation("branch name");
            //string branchCity = Validation.StringNamingValidation("branch city");
            //DateOnly branchEstablishDate = Validation.DateOnlyValidation("branch establish date");
            // Branch Name
            // Branch Name
            string branchName;
            do
            {
                branchName = Validation.StringNamingValidation("branch name");
                if (string.IsNullOrWhiteSpace(branchName))
                {
                    Console.WriteLine("Invalid branch name. Please try again.");
                }
            } while (string.IsNullOrWhiteSpace(branchName));

            // Branch City
            string branchCity;
            do
            {
                branchCity = Validation.StringNamingValidation("branch city");
                if (string.IsNullOrWhiteSpace(branchCity))
                {
                    Console.WriteLine("Invalid branch city. Please try again.");
                }
            } while (string.IsNullOrWhiteSpace(branchCity));

            // Branch Establish Date
            DateOnly branchEstablishDate;
            do
            {
                branchEstablishDate = Validation.DateOnlyValidation("branch establish date");

                // Assuming DateOnly.MinValue is returned when input is invalid
                if (branchEstablishDate == DateOnly.MinValue)
                {
                    Console.WriteLine("Invalid date. Please try again.");
                }

            } while (branchEstablishDate == DateOnly.MinValue);

            // View all hospitals to select hospital for the branch
            Console.WriteLine("Available Hospitals:");
            Console.WriteLine(new string('-', 40));
            // get all hospitals from the hospital class

            Console.WriteLine("Hospital ID: " + Hospital.HospitalId);
            Console.WriteLine("Hospital Name: " + Hospital.HospitalName);
            Console.WriteLine("Establish Date: " + Hospital.HospitalEstablishDate);
            Console.WriteLine(new string('-', 40));

            int hospitalId;
            do
            {
                hospitalId = Validation.IntValidation("hospital ID");

                if (hospitalId != Hospital.HospitalId)
                {
                    Console.WriteLine("Invalid hospital ID. Please try again.");
                    Additional.HoldScreen();
                }

            } while (hospitalId != Hospital.HospitalId);

            // Add branch to the hospital
            Branch.AddBranch(branchName, branchCity, branchEstablishDate, hospitalId); // Call the static method to add the branch
            // Branch instance creation
            //Branch newBranch = new Branch
            //{
            //    P_BranchName = branchName,
            //    P_BranchCity = branchCity,
            //    P_BranchEstablishDate = branchEstablishDate,
            //    HospitalId = hospitalId
            //};
            //Hospital.Branches.Add(newBranch); // Add the new branch to the hospital's branches list
            //SaveBranches();
            Console.WriteLine("Branch added successfully.");
            // save branches to file
            //SaveBranches();

            Additional.HoldScreen();
            SuperAdmin.AdminBranchMenu();
        }
        


        // Get All Branches
        public static void GetAllBranches()
        {
            Console.Clear();
            Console.WriteLine("List of All Branches");
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
            SuperAdmin.AdminBranchMenu();
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
            SuperAdmin.AdminBranchMenu();
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
            SuperAdmin.AdminBranchMenu();
        }


        // Get Branch Details By BranchName
        public static void GetBranchDetailsByBranchName()
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
            SuperAdmin.AdminBranchMenu();
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

        // Update Branch
        public static void UpdateBranch()
        {
            Console.Clear();
            Console.WriteLine("Update Branch");
            Console.WriteLine("All Branches");
            ViewAllBranch();
            int branchId = Validation.IntValidation("Enter the Branch ID to update :");
            var branchToUpdate = Hospital.Branches.FirstOrDefault(b => b.BranchId == branchId);
            if (branchToUpdate == null)
            {
                Console.WriteLine("Branch not found.");
                Additional.HoldScreen();
                return;
            }

            
                Console.Clear();
                Console.WriteLine("Update Branch Information");
                Console.WriteLine("------------------------");
                Console.WriteLine("1. Branch Name");
                Console.WriteLine("0. Exit");
                char choice = Validation.CharValidation("Choose an option: ");

                switch (choice)
                {
                    case '1':
                        branchToUpdate.BranchName = Validation.StringNamingValidation("Enter new branch name : ");
                        break;
                    case '0':
                        Console.WriteLine("Exiting update menu...");
                        Additional.HoldScreen();
                        SuperAdmin.AdminBranchMenu();
                        //return;
                        break;
                    default:
                        Console.WriteLine("Invalid option, please try again.");
                        break;
                }

                Console.WriteLine("Branch updated successfully.");
                Additional.HoldScreen();
                SuperAdmin.AdminBranchMenu();
        }
        

        // Delete Branch
        public static void DeleteBranch()
        {
            Console.Clear();
            Console.WriteLine("Delete Branch");
            Console.WriteLine(new string('-', 40));
            Console.WriteLine("All Branches");
            // view all branches
            ViewAllBranch();
            // Get branch ID to delete
            int branchId = Validation.IntValidation("Enter the Branch ID to delete : ");
            var branch = Hospital.Branches.FirstOrDefault(b => b.BranchId == branchId);
            if (branch == null)
            {
                Console.WriteLine("Branch not found.");
                Additional.HoldScreen();
                return;
            }
            // Confirm deletion
            if (Additional.ConfirmAction($"delete branch {branch.BranchName}"))
            {
                Hospital.Branches.Remove(branch);
                Console.WriteLine("Branch deleted successfully.");
            }
            else
            {
                Console.WriteLine("Branch deletion cancelled.");
            }
            Additional.HoldScreen();
            SuperAdmin.AdminBranchMenu();
        }



        // View all Branch
        public static void ViewAllBranch()
        {
            Console.WriteLine("List of Branches");

            // get all Available branches
            for (int i = 0; i < Hospital.Branches.Count; i++)
            {
                Console.WriteLine($"Branch ID       : {Hospital.Branches[i].BranchId}");
                Console.WriteLine($"Branch Name     : {Hospital.Branches[i].BranchName}");
                Console.WriteLine($"Branch City     : {Hospital.Branches[i].BranchCity}");
                Console.WriteLine($"Establish Date  : {Hospital.Branches[i].BranchEstablishDate}");
                Console.WriteLine($"Status          : {(Hospital.Branches[i].BranchStatus ? "Open" : "Closed")}");
                Console.WriteLine(new string('-', 40));
            }


        }

        // check if branch id is available or not 
        public static bool BranchIdIsExeist(int branchId)
        {
          // to check if the national id exists or not in SuperAdmin list hospital class ...  

            foreach (var branch in Hospital.Branches)
            {
                if (branch.BranchId == branchId)
                {

                    return true; 
                }
            }
            return false;
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

        // save to file
        public static void SaveBranches()
        {
            using (StreamWriter writer = new StreamWriter(BranchesFilePath))
            {
                foreach (var a in Hospital.Branches)
                {
                    writer.WriteLine($"{a.BranchId}|{a.BranchName}|{a.BranchCity}|{a.BranchEstablishDate}|{a.BranchStatus}");
                }
                
            }
            Console.WriteLine("Branches saved to file.");

        }

        // Load branches from file
        public static void LoadBranches()
        {
            if (File.Exists(BranchesFilePath))
            {
                using (StreamReader reader = new StreamReader(BranchesFilePath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        var parts = line.Split('|');
                        if (parts.Length == 5)
                        {
                            int branchId = int.Parse(parts[0]);
                            string branchName = parts[1];
                            string branchCity = parts[2];
                            DateOnly branchEstablishDate = DateOnly.Parse(parts[3]);
                            bool branchStatus = bool.Parse(parts[4]);
                            Branch branch = new Branch
                            {
                                BranchId = branchId,
                                BranchName = branchName,
                                BranchCity = branchCity,
                                BranchEstablishDate = branchEstablishDate,
                                BranchStatus = branchStatus
                            };
                            Hospital.Branches.Add(branch);
                        }
                    }
                }
                Console.WriteLine("Branches loaded from file.");
            }
            else
            {
                Console.WriteLine("No branches file found.");
            }
        }

        


    }
}
