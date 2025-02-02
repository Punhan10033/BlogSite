using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class BlogImage
    {
        [Key]
        public int Id { get; set; }
        public string PictureName { get; set; }
        public Blog Blog { get; set; }
    }
}
