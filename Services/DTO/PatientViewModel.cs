using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Services.DTO
{
  public  class PatientViewModel
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //[Key]
        public long Id { get; set; }
       //[Required]
        public string FirstName { get; set; }
       //[Required]
        public string LastName { get; set; }
       //[Required]
        public Gender Gender { get; set; }

        //[Required(ErrorMessage = "Phone Number is needed.")]
        //[Display(Name = "Phone")]
        //[DataType(DataType.PhoneNumber)]
        //[RegularExpression(@"^\(?([0-9]{4})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Invalid Phone number")]
        public string PhoneNumber { get; set; }
       //[Required]
        public int Age { get; set; }
    }
}
