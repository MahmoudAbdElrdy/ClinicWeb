using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Services.DTO
{
  public  class ItemsViewModel
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
    }
}
