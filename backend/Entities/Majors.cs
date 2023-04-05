using backend.Services;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace backend.Entities
{
    public class Majors
    {
        [Key]
        public int MajorId { get; set; }
        public string? MajorName { get; set; }
        public string? Degree { get; set; }
        public string? Description { get; set; }
    }

    [Keyless]
    public class MajorCourses
    {
        public int MajorId { get; set; }
        public string? CourseId { get; set; }

    }

}
