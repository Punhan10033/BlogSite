using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.IServices;
using Entities;
using Microsoft.AspNetCore.Mvc;

namespace BlogSite.ViewComponents.Categories
{
    public class CategoryList : ViewComponent
    {
        private readonly ICategoryService _service;
        public CategoryList(ICategoryService categoryService)
        {
            _service = categoryService;
        }
        public IViewComponentResult Invoke()
        {
            List<Category> values = _service.GetAll();
            return View(values);
        }
    }
}
