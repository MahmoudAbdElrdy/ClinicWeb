using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer
{
  public class RegisterModel
  {
    [Required(ErrorMessage = "User Name is required")]
    public string Username { get; set; }
    [EmailAddress]
    [Required(ErrorMessage = "Email is required")]
    public string Email { get; set; }
    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; }
    [Required(ErrorMessage = "Phone Number is needed.")]
    [Display(Name = "Phone")]
    [DataType(DataType.PhoneNumber)]
    public string PhoneNumber { get; set; }
    public string VerifyCode { get; set; }
    public bool IsActive { get; set; }
    public DateTime? CreatedDate { get; set; }
    public DateTime? ModifiedDate { get; set; }
    [Required]
    public string Address { get; set; }

  }
}
