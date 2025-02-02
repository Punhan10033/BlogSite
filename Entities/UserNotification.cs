using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
	public class UserNotification
	{
		public int Id { get; set; }
		public int SenderId { get; set; }
		public int ReceieverId { get; set; }
	}
}
