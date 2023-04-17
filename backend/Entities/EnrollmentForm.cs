using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace backend.Entities
{
    public class EnrollmentForm
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        public string? Major { get; set; }

        [Required]
        public string? Minor { get; set;}

        [Required]
        public bool OralRequirementComplete { get; set; }

    }

    public class EnrollmentFormRequest
    {
        public int UserId { get; set; }

        [Required]
        public string? Major { get; set; }

        [Required]
        public string? Minor { get; set;}

        [Required]
        public List<String>? Courses { get; set; }

        [Required]
        public bool OralRequirementComplete { get; set; }
    }

    public class CompletedCourses
    {
        public CompletedCourses() { }
        public CompletedCourses(int userId, string course, bool completed=true)
        {
            UserId = userId;
            CourseId = course;
            Completed = completed;
        }

        public int UserId { get; set; }

        public string? CourseId { get; set; }
        public bool Completed { get; set; }
    }

    public class EnrollmentResponse
    {
        public string? Message { get; set; }

    }
}
