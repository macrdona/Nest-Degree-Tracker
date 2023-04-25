using backend.Entities.MajorsRequirements;
using backend.Services;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace backend.Models
{
    public class Majors
    {
        [Key]
        public int MajorId { get; set; }
        public string? MajorName { get; set; }
        public string? Degree { get; set; }
        public string? Description { get; set; }
    }

    public class SpecificRequirements
    {
        [Required]
        public bool OralRequirement { get; set; }
    }

    public class Requirements
    {
        public Requirements(string name, int completed_credits, int total_credits, bool satisfied, List<RequiredCourses> missing_courses)
        { 
            Name = name;
            CompletedCredits = completed_credits;
            TotalCredits = total_credits;
            Satisfied = satisfied;
            MissingCourses = missing_courses;
        }

        [JsonIgnore]
        public int UserId { get; set; }
        public string? Name { get; set; }
        public int CompletedCredits { get; set; }
        public int TotalCredits { get; set; }
        public bool Satisfied { get; set; }
        public List<RequiredCourses> MissingCourses { get; set; }

    }
    public class RequiredCourses
    {
        public RequiredCourses(string? description, List<string> course_id)
        {
            Description = description;
            CourseId = course_id;
        }

        public string? Description { get; set; }
        public List<string> CourseId { get; set; }
    }
}
