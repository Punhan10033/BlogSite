using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Net.Mail;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.WebPages;
using BLL.IServices;
using BLL.Validator;
using BlogSite.Cyrpto;
using DAL.DataContext;
using DTO.UserDto;
using Entities;
using FluentValidation;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ValidationException = FluentValidation.ValidationException;

namespace BlogSite.Controllers
{
    public class AuthorizationController : Controller
    {
        private readonly IUserService _userService;
        public AuthorizationController(IUserService userService)
        {
            _userService = userService;
        }
        #region REGiSTER
        //REGISTER
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Registration(string returnUrl)
        {
            bool b = await _userService.CheckIfUserExist(User.Identity.Name);
            var Countries = await _userService.Countries();
            List<Country> countries = await _userService.Countries();
            ViewBag.Countries = new SelectList(countries, "CountryId", "CountryName");
            if (b)
            {
                return RedirectToAction("Index", "Blog");
            }
            else
            {
                ViewBag.ReturnUrl = returnUrl ?? Url.Action("Index", "Blog");
                return View();
            }
         ;
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registration(RegistrationDto model)
        {
            bool Status = false;
            string message = "";
            List<Country> countries = await _userService.Countries();
            ViewBag.Countries = new SelectList(countries, "CountryId", "CountryName");
            if (ModelState.IsValid)
            {
                if (await VerifyEmail(model.Authentication.Email))
                {
                    #region if email is in database
                    var isExist = await EmailExsist(model.Authentication.Email);
                    if (isExist)
                    {
                        ModelState.AddModelError("EmailExist", "Email already Exist");
                        ViewBag.ErorrMessage = "Email already Exist";
                        return View(model);
                    }
                    else
                    {
                        #region password hashing
                        model.Authentication.Password = Cyrpto1.Hash(model.Authentication.Password);
                        model.Authentication.ConfirmPassword = Cyrpto1.Hash(model.Authentication.ConfirmPassword);
                        #endregion

                        #region Check User's Age and creating user
                        var currentDate = DateTime.Now;
                        var UserBirth = model.Birth;
                        if (currentDate.Month >= UserBirth.Month)
                        {
                            model.Age = currentDate.Year - UserBirth.Year;
                        }
                        else
                        {
                            model.Age = currentDate.Year - UserBirth.Year - 1;
                        }

                        #endregion

                        try
                        {
                            model.UserImage = "defaultpic.jpg";

                            await _userService.Create(model);
							return RedirectToAction("Index", "Blog");
						}
						catch(ValidationException ex)
                        {
							foreach (var error in ex.Errors)
							{
								ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
							}

							return View(model);
						}

                    }
                }
                else
                {
                    ModelState.AddModelError("Email Valid", "Email is not valid");
                    ViewBag.ErorrMessage = "Email is not valid";
                    return View(model);
                }

                #endregion
            }
            else
            {
                message = "Invalid Request";
            }
            return View(model);
        }
        //[NonAction]
        //public async Task<bool> isEmailExsist(string Email)
        //{
        //    var users = await _userService.GetUsers();
        //    var v = users.Where(x => x.Email.ToLower() == Email.ToLower()).FirstOrDefault();
        //    return v != null;
        //}


        [NonAction]
        [HttpPost]
        //Check if email is valid to register 
        public async Task<bool> VerifyEmail(string email)
        {
            var apiKey = "d024e18eb35fbd7dc2505dd4966612c252ccbaf3";
            var client = new HttpClient();
            var response = await client.GetAsync($"https://api.hunter.io/v2/email-verifier?email={email.ToLower()}&api_key={apiKey}");
            var json = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<dynamic>(json);
            var result = data.data.result;
            return result == "deliverable";
        }

        [NonAction]
        public async Task<bool> EmailExsist(string Email)
        {
            var emails = await _userService.Emails();
            if (emails.Contains(Email.ToLower()))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        //REGISTER
        #endregion REGISTER
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string returnUrl)
        {
            string email = User.Identity.Name;
            if (await _userService.CheckIfUserExist(email.ToLower()))
            {
                return RedirectToAction("Index", "Blog");
            }
            ViewBag.ReturnUrl = returnUrl ?? Url.Action("Index", "Blog");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Login(UserLoginDto user)
        {

            var userauthentications = await _userService.Get();
            var users3 = userauthentications.Where(x => x.Email.ToLower() == user.Authentication.Email.ToLower()).FirstOrDefault();
            
            if (users3 != null)
            {
				var id = await _userService.IdOfLoggedUser(users3.Email);
				var result = users3.Password.CompareTo(Cyrpto1.Hash(user.Authentication.Password));
                if (result == 0)
                {
                    //string authenticationScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    //// Generate Claims from DbEntity
                    //var claims = new List<Claim>
                    //{
                    //new Claim(ClaimTypes.Name,user.Authentication.Email),
                    //new Claim(ClaimTypes.Email,user.Authentication.Email),

                    //};

                    //ClaimsIdentity claimsIdentity = new ClaimsIdentity(
                    //        claims, authenticationScheme);

                    //ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(
                    //        claimsIdentity);

                    //var authProperties = new AuthenticationProperties
                    //{
                    //    // AllowRefresh = <bool>,
                    //    // Refreshing the authentication session should be allowed.
                    //    // ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
                    //    // The time at which the authentication ticket expires. A 
                    //    // value set here overrides the ExpireTimeSpan option of 
                    //    // CookieAuthenticationOptions set with AddCookie.
                    //    IsPersistent = true,
                    //    // Whether the authentication session is persisted across 
                    //    // multiple requests. Required when setting the 
                    //    // ExpireTimeSpan option of CookieAuthenticationOptions 
                    //    // set with AddCookie. Also required when setting 
                    //    // ExpiresUtc.
                    //    // IssuedUtc = <DateTimeOffset>,
                    //    // The time at which the authentication ticket was issued.
                    //    //RedirectUri = "~/Blog/Index"
                    //    // The full path or absolute URI to be used as an http 
                    //    // redirect response value.
                    //};
                    var claims = new List<Claim>
                     {
                     new Claim(ClaimTypes.Name, user.Authentication.Email),
                     new Claim(ClaimTypes.NameIdentifier,id.ToString()),
                     };
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                    return RedirectToAction("Index", "Blog");
                }
                else
                {
                    ModelState.AddModelError("", "Incorrect Login Credentials. Try Again !");
                    return View();
                }
            }
            else
            {
                ModelState.AddModelError("", "Incorrect Login Credentials. Try Again !");
                return View();
            }

        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Blog");
        }
    }
}
