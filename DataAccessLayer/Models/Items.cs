using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccessLayer.Models
{
  public class Items
  {
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public long Id { get; set; }
    [Required]
    public DateTime StartDate { get; set; }
    [Required]
    public DateTime EndDate { get; set; }
    [Required]
    public string ItemName { get; set; }
    [Required]
    public long Count { get; set; }
    [Required]
    public long ItemPrice { get; set; }

    [Required]
    [ForeignKey("Store")]
    public long? StoreId { get; set; }
    public virtual Store Store { get; set; }
    public virtual ICollection<Bills> Bills { get; set; } = new List<Bills>();
  }
}
