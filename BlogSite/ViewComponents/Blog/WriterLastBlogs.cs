using System.Threading.Tasks;
using BLL.IServices;
using Microsoft.AspNetCore.Mvc;

namespace BlogSite.ViewComponents.Blog
{
    public class WriterLastBlogs:ViewComponent
    {
        private IBlogService _service;
        public WriterLastBlogs(IBlogService blogService)
        {
            _service=blogService;
        }

        public  IViewComponentResult Invoke(int Id)
        {
            var values = _service.GetBlogsByWriter(Id);
            return View(values);
        }
    }
}
