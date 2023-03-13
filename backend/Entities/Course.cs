using Microsoft.AspNetCore.Mvc;
using backend.Authorization;
using AutoMapper;
using backend.Entities;
using backend.Services;
using backend.Helpers;
using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations;

namespace backend.Entities
{
    public class Course
    {
        public string? CourseId { get; set; }
        public string? CourseName { get; set; }
        public int? Credits { get; set; }

        public string? Prerequisites { get; set; }

        public string? CoRequisites { get; set; }
        public string? Description { get; set; }
        public string? Availability { get; set; }
    }

    public class CourseRequest
    {
        [Required]
        public string? CourseId { get; set; }
    }
}