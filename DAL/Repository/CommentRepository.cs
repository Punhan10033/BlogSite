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
    public class CommentRepository : ICommentRepository
    {
        private readonly BlogContext _context;
        public CommentRepository(BlogContext context)
        {
            _context = context;
        }
        public async Task Add(Comment comment)
        {
            await _context.Comments.AddAsync(comment);
        }

        public async Task Delete(int Id)
        {
            _context.Comments.Remove(await _context.Comments.Where(x => x.CommentId == Id).FirstOrDefaultAsync());
        }

        public async Task<Comment> Get(int Id)
        {
            return await _context.Comments.Include(x=>x.Blog).Where(x => x.CommentId == Id).FirstOrDefaultAsync();
        }

        public  List<Comment> GetComments(int Id)
        {
            return  _context.Comments.Include(x=>x.User).ThenInclude(x=>x.Authentication).Where(x => x.BlogId == Id).OrderByDescending(x => x.WritedAt).ToList();
        }

        public void Update(Comment comment)
        {
            _context.Comments.Update(comment);
        }
    }
}
