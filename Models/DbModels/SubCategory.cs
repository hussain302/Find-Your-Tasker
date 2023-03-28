using System.ComponentModel.DataAnnotations;


namespace Models.DbModels
{
    public class SubCategory:CommonProperties
    {
        public SubCategory()
        {

        }


        [Key]
        public int SubCategoryId { get; set; }
        [Required]
        public string SubCategoryName { get; set; } = string.Empty;
        public string? SubCategoryDetails { get; set; } = string.Empty;


        [Required]
        public int CategoryId { get; set; }
        [Required]
        public virtual Category Category { get; set; }

    }
}
