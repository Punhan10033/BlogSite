using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace DTO.UserDto
{
	public class UserToListDto
	{
		public int UserId { get; set; }
		public string Email { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string UserImage { get; set; }
		public virtual ICollection<Message2> UserSender { get; set; }
		public virtual ICollection<Message2> UserReceiever { get; set; }
	}
}
