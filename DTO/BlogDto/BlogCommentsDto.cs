using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace DTO.BlogDto
{
    public class BlogCommentsDto
    {
        public Blog Blog { get; set; }
        public Comment Comment { get; set; }
    }
}
