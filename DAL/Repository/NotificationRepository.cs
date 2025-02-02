using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using DAL.DataContext;
using DAL.iRepository;
using Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repository
{
	public class NotificationRepository : INotificationRepository
	{
		private readonly BlogContext _context;
		public NotificationRepository(BlogContext context)
		{
			_context = context;
		}
		public async Task AddNotification(Notification notification)
		{
			var nf = await _context.Notifications
				.Where(x => x.UserSenderId == notification.UserSenderId && x.UserReceieverId == notification.UserReceieverId)
				.OrderByDescending(x=>x.NotificationDate).FirstOrDefaultAsync();
			if (nf != null && nf.NotificationType=="Message")
			{
				nf.NotificationDate = DateTime.Now;
				_context.Entry(nf).Property(x=>x.NotificationDate).IsModified= true;
			}
			else
			{
				await _context.Notifications.AddAsync(notification);
			}
		}

		public async Task DeleteNotification(int Id)
		{
			_context.Notifications.Remove(await _context.Notifications.FirstOrDefaultAsync(x => x.NotificationId == Id));
		}

		public async Task<List<Notification>> GetNotifications(int Id)
		{
			var notification=await _context.Notifications
				.Where(x=>x.UserReceieverId==Id)
				.OrderByDescending(x=>x.NotificationDate).ToListAsync();
			return notification;
		}

		public async Task Seen(int Id)
		{
			var notification= await _context.Notifications.FindAsync(Id);
			if (notification.NotificationStatus != true)
			{
				notification.NotificationStatus = true;
			}
			_context.Entry(notification).Property(x=>x.NotificationStatus).IsModified=true;
		}
	}
}
