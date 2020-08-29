using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;


namespace DataAccessLayer.Models
{
  public class Doctor
  {
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public long Id { get; set; }
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }
    [Required]
    public string Address { get; set; }

    [DataType(DataType.EmailAddress)]
    [EmailAddress]
    public string Email { get; set; }
    [Required(ErrorMessage = "Phone Number is needed.")]
    [Display(Name = "Phone")]
    [DataType(DataType.PhoneNumber)]
    [RegularExpression(@"^\(?([0-9]{4})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Invalid Phone number")]
    public string PhoneNumber { get; set; }

    public string VerifyCode { get; set; }

    public string Password { get; set; }
    [Required]
    public bool IsActive { get; set; }
    public byte[] image { get; set; }
    [Required]
    public int hourPrice { get; set; }
    [Required]
    public int AppointmentPrice { get; set; }
    [Required]

    public string Title { get; set; }


    [ForeignKey("User")]
    public string UserId { get; set; }
    public virtual ApplicationUser User { get; set; }
    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

  }
}
