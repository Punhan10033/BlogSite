using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Security.Claims;
using System.Threading.Tasks;
using BLL.IServices;
using BLL.Services;
using DTO.UserDto;
using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.View;
using Newtonsoft.Json;
using System.Windows;
using Microsoft.CodeAnalysis;
using System.Web.Helpers;
using DTO.LikeDTO;

namespace BlogSite.Controllers
{
    public class UserDefaultController : Controller
    {
        private readonly IUserService _service;
        private readonly IMessageService _messageService;
        private readonly IBlogService _blogService;
        public UserDefaultController(IMessageService messages, IUserService user, IBlogService blogService)
        {
            _service = user;
            _messageService = messages;
            _blogService = blogService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int Id)
        {
            UserUpdateDto userUpdateDto = new UserUpdateDto();
            int loggeduserid;
            bool d = int.TryParse(HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value, out loggeduserid);
            if (loggeduserid == Id || Id == 0)
            {
                userUpdateDto = await _service.User(loggeduserid);
            }
            else
            {
                userUpdateDto = await _service.User(Id);
            }
            if (String.IsNullOrEmpty(userUpdateDto.UserImage))
            {
                userUpdateDto.UserImage = "cdc32244-f3f1-48f9-9675-621bfee4933b_IMG_20220606_143422.jpg";
            }
            return View(userUpdateDto);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> SendRequest(int Id)
        {
            if (await _service.CheckIfUserExist(HttpContext.User.Identity.Name))
            {
                int sender = int.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value);
                if (await _service.CheckIfUserExist(HttpContext.User.Identity.Name))
                {
                    var friendrequestcheck = await _service.FriendRequests();
                    if (!friendrequestcheck.Any(x => x.SenderId == sender && x.ReceieverId == Id))
                    {
                        FriendRequest friendRequest = new FriendRequest()
                        {
                            SenderId = sender,
                            ReceieverId = Id,
                        };

                        await _service.SendRequest(friendRequest);
                    }
                    else
                    {
                        ModelState.AddModelError("FriendRequest", "Already sent request to this person");
                    }

                }
                bool b = await _service.CheckFriendShip(sender, Id);
                ViewBag.CheckFriendShip = b;
                ViewBag.LoggedUser = false;
                var user = await _service.User(Id);
                if (String.IsNullOrEmpty(user.UserImage))
                {
                    user.UserImage = "cdc32244-f3f1-48f9-9675-621bfee4933b_IMG_20220606_143422.jpg";
                }
                return View("Index", user);
            }
            else
            {
                return RedirectToAction("Login", "Authorization");
            }

        }

        public async Task<IActionResult> CancelFriendRequest(int Id)
        {
            int sender = int.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value);
            if (Id != 0)
            {
                await _service.CancelRequest(Id, sender);
            }
            bool b = await _service.CheckFriendShip(sender, Id);
            ViewBag.CheckFriendShip = b;
            ViewBag.LoggedUser = false;
            return RedirectToAction("Index", new { Id = Id });
        }


        [HttpGet]

