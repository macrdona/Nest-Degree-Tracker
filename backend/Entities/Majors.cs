using backend.Services;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace backend.Entities
{
    public class Majors
    {
        [Key]
        public int MajorId { get; set; }
        public string? MajorName { get; set; }
        public string? Degree { get; set; }
        public string? Description { get; set; }
    }

    public class RequirementsCheck
    {
        public Dictionary<string, bool> met { get; set; }
    }

    public static class UniversityRequirements
    {
        static List<string> humanities = new List<string> { "ARH2000", "HUM2020", "LIT2020", "MUL2010", "PHI2010", "THE2000" };
        static List<string> social = new List<string> { "AMH2020", "ANT2000", "ECO2013", "POS2041", "PSY2012", "SYG2000" };
        static List<string> math = new List<string> { "MAC1105", "MGF1106", "MGF1107", "STA2023", "STA2014", "MAC1101C", "MAC1105C", "MAC1147", "MAC1114", "MAC2233", "MAC2311" };
        static List<string> science = new List<string> { "AST2002", "BSC1005", "BSC1010C", "CHM2045", "ESC2000", "PHY1020", "CHM1020", "EVR1001" };
        static List<string> writing = new List<string> { "CRW2000", "CRW2100", "CRW2201", "CRW2300", "CRW2400", "CRW2600", "ENC2210", "ENC2443", "ENC2451", "ENC2461", "ENC3202", "ENC3246", "ENC3250" };
        
        public static void CheckRequirements(List<CompletedCourses> completedCourses, Dictionary<string, bool> met, List<Course> courses)
        {
            var results = StateRequirements(completedCourses, courses);
            met.Add(results.Item1, results.Item2);
        }

        public static int FindCourse(CompletedCourses course, List<Course> courses, List<CompletedCourses> completedCourses)
        {
            int credits = 0;
            var results = courses.FirstOrDefault(x => x.CourseId == course.CourseId);

            if (results.Credits != null)
                credits += (int)results.Credits;

            return credits;
        }

        public static (string,bool) StateRequirements(List<CompletedCourses> completedCourses, List<Course> courses)
        {
            bool communication_complete = false;
            bool humanities_complete = false;
            bool social_complete = false;
            bool math_complete = false;
            bool science_complete = false;

            int credits = 0;
           
            foreach (CompletedCourses course in completedCourses)
            {
                if (course.CourseId == "ENC1101" && !communication_complete)
                {
                    credits += FindCourse(course, courses, completedCourses);
                    communication_complete= true;
                }

                else if (humanities.Contains(course.CourseId) && !humanities_complete)
                {
                    credits += FindCourse(course, courses, completedCourses);
                    humanities_complete= true;
                }

                else if (social.Contains(course.CourseId) && !social_complete)
                {
                    credits += FindCourse(course, courses, completedCourses);
                    social_complete= true;
                }

                else if (math.Contains(course.CourseId) && !math_complete)
                {
                    credits += FindCourse(course, courses, completedCourses);
                    math_complete= true;
                }

                else if (science.Contains(course.CourseId) && !science_complete)
                {
                    credits += FindCourse(course, courses, completedCourses);
                    science_complete= true;
                }
            }

            return ("State of Florida Requirements", credits >= 15 ? true : false);
        }
    }
}
