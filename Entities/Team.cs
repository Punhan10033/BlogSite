using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
	public class Team
	{
		public int TeamId { get; set; }
		public string TeamName { get; set; }
		public virtual ICollection<Match> HomeMatches { get; set; }
		public virtual ICollection<Match> AwayMatches { get; set; }
	}
}
