using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace DAL.iRepository
{
    public interface IBlogRepository
    {
        Task<List<Blog>> GetBlogs();
        Task AddAsync(Blog blog);
        Task Delete(int id);
        void Udate(Blog blog);
        Task<Blog> GetById(int id);
        Task<List<Blog>> List(Expression<Func<Blog, bool>> filter);
        List<Blog> GetBLogsByWriter(int Id);
        Task<Blog>LastAddedBlog(int UserId);
        Task<ICollection<Like>> GetLikes(int BlogIdd);

    }
}
