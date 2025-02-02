using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace DAL.iRepository
{
	public interface IMessageRepository
	{
		Task<List<Message>> GetMessages();
		int UnreadMessageCountByLoggedUser(int Id);
		Task<List<Message2>> GetLoggedUserInbox(int Id);
		Task<List<Message2>> ChatofUsers(int loggedId, int receieverId);
		Task<User> UsersChat(int loggedId, int receieverId);
		Task AddMessage(Message2 message);
		Task Seen(int Id);
	}
}
