using System.ComponentModel.DataAnnotations;


namespace HW1.Models
{
    public class User
    {
        [Required]
        [Key]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Length must be between 2 and 20 characters")]
        public string UserName { get; set;}

        [Required]
        [RegularExpression("^[0-9A-Za-z]{6}$", ErrorMessage = "Please enter exactly 6 characters in password field.")]
        public string Password { get; set; }

        public string FullName { get; set; }

    }
}