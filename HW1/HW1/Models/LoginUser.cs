using System.ComponentModel.DataAnnotations;


namespace HW1.Models
{
    public class LoginUser
    {

        [Required]
        [Key]
        [RegularExpression("^[A-Z0-9a-z._%+-]+@[A-Z0-9a-z.-]+.[A-Za-z]{2,}$", ErrorMessage = "This email is incorrect. Please try again")]
        public string Email { get; set; }

        [Required]
        [RegularExpression("^[0-9A-Za-z]{6}$", ErrorMessage = "Please enter exactly 6 characters in password field.")]
        public string Pass { get; set; }

    }
}