using System.ComponentModel.DataAnnotations;

namespace Models.DbModels
{
    public class Employee
    {
        public Employee()
        {

        }

        [Key]
        public int Id { get; set; }
        [Required]
        public string FullName { get; set; } = string.Empty;
        [Required]
        public string? Designation { get; set; } = string.Empty;
        [Required]
        public double Salary { get; set; }        
        public string? Picture_URL { get; set; } = string.Empty;        
        public string? Address { get; set; } = string.Empty;
        public string? Email { get; set; } = string.Empty;
        public string Phone1 { get; set; } = string.Empty;
        public string? Phone2 { get; set; } = string.Empty;
    }
}