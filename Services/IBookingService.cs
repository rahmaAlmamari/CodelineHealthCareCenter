using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodelineHealthCareCenter.Services
{
    interface IBookingService
    {
        void BookAppointment();
        void CancelAppointment();
        void DeleteAppointment();
        void UpdateBookedAppointment();
        void GetAllBooking();
        void GetBookingById(int id);
        void GetBookingByClinicIdAndDate(int patientId);
        void GetBookingByPatientId(int patientId);
        void GetAvailableAppointmentsByClinicIdAndDate(DateTime date, int clinicId);
        void ScheduleAppointment(int patientId, int clinicId, DateTime date, TimeSpan time);

    }
}
