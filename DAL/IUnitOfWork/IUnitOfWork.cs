using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.iRepository;

namespace DAL.IUnitOfWork1
{
    public interface IUnitOfWork
    {
        ICategoryRepository _categoryRepository { get; set; }
        IBlogRepository _blogRepository { get; set; }
        ICommentRepository _commentRepository { get; set; }
        IUserRepository _userRepository { get; set; }
        INotificationRepository _notificationRepository { get; set; }
        IMessageRepository _messageRepository { get; set; }
        Task Commit();
    }
}
