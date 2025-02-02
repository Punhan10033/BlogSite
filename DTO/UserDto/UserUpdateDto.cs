using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace DTO.UserDto
{
    public class UserUpdateDto
    {
        [Key]
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserImage { get; set; }
        [Display(Name = "Biography")]
        [StringLength(150, ErrorMessage = "Maximum 150 charachter")]
        public string Biography { get; set; }
        public int Age { get; set; }
        public DateTime Birth { get; set; }
        public string CountryId { get; set; }
        public Country Country { get; set; }
        public string PhoneNumber { get; set; }
        public virtual ICollection<Message2> UserSender { get; set; }
        public virtual ICollection<Message2> UserReceiever { get; set; }

    }
}
