using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace DTO.UserDto
{
    public class UserAuthenticationLoginDto
    {
        [Key]
        public int AuthenticationId { get; set; }
        [Required]
        [Display(Name = "Email Addres")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
        public int UserId { get; set; }
        public User user { get; set; }
    }
}
