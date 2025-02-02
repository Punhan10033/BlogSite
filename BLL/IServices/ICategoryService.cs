using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace BLL.IServices
{
    public interface ICategoryService
    {
        Task Add (Category category);
        void Update (Category category);
        Task Delete(int Id);
        Task<Category> GetById (int Id);
        List<Category> GetAll();
    }
}
