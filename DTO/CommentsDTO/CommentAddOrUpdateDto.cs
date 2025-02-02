using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.CommentsDTO
{
    public class CommentAddOrUpdateDto
    {
        public int? CommentId { get; set; }
        public string CommentUserName { get; set; }
        public string CommentTitle { get; set; }
        public string CommentContent { get; set; }
        public DateTime WritedAt { get; set; }
        public bool Status { get; set; }
        
    }
}
