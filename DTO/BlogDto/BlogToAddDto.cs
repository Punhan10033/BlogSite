using Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.BlogDto
{
    public class BlogToAddDto
    {
        public int? BlogId { get; set; }
        public string BlogTitle { get; set; }
        public string BlogContent { get; set; }
        public List<BlogImage> BlogImages { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime CreatedAt { get; set; }
        public bool BlogStatus { get; set; }
        public int? UserId { get; set; }
        public User User { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
