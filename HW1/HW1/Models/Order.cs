using System;
using System.ComponentModel.DataAnnotations;

namespace HW1.Models
{
    public class Order
    {
        [Required]
        [Key]
        public string OrderNum { get; set; }

        [Required]
        [RegularExpression("^[0-9]{4}$", ErrorMessage = "Please enter exactly 4 digits")]
        public string ProductId { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string ProName { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string BuyerName { get; set; }

        [Required]
        [RegularExpression("^[1-9][0-9]*", ErrorMessage = "Please enter only numbers to field.")]
        public string Quantity { get; set; }

        [Required]
        public DateTime PurchaseDate { get; set; }
    }
}