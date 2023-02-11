using Microsoft.Extensions.Options;
using backend.Authorization;
using backend.Entities;
using backend.Helpers;
using backend.Models;
using BCrypt.Net;
using AutoMapper;

namespace backend.Services
{
    public interface ICourseService
    {
        Course getByID(string id);
        
        IEnumerable<Course> getAll();
    }
    public class CourseService : ICourseService
    {
        private DataContext _context;
        private IJwtUtils _jwtUtils;
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

        public IEnumerable<Course> getAll()
        {
            return _context.Courses;
        }

        public Course getByID(string id)
        {
            return getCourse(id);
        }

        public Course getCourse(string id)
        {
            var course = _context.Courses.Find(id);
            if (course == null) throw new KeyNotFoundException("Course not found");
            return course;
        }
    }
}