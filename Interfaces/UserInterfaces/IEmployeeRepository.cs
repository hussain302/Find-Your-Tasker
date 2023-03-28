using Models.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.UserInterfaces
{
    public interface IEmployeeRepository
    {
        Task<bool> Add(Employee model);
        Task<bool> Remove(int id);
        Task<Employee> Get(string employeeName);
        Task<bool> Update(Employee newEmployee);
        Task<IEnumerable<Employee>> GetAll();
        Task<Employee> Get(int Id);
    }
}
