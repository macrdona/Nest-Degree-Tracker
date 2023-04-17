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

    public class SpecificRequirements
    {
        public bool OralRequirement { get; set; }
        [Required]
        public int GeneralRequirements { get; set; }
    }

    public class Requirements
    {
        public Requirements(string name, int completedCredits, int totalCredits, bool satisfied)
        {
            this.Name = name;
            this.CompletedCredits = completedCredits;
            this.TotalCredits = totalCredits;
            this.Satisfied = satisfied;
        }

        [JsonIgnore]
        public int UserId { get; set; }
        public string? Name { get; set; }
        public int CompletedCredits { get; set; }
        public int TotalCredits { get; set; }
        public bool Satisfied { get; set; }
    }

    public static class UniversityRequirements
    {
        static List<string> humanities = new List<string> { "ARH2000", "HUM2020", "LIT2020", "MUL2010", "PHI2010", "THE2000" };
        static List<string> social = new List<string> { "AMH2020", "ANT2000", "ECO2013", "POS2041", "PSY2012", "SYG2000" };
        static List<string> math = new List<string> { "MAC1105", "MGF1106", "MGF1107", "STA2023", "STA2014", "MAC1101C", "MAC1105C", "MAC1147", "MAC1114", "MAC2233", "MAC2311" };
        static List<string> science = new List<string> { "AST2002", "BSC1005", "BSC1010C", "CHM2045", "ESC2000", "PHY1020", "CHM1020", "EVR1001" };
        
        public static List<Requirements> CheckRequirements(IEnumerable<CoursesRequest> courses, int majorId)
        {
            List<Requirements> requirements = new List<Requirements>();
            var results = StateRequirements(courses);
            requirements.Add(results);

            var major_results = Majors(courses, majorId);
            foreach(var result in major_results)
            {
                requirements.Add(result);
            }

            return requirements;
        }

        public static Requirements StateRequirements(IEnumerable<CoursesRequest> courses)
        {
            bool communication_complete = false;
            bool humanities_complete = false;
            bool social_complete = false;
            bool math_complete = false;
            bool science_complete = false;

            int credits = 0;
           
            foreach (CoursesRequest course in courses)
            {
                if(credits >= 15) { break; }

                if (course.CourseId == "ENC1101" && !communication_complete)
                {
                    credits += course.Credits;
                    communication_complete= true;
                }

                else if (humanities.Contains(course.CourseId) && !humanities_complete)
                {
                    credits += course.Credits;
                    humanities_complete = true;
                }

                else if (social.Contains(course.CourseId) && !social_complete)
                {
                    credits += course.Credits;
                    social_complete= true;
                }

                else if (math.Contains(course.CourseId) && !math_complete)
                {
                    credits += course.Credits;
                    math_complete= true;
                }

                else if (science.Contains(course.CourseId) && !science_complete)
                {
                    credits += course.Credits;
                    science_complete= true;
                }
            }

            var satisfied = false;
            if(credits >= 15)
            {
                credits = 15;
                satisfied = true;
            }

            return new Requirements("State of Florida Requirements", credits, 15, satisfied);
        }

        public static List<Requirements> Majors(IEnumerable<CoursesRequest> courses, int majorId)
        {
            List<Requirements> result = null;

            switch(majorId)
            {
                case 1: result = ComputerScience.CheckAll(courses); break;
                case 2: result = DataScience.CheckAll(courses); break;
                case 3: result = InformationTechnology.CheckAll(courses); break;
                case 4: result = InformationSystems.CheckAll(courses); break;
                case 5: result = InformationScience.CheckAll(courses); break;
            }

            return result;
        }
    }
}