        public async Task<IActionResult> UpdateUser(int Id)
        {
            int d;
            bool checkifuserlogged = int.TryParse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value, out d);
            if (checkifuserlogged)
            {
                if (d != Id)
                {
                    ModelState.AddModelError("", "This is not your profile. You can't edit this profile");
                    return View();
                }
                else
                {
                    List<Country> countries = await _service.Countries();
                    ViewBag.Countries = new SelectList(countries, "CountryId", "CountryName");
                    UserUpdateDto userUpdateDto = await _service.User(Id);
                    return View(userUpdateDto);
                }
            }
            else
            {
                List<Country> countries = await _service.Countries();
                ViewBag.Countries = new SelectList(countries, "CountryId", "CountryName");
                UserUpdateDto userUpdateDto = await _service.User(Id);
                return View(userUpdateDto);
            }

        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(UserUpdateDto userUpdateDto, IFormFile FileUpload)
        {
            List<Country> countries = await _service.Countries();
            ViewBag.Countries = new SelectList(countries, "CountryId", "CountryName");
            ViewBag.Countries1 = countries;
            string json1 = JsonConvert.SerializeObject(countries);
            ViewBag.Json = json1;
            if (FileUpload != null)
            {
                #region Check Old Image Before Updating
                var image = _service.ImgOfUser(userUpdateDto.UserId);
                FileInfo file = new FileInfo($"wwwroot/images/{image}");
                if (file.Exists)
                {
                    file.Delete();
                }
                #endregion
                var uniqueFileName = Guid.NewGuid().ToString() + "_" + FileUpload.FileName;
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/userimages", uniqueFileName);
                var stream = new FileStream(path, FileMode.Create);
                await FileUpload.CopyToAsync(stream);
                userUpdateDto.UserImage = uniqueFileName;
                stream.Close();
                await _service.Update(userUpdateDto);
            }
            else
            {
                await _service.UpdateWithouthImage(userUpdateDto);
            }

            return RedirectToAction("Index", userUpdateDto);
        }

        public async Task<IActionResult> Chat(int Id)
        {
            int loggeduserid;
            bool b = int.TryParse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value, out loggeduserid);
            UserToListDto dto = new UserToListDto();
            if (loggeduserid != 0)
            {
                ViewBag.SenderId = loggeduserid;
                if (Id != 0)
                {
                    dto = await _messageService.UserChat(loggeduserid, Id);
                    foreach (var item in dto.UserSender)
                    {
                        await _messageService.Seen(item.MessageId);
                    }
                    ViewBag.ReceieverId = Id;
                }
                return View(dto);
            }
            else
            {
                return RedirectToAction("Login", "Authorization");
            }

        }


        public async Task<IActionResult> ReloadChat(int Id)
        {
            int loggeduserid;
            bool b = int.TryParse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value, out loggeduserid);
            UserToListDto dto = new UserToListDto();
            if (loggeduserid != 0)
            {
                if (Id != 0)
                {
                    dto = await _messageService.UserChat(loggeduserid, Id);
                }
            }
            return PartialView("_ChatPartialView", dto);
        }




        public async Task<IActionResult> RefreshChatComponent(string filter)
        {
            int loggedid;
            bool checklogged = int.TryParse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value, out loggedid);
            List<UserToListDto> users = new List<UserToListDto>();
            if (checklogged)
            {
                if (!string.IsNullOrEmpty(filter))
                {
                    users = await _service.GetUserFiltered(filter);
                }
                else
                {
                    users = await _service.GetInteractionsAndFriends(loggedid);
                }
            }
            return PartialView("~/Views/Shared/Components/FriendBar/Default.cshtml", users);
        }


        [HttpGet]
        public async Task<IActionResult> CountryByName(int Id)
        {
            if (Id != 0)
            {
                var country = await _service.CountryByName(Id);
                return Json(country);
            }
            else
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LikeBlog(int BlogId)
        {
            int loggeduserid;
            int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out loggeduserid);

            if (loggeduserid != 0)
            {
                LikeToAddDto likeToAddDto = new LikeToAddDto
                {
                    LikeSender = loggeduserid,
                    BlogId = BlogId
                };

                try
                {
                    // Await the Like method to ensure it completes
                    await _service.Like(likeToAddDto);

                    // Get the updated list of likes
                    var data = await _blogService.GetLikes(loggeduserid);
                    return Json(new { success = true, likes = data });
                }
                catch (Exception ex)
                {
                    // Log the error and return a failure response
                    // You could use a logging framework or console for logging
                    Console.WriteLine($"Error while liking blog: {ex.Message}");
                    return Json(new { success = false, message = "An error occurred while liking the blog." });
                }
            }
            else
            {
                // Redirect to the login page if user is not logged in
                return RedirectToAction("Login", "Authorization");
            }

        }

    }
}
