using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class UserAuthentication
    {
        [Key]
        public int AuthenticationId { get; set; }
        [Required]
        [Display(Name = "Email Addres")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression("[a-zA-Z@.0-9]+", ErrorMessage = "Only Alphabets and Numbers allowed.")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [StringLength(100, ErrorMessage = "Password must be at least 8 characters long", MinimumLength = 8)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [StringLength(100, ErrorMessage = "Password must be at least 8 characters long", MinimumLength = 8)]
        [Compare("Password", ErrorMessage = "Password and Confirm Password doesn't match")]
        [NotMapped]
        public string ConfirmPassword { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
