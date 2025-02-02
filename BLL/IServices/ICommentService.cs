using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO.CommentsDTO;
using Entities;

namespace BLL.IServices
{
    public interface ICommentService
    {
        Task Add(Comment comment);
        void Update(Comment comment);
        List<Comment> Comments(int Id);
        Task<Comment> GetById(int id);
        Task Delete(int Id);
    }
}
