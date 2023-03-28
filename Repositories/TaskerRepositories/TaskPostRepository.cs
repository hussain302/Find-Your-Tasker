using DataAccessLayer.DataBaseContext;
using Interfaces.TaskerInterfaces;
using Microsoft.EntityFrameworkCore;
using Models.DbModels;
using Models.WebModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.TaskerRepositories
{
    public class TaskPostRepository : ITaskPostRepository
    {
        private readonly TaskerContext context;

        public TaskPostRepository(TaskerContext context)
        {
            this.context = context;
        }

        public async Task<bool> Add(Tasks model)
        {
            try
            {
                #region Logic
                //var taskStatus = await context.TaskStatus.Where(s => s.TaskStatusName == "unapproved").FirstOrDefaultAsync();
                //var findPoster = await context.Users.Where(u => u.UserName == model.PostedBy.UserName).FirstOrDefaultAsync();
                //model.TaskStatusId = taskStatus.TaskStatusId;
                //model.PostedById = findPoster.UserId;
                #endregion Logic

                await context.AddAsync<Tasks>(model);
                await context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var find = await Get(id);
                context.Remove<Tasks>(find);
                await context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<Tasks> Get(int id)
        {
            try
            {
               var find = await context.Tasks.Where(x => x.TaskId == id).FirstOrDefaultAsync();
                return find;
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<Tasks>> GetAllForAdmin()
        {
            try
            {
                var models = await context.Tasks
                    .Include(u => u.Tasker).Include(u => u.PostedBy).Include(t => t.TaskStatus)
                    .ToListAsync();
                return models;
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<Tasks>> GetAll()
        {
            try
            {
                var models = await context.Tasks
                    .Where(x => x.TaskStatus.TaskStatusName == "approved")
                    .Include(u=>u.Tasker).Include(u=>u.PostedBy).Include(t=>t.TaskStatus)
                    .ToListAsync();
                return models;
            }
            catch
            {
                return null;
            }
        }
        public async Task<List<Tasks>> GetAll(string username)
        {
            try
            {
                var models = await context.Tasks
                    .Where(x => x.TaskStatus.TaskStatusName == "approved").Where(x=>x.PostedBy.UserName == username)
                    .Include(u=>u.Tasker).Include(u=>u.PostedBy).Include(t=>t.TaskStatus)
                    .ToListAsync();
                return models;
            }
            catch
            {
                return null;
            }
        }

        public List<Tasks> GetAll(Expression<Func<Tasks, bool>> filter = null, Func<IQueryable<Tasks>, IOrderedQueryable<Tasks>> orderBy = null, params Expression<Func<Tasks, object>>[] includes)
        {
            IQueryable<Tasks> query = context.Tasks.Include(u => u.Tasker).Include(u => u.PostedBy).Include(t => t.TaskStatus);
            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        public async Task<bool> Update(Tasks model)
        {
            if (model == null) return false;

            try
            {
                var find = await Get(model.TaskId);

                if (find == null) return false;
                
                find.Address = model.Address;
                //find.PostedBy = model.PostedBy;
                find.Budget = model.Budget;
                find.DateOfAssiging = model.DateOfAssiging;
                find.DueDate = model.DueDate;
                find.TaskDetails = model.TaskDetails;
                //find.TaskStatus = model.TaskStatus;
                find.TaskStatusId = model.TaskStatusId;
                find.TaskTitle = model.TaskTitle;
                find.TaskerId = model.TaskerId;
                find.PostedById = model.PostedById;
                find.IsValid = model.IsValid;
                find.RejectionReason = model.RejectionReason;
                find.SubCategoryId = model.SubCategoryId;
                
                //context.Update<Tasks>(model);
                await context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> AddOffer(Offer model)
        {
            try
            {
                model.Tasks = await Get((int)model.TaskId);
                context.Offers.Add(model);
                context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task<List<Offer>> GetOffers(int id)
        {
            try
            {
                return await context.Offers.Where(x=>x.TaskId == id).ToListAsync();
            }
            catch
            {
                return null;
            }
        }

    }
}
