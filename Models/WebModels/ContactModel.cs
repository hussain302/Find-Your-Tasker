using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.WebModels
{
    public class ContactModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Details { get; set; } = string.Empty;
        [Required]
        public string Email { get; set; } = string.Empty;

    }
}
