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


        public IEnumerable<Requirements> CheckRequirements(int id, IEnumerable<CoursesRequest> courses)
        {
            //grab existing records of the requirements user has completed
            var reqs_query = _context.Requirements.Where(x => x.UserId == id);

            //grab all courses user has completed
            var course_query = courses.Where(x => x.Completed);
            if (course_query == null) throw new AppException("No courses available");

            //grab user's major
            var enrollment_query = _context.Enrollments.FirstOrDefault(x => x.UserId == id);
            var major_query = _context.Majors.FirstOrDefault(x => x.MajorName == enrollment_query.Major);

            //check what requirements user has completed
            var results = UniversityRequirements.CheckRequirements(course_query, major_query.MajorId);

            //check if user has complete oral requirement
            var oral_req = (enrollment_query.OralRequirementComplete) ? 1 : 0;
            results.Add(new Requirements("Oral Requirement", oral_req, 1, false));

            //add a default record for UNF general requirements
            var general_reqs = reqs_query.FirstOrDefault(x => x.Name.Equals("UNF General Education Requirements"));
            if (general_reqs == null)
            {
                results.Add(new Requirements("UNF General Education Requirements", 0, 21, false));
                foreach(Requirements requirements in results)
                {
                    requirements.UserId = id;
                    _context.Requirements.Add(requirements);
                }
            }
            else
            {
                results.Add(new Requirements("UNF General Education Requirements", general_reqs.CompletedCredits, 21, false));
                foreach (Requirements requirements in results)
                {
                    _context.ChangeTracker.Clear();
                    requirements.UserId = id;
                    _context.Requirements.Update(requirements);
                }
            }

            _context.SaveChanges();
           
            return results;
        }

        public void UpdateSpecificRequirements(int id, SpecificRequirements user_requirements)
        {
            var general_requirements = _context.Requirements.Where(x => x.UserId == id);

            if (general_requirements == null) throw new AppException("User not found");

            var general_reqs = general_requirements.FirstOrDefault(x => x.Name == "UNF General Education Requirements");
            general_reqs.CompletedCredits = (user_requirements.GeneralRequirements > 21) ? 21 : user_requirements.GeneralRequirements;
            

            if (user_requirements.OralRequirement)
            {
                var oral_requirements = _context.Enrollments.FirstOrDefault(x => x.UserId == id);
                oral_requirements.OralRequirementComplete = user_requirements.OralRequirement;
            }
            _context.SaveChanges();
        }

    }
}
