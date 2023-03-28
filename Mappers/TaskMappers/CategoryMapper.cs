using Models.DbModels;
using Models.WebModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mappers.TaskMappers
{
    public static class CategoryMapper
    {

        public static CategoryModel ToModel(this Category source)
        {
            return new CategoryModel
            {
                CategoryDetails = source.CategoryDetails,
                CategoryId = source.CategoryId,
                CategoryName = source.CategoryName,
                CreatedBy = source.CreatedBy,
                CreatedOn = source.CreatedOn,
                UpdatedBy = source.UpdatedBy,
                UpdatedOn = source.UpdatedOn,
            };
        }
        public static Category ToDb(this CategoryModel source)
        {
            return new Category
            {
                CategoryDetails = source.CategoryDetails,
                CategoryId = source.CategoryId,
                CategoryName = source.CategoryName,
                CreatedBy = source.CreatedBy,
                CreatedOn = source.CreatedOn,
                UpdatedBy = source.UpdatedBy,
                UpdatedOn = source.UpdatedOn,
            };
        }
    }
}
