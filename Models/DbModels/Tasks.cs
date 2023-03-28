using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DbModels
{
    public class Tasks:CommonProperties
    {
        public Tasks()
        {

        }

        [Key]
        public int TaskId { get; set; }

        [Required]
        public string TaskTitle { get; set; } = string.Empty;
        
        [Required]
        public string TaskDetails { get; set; } = string.Empty;

        [Required]
        public string Address { get; set; } = string.Empty;

        [Required]
        public DateTime DateOfAssiging { get; set; }
        
        public DateTime DueDate { get; set; }

        [Required]
        public double Budget { get; set; }


        [Required]
        public bool IsValid { get; set; }


        public string? RejectionReason { get; set; }

        public int? PostedById { get; set; }
        public virtual User? PostedBy { get; set; }


        public int? TaskerId { get; set; }
        public virtual User? Tasker { get; set; }


        //All Forigen Keys are Below

        [Required]
        public int TaskStatusId { get; set; }
        
        [Required]
        public virtual TaskStatus TaskStatus { get; set; }

        [Required]
        public int SubCategoryId { get; set; }
        [Required]
        public virtual SubCategory SubCategory { get; set; }


    }
}
