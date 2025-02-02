using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BLL.IServices;
using DAL.IUnitOfWork1;
using DTO.UserDto;
using Entities;

namespace BLL.Services
{
	public class MessageService : IMessageService
	{
		private readonly IUnitOfWork _work;
		private readonly IMapper _mapper;
		public MessageService(IUnitOfWork work,IMapper mapper)
		{
			_work = work;
			_mapper = mapper;
		}

        public async Task Add(Message2 message)
        {
			message.MessageDate = DateTime.Now;
			await _work._messageRepository.AddMessage(message);
			await _work.Commit();
        }


	
		public async Task<List<Message2>> GetChat(int loggedId, int receieverId)
		{
			return await _work._messageRepository.ChatofUsers(loggedId, receieverId);
		}

		public Task<List<UserToListDto>> GetChatOfUsers(int loggedId)
		{
			throw new NotImplementedException();
		}

		public async Task<List<Message>> GetMessages()
		{
			return await _work._messageRepository.GetMessages();
		}

        public async Task<List<Message2>> LoggedInbox(int Id)
        {
			return await _work._messageRepository.GetLoggedUserInbox(Id);
        }

		public async Task Seen(int Id)
		{
			await _work._messageRepository.Seen(Id);
			await _work.Commit();
		}

		public int UnreadCountByLogged(int Id)
		{
			return _work._messageRepository.UnreadMessageCountByLoggedUser(Id);
		}

		public async Task<UserToListDto> UserChat(int loggedid, int receieverId)
		{
			User user= await _work._messageRepository.UsersChat(loggedid, receieverId);
			UserToListDto dto=_mapper.Map<UserToListDto>(user);
			return dto;
		}
	}
}
