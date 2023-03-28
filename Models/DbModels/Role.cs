using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DbModels
{
    public class Role:CommonProperties
    {

        public Role()
        {

        }

        [Key]
        public int RoleId { get; set; }
        [Required]
        public string RoleName { get; set; } = string.Empty;
        public string? RoleDetails { get; set; } = string.Empty;



        [NotMapped]
        public virtual ICollection<User> Users { get; set; }

    }
}
