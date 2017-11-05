using System.ComponentModel.DataAnnotations;


namespace HW1.Models
{
    public class Product
    {
        
        [Required]
        [Key]
        [RegularExpression("^[0-9]{4}$", ErrorMessage = "Please enter exactly 4 digits")]
        public string ProductId { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string ProName { get; set; }

        [Required]
        [RegularExpression("^[1-9][0-9]*", ErrorMessage = "Please enter only numbers to field.")]
        public string ProAmount { get; set; }

        [Required]
        [RegularExpression("^[1-9][0-9]*", ErrorMessage = "Please enter only numbers to field.")]
        public int Price { get; set; }

    }
}