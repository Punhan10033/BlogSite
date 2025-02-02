using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DataContext;
using DAL.iRepository;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly BlogContext _context;
        public CategoryRepository(BlogContext blog)
        {
            _context = blog;
        }
        public async Task Add(Category category)
        {
            await _context.AddAsync(category);
        }

        public List<Category> Categories()
        {
            return _context.Categories.Include(x=>x.Blogs).ToList();
        }

        public async Task Delete(int id)
        {
            _context.Categories.Remove(_context.Categories.Where(x => x.CategoryId == id).FirstOrDefault());
        }

        public async Task<Category> Get(int id)
        {
            return await _context.Categories.Where(x => x.CategoryId == id).FirstOrDefaultAsync();
        }

        public void Update(Category category)
        {
            _context.Categories.Update(category);
        }
    }
}
