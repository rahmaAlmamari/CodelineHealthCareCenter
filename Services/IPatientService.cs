using CodelineHealthCareCenter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodelineHealthCareCenter.Services
{
    interface IPatientService
    {
        // Methods for Patient Service
        void AddPatient(string username, string password, string email, int phoneNumber, string userNationalID, string city, Branch PationtBranch);//to sinUp new patient ...
        //void UpdatePatientDetails(int patientId, string username, string email, string phoneNumber);

        //void GetPatientById(int patientId);
        //void GetPatientByName(string username);
        //void GetAllPatients();
        //void GetPatientData(int patientId);

    }
}
