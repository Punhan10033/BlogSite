using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace BLL.IServices
{
	public interface INotificationService
	{
		Task Add(Notification notification);
		Task Delete(int Id);
		Task<List<Notification>> GetAll(int Id);
		Task NotificationSeen (int Id);
	}
}
