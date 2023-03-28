using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.WebModels
{
    public class TaskStatusModel:CommonProperties
    {
        [Key]
        public int TaskStatusId { get; set; }
        [Required]
        public string TaskStatusName { get; set; } = string.Empty;

    }
}
