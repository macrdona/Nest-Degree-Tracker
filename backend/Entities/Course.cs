using Microsoft.AspNetCore.Mvc;
using backend.Authorization;
using AutoMapper;
using backend.Entities;
using backend.Services;
using backend.Helpers;
using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations;
using Duende.IdentityServer.Extensions;

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

    public class CoursesRequest
    {
        public CoursesRequest(string? courseId, string? courseName, int? credits, string[]? prerequisites, string[]? coRequisites, string? description, string? availability, bool completed)
        {
            CourseId = courseId;
            CourseName = courseName;
            Credits = credits;
            Prerequisites = prerequisites;
            CoRequisites = coRequisites;
            Description = description;
            Availability = availability;
            Completed = completed;
        }

        public string? CourseId { get; set; }
        public string? CourseName { get; set; }
        public int? Credits { get; set; }

        public string[]? Prerequisites { get; set; }

        public string[]? CoRequisites { get; set; }
        public string? Description { get; set; }
        public string? Availability { get; set; }
        public bool Completed { get; set; }
    }

    public class Recommendations
    {
        public List<Course>? Courses { get; set; }
    }
}