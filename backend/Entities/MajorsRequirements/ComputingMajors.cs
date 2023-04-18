using backend.Models;
using Duende.IdentityServer.Extensions;
using Microsoft.AspNetCore.Components.Web.Virtualization;

namespace backend.Entities.MajorsRequirements
{
    public class CollegeOfComputing
    {
        public virtual int requisites_credits { get; } = 3;
        public virtual int prerequisites_credits { get; }  = 25;
        public virtual int core_requirement_credits { get; }  = 18;
        public virtual int major_requirement_credits { get; }  = 32;

        public virtual Requirements Requisites(IEnumerable<CoursesRequest> courses)
        {
            Dictionary<string, List<string>> missing_courses = new Dictionary<string, List<string>>();
            var name = "Requisites";
            var total_credits = requisites_credits;
            var earned_credits = 0;

            foreach (CoursesRequest course in courses)
            {
                if (course.CourseId.Contains("SPC"))
                {
                    earned_credits += course.Credits;
                    break;
                }
            }

            if(total_credits != earned_credits)
            {
                missing_courses["Must take any public speaking courses. Recommended course: "] = new List<string>() { "SPC4064" };
            }

            return new Requirements(name, earned_credits, total_credits, earned_credits == total_credits ? true : false, missing_courses);
        }

        public virtual Requirements Prerequisites(IEnumerable<CoursesRequest> courses)
        {
            List<string> required = new List<string>() { "COP2220", "MAC2311", "MAC2312", "PHY2048", "PHY2048L", "PHY2048C", "PHY2049", "PHY2049L", "PHY2049C" };
            List<string> user_selection_courses = new List<string>() { "AST2002", "BSC1010C", "BSC1011C", "CHM2045", "CHM2046", "ESC2000"};
            List<string> user_selection_prefixes = new List<string>() { "APB", "AST", "BCH", "BOT", "BSC", "CHM", "CHS", "ESC", "GLY", "ISC", "MCB", "PCB", "PHY", "PHZ", "PSC", "ZOO" };
            List<string> exceptions = new List<string>() { "BSC1005C", "BSC1930", "BCH3023C", "CHM1025", "PHY1020", "PHY2053", "PHY2054" };
            Dictionary<string, List<string>> missing_courses = new Dictionary<string, List<string>>();

            var name = "Prerequisites";
            var total_credits = prerequisites_credits;
            var earned_credits = 0;
            int user_selection_limit = 2;

            foreach (CoursesRequest course in courses)
            {
                var course_id = course.CourseId;
                var course_prefix = course_id.Substring(0, 2);

                if (required.Contains(course_id))
                {
                    earned_credits += course.Credits;
                    required.Remove(course_id);

                    if(course_id == "PHY2048C")
                    {
                        required.Remove("PHY2048");
                        required.Remove("PHY2048L");
                    }
                    else if(course_id == "PHY2049C")
                    {
                        required.Remove("PHY2049");
                        required.Remove("PHY2049L");
                    }
                    else if(course_id == "PHY2048L" || course_id == "PHY2048")
                    {
                        required.Remove("PHY2048C");
                    }
                    else if(course_id == "PHY2049L" || course_id == "PHY2049")
                    {
                        required.Remove("PHY2049C");
                    }
                }
                else if (user_selection_limit > 0 && !exceptions.Contains(course_id) && user_selection_courses.Contains(course_id) || user_selection_prefixes.Contains(course_prefix))
                {
                    earned_credits += course.Credits;
                    user_selection_courses.Remove(course_id);
                    user_selection_limit--;
                }
            }

            var satisfied = false;
            if (earned_credits >= total_credits)
            {
                earned_credits = total_credits;
                satisfied = true;
            }

            if (!required.IsNullOrEmpty())
            {
                missing_courses.Add("Must complete the following courses. For physics you only have to complete PHY2048C or PHY2048 & PHY2048L (same applies for PHY2049).", required);
            }
            
            if(user_selection_limit > 0)
            {
                missing_courses.Add($"Must take {user_selection_limit} of the following courses", user_selection_courses);
            }

            return new Requirements(name, earned_credits, total_credits, satisfied, missing_courses);
        }

