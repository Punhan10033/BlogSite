using System;
using System.ComponentModel.DataAnnotations;
using Entities;

namespace DTO.UserDto
{
    public class RegistrationDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [DataType(DataType.EmailAddress)]
        public string UserImage { get; set; }
        public int Age { get; set; }
        public DateTime Birth { get; set; }
        public int CountryId { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime JoinedAt { get; set; }
        public UserAuthentication Authentication { get; set; }
    }
}
