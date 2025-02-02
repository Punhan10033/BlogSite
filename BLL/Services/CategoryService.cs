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
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _work;
        public CategoryService(IUnitOfWork unitOfWork)
        {
            _work = unitOfWork;
        }
        public async Task Add(Category category)
        {
            if (category.CategoryName != "" && category.CategoryDescription != "" && category.CategoryName.Length >= 5
                && category.CategoryStatus == true)
            {
                await _work._categoryRepository.Add(category);
                await _work.Commit();
            }
            else
            {
                //
            }
        }

        public async Task Delete(int Id)
        {
            await _work._categoryRepository.Delete(Id);
            await _work.Commit();
        }

        public List<Category> GetAll()
        {
            return _work._categoryRepository.Categories();
        }

        public async Task<Category> GetById(int Id)
        {
            return await _work._categoryRepository.Get(Id);
        }

        public void Update(Category category)
        {
            _work._categoryRepository.Update(category);
        }
    }
}
