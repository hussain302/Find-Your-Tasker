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
    public class CategoryRepository : ICategoryRepository
    {
        private readonly TaskerContext context;

        public CategoryRepository(TaskerContext context)
        {
            this.context = context;
        }


        public async Task<bool> Add(Category model)
        {
            try
            {
                await context.AddAsync<Category>(model);
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
                context.Remove<Category>(find);
                await context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<Category> Get(int id)
        {
            try
            {
                var find = await context.Categories.Where(x => x.CategoryId == id).FirstOrDefaultAsync();
                return find;
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<Category>> GetAll()
        {
            try
            {
                var models = await context.Categories.ToListAsync();
                return models;
            }
            catch
            {
                return null;
            }
        }

        public List<Category> GetAll(Expression<Func<Category, bool>> filter = null, Func<IQueryable<Category>, IOrderedQueryable<Category>> orderBy = null, params Expression<Func<Category, object>>[] includes)
        {
            IQueryable<Category> query = context.Categories;
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

        public async Task<bool> Update(Category model)
        {
            if (model == null) return false;

            try
            {

                context.Update<Category>(model);
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
