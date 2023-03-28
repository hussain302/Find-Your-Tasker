using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Models.WebModels
{
    public  class UserModel : CommonProperties
    {
        [Key]
        public int UserId { get; set; }
        
        [Required]
        public string Email { get; set; } = string.Empty;
        public int? Reviews { get; set; }


        public bool IsEmployee { get; set; }

        public bool IsRecommended { get; set; }

        [Required]
        public string UserName { get; set; } = string.Empty;
        
        [Required]
        public string FirstName { get; set; } = string.Empty;
        
        [NotMapped]
        public string? FullName { get
            {
                return $"{FirstName}, {LastName}";
            } 
        }

        [Required]
        public bool IsTasker { get; set; }

        [Required]
        public string LastName { get; set; } = string.Empty;
        public string? MiddleName { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
        [Required]
        public string PhoneOne { get; set; } = string.Empty;        
        public string? PhoneTwo { get; set; } = string.Empty;

        public bool? IsApproved { get; set; }
        public string ConfirmPassword { get; set; } = String.Empty;
        public string OldPassword { get; set; } = String.Empty;
        public string? ImageUrl { get; set; }
        public Nullable<DateTime> BirthDate { get; set; }

        /// <summary name="Role_FK">
        /// Forigen key for roles in user 
        /// </summary>
        public virtual RoleModel Role { get; set; }

    }
}