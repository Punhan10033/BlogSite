using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using DAL.DataContext;
using DAL.iRepository;
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace DAL.Repository
{
	public class MessageRepository : IMessageRepository
	{
		private readonly BlogContext _context;
		public MessageRepository(BlogContext blogContext)
		{
			_context= blogContext;
		}

        public async Task AddMessage(Message2 message)
        {
			await _context.Messages2.AddAsync(message);
        }

		public async Task<User>Chat(int receieverId)
		{
			return await _context.Users.FindAsync(receieverId);
		}

		public async Task<List<Message2>> ChatofUsers(int loggedId, int receieverId)
		{
			//var chat=await _context.Messages2.Include(x=>x.MessageSender).ThenInclude(x=>x.UserSender).Include(x=>x.MessageReceiever)
			//	.Where(x=>x.SenderId==loggedId && x.ReceieverId==receieverId ||
			//x.SenderId==receieverId && x.ReceieverId==loggedId).ToListAsync();
			var chat2 = await _context.Messages2.Where(x => x.SenderId == loggedId && x.ReceieverId == receieverId ||
			x.SenderId == receieverId && x.ReceieverId == loggedId).ToListAsync();
			return chat2;
		}

		public async Task<List<Message2>> GetLoggedUserInbox(int Id)
        {
			return await _context.Messages2.Include(x=>x.MessageSender).Include(x=>x.MessageReceiever).Where(x=>x.ReceieverId== Id).ToListAsync();
        }

        public async Task<List<Message>> GetMessages()
		{
			return await _context.Messages.ToListAsync();
		}

		public async Task Seen(int Id)
		{
			var message=await _context.Messages2.FindAsync(Id);
			message.MessageStatus = true;
			_context.Entry(message).Property(x=>x.MessageStatus).IsModified = true; 
		}

		public int UnreadMessageCountByLoggedUser(int Id)
		{
			return _context.Messages2.Where(x=>x.ReceieverId== Id).Count();
		}

		public async Task<User> UsersChat(int loggedId, int receieverId)
		{
			//var user =await _context.Users.Where(x=>x.UserId==receieverId).Include(x=>x.UserSender).Include(x=>x.UserReceiever).FirstOrDefaultAsync();
			var user = await _context.Users.Where(x => x.UserId == receieverId).FirstOrDefaultAsync();
			user.UserReceiever = await _context.Messages2.Where(x => x.ReceieverId == receieverId && x.SenderId == loggedId).ToListAsync();
			user.UserSender = await _context.Messages2.Where(x => x.SenderId == receieverId && x.ReceieverId == loggedId).ToListAsync();
			
			return user;
		}
	}
}
