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

        Majors GetByName(string majorName);

        Majors GetById(int id);
        IEnumerable<Requirements> CheckRequirements(int id);
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

        public Majors GetByName(string majorName)
        {
            //checks if major exists
            var major = _context.Majors.FirstOrDefault(x => x.MajorName == majorName);

            if ( major == null )
            {
                throw new KeyNotFoundException("Major not found");
            }

            return major;
        }

        public Majors GetById(int id)
        {
            //checks if major exists
            var major = _context.Majors.FirstOrDefault(x => x.MajorId == id);

            if (major == null)
            {
                throw new KeyNotFoundException("Major not found");
            }


            return major;
        }

        public IEnumerable<Requirements> CheckRequirements(int id)
        {
            var course_query = _context.CompletedCourses.Where(x => x.UserId == id).ToList();
            if (course_query == null) throw new AppException("No courses available");

            var enrollment_query = _context.Enrollments.FirstOrDefault(x => x.UserId == id);

            var major_query = _context.Majors.FirstOrDefault(x => x.MajorName == enrollment_query.Major);

            var results = UniversityRequirements.CheckRequirements(course_query, _context.Courses.ToList(), major_query.MajorId);

            return results;
        }

    }
}
