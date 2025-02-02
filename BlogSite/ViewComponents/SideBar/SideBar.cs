using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BLL.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace BlogSite.ViewComponents.SideBar
{
    public class SideBar:ViewComponent
    {
        private readonly IUserService _service;
        private readonly IMessageService _messageService;
        public SideBar(IUserService service, IMessageService messageservice)
        {
            _service= service;
			_messageService = messageservice;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
			int loggeduserid;
			bool d = int.TryParse(HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value, out loggeduserid); 
            var messagescounts = _messageService.UnreadCountByLogged(loggeduserid);
			if (loggeduserid != 0)
            {
				if (messagescounts > 0)
				{
					ViewBag.MessageCount = messagescounts;
				}
				var user = await _service.User(loggeduserid);
				return View(user);

			}
			return View();
        }
    }
}
