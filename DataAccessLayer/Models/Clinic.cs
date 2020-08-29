using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccessLayer.Models
{
  public class Clinic
  {
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public long Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public DateTime StartDate { get; set; }
    [ForeignKey("User")]
    public string UserId { get; set; }
    public virtual ApplicationUser User { get; set; }
    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
  }
}
