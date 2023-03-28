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
    public class UserRepository : IUserRepository
    {
        private readonly TaskerContext context;

        public UserRepository(TaskerContext context)
        {
            this.context = context;
        }

        #region User Create Update & Delete Operations
        public async Task<bool> AddUser(User newUser)
        {
            try
            {
                newUser.IsApproved = false;
                newUser.Role = await context.Roles.Where(r => r.RoleName == "pending").FirstOrDefaultAsync();
                await context.Users.AddAsync(newUser);
                await context.SaveChangesAsync();
                return await Task.FromResult(true);
            }
            catch
            {
                return await Task.FromResult(false);
            }
        }


        public async Task<bool> AddAdminUser(User newUser)
        {
            try
            {
                newUser.Role = await context.Roles.Where(r => r.RoleName == "admin").FirstOrDefaultAsync();
                await context.Users.AddAsync(newUser);
                await context.SaveChangesAsync();
                return await Task.FromResult(true);
            }
            catch
            {
                return await Task.FromResult(false);
            }
        }

        


        public async Task<bool> AddUser(User newUser, string role)
        {
            try
            {
                if (role == "admin") newUser.IsApproved = false;
                else newUser.IsApproved = true;
                newUser.Role = await context.Roles.Where(r => r.RoleName == role).FirstOrDefaultAsync();
                await context.Users.AddAsync(newUser);
                await context.SaveChangesAsync();
                return await Task.FromResult(true);
            }
            catch
            {
                return await Task.FromResult(false);
            }
        }

        public async Task<bool> UpdateUser(User newUser)
        {
            try
            {
               
                context.Users.Update(newUser);
                await context.SaveChangesAsync();
                return await Task.FromResult(true);
            }
            catch
            {
                return await Task.FromResult(false);
            }
        }
        
        
        public async Task<bool> ChangePassword(string newPassword, string oldPassword, string username)
        {
            try
            {
                var find = await GetUser(username);
                if(find.Password != oldPassword) return false;
                find.Password = newPassword;
                await context.SaveChangesAsync();
                return await Task.FromResult(true);
            }
            catch
            {
                return await Task.FromResult(false);
            }
        }

        public async Task<bool> DeleteUser(int userId)
        {
            try
            {
                var find = await GetUser(userId);
                context.Users.Remove(find);
                await context.SaveChangesAsync();
                return await Task.FromResult(true);
            }
            catch
            {
                return await Task.FromResult(false);
            }
        }


        #endregion

        #region Fetch Users
        public async Task<User> GetUser(int userId)
        {
            try
            {
                var findById = await context.Users.Include(x => x.Role)
                    .Where(x => x.UserId == userId).FirstOrDefaultAsync();

                if (findById == null) return null;
                else return findById;
            }
            catch
            {
                return null;
            }
        }
        public async Task<User> GetUser(string username)
        {
            try
            {
                var findByName = await context.Users.Include(x => x.Role)
                    .Where(x => x.UserName == username).FirstOrDefaultAsync();

                if (findByName == null) return null;
                else return findByName;
            }
            catch
            {
                return null;
            }
        }
        public async Task<IEnumerable<User>> GetUsersByRole(Role role)
        {
            try
            {
                var find = await context.Users.Include(x => x.Role).Where(x=>x.Role == role).ToListAsync();

                if (find != null) return find;
                else return Enumerable.Empty<User>();
            }
            catch
            {
                return Enumerable.Empty<User>();
            }
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            try
            {
                var find = await context.Users.Include(x => x.Role).ToListAsync();

                if (find != null) return find;
                else return Enumerable.Empty<User>();
            }
            catch
            {
                return Enumerable.Empty<User>();
            }
        }

        public async Task<User> LoginUserRequest(string username, string password)
        {
            try
            {
                var find = await context.Users.Include(x=>x.Role)
                    .Where(x=>x.UserName == username).Where(x => x.Password == password).FirstOrDefaultAsync();

                if (find != null) return find;
                else return null;
            }
            catch
            {
                return null;
            }
        }

        public async Task<User> LoginUserRequestAdmin(string username, string password)
        {
            try
            {
                var find = await context.Users
                    .Where(x => x.UserName == username).Where(x => x.Password == password).FirstOrDefaultAsync();

                if (find != null) return find;
                else return null;
            }
            catch
            {
                return null;
            }
        }
        #endregion


        public async Task<int> AddQuery(Contact model)
        {
            try
            {
                context.Add(model);
                return context.SaveChanges();
            }
            catch
            {
                return 0;
            }
        }

        public async Task<bool> RemoveQuery(Contact model)
        {
            try
            {
                var find = context.Contacts.Find(model.Id);
                if(find != null)
                {
                    context.Remove(find);
                    context.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<List<Contact>> GetAllQueries()
        {
            try
            {
                return await context.Contacts.ToListAsync();
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
