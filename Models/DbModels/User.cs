using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Models.DbModels
{
    public  class User : CommonProperties
    {
        public User()
        {

        }

        [Key]
        public int UserId { get; set; }
        
        [Required]       
        public string Email { get; set; } = string.Empty; 
       
        [Required]
        public string UserName { get; set; } = string.Empty;
       
        [Required]
        public string FirstName { get; set; } = string.Empty;
       
        [Required]
        public string LastName { get; set; } = string.Empty;
        
        public string? MiddleName { get; set; } = string.Empty;
        
        [Required]
        public string Password { get; set; } = string.Empty;

        [Required]
        public bool IsTasker { get; set; }

        
        public int? Reviews { get; set; }


        public bool? IsEmployee { get; set; }


        public bool? IsRecommended { get; set; }

        [Required]
        public string PhoneOne { get; set; } = string.Empty;        
        public string? PhoneTwo { get; set; } = string.Empty;
        public string? ImageUrl { get; set; }
        public Nullable<DateTime> BirthDate { get; set; }
        public bool? IsApproved { get; set; }
        /// public int RoleId { get; set; }
        public virtual Role Role { get; set; }
    }
}