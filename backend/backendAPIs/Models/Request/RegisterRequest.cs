using System.ComponentModel.DataAnnotations;

namespace backendAPIs.Models.Request
{
    public class RegisterRequest
    {
                
        [Required]
        public string Password { get; set; }
       
        [Required]
        public string EmployeeName { get; set; }
        
        [Required]
        public string Designation { get; set; }
        
        [Required]
        public string Department { get; set; }
        
        [Required]
        public string Gender { get; set; }

        [Required]
        public DateTime? DateOfBirth { get; set; }
    }
}
