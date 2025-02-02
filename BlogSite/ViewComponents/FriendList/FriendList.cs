using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BLL.IServices;
using DTO.UserDto;
using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogSite.ViewComponents.UserProfile
{
	public class FriendList:ViewComponent
	{
        private readonly IUserService _service;

        public FriendList( IUserService service)
		{
            _service=service;
		}
        [HttpGet]
        public async Task<IViewComponentResult> InvokeAsync(int UserId)
        {
            List<UserUpdateDto> friends=new List<UserUpdateDto>();
            if (UserId != 0)
			{
                 friends=await _service.GetFriendsofUser(UserId);
			}
            return View(friends);
        }
    }
}
