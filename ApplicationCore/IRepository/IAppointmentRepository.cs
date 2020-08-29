using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore
{
  public interface IAppointmentRepository : IGenericRepositry<Appointment>
  {
    Appointment getAppointmentByID(long Id);
  }
}
