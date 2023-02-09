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
        void create(CreateCourseRequest model);
        Course getByID(string id);
        void update(string id, UpdateCourseRequest model);
        void delete(string id);
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
        public void create(CreateCourseRequest model)
        {
            // validate course
            if (_context.Courses.Any(x => x.CourseID == model.CourseID))
                throw new AppException("Course ID '" + model.CourseID + "' is already in use.");
            // copy current model to the course model whose data will be stored in the database
            var course = _mapper.Map<Course>(model);
            // save course
            _context.Courses.Add(course);
            _context.SaveChanges();
        }
        public void update(string id, UpdateCourseRequest model)
        {
            var course = getCourse(id);

            // validate course
            if (model.CourseID != course.CourseID && _context.Courses.Any(x => x.CourseID == model.CourseID))
                throw new AppException("Course ID '" + model.CourseID + "' is already in use.");
            // copy model to course and save
            _mapper.Map(model, course);
            _context.Courses.Update(course);
            _context.SaveChanges();

        }
        public void delete(string id)
        {
            var course = getCourse(id);
            _context.Courses.Remove(course);
            _context.SaveChanges();
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