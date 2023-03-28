using Models.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.UserInterfaces
{
    public interface IUserRepository
    {
        Task<User> LoginUserRequest(string username, string password);
        Task<bool> AddUser(User newUser, string role);
        Task<bool> AddUser(User newUser);
        Task<User> LoginUserRequestAdmin(string username, string password);
        Task<bool> AddAdminUser(User newUser);
        Task<bool> UpdateUser(User newUser);
        Task<bool> ChangePassword(string newPassword,string oldPassword,string username);
        Task<bool> DeleteUser(int userId);
       // Task<bool> DeleteUser(string email);
        Task<User> GetUser(int userId);
        Task<User> GetUser(string username);
        Task<IEnumerable<User>> GetUsersByRole(Role role);
        Task<IEnumerable<User>> GetUsers();
        Task<int> AddQuery(Contact model);
        Task<bool> RemoveQuery(Contact model);
        Task<List<Contact>> GetAllQueries();
    }
}
