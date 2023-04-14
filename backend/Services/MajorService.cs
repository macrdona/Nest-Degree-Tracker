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
    public interface IMajorService
    {
        IEnumerable<Majors> GetAll();

        //IEnumerable<Course> GetByName(string majorName);

        //IEnumerable<Course> GetById(int id);
        IEnumerable<RequirementsCheck> CheckRequirements(int id);
    }

    public class MajorService : IMajorService
    {
        private readonly DataContext _context;
        private readonly IJwtUtils _jwtUtils;
        private readonly IMapper _mapper;

        public MajorService(
            DataContext context,
            IJwtUtils jwtUtils,
            IMapper mapper
        )
        {
            _context = context;
            _jwtUtils = jwtUtils;
            _mapper = mapper;
        }

        public IEnumerable<Majors> GetAll() => _context.Majors;

        /*public IEnumerable<Course> GetByName(string majorName)
        {
            //checks if major exists
            var major = _context.Majors.FirstOrDefault(x => x.MajorName == majorName);

            if ( major == null )
            {
                throw new KeyNotFoundException("Major not found");
            }

            //grabs all courses from specified major
            var majorCourses = _context.MajorCourses.Where(x => x.MajorId == major.MajorId).ToList();
            
            List<string> courses = new List<string>();
            foreach(MajorCourses course in majorCourses)
            {
                courses.Add(course.CourseId);
            }

            return _context.Courses.Where(x => courses.Contains(x.CourseId));
        }*/

        /*public IEnumerable<Course> GetById(int id)
        {
            //checks if major exists
            var major = _context.Majors.FirstOrDefault(x => x.MajorId == id);

            if (major == null)
            {
                throw new KeyNotFoundException("Major not found");
            }

            //grabs all courses from specified major
            var majorCourses = _context.MajorCourses.Where(x => x.MajorId == major.MajorId).ToList();

            List<string> courses = new List<string>();
            foreach (MajorCourses course in majorCourses)
            {
                courses.Add(course.CourseId);
            }

            return _context.Courses.Where(x => courses.Contains(x.CourseId));
        }*/

        public IEnumerable<RequirementsCheck> CheckRequirements(int id)
        {
            var course_query = _context.CompletedCourses.Where(x => x.UserId == id).ToList();
            if (course_query == null) throw new AppException("No courses available");

            var user = _context.Requirements.Find(id);
            if (user == null) throw new KeyNotFoundException("User not found");

            var results = UniversityRequirements.CheckRequirements(course_query, _context.Courses.ToList());

            foreach(var result in results )
            {
                switch(result.Name)
                {
                    case "State of Florida Requirements": if (result.Satisfied) user.StateRequirements = true; break;
                }
            }

            _context.SaveChanges();

            return results;
        }

    }
}
