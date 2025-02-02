using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
	public class FriendRequest
	{
		[Key]
		public int Id { get; set; }
		public int SenderId { get; set; }
		public int ReceieverId { get; set; }
		public bool IsAccepted { get; set; }
	}
}
