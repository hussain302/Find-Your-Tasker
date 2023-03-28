using Models.DbModels;
using Models.WebModels;

namespace Mappers.UserMappers
{
    public static class RoleMapper
    {

        public static RoleModel ToModel(this Role source)
        {
            return new RoleModel
            {
                RoleId = source.RoleId,
                RoleName = source.RoleName,
                RoleDetails = source.RoleDetails,
                CreatedBy = source.CreatedBy,
                CreatedOn = source.CreatedOn,
                UpdatedBy = source.UpdatedBy,
                UpdatedOn = source.UpdatedOn,
            };
        }

        public static Role ToDb(this RoleModel source)
        {
            return new Role
            {
                RoleId = source.RoleId,
                RoleName = source.RoleName,
                RoleDetails = source.RoleDetails,
                CreatedBy = source.CreatedBy,
                CreatedOn = source.CreatedOn,
                UpdatedBy = source.UpdatedBy,
                UpdatedOn = source.UpdatedOn,
            };
        }


    }
}
