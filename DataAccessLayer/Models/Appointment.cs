using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccessLayer.Models
{

  public class Appointment
  {

    [Key]
    public Day Day { get; set; }
    [Key]
    public string StartFrom { get; set; }
    [Key]
    public string ExpiresIn { get; set; }


    [Key]
    [ForeignKey("Clinic")]
    public long? ClinicId { get; set; }
    public virtual Clinic Clinic { get; set; }
    [Key]
    [ForeignKey("Doctor")]
    public long? DoctorId { get; set; }
    public virtual Doctor Doctor { get; set; }

    public bool IsActive { get; set; }

    [ForeignKey("User")]
    public string UserId { get; set; }
    public virtual ApplicationUser User { get; set; }
    public virtual ICollection<Visit> Visits { get; set; } = new List<Visit>();
  }
}
