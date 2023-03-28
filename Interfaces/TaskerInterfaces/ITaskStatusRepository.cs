using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.TaskerInterfaces
{
    public interface ITaskStatusRepository
    {
        public Task<Models.DbModels.TaskStatus> Get(int id);

        public Task<Models.DbModels.TaskStatus> Get(string name);
        public Task<List<Models.DbModels.TaskStatus>> GetAll();
        public Task<bool> Add(Models.DbModels.TaskStatus model); 
        public Task<bool> Update(Models.DbModels.TaskStatus model);
        public Task<bool> Delete(int id);
    }
}
