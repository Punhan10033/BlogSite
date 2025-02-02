using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BLL.IServices;
using DTO.UserDto;
using Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlogSite.ViewComponents
{
    public class LogOrReg : ViewComponent
    {
        private readonly IUserService userService;
        public LogOrReg(IUserService _userService)
        {
            userService = _userService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var username = User.Identity.Name;
            bool b=await userService.CheckIfUserExist(username);
            ViewBag.UserName = b;
            return View();
        }
    }
}
