using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace DAL.iRepository
{
    public interface ICategoryRepository
    {
        List<Category> Categories();
        Task Add(Category category);
        void Update(Category category);
        Task Delete(int id);
        Task<Category>Get(int id);  
    }
}
