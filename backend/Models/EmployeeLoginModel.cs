using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    public class EmployeeLoginModel
    {
        [Required]
        public string EmployeeId { get; set; } = null!;

        [Required]
        public string Password { get; set; }   = null!;
    }
}
