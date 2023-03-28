using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DbModels
{
    public class Offer
    {

        public Offer()
        {

        }
        [Key]
        public int Id { get; set; }

        [Required]
        public string OfferDetails { get; set; } = string.Empty;
        [Required]
        public double Ofer { get; set; }
        
        public int? UserId { get; set; }
        public virtual User User { get; set; }
        
        public int? TaskId { get; set; }
        public virtual Tasks Tasks { get; set; }

    }
}