using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO.UserDto;
using Entities;
using FluentValidation;

namespace BLL.Validator
{
    public class RegistrationValidator:AbstractValidator<RegistrationDto>
    {
        public RegistrationValidator()
        {
            RuleFor(user=>user.FirstName).NotNull().Length(2,50);
            RuleFor(user => user.LastName).NotNull().Length(2,50);
            RuleFor(user => user.Birth).NotNull();

        }
    }
}
