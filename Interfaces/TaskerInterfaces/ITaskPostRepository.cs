using Models.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TaskStatus = Models.DbModels.TaskStatus;

namespace Interfaces.TaskerInterfaces
{
    public interface ITaskPostRepository
    {

        public Task<Tasks> Get(int id);

        public Task<List<Tasks>> GetAll();
        public Task<List<Tasks>> GetAll(string username);

        public Task<bool> Add(Tasks model);
        public Task<List<Tasks>> GetAllForAdmin();
        public Task<bool> Update(Tasks model);
        
        public Task<bool> Delete(int id);

        List<Tasks> GetAll(Expression<Func<Tasks, bool>> filter = null, Func<IQueryable<Tasks>, IOrderedQueryable<Tasks>> orderBy = null, params Expression<Func<Tasks, object>>[] includes);
        Task<bool> AddOffer(Offer model);
        Task<List<Offer>> GetOffers(int id);
        //Tasks Get(Expression<Func<Tasks, bool>> filter = null, Func<IQueryable<Tasks>, IOrderedQueryable<Tasks>> orderBy = null, params Expression<Func<Tasks, object>>[] includes);
    }
}