        public virtual Requirements CoreReqs(IEnumerable<CoursesRequest> courses)
        {
            List<string> required = new List<string>() { "COT3100", "COP3503", "COP3530", "CIS3253", "COP3703", "CNT4504" };
            Dictionary<string, List<string>> missing_courses = new Dictionary<string, List<string>>();

            var name = "Core Requirements";
            var total_credits = core_requirement_credits;
            var earned_credits = 0;

            foreach (CoursesRequest course in courses)
            {
                var course_id = course.CourseId;

                if (required.Contains(course_id))
                {
                    earned_credits += course.Credits;
                    required.Remove(course_id);
                }
            }

            var satisfied = false;
            if (earned_credits >= total_credits)
            {
                earned_credits = total_credits;
                satisfied = true;
            }

            missing_courses.Add("Must complete the following courses", required);

            return new Requirements(name, earned_credits, total_credits, satisfied, missing_courses);
        }

        public virtual Requirements MajorReqs(IEnumerable<CoursesRequest> courses)
        {
            List<string> required = new List<string>() { "CDA3100", "COT3210", "COP3404", "CEN4010", "COP4610", "COP4620", "CAP4630", "MAS3105", "STA3032" };
            Dictionary<string, List<string>> missing_courses = new Dictionary<string, List<string>>();

            var name = "Major Requirements";
            var total_credits = major_requirement_credits;
            var earned_credits = 0;

            foreach (CoursesRequest course in courses)
            {
                var course_id = course.CourseId;

                if (required.Contains(course_id))
                {
                    earned_credits += course.Credits;
                    required.Remove(course_id);
                }
            }

            var satisfied = false;
            if (earned_credits >= total_credits)
            {
                earned_credits = total_credits;
                satisfied = true;
            }

            missing_courses.Add("Must complete the following courses", required);

            return new Requirements(name, earned_credits, total_credits, satisfied, missing_courses);
        }

        public virtual Requirements Other(IEnumerable<CoursesRequest> courses)
        {
            var name = "Total Credits";
            var total_credits = 120;
            var earned_credits = 0;

            foreach (CoursesRequest course in courses)
            {
                var courseId = course.CourseId;
                earned_credits += course.Credits;
            }

            var satisfied = false;
            if (earned_credits >= total_credits)
            {
                earned_credits = total_credits;
                satisfied = true;
            }

            return new Requirements(name, earned_credits, total_credits, satisfied, new Dictionary<string, List<string>>());
        }

        public List<Requirements> CheckAll(IEnumerable<CoursesRequest> courses)
        {
            List<Requirements> reqs = new List<Requirements>();
            var results = Prerequisites(courses);
            reqs.Add(results);

            results = Requisites(courses);
            reqs.Add(results);

            results = CoreReqs(courses);
            reqs.Add(results);

            results = MajorReqs(courses);
            reqs.Add(results);

            results = Other(courses);
            reqs.Add(results);

            return reqs;
        }
    }

    public class ComputerScience : CollegeOfComputing { }

