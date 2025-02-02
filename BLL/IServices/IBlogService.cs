using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DTO.BlogDto;
using Entities;

namespace BLL.IServices
{
    public interface IBlogService
    {
        Task AddAsync(BlogToAddDto blog);
        void Update(BlogToAddDto blog);
        Task Delete(int Id);
        Task<List<Blog>> Get();
        Task<Blog>Get(int Id);
        List<Blog> GetBlogsByWriter(int Id);
        Task<BlogCommentsDto> LastAddedBlog(int UserId);
        Task <ICollection<Like>> GetLikes(int BlogId);
    }
}
