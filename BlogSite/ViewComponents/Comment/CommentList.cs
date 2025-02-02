using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.IServices;
using BlogSite.Models;
using Entities;
using Microsoft.AspNetCore.Mvc;

namespace BlogSite.Views.ViewComponents
{
    public class CommentList : ViewComponent
    {
        private readonly ICommentService _service;
        public CommentList(ICommentService commentService)
        {
            _service = commentService;
        }
        public IViewComponentResult Invoke(int Id)
        {
            List<Comment> comments = _service.Comments(Id);
            return View(comments);
        }
    }
}