    public class DataScience : CollegeOfComputing
    {
        override public int requisites_credits { get; } = 14;
        override public int prerequisites_credits { get; } = 11;
        override public int core_requirement_credits { get; } = 18;
        override public int major_requirement_credits { get; } = 33;
        override public Requirements Requisites(IEnumerable<CoursesRequest> courses)
        {
            List<string> required = new List<string>() { "ENC2210" };
            List<string> sequence1_courses = new List<string>() { "BSC1010", "BSC1011" };
            List<string> sequence2_courses = new List<string>() { "CHM2045", "CHM2045L", "CHM2046L", "CHM2046L" };
            List<string> sequence3_courses = new List<string>() { "PHY2048", "PHY2048L", "PHY2048C", "PHY2049", "PHY2049L", "PHY2049C" };
            Dictionary<string, List<string>> missing_courses = new Dictionary<string, List<string>>();

            var name = "Requisites";
            var total_credits = requisites_credits;
            var earned_credits = 0;


            var speaking_req = false;
            var sequence1 = false;
            var sequence2 = false;
            var sequence3 = false;

            foreach (CoursesRequest course in courses)
            {
                var course_id = course.CourseId;

                if (!speaking_req && course.CourseId.Contains("SPC"))
                {
                    earned_credits += course.Credits;
                    speaking_req = true;
                }
                else if (course_id == "ENC2210")
                {
                    earned_credits += course.Credits;
                    required.Remove(course_id);
                }
                else if (!sequence2 && !sequence3 && sequence1_courses.Contains(course_id))
                {
                    earned_credits += course.Credits;
                    sequence1 = true;
                    sequence1_courses.Remove(course_id);
                }
                else if (!sequence1 && !sequence3 && sequence2_courses.Contains(course_id))
                {
                    earned_credits += course.Credits;
                    sequence2 = true;
                    sequence2_courses.Remove(course_id);
                }
                else if (!sequence2 && !sequence1 && sequence3_courses.Contains(course_id))
                {
                    earned_credits += course.Credits;
                    sequence3_courses.Remove(course_id);
                    sequence3 = true;

                    if (course_id == "PHY2048C")
                    {
                        sequence3_courses.Remove("PHY2048");
                        sequence3_courses.Remove("PHY2048L");
                    }
                    else if (course_id == "PHY2049C")
                    {
                        sequence3_courses.Remove("PHY2049");
                        sequence3_courses.Remove("PHY2049L");
                    }
                    else if (course_id == "PHY2048L" || course_id == "PHY2048")
                    {
                        sequence3_courses.Remove("PHY2048C");
                    }
                    else if (course_id == "PHY2049L" || course_id == "PHY2049")
                    {
                        sequence3_courses.Remove("PHY2049C");
                    }
                }

            }

            if (!speaking_req)
            {
                missing_courses["Must take any public speaking courses. Recommended course: "] = new List<string>() { "SPC4064" };
            }

            if (!required.IsNullOrEmpty())
            {
                missing_courses["Must complete the following course(s). For physics you only have to complete PHY2048C or PHY2048 & PHY2048L (same applies for PHY2049)."] = required;
            }

            if(sequence1 && !sequence1_courses.IsNullOrEmpty())
            {
                missing_courses["Must complete the following biology course(s) "] = sequence1_courses;
            }
            else if (sequence2 && !sequence2_courses.IsNullOrEmpty())
            {
                missing_courses["Must complete the following chemistry course(s) "] = sequence2_courses;
            }
            else if (sequence3 && !sequence3_courses.IsNullOrEmpty())
            {
                missing_courses["Must complete some the following physics lab & course(s) "] = sequence3_courses;
            }
            else if(!sequence1 && !sequence2 && !sequence3)
            {
                missing_courses["Must complete the following biology course(s) "] = sequence1_courses;
            }

            return new Requirements(name, earned_credits, total_credits, earned_credits == total_credits ? true : false, missing_courses);
        }

        override public Requirements Prerequisites(IEnumerable<CoursesRequest> courses)
        {
            List<string> required = new List<string>() { "COP2220", "MAC2311", "MAC2312" };
            Dictionary<string, List<string>> missing_courses = new Dictionary<string, List<string>>();

            var name = "Prerequisites";
            var total_credits = prerequisites_credits;
            var earned_credits = 0;

            foreach (CoursesRequest course in courses)
            {
                var course_id = course.CourseId;

                if (required.Contains(course_id))
                {
                    earned_credits += course.Credits;
                    required.Remove(course_id);
                }
            }

            var satisfied = false;
            if (earned_credits >= total_credits)
            {
                earned_credits = total_credits;
                satisfied = true;
            }

            if (!required.IsNullOrEmpty())
            {
                missing_courses["Must complete the following course(s) "] = required;
            }

            return new Requirements(name, earned_credits, total_credits, satisfied, missing_courses);
        }

        override public Requirements CoreReqs(IEnumerable<CoursesRequest> courses)
        {
            List<string> required = new List<string>() { "COP3503", "COP3530", "CIS3253", "COP3703", "CNT4504" };
            List<string> user_selection_courses = new List<string>() { "COT3100", "MAD3107" };
            Dictionary<string, List<string>> missing_courses = new Dictionary<string, List<string>>();

            var name = "Core Requirements";
            var total_credits = core_requirement_credits;
            var earned_credits = 0;
            int user_selection_limit = 1;

            foreach (CoursesRequest course in courses)
            {
                var course_id = course.CourseId;

                if (required.Contains(course_id))
                {
                    earned_credits += course.Credits;
                    required.Remove(course_id);
                }
                else if (user_selection_limit > 0 && user_selection_courses.Contains(course_id))
                {
                    earned_credits += course.Credits;
                    user_selection_limit--;
                }
                
            }

            var satisfied = false;
            if (earned_credits >= total_credits)
            {
                earned_credits = total_credits;
                satisfied = true;
            }

            if (!required.IsNullOrEmpty())
            {
                missing_courses.Add("Must complete the following courses", required);
            }

            if (user_selection_limit > 0)
            {
                missing_courses.Add($"Must take {user_selection_limit} of the following courses", user_selection_courses);
            }

            return new Requirements(name, earned_credits, total_credits, satisfied, missing_courses);
        }

