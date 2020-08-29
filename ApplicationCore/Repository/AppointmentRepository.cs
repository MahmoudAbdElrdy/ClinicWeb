using DataAccessLayer;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Repository
{

  public class AppointmentRepository : GenericRepository<Appointment>, IAppointmentRepository
  {
    private readonly ApiContext apiContext;

    public AppointmentRepository(ApiContext dbcontext) : base(dbcontext)
    {
      apiContext = dbcontext;
    }

    public Appointment getAppointmentByID(long Id)
    {
      return null;
      // return apiContext.Appointments.FirstOrDefault(x => x.Id == Id);
    }
  }
}
