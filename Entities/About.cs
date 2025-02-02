using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class About
    {
        [Key]
        public int AboutId { get; set; }
        public string AboutdDetails { get; set; }
        public string AboutdDetails2 { get; set; }
        public string AboutImage { get; set; }
        public string AboutImage2 { get; set; }
        public string AboutMapLocation { get; set; }
        public bool AboutStatus { get; set; }

    }
}
