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
        public int DoctorId;
        public static int BookingCount = 0;
        public List<Service> BookingService = new List<Service>();


        //====================================================
        //2. class properity ...

        //====================================================
        //3. class method ...
        //to create a new book appointment ...
        public static void BookAppointment()
        {
            //to list all departments ...
            Validation.ListAllDepartments();
            int departmentId = Validation.IntValidation("Department ID");
            //to list all clinics in the selected department ...
            GetAllClinicsByDepartmentId(departmentId);
            int clinicId = Validation.IntValidation("Clinic ID");
            //to list all doctors in the selected clinic ...
            GetAllDoctorsByClinicId(clinicId);
            int doctorId = Validation.IntValidation("Doctor ID");
            //to list all services in the selected clinic ...
            GetAllDServicsByClinicId(clinicId);
            int serviceId = Validation.IntValidation("Service ID");
            //to list all spots in the selected clinic ...
            GetAllSpotsByClinicId(clinicId, departmentId);
            DateTime SpotDateTime = Validation.DateTimeValidation("Spot Date and Time (yyyy-MM-dd HH:mm:ss)");
            //to remove the selected spot from the clinic spots ...
            foreach (var department in BranchDepartment.Departments)
            {
                if (department.DepartmentId == departmentId)
                {
                    foreach (var clinic in department.Clinics)
                    {
                        if (clinic.ClinicId == clinicId)
                        {
                            if (clinic.ClinicSpots.Contains(SpotDateTime))
                            {
                                clinic.ClinicSpots.Remove(SpotDateTime);
                                Console.WriteLine($"Spot {SpotDateTime} removed from Clinic ID {clinicId}.");
                            }
                            else
                            {
                                Console.WriteLine($"Spot {SpotDateTime} not found in Clinic ID {clinicId}.");
                                return;
                            }
                        }
                    }
                }
            }
            //to create a new booking ...
            Booking newBooking = new Booking
            {
                BookingDateTime = SpotDateTime,
                ClinicId = clinicId,
                DoctorId = doctorId
            };
            //to add the selected service to the new booking ...
            var selectedService = Service.Services.FirstOrDefault(s => s.ServiceId == serviceId);
            if (selectedService != null)
            {
                newBooking.BookingService.Add(selectedService);
                Console.WriteLine($"Service '{selectedService.ServiceName}' added to the booking.");
            }
            else
            {
                Console.WriteLine("Selected service not found.");
                return;
            }
            //to add the new booking to the DoctorAppointments list
            foreach (var doctor in BranchDepartment.Doctors)
            {
                if (doctor.UserId == doctorId)
                {
                    doctor.DoctorAppointments.Add(newBooking);
                    Console.WriteLine($"Appointment booked successfully for Doctor ID {doctorId} in Clinic ID {clinicId} at {SpotDateTime}.");
                    Additional.HoldScreen(); //just to hold the screen ...
                }
            }
            //to add the new booking to the PatientAppointments list
            foreach (var branch in Hospital.Branches) 
            {
                foreach (var patient in branch.Patients) 
                {
                    if (patient.UserNationalID == Validation.StringValidation("Patient National ID"))
                    {
                        patient.PatientAppointments.Add(newBooking);
                        Console.WriteLine($"Appointment booked successfully for Patient ID {patient.UserNationalID} in Clinic ID {clinicId} at {SpotDateTime}.");
                    }
                }
            }
            Additional.HoldScreen(); //just to hold the screen ...

        }
        //to GetAllClinicsByDepartmentId ...
        public static void GetAllClinicsByDepartmentId(int departmentId)
        {
            if (Clinic.ClinicCount == 0)
            {
                Console.WriteLine("No clinic available.");
                return;
            }
            Console.WriteLine("List of Clini in the Selected Department:");
            foreach (var department in BranchDepartment.Departments)
            {
                if(department.DepartmentId == departmentId)
                {
                    foreach (var clinic in department.Clinics)
                    {
                        Console.WriteLine($"ID: {clinic.ClinicId}, Name: {clinic.ClinicName}, Department ID: {clinic.DepartmentId}");
                    }
                }
            }

        }
        public static void GetAllDoctorsByClinicId(int clinicId)
        {
            if (BranchDepartment.Doctors.Count == 0)
            {
                Console.WriteLine("No doctors available.");
                return;
            }
            Console.WriteLine("List of Doctors in the Selected Clinic:");
            foreach (var doctor in BranchDepartment.Doctors)
            {
                if (doctor.ClinicID == clinicId)
                { 
                            Console.WriteLine($"ID: {doctor.UserId}, Name: {doctor.UserName}, Specialization: {doctor.DoctorSpecialization}");
                }
            }
        }
        public static void GetAllDServicsByClinicId(int clinicId)
        {
            if (Service.Services.Count == 0)
            {
                Console.WriteLine("No services available.");
                return;
            }
            Console.WriteLine("List of Services in the Selected Clinic:");
            foreach (var service in Service.Services)
            {
                if (service.ClinicId == clinicId)
                {
                    Console.WriteLine($"ID: {service.ServiceId}, Name: {service.ServiceName}, Price: {service.Price}");
                }
            }
        }
        public static void GetAllSpotsByClinicId(int clinicId, int departmentId) 
        {
           foreach(var department in BranchDepartment.Departments)
            {
                if (department.DepartmentId == departmentId)
                {
                    foreach(var clinic in department.Clinics) 
                    { 
                        if(clinic.ClinicId == clinicId)
                        {
                            foreach(var spot in clinic.ClinicSpots)
                            {
                                Console.WriteLine($"spot: {spot}");
                            }
                        }
                    }
                    
                }
            }
        }
        //====================================================
        //4. class constructor ...
        public Booking()
        {
            BookingCount++;
            BookingId = BookingCount;
        }
    }
}
