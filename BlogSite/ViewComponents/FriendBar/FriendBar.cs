using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BLL.IServices;
using DTO.UserDto;
using Microsoft.AspNetCore.Mvc;

namespace BlogSite.ViewComponents.FriendBar
{
	public class FriendBar : ViewComponent
	{
		private readonly IUserService _service;
		//private readonly IMessageService _messageService;
		public FriendBar(IUserService userService )
		{
			_service = userService;
		}
		public async Task<IViewComponentResult> InvokeAsync()
		{
			int loggeduser;
			bool b = int.TryParse(HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.
				Value, out loggeduser);
			List<UserToListDto> users = new List<UserToListDto>();
			users = await _service.GetInteractionsAndFriends(loggeduser);
			//var messages = await _messageService.LoggedInbox(loggeduser);
			return View(users);
		}
	}
}
