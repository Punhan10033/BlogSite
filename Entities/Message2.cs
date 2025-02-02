using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
	public class Message2
	{
		[Key]
		public int MessageId { get; set; }
		public int? SenderId { get; set; }
		public int? ReceieverId { get; set; }
		public string Subject { get; set; }
		public string MessageDetails { get; set; }
		public DateTime MessageDate { get; set; }
		public bool MessageStatus { get; set; }
		public User MessageSender { get; set; }
		public User MessageReceiever { get; set; }
	
	}

}
