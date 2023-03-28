using DataAccessLayer.DataBaseContext;
using Interfaces.UserInterfaces;
using Microsoft.EntityFrameworkCore;
using Models.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.UserRepositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly TaskerContext context;

        public EmployeeRepository(TaskerContext context)
        {
            this.context = context;
        }

        public async Task<bool> Add(Employee model)
        {
            try
            {
                if (model == null) return false;
                await context.AddAsync<Employee>(model);
                await context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<Employee> Get(string employeeName)
        {
            try
            {
                var find = await context.Employees.Where(x => x.FullName == employeeName)
                                                         .FirstOrDefaultAsync<Employee>();

                if (find == null) return null;
                return find;
            }
            catch
            {
                return null;
            }
        }

        public async Task<Employee> Get(int Id)
        {
            try
            {
                var find = await context.Employees.Where(x => x.Id == Id)
                                         .FirstOrDefaultAsync<Employee>();

                if (find == null) return null;
                return find;
            }
            catch
            {
                return null;
            }
        }

        public async Task<IEnumerable<Employee>> GetAll()
        {
            try
            {
                var findEmployees = await context.Employees.ToListAsync<Employee>();

                if (findEmployees == null) return Enumerable.Empty<Employee>();
                return findEmployees;
            }
            catch
            {
                return Enumerable.Empty<Employee>();
            }
        }

        public async Task<bool> Remove(int id)
        {
            try
            {
                var find = await Get(id);
                if (find == null) return false;
                context.Remove(find);
                await context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> Update(Employee newEmployee)
        {
            try
            {
                if (newEmployee == null) return false;
                context.Update<Employee>(newEmployee);
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
