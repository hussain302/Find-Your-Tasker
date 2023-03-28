using Models.DbModels;
using Models.WebModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mappers.UserMappers
{
    public static class ContactMapper
    {

        public static ContactModel ToModel(this Contact source)
        {
            return new ContactModel
            {
                Id = source.Id,
                Details = source.Details,
                Email = source.Email,
            };
        }
        
        public static Contact ToDb(this ContactModel source)
        {
            return new Contact
            {
                Id = source.Id,
                Details = source.Details,
                Email = source.Email,
            };
        }


    }
}
