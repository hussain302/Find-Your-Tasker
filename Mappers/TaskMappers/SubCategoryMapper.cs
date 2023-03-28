using Models.DbModels;
using Models.WebModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mappers.TaskMappers
{
    public static class SubCategoryMapper
    {

        public static SubCategoryModel ToModel(this SubCategory source)
        {
            return new SubCategoryModel
            {
                
                CategoryId = source.CategoryId,
                SubCategoryName = source.SubCategoryName,
                Category = source.Category.ToModel(),
                SubCategoryDetails = source.SubCategoryDetails,
                SubCategoryId = source.SubCategoryId,
                CreatedBy = source.CreatedBy,
                CreatedOn = source.CreatedOn,
                UpdatedBy = source.UpdatedBy,
                UpdatedOn = source.UpdatedOn,
            };
        }

        public static SubCategory ToDb(this SubCategoryModel source)
        {
            return new SubCategory
            {

                CategoryId = source.CategoryId,
                SubCategoryName = source.SubCategoryName,                
                SubCategoryDetails = source.SubCategoryDetails,
                SubCategoryId = source.SubCategoryId,
                CreatedBy = source.CreatedBy,
                CreatedOn = source.CreatedOn,
                UpdatedBy = source.UpdatedBy,
                UpdatedOn = source.UpdatedOn,
            };
        }

    }
}
