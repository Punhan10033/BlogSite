using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BLL.IServices;
using Microsoft.AspNetCore.Mvc;

namespace BlogSite.ViewComponents.NotificationList
{
	public class NotificationList:ViewComponent
	{
        private readonly INotificationService _service;
        public NotificationList(INotificationService service)
        {
            _service= service;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            int id=int.Parse(HttpContext.User.Claims.FirstOrDefault(x=>x.Type==ClaimTypes.NameIdentifier)?.Value);
            var values=await _service.GetAll(id);
            return View(values);
        }
    }
}
