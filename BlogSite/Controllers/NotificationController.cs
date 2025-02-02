using System.Threading.Tasks;
using BLL.IServices;
using Microsoft.AspNetCore.Mvc;

namespace BlogSite.Controllers
{
	public class NotificationController : Controller
	{
		private readonly INotificationService _notificationService;
		public NotificationController(INotificationService notificationService)
		{
			_notificationService = notificationService;
		}

		public IActionResult Index()
		{
			return View();
		}


		[HttpPut]
		public async Task<IActionResult> Seen(int Id)
		{
			if (Id != 0)
			{
				await _notificationService.NotificationSeen(Id);
			}
			return NoContent();
		}

	}
}
