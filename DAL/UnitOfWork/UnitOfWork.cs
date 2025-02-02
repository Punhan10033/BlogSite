using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DataContext;
using DAL.iRepository;
using DAL.IUnitOfWork1;

namespace DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        public ICategoryRepository _categoryRepository { get; set; }
		public IBlogRepository _blogRepository { get; set; }
        public ICommentRepository _commentRepository { get ; set ; }
		public IUserRepository _userRepository { get ; set ; }
		public INotificationRepository _notificationRepository { get; set; }
		public IMessageRepository _messageRepository { get; set; }

		private readonly BlogContext _context;
        public UnitOfWork(IMessageRepository message, INotificationRepository notificationrepository,IUserRepository userRepository, IBlogRepository blog, ICategoryRepository categoryRepository, BlogContext blogContext,
                            ICommentRepository commentRepository)
        {
            _messageRepository= message;
            _notificationRepository= notificationrepository;
            _userRepository=userRepository;
            _commentRepository = commentRepository;
            _blogRepository=blog;
            _categoryRepository = categoryRepository;
            _context = blogContext;
        }
        public async Task Commit()
        {
            await _context.SaveChangesAsync();
        }
    }
}
