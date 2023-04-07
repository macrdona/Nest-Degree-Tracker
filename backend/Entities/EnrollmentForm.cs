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
    }

    public class EnrollmentCompletedCourses
    {
        public EnrollmentCompletedCourses() { }
        public EnrollmentCompletedCourses(int userId, string course)
        {
            UserId = userId;
            CourseId = course;
        }

        public int UserId { get; set; }

        public string? CourseId { get; set; }
    }

    public class EnrollmentResponse
    {
        public string? Message { get; set; }

    }
}
