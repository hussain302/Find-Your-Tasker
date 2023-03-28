using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DbModels
{
    public class TaskStatus:CommonProperties
    {
        public TaskStatus()
        {

        }

        [Key]
        public int TaskStatusId { get; set; }
        [Required]
        public string TaskStatusName { get; set; } = string.Empty;

    }
}
