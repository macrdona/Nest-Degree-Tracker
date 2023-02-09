using Microsoft.AspNetCore.Mvc;
using backend.Authorization;
using AutoMapper;
using backend.Entities;
using backend.Services;
using backend.Helpers;
using Microsoft.Extensions.Options;

namespace backend.Entities
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class Course
    {
        public string? CourseID { get; set; }
        public string? Description { get; set; }
        public Course? Requirement { get; set; }
    }
    public class CreateCourseRequest
    {
        public string? CourseID { get; set; }
        public string? Description { get; set; }
        public Course? Requirement { get; set; }
    }
    public class UpdateCourseRequest
    {
        public string? CourseID { get; set; }
        public string? Description { get; set; }
        public Course? Requirement { get; set; }
    }
}