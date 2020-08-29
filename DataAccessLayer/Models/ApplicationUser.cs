using DataAccessLayer.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer
{
  public class ApplicationUser : IdentityUser
  {

    public string VerifyCode { get; set; }
    public bool IsActive { get; set; }
    public DateTime? CreatedDate { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public string Address { get; set; }
    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    public virtual ICollection<Visit> Visits { get; set; } = new List<Visit>();
    public virtual ICollection<Store> Stores { get; set; } = new List<Store>();
    public virtual ICollection<Items> Items { get; set; } = new List<Items>();
    public virtual ICollection<Doctor> Doctors { get; set; } = new List<Doctor>();
    public virtual ICollection<Clinic> Clinics { get; set; } = new List<Clinic>();
  }
}
