using Mappers.UserMappers;
using Models.DbModels;
using Models.WebModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mappers.TaskMappers
{
    public static class OfferMapper
    {
        public static OfferModel ToModel(this Offer source)
        {
            return new OfferModel
            {
                OfferDetails= source.OfferDetails,
                TaskId= source.TaskId,
                UserId= source.UserId,
                Offer = source.Ofer,
                Tasks= source.Tasks.ToModel(),
                User = source.User.ToModel(),
            };
        }
        public static Offer ToDb(this OfferModel source)
        {
            return new Offer
            {
                OfferDetails = source.OfferDetails,
                TaskId = source.TaskId,
                UserId = source.UserId,
                Ofer = source.Offer
            };
        }
    }
}
