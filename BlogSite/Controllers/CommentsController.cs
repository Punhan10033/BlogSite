using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Linq;
using BLL.IServices;
using DTO.BlogDto;
using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogSite.Controllers
{
	public class CommentsController : Controller
	{
		private readonly ICommentService _service;
		private readonly IUserService _userService;
		public CommentsController(IUserService userService, ICommentService service)
		{
			_userService = userService;
			_service = service;
		}

		public PartialViewResult CommentListByBlog(int Id)
		{
			var comments = _service.Comments(Id);
			return PartialView(comments);
		}

		[HttpGet]
		public PartialViewResult AddCommentPartial()
		{
			return PartialView();
		}
		[HttpPost]
		public async Task<IActionResult> AddComment(BlogCommentsDto BlogCommentsDto)
		{
			List<Comment> comments;
			if (await _userService.CheckIfUserExist(User.Identity.Name))
			{
				var user = await _userService.GetUserByEmail(User.Identity.Name);
				BlogCommentsDto.Comment.UserId = user.UserId;
				BlogCommentsDto.Comment.Status = true;
				BlogCommentsDto.Comment.BlogId = BlogCommentsDto.Blog.BlogId;
				BlogCommentsDto.Comment.CommentUserName = user.FirstName + " " + user.LastName;
				BlogCommentsDto.Comment.WritedAt = DateTime.Now;
				await _service.Add(BlogCommentsDto.Comment);
				comments = _service.Comments(BlogCommentsDto.Blog.BlogId);
				//var options = new JsonSerializerOptions
				//{
				//	ReferenceHandler = ReferenceHandler.Preserve,
				//	MaxDepth = 64
				//};
				//string json=JsonSerializer.Serialize(comments,options);

				return ViewComponent("CommentList", new { Id = BlogCommentsDto.Blog.BlogId });
			}
			else
			{
				return RedirectToAction("LogIn", "Authorization");
			}
		}
		public async Task<IActionResult> DeleteConfirm(int Id)
		{
			var comment = await _service.GetById(Id);
			var id2 = comment.BlogId;
			await _service.Delete(Id);
			return ViewComponent("CommentList", new { Id = comment.BlogId });
		}

	}
}
