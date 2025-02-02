using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using DAL.DataContext;
using DAL.iRepository;
using DTO.BlogDto;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repository
{
    public class BlogRepository : IBlogRepository
    {
        private readonly BlogContext _context;
        public BlogRepository(BlogContext blogContext)
        {
            _context = blogContext;
        }
        public async Task AddAsync(Blog blog)
        {
            blog.CreatedAt = DateTime.Now;
            await _context.Blogs.AddAsync(blog);
        }

        public async Task Delete(int id)
        {
            _context.Blogs.Remove(await _context.Blogs.Where(x => x.BlogId == id).FirstOrDefaultAsync());
        }

        public async Task<List<Blog>> GetBlogs()
        {
            return await _context.Blogs.Include(x => x.User).Include(x => x.Category).Include(x => x.Comments).ToListAsync();
        }

        public List<Blog> GetBLogsByWriter(int Id)
        {
            return _context.Blogs.Where(x => x.UserId == Id).ToList();
        }

        public async Task<Blog> GetById(int id)
        {
            return await _context.Blogs.Where(x => x.BlogId == id).Include(x=>x.BlogImages).FirstOrDefaultAsync();
        }

        public async Task<ICollection<Like>> GetLikes(int BlogIdd)
        {
            return await _context.Likes.Where(x => x.BlogId == BlogIdd).ToListAsync();
        }

        public async Task<Blog>LastAddedBlog(int UserId)
        {
            var blog=await  _context.Blogs.OrderByDescending(x=>x.CreatedAt).FirstOrDefaultAsync(x=>x.UserId==UserId);
            return blog;
        }

        public async Task<List<Blog>> List(Expression<Func<Blog, bool>> filter)
        {
            return await _context.Blogs.Where(filter).ToListAsync();
        }

        public void Udate(Blog blog)
        {
            _context.Blogs.Update(blog);
        }
    }
}