        override public Requirements MajorReqs(IEnumerable<CoursesRequest> courses)
        {
            List<string> required = new List<string>() { "MAS3105", "STA3163", "STA3164", "STA4321", "CAP4784", "CAP4770", "COT4400" };
            List<string> user_selection_courses1 = new List<string>() { "COT4560", "COT4111", "COT4461", "MAD4301", "MAD4203", "MAD4505" };
            List<string> user_selection_courses2 = new List<string>() { "STA4502", "STA4504" };
            List<string> user_selection_courses3 = new List<string>() { "STA4945", "CIS4900", "MAS4932", "MAT4906" };
            Dictionary<string, List<string>> missing_courses = new Dictionary<string, List<string>>();

            var name = "Major Requirements";
            var total_credits = major_requirement_credits;
            var earned_credits = 0;

            var user_selection_limit1 = 1;
            var user_selection_limit2 = 1;
            var user_selection_limit3 = 1;

            foreach (CoursesRequest course in courses)
            {
                var course_id = course.CourseId;

                if (required.Contains(course_id))
                {
                    earned_credits += course.Credits;
                    required.Remove(course_id);
                }
                else if (user_selection_limit1 > 0 && user_selection_courses1.Contains(course_id))
                {
                    earned_credits += course.Credits;
                    user_selection_limit1--;
                }
                else if (user_selection_limit2 > 0 && user_selection_courses2.Contains(course_id))
                {
                    earned_credits += course.Credits;
                    user_selection_limit2--;
                }
                else if (user_selection_limit3 > 0 && user_selection_courses3.Contains(course_id))
                {
                    earned_credits += course.Credits;
                    user_selection_limit3--;
                }
            }

            var satisfied = false;
            if (earned_credits >= total_credits)
            {
                earned_credits = total_credits;
                satisfied = true;
            }

            if (!required.IsNullOrEmpty())
            {
                missing_courses["Must complete the following course(s) "] = required;
            }

            if (user_selection_limit1 > 0 && !user_selection_courses1.IsNullOrEmpty())
            {
                missing_courses[$"Must complete {user_selection_limit1} of the following course(s) from selection 1"] = user_selection_courses1;
            }
            
            if (user_selection_limit2 > 0 && !user_selection_courses2.IsNullOrEmpty())
            {
                missing_courses[$"Must complete {user_selection_limit2} of the following course(s) from selection 2"] = user_selection_courses2;
            }
            
            if (user_selection_limit3 > 0 && !user_selection_courses3.IsNullOrEmpty())
            {
                missing_courses[$"Must complete {user_selection_limit3} of the following course(s) from selection 3"] = user_selection_courses3;
            }

            return new Requirements(name, earned_credits, total_credits, satisfied, missing_courses);
        }
    }

    public class InformationScience : CollegeOfComputing
    {
        override public int prerequisites_credits { get; } = 12;
        override public int major_requirement_credits { get; } = 19;

