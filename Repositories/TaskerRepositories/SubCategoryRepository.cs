using DataAccessLayer.DataBaseContext;
using Interfaces.TaskerInterfaces;
using Microsoft.EntityFrameworkCore;
using Models.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.TaskerRepositories
{
    public class SubCategoryRepository : ISubCategoryRepository
    {
        private readonly TaskerContext context;

        public SubCategoryRepository(TaskerContext context)
        {
            this.context = context;
        }


        public async Task<bool> Add(SubCategory model)
        {
            try
            {
                await context.AddAsync<SubCategory>(model);
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
                context.Remove<SubCategory>(find);
                await context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<SubCategory> Get(int id)
        {
            try
            {
                var find = await context.SubCategories.Where(x => x.SubCategoryId == id).FirstOrDefaultAsync();
                return find;
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<SubCategory>> GetAll()
        {
            try
            {
                var models = await context.SubCategories.Include(x=>x.Category).ToListAsync();
                return models;
            }
            catch
            {
                return null;
            }
        }

        public List<SubCategory> GetAll(Expression<Func<SubCategory, bool>> filter = null, Func<IQueryable<SubCategory>, IOrderedQueryable<SubCategory>> orderBy = null, params Expression<Func<SubCategory, object>>[] includes)
        {
            IQueryable<SubCategory> query = context.SubCategories;
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

        public async Task<bool> Update(SubCategory model)
        {
            if (model == null) return false;

            try
            {

                context.Update<SubCategory>(model);
                await context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
