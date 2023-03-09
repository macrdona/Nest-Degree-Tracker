using System.ComponentModel.DataAnnotations;

namespace backend.Entities
{
    public class EnrollmentForm
    {
        [Key]
        public int UserId { get; set; }
        public string? Username { get; set; }

        public bool Completed { get; set; } = false;

        [Required]
        public string? Question1 { get; set; }
    }

    public class EnrollmentCompleted
    {
        public bool Completed { get; set; }

        public string? Message { get; set; }

    }
}
