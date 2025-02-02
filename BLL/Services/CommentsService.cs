using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.IServices;
using DAL.IUnitOfWork1;
using Entities;

namespace BLL.Services
{
    public class CommentsService : ICommentService
    {
        private readonly IUnitOfWork _work;
        public CommentsService(IUnitOfWork work)
        {
            _work = work;
        }
        public async Task Add(Comment comment)
        {
            await _work._commentRepository.Add(comment);
            await _work.Commit();
        }

        public async Task Delete(int Id)
        {
            await _work._commentRepository.Delete(Id);
            await _work.Commit();
        }

        public  List<Comment> Comments(int Id)
        {
            return  _work._commentRepository.GetComments(Id);
        }

        public async Task<Comment> GetById(int id)
        {
            return await _work._commentRepository.Get(id);
        }

        public void Update(Comment comment)
        {
             _work._commentRepository.Update(comment);
        }
    }
}
