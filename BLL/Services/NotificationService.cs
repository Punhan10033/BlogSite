using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.IServices;
using DAL.IUnitOfWork1;
using Entities;
using Microsoft.EntityFrameworkCore.Storage.Internal;

namespace BLL.Services
{
	public class NotificationService : INotificationService
	{
		private readonly IUnitOfWork _unitOfWork;
		public NotificationService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public async Task Add(Notification notification)
		{
			await _unitOfWork._notificationRepository.AddNotification(notification);
			await _unitOfWork.Commit();
		}

		public async Task Delete(int Id)
		{
			await _unitOfWork._notificationRepository.DeleteNotification(Id);
			await _unitOfWork.Commit();
		}

		public async Task<List<Notification>> GetAll(int id)
		{  
			return await _unitOfWork._notificationRepository.GetNotifications(id);
		}

		public async Task NotificationSeen(int Id)
		{
			await _unitOfWork._notificationRepository.Seen(Id);
			await _unitOfWork.Commit();
		}
	}
}
