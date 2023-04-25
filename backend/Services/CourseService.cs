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
using System.Runtime.Intrinsics.X86;
using System.Text.Json;

namespace backend.Services
{
    public interface ICourseService
    {
        Course GetCourseById(string id);

        Course GetCourseByName(string name);
        
        IEnumerable<CoursesRequest> GetAll(int id);

        IEnumerable<Course> CourseRecommendations(int id);

        void AddCourse(CompletedCourses course);

        void RemoveCourse(CompletedCourses course);
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

        public IEnumerable<CoursesRequest> GetAll(int id)
        {
            List<CoursesRequest> course_list = new List<CoursesRequest>();

            try
            {
                var completed_courses = _context.CompletedCourses.Where(x => x.UserId == id);

                //left join between Courses and CompletedCourses
                var course_query2 = _context.Courses
                                    .GroupJoin(
                                        completed_courses,
                                        all => all.CourseId,
                                        completed => completed.CourseId,
                                        (all_courses, completed) => new { all_courses, completed })
                                            .SelectMany(
                                                x => x.completed.DefaultIfEmpty(),
                                                (_all, _comp) => new
                                                {
                                                    CourseId = _all.all_courses.CourseId,
                                                    CourseName = _all.all_courses.CourseName,
                                                    Credits = _all.all_courses.Credits,
                                                    Prerequisites = _all.all_courses.Prerequisites,
                                                    CoRequisites = _all.all_courses.CoRequisites,
                                                    Description = _all.all_courses.Description,
                                                    Availability = _all.all_courses.Availability,
                                                    Completed = (_comp == null) ? false : true
                                                }).ToList();

                foreach (var course in course_query2)
                {
                    var prerequisites = course.Prerequisites.Split(",");
                    if (prerequisites[0] == "N/A") { prerequisites = null; }

                    var corequisites = course.CoRequisites.Split(",");
                    if (corequisites[0] == "N/A") { corequisites = null; }

                    course_list.Add(new CoursesRequest(
                            course.CourseId,
                            course.CourseName,
                            course.Credits,
                            prerequisites,
                            corequisites,
                            course.Description,
                            course.Availability,
                            course.Completed
                        ));
                }
            }
            catch(Exception ex)
            {
                throw new AppException(ex.Message);
            }

            return course_list;
        }

        public Course GetCourseById(string id)
        {
            var course = _context.Courses.FirstOrDefault(x => x.CourseId == id);
            if (course == null) throw new KeyNotFoundException("Course not found");
            return course;
        }

        public Course GetCourseByName(string name)
        {
            var course = _context.Courses.FirstOrDefault(x => x.CourseName == name);
            if (course == null) throw new KeyNotFoundException("Course not found");
            return course;
        }

        public IEnumerable<Course> CourseRecommendations(int id)
        {
            try
            {
                var user = _context.Users.Find(id);
                if (user == null) throw new KeyNotFoundException("User not found");

                var courses = _context.CompletedCourses.Where(x => x.UserId == id);
                if (courses.FirstOrDefault(x => x.UserId == id) == null) throw new KeyNotFoundException("User has not completed any courses");

                List<string> courses_taken = new List<string>();
                foreach (var course in courses)
                {
                    courses_taken.Add(course.CourseId);
                }

                var results = _context.Courses;
                List<Course> recommendations = new List<Course>();
                foreach (Course course in results)
                {
                    FindRecommendations(courses_taken, course, recommendations);
                }

                return recommendations;
            }
            catch (Exception ex) 
            {
                throw new AppException(ex.Message);
            }
            
        }

        public void FindRecommendations(List<string> courses, Course course, List<Course> recommendations)
        {
            if (!courses.Contains(course.CourseId))
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

        public void AddCourse(CompletedCourses course)
        {
            try
            {
                var course_query = _context.Courses.FirstOrDefault(x => x.CourseId == course.CourseId);
                if (course_query == null) throw new AppException("Course not found");

                var course_query2 = _context.CompletedCourses.FirstOrDefault(x => (x.UserId == course.UserId) && (x.CourseId == course.CourseId));
                if (course_query2 != null) throw new AppException("Course has already been registered.");

                _context.CompletedCourses.Add(course);
                _context.SaveChanges();
            }
            catch(Exception ex) 
            {
                throw new AppException(ex.Message);
            }
        }

        public void RemoveCourse(CompletedCourses course)
        {
            try
            {
                var course_query = _context.Courses.FirstOrDefault(x => x.CourseId == course.CourseId);
                if (course_query == null) throw new AppException("Course not found");

                var course_query2 = _context.CompletedCourses.FirstOrDefault(x => (x.UserId == course.UserId) && (x.CourseId == course.CourseId));
                if (course_query2 == null) throw new AppException("User has not registered this course.");

                //Change tracker provides access to information and operations for entity instances this context is tracking
                _context.ChangeTracker.Clear();
                _context.CompletedCourses.Remove(course);
                _context.SaveChanges();
            }
            catch(Exception ex)
            {
                throw new AppException(ex.Message);
            }
            
        }
    }
}