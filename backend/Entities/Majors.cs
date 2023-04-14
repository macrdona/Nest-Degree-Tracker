using backend.Services;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

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

    public class Requirements
    {
        [Key]
        public int UserId { get; set; }
        public bool StateRequirements { get; set; } = false;
        public bool UNFRequirements { get; set; } = false;
        public bool OralRequirement { get; set; } = false;
        public bool Prerequisites { get; set; } = false;
        public bool CoreRequirements { get; set; } = false;
        public bool MajorRequirements { get; set; } = false;
        public bool MajorElectives { get; set; } = false;
    }

    public class RequirementsCheck
    {
        public RequirementsCheck(string name, int completedCredits, int totalCredits, bool satisfied)
        {
            this.Name = name;
            this.CompletedCredits = completedCredits;
            this.TotalCredits = totalCredits;
            this.Satisfied = satisfied;
        }

        public string? Name { get; set; }
        public int CompletedCredits { get; set; }
        public int TotalCredits { get; set; }

        [JsonIgnore]
        public bool Satisfied { get; set; }
    }

    public static class UniversityRequirements
    {
        static List<string> humanities = new List<string> { "ARH2000", "HUM2020", "LIT2020", "MUL2010", "PHI2010", "THE2000" };
        static List<string> social = new List<string> { "AMH2020", "ANT2000", "ECO2013", "POS2041", "PSY2012", "SYG2000" };
        static List<string> math = new List<string> { "MAC1105", "MGF1106", "MGF1107", "STA2023", "STA2014", "MAC1101C", "MAC1105C", "MAC1147", "MAC1114", "MAC2233", "MAC2311" };
        static List<string> science = new List<string> { "AST2002", "BSC1005", "BSC1010C", "CHM2045", "ESC2000", "PHY1020", "CHM1020", "EVR1001" };
        
        public static List<RequirementsCheck> CheckRequirements(List<CompletedCourses> completedCourses, List<Course> courses)
        {
            List<RequirementsCheck> requirements = new List<RequirementsCheck>();

            var results = StateRequirements(completedCourses, courses);
            requirements.Add(results);

            return requirements;
        }

        public static int FindCourse(CompletedCourses course, List<Course> courses, List<CompletedCourses> completedCourses)
        {
            int credits = 0;
            var results = courses.FirstOrDefault(x => x.CourseId == course.CourseId);

            if (results.Credits != null)
                credits += (int)results.Credits;

            return credits;
        }

        public static RequirementsCheck StateRequirements(List<CompletedCourses> completedCourses, List<Course> courses)
        {
            bool communication_complete = false;
            bool humanities_complete = false;
            bool social_complete = false;
            bool math_complete = false;
            bool science_complete = false;

            int credits = 0;
           
            foreach (CompletedCourses course in completedCourses)
            {
                if(credits >= 15) { break; }

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

            return new RequirementsCheck("State of Florida Requirements", credits, 15, credits >= 15 ? true : false);
        }
    }
}
