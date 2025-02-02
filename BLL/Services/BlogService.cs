using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BLL.AutoMapper;
using BLL.IServices;
using DAL.IUnitOfWork1;
using DTO.BlogDto;
using Entities;

namespace BLL.Services
{
    public class BlogService : IBlogService
    {
        public readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public BlogService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task AddAsync(BlogToAddDto blog)
        {
            Blog blog2 = _mapper.Map<Blog>(blog);
            await _unitOfWork._blogRepository.AddAsync(blog2);
            await _unitOfWork.Commit();
        }

        public void Update(BlogToAddDto blogToAddDto)
        {
            Blog blog1 = _mapper.Map<Blog>(blogToAddDto);
            _unitOfWork._blogRepository.Udate(blog1);
            _unitOfWork.Commit();
        }

        public async Task Delete(int Id)
        {
            await _unitOfWork._commentRepository.Delete(Id);
            await _unitOfWork.Commit();
        }

        public async Task<List<Blog>> Get()
        {
            return await _unitOfWork._blogRepository.GetBlogs();
        }

        public async Task<Blog> Get(int Id)
        {
            return await _unitOfWork._blogRepository.GetById(Id);
        }

        public List<Blog> GetBlogsByWriter(int Id)
        {
            return _unitOfWork._blogRepository.GetBLogsByWriter(Id);
        }

       

        public async Task<BlogCommentsDto> LastAddedBlog(int UserId)
        {
            Blog blog=await _unitOfWork._blogRepository.LastAddedBlog(UserId);
            BlogCommentsDto blog1 = new BlogCommentsDto { Blog = blog };
            return blog1;
        }

        public async Task<ICollection<Like>> GetLikes(int BlogId)
        {
            return await _unitOfWork._blogRepository.GetLikes(BlogId);
        }
    }
}
