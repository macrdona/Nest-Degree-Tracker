using System.ComponentModel.DataAnnotations;

namespace backend.Entities
{
    public class EnrollmentForm
    {
        [Key]
        [Required]
        public int UserId { get; set; }

        [Required]
        public string? Major { get; set; }

        [Required]
        public string? Minor { get; set;}

        [Required]
        public string? Courses { get; set; }
    }

    public class EnrollmentResponse
    {
        public string? Message { get; set; }

    }
}
