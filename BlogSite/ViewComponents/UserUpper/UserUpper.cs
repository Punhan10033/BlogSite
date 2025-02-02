using DTO.UserDto;
using System.Linq;
using System;
using Microsoft.AspNetCore.Mvc;
using BLL.IServices;
using System.Threading.Tasks;
using System.Security.Claims;

namespace BlogSite.ViewComponents.UserUpper
{
	public class UserUpper : ViewComponent
	{
		private readonly IUserService _service;
		public UserUpper(IUserService service)
		{
			_service = service;
		}
		public async Task<IViewComponentResult> InvokeAsync(int Id)
		{
			UserUpdateDto userUpdateDto = new UserUpdateDto();
			int loggeduserid;
			bool d = int.TryParse(HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value, out loggeduserid);
			if (Id == 0 || loggeduserid == Id)
			{
				ViewBag.LoggedUser = true;
				userUpdateDto = await _service.User(loggeduserid);
			}
			else
			{
				ViewBag.LoggedUser = false;
				userUpdateDto = await _service.User(Id);
				ViewBag.CheckFriendShip = await _service.CheckFriendShip(loggeduserid, Id);
			}
			if (String.IsNullOrEmpty(userUpdateDto.UserImage))
			{
				userUpdateDto.UserImage = "cdc32244-f3f1-48f9-9675-621bfee4933b_IMG_20220606_143422.jpg";
			}
			return View(userUpdateDto);
		}

	}
}
