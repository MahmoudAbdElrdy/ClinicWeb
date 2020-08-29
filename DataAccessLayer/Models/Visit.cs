using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccessLayer.Models
{
  public class Visit
  {
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public long Id { get; set; }
    public long VisitCode { get; set; }

    public DateTime CreateDate { get; set; }

    [ForeignKey("User")]
    public string UserId { get; set; }
    public virtual ApplicationUser User { get; set; }
    public long PatientId { get; set; }
    public string ExpiresIn { get; set; }
    public Day Day { get; set; }
    public string StartFrom { get; set; }
    public long ClinicId { get; set; }
    public long DoctorId { get; set; }
    [ForeignKey("ExpiresIn,Day,StartFrom,ClinicId,DoctorId")]
    public virtual Appointment Appointment { get; set; }
  }
}
