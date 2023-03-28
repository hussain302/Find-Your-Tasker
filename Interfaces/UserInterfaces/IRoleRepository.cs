using Models.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.UserInterfaces
{
    public interface IRoleRepository
    {
        Task<bool> AddRole(Role model);
        Task<bool> RemoveRole(int roleId);
        Task<Role> GetRole(string roleName);
        Task<bool> UpdateRole(Role newRole);
        Task<IEnumerable<Role>> GetRoles();
        Task<Role> GetRole(int RoleId);
    }
}