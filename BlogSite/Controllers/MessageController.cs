using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Security.Claims;
using System.Threading.Tasks;
using BLL.IServices;
using BLL.Services;
using DTO.UserDto;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace BlogSite.Controllers
{
	public class MessageController : Controller
	{
		private readonly IMessageService _messageservice;
		private readonly IUserService _userservice;
		private readonly INotificationService _nfservice;
		public MessageController(IMessageService service, IUserService user, INotificationService nfservice)
		{
			_messageservice = service;
			_userservice = user;
			_nfservice = nfservice;
		}

		[HttpPost]
		public async Task<IActionResult> SendMessage(Message2 message2)
		{
			int loggedid;
			bool b = int.TryParse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value, out loggedid);
			UserToListDto dto = new UserToListDto();
			int senderid = message2.ReceieverId ?? 0;
		
			if (b && message2 != null && loggedid != message2.ReceieverId)
			{
				message2.SenderId = loggedid;
				await _messageservice.Add(message2);
				dto = await _messageservice.UserChat(loggedid, senderid);
				var user = await _userservice.User(loggedid);
				if (user != null)
				{
					Notification nf = new Notification()
					{
						NotificationType = "Message",
						UserSenderId = user.UserId,
						NotificationDate = DateTime.Now,
						NotificationStatus = false,
						NotificationDetails = $"{user.FirstName} sent you message",
						NotificationTypeSymbol = "Message",
					};
					if (message2.ReceieverId != null)
					{
						nf.UserReceieverId = message2.ReceieverId.Value;
					}
					await _nfservice.Add(nf);
				}
				
			}
			return PartialView("_ChatPartialView",dto);
		}


		public async Task<IActionResult> GetChat(int receieverId)
		{
			int loggedid;
			bool b = int.TryParse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value, out loggedid);
			UserToListDto dto = new UserToListDto();
			if (b && receieverId != 0)
			{
				dto = await _messageservice.UserChat(loggedid, receieverId);
				return PartialView("_ChatPartialView", dto);
			}
			else
			{
				ModelState.AddModelError("", "Some error occured");
				return PartialView("_ChatPartialView");
			}
		}


	



	}
}
