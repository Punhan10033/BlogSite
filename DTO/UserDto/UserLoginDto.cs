using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace DTO.UserDto
{
    public class UserLoginDto
    {
        public int Id { get; set; }
       
        public bool RememberMe { get; set; }
        public UserAuthenticationLoginDto Authentication { get; set; }

    }
}
