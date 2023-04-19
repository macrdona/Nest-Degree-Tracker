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
        IEnumerable<Requirements> CheckRequirements(int id, IEnumerable<CoursesRequest> courses);

        void UpdateSpecificRequirements(int id, SpecificRequirements user_requirements);
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

            if ( major == null ) throw new KeyNotFoundException("Major not found");

            return major;
        }

        public Majors GetById(int id)
        {
            //checks if major exists
            var major = _context.Majors.FirstOrDefault(x => x.MajorId == id);

            if (major == null) throw new KeyNotFoundException("Major not found");
            
            return major;
        }


        public IEnumerable<Requirements> CheckRequirements(int id, IEnumerable<CoursesRequest> courses)
        {
            try
            {
                //grab all courses user has completed
                var course_query = courses.Where(x => x.Completed);
                if (course_query == null) throw new AppException("No courses available");

                //grab user's major
                var enrollment_query = _context.Enrollments.FirstOrDefault(x => x.UserId == id);
                var major_query = _context.Majors.FirstOrDefault(x => x.MajorName == enrollment_query.Major);

                //check what requirements user has completed
                var results = new UniversityRequirements().CheckRequirements(course_query, major_query.MajorId);

                //check if user has complete oral requirement
                var oral_req = (enrollment_query.OralRequirementComplete) ? 1 : 0;
                results.Add(new Requirements("Oral Requirement", oral_req, 1, enrollment_query.OralRequirementComplete, new List<RequiredCourses>()));

                return results;
            }
            catch (Exception ex)
            {
                throw new AppException(ex.Message);
            }
            
        }

        public void UpdateSpecificRequirements(int id, SpecificRequirements met_requirements)
        {
            try
            {
                var oral_requirements = _context.Enrollments.FirstOrDefault(x => x.UserId == id);
                if (oral_requirements == null) throw new AppException("User not found");

                oral_requirements.OralRequirementComplete = met_requirements.OralRequirement;
                _context.SaveChanges();
            }
            catch(Exception ex)
            {
                throw new AppException(ex.Message);
            }
            
        }

    }
}
