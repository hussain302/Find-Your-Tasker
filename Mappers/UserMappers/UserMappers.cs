using Models.DbModels;
using Models.WebModels;

namespace Mappers.UserMappers
{
    public static class UserMappers
    {
        public static UserModel ToModel(this User source)
        {
            return new UserModel
            {
                UserId = source.UserId,
                FirstName = source.FirstName,
                UserName = source.UserName,
                Password = source.Password,
                PhoneOne = source.PhoneOne,
                Role = source.Role.ToModel(),
                LastName = source.LastName,
                MiddleName = source.MiddleName,
                IsApproved = source.IsApproved,
                PhoneTwo = source.PhoneTwo,
                BirthDate = source.BirthDate,
                Email = source.Email,
                ImageUrl = source.ImageUrl,
                CreatedBy = source.CreatedBy,
                CreatedOn = source.CreatedOn,
                UpdatedBy = source.UpdatedBy,
                UpdatedOn = source.UpdatedOn,
                IsTasker = source.IsTasker,
                IsEmployee =   (source.IsEmployee == null)? false: (bool) source.IsEmployee,
                IsRecommended = (source.IsRecommended == null) ? false : (bool) source.IsRecommended,
                Reviews = source.Reviews,
            };
        }
        public static User ToDb(this UserModel source)
        {
            return new User
            {
                UserId = source.UserId,
                FirstName = source.FirstName,
                UserName = source.UserName,
                Password = source.Password,
                PhoneOne = source.PhoneOne,
                Email = source.Email,
                //Role = source.Role.ToDb(),
                LastName = source.LastName,
                MiddleName = source.MiddleName,
                IsApproved = source.IsApproved,
                PhoneTwo = source.PhoneTwo,
                CreatedBy = source.CreatedBy,
                CreatedOn = source.CreatedOn,
                UpdatedBy = source.UpdatedBy,
                IsTasker = source.IsTasker,
                UpdatedOn = source.UpdatedOn,
                BirthDate = source.BirthDate,
                ImageUrl = source.ImageUrl,
                IsEmployee = source.IsEmployee,
                IsRecommended = source.IsRecommended,
                Reviews = source.Reviews,
            };
        }

    }
}
