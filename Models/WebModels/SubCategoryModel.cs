using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.WebModels
{
    public class SubCategoryModel:CommonProperties
    {
        [Key]
        public int SubCategoryId { get; set; }
        [Required]
        public string SubCategoryName { get; set; } = string.Empty;
        public string? SubCategoryDetails { get; set; } = string.Empty;


        [Required]
        public int CategoryId { get; set; }
        [Required]
        public virtual CategoryModel Category { get; set; }

    }
}
