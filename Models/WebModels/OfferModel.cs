using Models.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.WebModels
{
    public class OfferModel
    {
        //public int Id { get; set; }
        public string OfferDetails { get; set; } = string.Empty;
        public double Offer { get; set; }

        public int? UserId { get; set; }
        public virtual UserModel User { get; set; }

        public int? TaskId { get; set; }
        public virtual TasksModel Tasks { get; set; }
    }
}