using CodelineHealthCareCenter.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodelineHealthCareCenter.Models
{
    class User : IAuthService, IUserService
    {
        //1. class feilds ...
        public int UserId;
        public string UserName;
        private string UserPassword;
        public string UserEmail;
        public int UserPhoneNumber;
        public string UserRole;
        public string UserNationalID;
        public string UserStatus;
        public static int UserCount = 0;

        //====================================================
        //2. class properity ...
        //UserPhoneNumber proprity ...
        public int P_UserPhoneNumber
        {
            get { return UserPhoneNumber; }
            set
            {
                bool FalgError = false; //to handle the error ...
                do
                {
                    FalgError = false; //to reset the error flag ...
                    //to check if the phone number is 8 digits or not ...
                    if (value.ToString().Length == 8)
                    {
                        UserPhoneNumber = value;
                    }
                    else
                    {
                        Console.WriteLine("Phone number must be 8 digits.");
                        value = Validation.IntValidation("patient phone number");
                        FalgError = true; //to handle the error ...
                    }

                } while (FalgError);

            }
        }
        //UserPassword proprity ...
        public string P_UserPassword
        {
            get { return UserPassword; }
            set
            {
                //to do hashing for the password ...
                UserPassword = value;
            }
        }

        //====================================================
        //3. class method ...


        //====================================================
        //4. class constructor ...
        public User()
        {
            UserCount++;
            UserId = UserCount;//to make user id unique ...
        }
    }
}
