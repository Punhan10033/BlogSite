using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace DAL.iRepository
{
	public interface INotificationRepository
	{
		Task AddNotification(Notification notification);
		Task DeleteNotification(int Id);
		Task<List<Notification>> GetNotifications(int Id);
		Task Seen (int Id);	
	}
}
