using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Services;
using DTO.UserDto;
using Entities;

namespace BLL.IServices
{
	public interface IMessageService
	{
		Task<List<Message>> GetMessages();
		int UnreadCountByLogged(int Id);
		Task<List<Message2>> LoggedInbox(int Id);
		Task<List<Message2>> GetChat(int loggedId, int receieverId);
		Task<List<UserToListDto>> GetChatOfUsers(int loggedId);
		Task<UserToListDto> UserChat(int loggedid, int receieverId);
		Task Add(Message2 message);
		Task Seen(int Id);
	}
}
