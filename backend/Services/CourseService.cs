using Microsoft.Extensions.Options;
using backend.Authorization;
using backend.Entities;
using backend.Helpers;
using backend.Models;
using BCrypt.Net;
using AutoMapper;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace backend.Services
{
    public interface ICourseService
    {
        Course GetByID(string id);
        
        IEnumerable<Course> GetAll();

        IEnumerable<Course> CourseRecommendations(int id);

    }
    public class CourseService : ICourseService
    {
        private readonly DataContext _context;
        private readonly IJwtUtils _jwtUtils;
        private readonly IMapper _mapper;

        public CourseService(
            DataContext context,
            IJwtUtils jwtUtils,
            IMapper mapper
        )
        {
            _context = context;
            _jwtUtils = jwtUtils;
            _mapper = mapper;
        }

        public IEnumerable<Course> GetAll() => _context.Courses;

        public Course GetByID(string id) => GetCourseById(id);

        public Course GetByName(string name) => GetCourseByName(name);

        private Course GetCourseById(string id)
        {
            var course = _context.Courses.FirstOrDefault(x => x.CourseId == id);
            if (course == null) throw new KeyNotFoundException("Course not found");
            return course;
        }

        private Course GetCourseByName(string name)
        {
            var course = _context.Courses.FirstOrDefault(x => x.CourseName == name);
            if (course == null) throw new KeyNotFoundException("Course not found");
            return course;
        }

        public IEnumerable<Course> CourseRecommendations(int id)
        {
            var user = _context.Users.Find(id);
            if(user == null) throw new KeyNotFoundException("User not found");

            var courses = _context.CompletedCourses.Where(x => x.UserId == id);
            List<string> courses_taken = new List<string>();
            foreach (var course in courses)
            {
                courses_taken.Add(course.CourseId);
            }

            var results = _context.Courses;
            List<Course> recommendations = new List<Course>();
            foreach(Course course in results)
            {
                FindRecommendations(courses_taken, course, recommendations);
            }

            return recommendations;
        }

        public void FindRecommendations(List<string> courses, Course course, List<Course> recommendations)
        {
            string[] prereqs = course.Prerequisites.Split(",");
            foreach (string prereq in prereqs)
            {
                if (courses.Contains(prereq))
                {
                    recommendations.Add(course);
                }
            }
        }
    }
}