        override public Requirements Prerequisites(IEnumerable<CoursesRequest> courses)
        {
            List<string> required = new List<string>() { "COP2220", "CGS1570" };
            List<string> user_selection_courses1 = new List<string>() { "MAC2233", "MAC2311" };
            List<string> user_selection_courses2 = new List<string>() { "STA2023", "STA2122" };
            Dictionary<string, List<string>> missing_courses = new Dictionary<string, List<string>>();

            var name = "Prerequisites";
            var total_credits = prerequisites_credits;
            var earned_credits = 0;

            var user_selection_limit1 = 1;
            var user_selection_limit2 = 1;

            foreach (CoursesRequest course in courses)
            {
                var course_id = course.CourseId;

                if (required.Contains(course_id))
                {
                    earned_credits += course.Credits;
                    required.Remove(course_id);
                }
                else if (user_selection_limit1 > 0 && user_selection_courses1.Contains(course_id))
                {
                    earned_credits += course.Credits;
                    user_selection_limit1--;
                }
                else if (user_selection_limit2 > 0 && user_selection_courses2.Contains(course_id))
                {
                    earned_credits += course.Credits;
                    user_selection_limit2--;
                }
            }

            var satisfied = false;
            if (earned_credits >= total_credits)
            {
                earned_credits = total_credits;
                satisfied = true;
            }

            if (!required.IsNullOrEmpty())
            {
                missing_courses["Must complete the following course(s) "] = required;
            }

            if (user_selection_limit1 > 0 && !user_selection_courses1.IsNullOrEmpty())
            {
                missing_courses[$"Must complete {user_selection_limit1} the following course(s) from selection 1"] = user_selection_courses1;
            }
            
            if (user_selection_limit2 > 0 && !user_selection_courses2.IsNullOrEmpty())
            {
                missing_courses[$"Must complete {user_selection_limit2} the following course(s) from selection 2"] = user_selection_courses2;
            }

            return new Requirements(name, earned_credits, total_credits, satisfied, missing_courses);
        }

        override public Requirements MajorReqs(IEnumerable<CoursesRequest> courses)
        {
            List<string> required = new List<string>() { "COP3855", "CDA4010", "COP4813", "CAP4784", "CIS4327", "CIS4328" };
            Dictionary<string, List<string>> missing_courses = new Dictionary<string, List<string>>();

            var name = "Major Requirements";
            var total_credits = major_requirement_credits;
            var earned_credits = 0;

            foreach (CoursesRequest course in courses)
            {
                var course_id = course.CourseId;

                if (required.Contains(course_id))
                {
                    earned_credits += course.Credits;
                    required.Remove(course_id);
                }
            }

            var satisfied = false;
            if (earned_credits >= total_credits)
            {
                earned_credits = total_credits;
                satisfied = true;
            }

            if (!required.IsNullOrEmpty())
            {
                missing_courses["Must complete the following course(s) "] = required;
            }

            return new Requirements(name, earned_credits, total_credits, satisfied, missing_courses);
        }
    }

    public class InformationSystems : CollegeOfComputing
    {
        override public int prerequisites_credits { get; } = 24;
        override public int major_requirement_credits { get; } = 31;
        override public Requirements Prerequisites(IEnumerable<CoursesRequest> courses)
        {
            List<string> required = new List<string>() { "COP2220", "ACG2021", "ACG2071", "CGS1100", "ECO2013", "ECO2023" };
            List<string> user_selection_courses1 = new List<string>() { "MAC2233", "MAC2311" };
            List<string> user_selection_courses2 = new List<string>() { "STA2023", "STA2122" };
            Dictionary<string, List<string>> missing_courses = new Dictionary<string, List<string>>();

            var name = "Prerequisites";
            var total_credits = prerequisites_credits;
            var earned_credits = 0;

            var user_selection_limit1 = 1;
            var user_selection_limit2 = 1;

            foreach (CoursesRequest course in courses)
            {
                var course_id = course.CourseId;

                if (required.Contains(course_id))
                {
                    earned_credits += course.Credits;
                    required.Remove(course_id);
                }
                else if (user_selection_limit1 > 0 && user_selection_courses1.Contains(course_id))
                {
                    earned_credits += course.Credits;
                    user_selection_limit1--;
                }
                else if (user_selection_limit2 > 0 && user_selection_courses2.Contains(course_id))
                {
                    earned_credits += course.Credits;
                    user_selection_limit2--;
                }
            }

            var satisfied = false;
            if (earned_credits >= total_credits)
            {
                earned_credits = total_credits;
                satisfied = true;
            }

            if (!required.IsNullOrEmpty())
            {
                missing_courses["Must complete the following course(s) "] = required;
            }

            if (user_selection_limit1 > 0 && !user_selection_courses1.IsNullOrEmpty())
            {
                missing_courses[$"Must complete {user_selection_limit1} the following course(s) from selection 1"] = user_selection_courses1;
            }
            
            if (user_selection_limit2 > 0 && !user_selection_courses2.IsNullOrEmpty())
            {
                missing_courses[$"Must complete {user_selection_limit2} the following course(s) from selection 2"] = user_selection_courses2;
            }

            return new Requirements(name, earned_credits, total_credits, satisfied, missing_courses);
        }

