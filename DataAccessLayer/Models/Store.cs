using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccessLayer.Models
{
  public class Store
  {
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public long Id { get; set; }
    [Required]
    public string StoreName { get; set; }
    public virtual ICollection<Items> Items { get; set; } = new List<Items>();
    public virtual ICollection<Bills> Bills { get; set; } = new List<Bills>();
  }
}
