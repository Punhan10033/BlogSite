using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.LikeDTO
{
    public class LikeToAddDto
    {
        public int Id { get; set; }
        public int LikeSender {  get; set; }
        public int BlogId {  get; set; }
    }
}
