using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace DAL.iRepository
{
    public interface ICommentRepository
    {
        Task Add(Comment comment);
        void Update(Comment comment);
        Task Delete(int Id);
        List<Comment>GetComments(int Id);
        Task<Comment> Get(int Id);
    }
}
