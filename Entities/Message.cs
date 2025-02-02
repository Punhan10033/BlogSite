using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
	public class Message
	{
		public int MessageId { get; set; }
		public string SenderId { get; set; }
		public string ReceieverId { get; set; }
		public string Subject { get; set; }
		public string MessageDetails { get; set; }
		public DateTime MessageDate { get; set; }
		public bool MessageStatus { get; set; }
		public virtual ICollection<Message> UserSender { get; set; }
		public virtual ICollection<Message> UserReceiever { get; set; }

	}

}
