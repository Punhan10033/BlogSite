using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        [Required]
        [Display(Name = "First Name ")]
        [RegularExpression("[a-zA-Z\\s]+", ErrorMessage = "Only Alphabets")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last Name ")]
        [RegularExpression("[a-zA-Z\\s]+", ErrorMessage = "Only Alphabets")]
        public string LastName { get; set; }
        [Display(Name = "Biography")]
        [StringLength(150, ErrorMessage = "Maximum 150 charachter")]
        public string Biography { get; set; }
        public string UserImage { get; set; }
        public int Age { get; set; }
        public DateTime Birth { get; set; }
        public string PhoneNumber { get; set; }
        public List<Role> Roles { get; set; }
        public List<Comment> Comments { get; set; }
        public List<Blog> Blogs { get; set; }
        public DateTime JoinedAt { get; set; }
        public UserAuthentication Authentication { get; set; }
        public int CountryId { get; set; }
        public Country Country { get; set; }
        public virtual ICollection<Message2> UserSender { get; set; }
		public virtual ICollection<Message2> UserReceiever { get; set; }
        public virtual ICollection<Notification> NotificationReceiever { get; set; }
        public ICollection<Like>Likes { get; set; }
	}
}
