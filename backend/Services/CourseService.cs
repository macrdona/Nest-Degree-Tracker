using Microsoft.Extensions.Options;
using backend.Authorization;
using backend.Entities;
using backend.Helpers;
using backend.Models;
using BCrypt.Net;
using AutoMapper;
using System.Globalization;

namespace backend.Services
{
    public interface ICourseService
    {
        Course GetByID(string id);
        
        IEnumerable<Course> GetAll();

        IEnumerable<Course> GetMajorCourses(List<string> courses);
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
        
        public IEnumerable<Course> GetMajorCourses(List<string> courses)
        {
            List<Course> list = new List<Course>();
            
            foreach (string course in courses)
            {
                try 
                {
                    list.Add(GetCourseById(course));
                }
                catch(KeyNotFoundException) 
                {
                    continue;
                }
                
            }
            return list.AsEnumerable<Course>();
        }

        private Course GetCourseById(string id)
        {
            var course = _context.Courses.FirstOrDefault(x => x.CourseID == id);
            if (course == null) throw new KeyNotFoundException("Course not found");
            return course;
        }

        private Course GetCourseByName(string name)
        {
            var course = _context.Courses.FirstOrDefault(x => x.CourseName == name);
            if (course == null) throw new KeyNotFoundException("Course not found");
            return course;
        }
    }
}