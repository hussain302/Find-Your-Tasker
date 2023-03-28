using Models.DbModels;
using Models.WebModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mappers.UserMappers
{
    public static class EmployeeMapper
    {
        public static EmployeeModel ToModel(this Employee source)
        {
            return new EmployeeModel
            {
                Id = source.Id,
                Address = source.Address,
                Designation = source.Designation,
                Email = source.Email,
                FullName = source.FullName,
                Phone1 = source.Phone1,
                Phone2 = source.Phone2,
                Picture_URL = source.Picture_URL,
                Salary = source.Salary

            };
        }

        public static Employee ToDb(this EmployeeModel source)
        {
            return new Employee
            {
                Id = source.Id,
                Address = source.Address,
                Designation = source.Designation,
                Email = source.Email,
                FullName = source.FullName,
                Phone1 = source.Phone1,
                Phone2 = source.Phone2,
                Picture_URL = source.Picture_URL,
                Salary = source.Salary
            };
        }
    }
}
