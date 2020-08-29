using DataAccessLayer.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer
{
  public class ApiContext : IdentityDbContext<ApplicationUser>
  {
    public ApiContext(DbContextOptions<ApiContext> options) : base(options)
    {

    }
    public virtual DbSet<Clinic> Clinics { get; set; }
    public virtual DbSet<Store> Stores { get; set; }
    public virtual DbSet<Items> Items { get; set; }
    public virtual DbSet<Appointment> Appointments { get; set; }
    public virtual DbSet<Doctor> Doctors { get; set; }
    public virtual DbSet<Patient> Patients { get; set; }
    public virtual DbSet<Visit> Visits { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {
      builder.Entity<Appointment>().HasKey(ba => new { ba.ClinicId, ba.Day, ba.DoctorId, ba.ExpiresIn, ba.StartFrom });
      builder.Entity<Visit>().HasOne(x => x.Appointment).WithMany(x => x.Visits).HasPrincipalKey(ba => new { ba.ClinicId, ba.Day, ba.DoctorId, ba.ExpiresIn, ba.StartFrom });
      base.OnModelCreating(builder);
      }
  }
}
