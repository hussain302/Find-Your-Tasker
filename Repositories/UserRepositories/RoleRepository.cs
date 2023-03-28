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
    public class RoleRepository : IRoleRepository
    {
        private readonly TaskerContext context;

        public RoleRepository(TaskerContext context)
        {
            this.context = context;
        }

        #region Role Create Update & Delete Operations

        public async Task<bool> AddRole(Role model)
        {
            try
            {
                await context.Roles.AddAsync(model);
                await context.SaveChangesAsync();  
                return await Task.FromResult(true);
            }
            catch
            {
                return await Task.FromResult(false);
            }
        }
        public async Task<bool> RemoveRole(int roleId)
        {
            try
            {
                var find = await GetRole(roleId);
                context.Roles.Remove(find);
                await context.SaveChangesAsync();
                return await Task.FromResult(true);
            }
            catch
            {
                return await Task.FromResult(false);
            }
        }

        public async Task<bool> UpdateRole(Role newRole)
        {

            try
            {
                context.Roles.Update(newRole);
                await context.SaveChangesAsync();
                return await Task.FromResult(true);
            }
            catch
            {
                return await Task.FromResult(false);
            }
        }


        #endregion

        #region fetch Roles

        public async Task<Role> GetRole(string roleName)
        {
            try
            {
                var findByName = await context.Roles
                    .Where(x => x.RoleName == roleName).FirstOrDefaultAsync();

                if (findByName == null) return null;
                else return findByName;                
            }
            catch
            {
                return null;
            }
        }

        public async Task<Role> GetRole(int RoleId)
        {
            try
            {
                var role = await context.Roles.Where(x => x.RoleId == RoleId).FirstOrDefaultAsync();
                if (role != null) return role;
                else return null;
            }
            catch
            {
                return null;
            }
        }

        public async Task<IEnumerable<Role>> GetRoles()
        {
            try
            {
                var roleList = await context.Roles.ToListAsync();
                return (roleList != null) ? roleList : Enumerable.Empty<Role>();
            }
            catch
            {
                return Enumerable.Empty<Role>();
            }
        }
        #endregion

    }
}
