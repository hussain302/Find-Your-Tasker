using DataAccessLayer.DataBaseContext;
using Interfaces.TaskerInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.TaskerRepositories
{
    public class TaskStatusRepository : ITaskStatusRepository
    {
        private readonly TaskerContext context;

        public TaskStatusRepository(TaskerContext context)
        {
            this.context = context;
        }

        public async Task<bool> Add(Models.DbModels.TaskStatus model)
        {
            try
            {
                await context.AddAsync(model);
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
                context.Remove(find);
                await context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<Models.DbModels.TaskStatus> Get(int id)
        {

            try
            {
                var findTaskStatus  = await context.TaskStatus.FindAsync(id);
                return findTaskStatus;
            }
            catch
            {
                return null;
            }
        }
        public async Task<Models.DbModels.TaskStatus> Get(string name)
        {

            try
            {
                var findTaskStatus  = await context.TaskStatus
                    .Where(x=>x.TaskStatusName == name).FirstOrDefaultAsync();
                return findTaskStatus;
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<Models.DbModels.TaskStatus>> GetAll()
        {
            try
            {
                var findModels = await context.TaskStatus.ToListAsync();
                return findModels;
            }
            catch
            {
                return  null;
            }
        }

        public async Task<bool> Update(Models.DbModels.TaskStatus model)
        {
            try
            {
                if(model != null)
                {
                    context.Update<Models.DbModels.TaskStatus>(model);
                    await context.SaveChangesAsync();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
