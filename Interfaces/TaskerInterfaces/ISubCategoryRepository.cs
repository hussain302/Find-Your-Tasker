using Models.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.TaskerInterfaces
{
    public interface ISubCategoryRepository
    {
        public Task<SubCategory> Get(int id);

        public Task<List<SubCategory>> GetAll();

        public Task<bool> Add(SubCategory model);

        public Task<bool> Update(SubCategory model);

        public Task<bool> Delete(int id);

        public List<SubCategory>
            GetAll(Expression<Func<SubCategory, bool>> filter = null, 
            Func<IQueryable<SubCategory>, IOrderedQueryable<SubCategory>> orderBy = null,
            params Expression<Func<SubCategory, object>>[] includes);
    }
}
