using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Like
    {
        public int Id { get; set; }
        public int BlogId { get; set; }
        public Blog Blog { get; set; }
        public int LikeSender { get; set; }
        public User User { get; set; }
        public DateTime LikeDate { get; set; }
    }
}
