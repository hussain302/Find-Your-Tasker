using Models.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.TaskerInterfaces
{
    public interface ICategoryRepository
    {
        public Task<Category> Get(int id);

        public Task<List<Category>> GetAll();

        public Task<bool> Add(Category model);

        public Task<bool> Update(Category model);

        public Task<bool> Delete(int id);

        public List<Category> GetAll(Expression<Func<Category, bool>> filter = null, Func<IQueryable<Category>, IOrderedQueryable<Category>> orderBy = null, params Expression<Func<Category, object>>[] includes);
    }
}
