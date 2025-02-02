using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using BLL.IServices;
using DTO.BlogDto;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace BlogSite.Controllers
{
    public class BlogController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IBlogService _blogService;
        public BlogController(ICategoryService category, IBlogService blog)
        {
            _blogService = blog;
            _categoryService = category;
        }
        public IActionResult ViewJustToCheck()
        {
            return View();
        }
        public IActionResult Categories()
        {
            List<Category> caregories = _categoryService.GetAll();
            return View(caregories);
        }
        [Authorize]
        public async Task<IActionResult> Details(int Id)
        {
            BlogCommentsDto dto = new BlogCommentsDto();
            dto.Blog = new Blog();
            dto.Blog = await _blogService.Get(Id);
            return View(dto);
        }
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var values = await _blogService.Get();
            return View(values);
        }

        [HttpGet]
        public async Task<IActionResult> NewBlogOrUpdate(BlogToAddDto blogToAddDto)
        {
            BlogToAddDto blog = new BlogToAddDto();
            int loggeduserid;
            List<Category> categoryList = _categoryService.GetAll();
            SelectList Categories = new SelectList(categoryList, "CategoryId", "CategoryName");
            ViewBag.Categories = Categories;
            bool isParsed = int.TryParse(HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value, out loggeduserid);
            if (loggeduserid != 0)
            {

                if (blogToAddDto.BlogId != null && blog.UserId == loggeduserid)
                {
                    int blogid;
                    int.TryParse(blogToAddDto.BlogId.ToString(), out blogid);
                    Blog blog1 = await _blogService.Get(blogid);
                    blog = new BlogToAddDto()
                    {
                        BlogId = blog1.BlogId,
                        BlogContent = blog1.BlogContent,
                        CategoryId = blog1.CategoryId,
                        BlogTitle = blog1.BlogTitle,
                        BlogStatus = blog1.BlogStatus,
                        BlogImages = blog1.BlogImages
                    };

                }
                //else
                //{

                //    ModelState.AddModelError("", "Error occured. Please Try again !");
                //}
            }
            else
            {
                return RedirectToAction("Login", "Authorization");
            }
            return View(blog);
        }
        public async Task<IActionResult> AddOrUpdateBlog(BlogToAddDto blog, IFormFile[] BlogPictures)
        {
            int loggeduserid;
            bool isParsed = int.TryParse(HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value, out loggeduserid);
            if (loggeduserid != 0)
            {
                if (blog.BlogId != 0 && loggeduserid == blog.UserId)
                {
                    _blogService.Update(blog);
                    var lastadded = await _blogService.LastAddedBlog(loggeduserid);
                    return RedirectToAction("Details", lastadded);
                }
                else
                {

                    var categories = _categoryService.GetAll();
                    SelectList gategorySelectList = new SelectList(categories, "CategoryId", "CategoryName");
                    blog.BlogImages = new List<BlogImage>();
                    foreach (IFormFile pic in BlogPictures)
                    {
                        var uniqueId = Guid.NewGuid();
                        var uniquiename = $"{uniqueId}_{pic.FileName}";
                        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/blogimages", uniquiename);
                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            await pic.CopyToAsync(stream);
                        }
                        blog.BlogImages.Add(new BlogImage { PictureName = uniquiename });

                    }
                    blog.UserId = loggeduserid;
                    await _blogService.AddAsync(blog);
                    var lastadded = await _blogService.LastAddedBlog(loggeduserid);
                    return View("Details", lastadded);
                }
            }
            return RedirectToAction("Login", "Authorization");
        }

        //public async Task<IActionResult> AddOrUpdateBlog(BlogToAddDto blog, IFormFile[] BlogPictures)
        //{
        //    int loggeduserid;
        //    bool isParsed = int.TryParse(HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value, out loggeduserid);
        //    if (loggeduserid != 0)
        //    {
        //        if (blog.BlogId != 0 && loggeduserid == blog.UserId)
        //        {
        //            int blogid;
        //            bool b = int.TryParse(blog.BlogId.ToString(), out blogid);
        //            _blogService.Update(blog);
        //            var lastadded = await _blogService.IdOfTheLastAdded(loggeduserid);
        //            return RedirectToAction("Details", lastadded);
        //        }
        //        else
        //        {
        //            var categories = _categoryService.GetAll();
        //            SelectList gategorySelectList = new SelectList(categories, "CategoryId", "CategoryName");
        //            blog.BlogImages = new List<BlogImage>();
        //            foreach (IFormFile pic in BlogPictures)
        //            {
        //                var uniqueId = Guid.NewGuid();
        //                var uniquiename = $"{uniqueId}_{pic.FileName}";
        //                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/blogimages", uniquiename);
        //                using var stream = new FileStream(path, FileMode.Create);
        //                await pic.CopyToAsync(stream);
        //                blog.BlogImages.Add(new BlogImage { PictureName = uniquiename });
        //            }
        //            blog.UserId = loggeduserid;

        //            try
        //            {
        //                var addResult =  _blogService.Add(blog); // Await the Task returned by Add
        //                var lastadded = await _blogService.IdOfTheLastAdded(loggeduserid);
        //                return View("Details", lastadded); // Success, display details
        //            }
        //            catch (Exception ex)
        //            {
        //                ModelState.AddModelError("", "An error occurred while adding the blog. Please try again.");
        //                // Log the error if necessary, e.g., _logger.LogError(ex, "Error adding blog");
        //                return View(); // Return to the form with an error message
        //            }
        //        }
        //    }
        //    return RedirectToAction("Login", "Authorization");
        //}

    }
}
