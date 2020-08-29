using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccessLayer.Models
{
 public class Bills
  {
    public long Id { get; set; }
    public BillType BillTypeId { get; set; }
    public decimal? PurchasePrice { get; set; }
    public DateTime? PurchaseTime { get; set; }
    public decimal? SalePrice { get; set; }
    public DateTime? SaleTime { get; set; }
    public string ClientName { get; set; }
    [Required]
    [ForeignKey("Store")]
    public long? StoreId { get; set; }
    public virtual Store Store { get; set; }
    [Required]
    [ForeignKey("Items")]
    public long? ItemId { get; set; }
    public virtual Items Items { get; set; }
  }
}
