using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.WebModels
{
    public class TasksModel:CommonProperties
    {
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
        public string? RejectionReason { get; set; }


        [Required]
        public bool IsValid { get; set; }



        public int? PostedById { get; set; }
        public virtual UserModel? PostedBy { get; set; }


        public int? TaskerId { get; set; }
        public virtual UserModel? Tasker { get; set; }

        //All Forigen Keys are Below

        [Required]
        public int TaskStatusId { get; set; }
        
        [Required]
        public virtual TaskStatusModel TaskStatus { get; set; }
        
        
        [Required]
        public int SubCategoryId { get; set; }
        [Required]
        public virtual SubCategoryModel SubCategory { get; set; }





    }
}
