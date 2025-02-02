using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DTO.BlogDto;
using DTO.LikeDTO;
using DTO.UserDto;
using Entities;

namespace BLL.AutoMapper
{
    public class NewMapper:Profile
    {
        public NewMapper()
        {
            CreateMap<User, UserUpdateDto>();
            CreateMap<UserUpdateDto, User>().ReverseMap();
            CreateMap<Blog,BlogToAddDto>();
            CreateMap<BlogToAddDto, Blog>();

            CreateMap<User,UserUpdateDto>();
            CreateMap<UserUpdateDto, User>();

            CreateMap<LikeToAddDto,Like>();

            CreateMap<User,UserToListDto>().ReverseMap();
        }
    }
}