        override public Requirements MajorReqs(IEnumerable<CoursesRequest> courses)
        {
            List<string> required = new List<string>() { "COP3855", "CDA4010", "COP3813", "COP4854", "CAP4784", "CIS4327", "CIS4328", "ISM4011", "MAN3025", "FIN3403" };
            Dictionary<string, List<string>> missing_courses = new Dictionary<string, List<string>>();

            var name = "Major Requirements";
            var total_credits = major_requirement_credits;
            var earned_credits = 0;

            foreach (CoursesRequest course in courses)
            {
                var course_id = course.CourseId;

                if (required.Contains(course_id))
                {
                    earned_credits += course.Credits;
                    required.Remove(course_id);
                }
            }

            var satisfied = false;
            if (earned_credits >= total_credits)
            {
                earned_credits = total_credits;
                satisfied = true;
            }

            if (!required.IsNullOrEmpty())
            {
                missing_courses["Must complete the following course(s) "] = required;
            }

            return new Requirements(name, earned_credits, total_credits, satisfied, missing_courses);
        }
    }

    public class InformationTechnology : CollegeOfComputing
    {
        override public int prerequisites_credits { get; } = 12;
        override public int major_requirement_credits { get; } = 30;
        override public Requirements Prerequisites(IEnumerable<CoursesRequest> courses)
        {
            List<string> required = new List<string>() { "COP2220", "CGS1570" };
            List<string> user_selection_courses1 = new List<string>() { "MAC2233", "MAC2311" };
            List<string> user_selection_courses2 = new List<string>() { "STA2023", "STA2122" };
            Dictionary<string, List<string>> missing_courses = new Dictionary<string, List<string>>();

            var name = "Prerequisites";
            var total_credits = prerequisites_credits;
            var earned_credits = 0;

            var user_selection_limit1 = 1;
            var user_selection_limit2 = 1;

            foreach (CoursesRequest course in courses)
            {
                var course_id = course.CourseId;

                if (required.Contains(course_id))
                {
                    earned_credits += course.Credits;
                    required.Remove(course_id);
                }
                else if (user_selection_limit1 > 0 && user_selection_courses1.Contains(course_id))
                {
                    earned_credits += course.Credits;
                    user_selection_limit1--;
                }
                else if (user_selection_limit2 > 0 && user_selection_courses2.Contains(course_id))
                {
                    earned_credits += course.Credits;
                    user_selection_limit2--;
                }
            }

            var satisfied = false;
            if (earned_credits >= total_credits)
            {
                earned_credits = total_credits;
                satisfied = true;
            }

            if (!required.IsNullOrEmpty())
            {
                missing_courses["Must complete the following course(s) "] = required;
            }

            if (user_selection_limit1 > 0 && !user_selection_courses1.IsNullOrEmpty())
            {
                missing_courses[$"Must complete {user_selection_limit1} the following course(s) from selection 1"] = user_selection_courses1;
            }
            
            if (user_selection_limit2 > 0 && !user_selection_courses2.IsNullOrEmpty())
            {
                missing_courses[$"Must complete {user_selection_limit2} the following course(s) from selection 2"] = user_selection_courses2;
            }

            return new Requirements(name, earned_credits, total_credits, satisfied, missing_courses);
        }

        override public Requirements MajorReqs(IEnumerable<CoursesRequest> courses)
        {
            List<string> required = new List<string>() { "CIS3526", "COP4640", "CIS4360", "CIS4362", "CIS4364", "CIS4365", "CIS4366", "CNT4406", "CEN4083", "CIS4325" };
            Dictionary<string, List<string>> missing_courses = new Dictionary<string, List<string>>();

            var name = "Major Requirements";
            var total_credits = major_requirement_credits;
            var earned_credits = 0;

            foreach (CoursesRequest course in courses)
            {
                var course_id = course.CourseId;

                if (required.Contains(course_id))
                {
                    earned_credits += course.Credits;
                    required.Remove(course_id);
                }
            }

            var satisfied = false;
            if (earned_credits >= total_credits)
            {
                earned_credits = total_credits;
                satisfied = true;
            }

            if (!required.IsNullOrEmpty())
            {
                missing_courses["Must complete the following course(s) "] = required;
            }

            return new Requirements(name, earned_credits, total_credits, satisfied, missing_courses);
        }
    }
}
