using backend.Entities.MajorsRequirements;
using backend.Models;

namespace backend.Entities
{
    public class UniversityRequirements
    {
        public List<string> humanities = new List<string> { "ARH2000", "HUM2020", "LIT2020", "MUL2010", "PHI2010", "THE2000" };
        public List<string> social = new List<string> { "AMH2020", "ANT2000", "ECO2013", "POS2041", "PSY2012", "SYG2000" };
        public List<string> math = new List<string> { "MAC1105", "MGF1106", "MGF1107", "STA2023", "STA2014", "MAC1101C", "MAC1105C", "MAC1147", "MAC1114", "MAC2233", "MAC2311" };
        public List<string> science = new List<string> { "AST2002", "BSC1005", "BSC1010C", "CHM2045", "ESC2000", "PHY1020", "CHM1020", "EVR1001" };

        public List<Requirements> CheckRequirements(IEnumerable<CoursesRequest> courses, int majorId)
        {
            List<Requirements> requirements = new List<Requirements>();
            var results = StateRequirements(courses);
            requirements.Add(results);

            results = UNFGeneralRequirements(courses);
            requirements.Add(results);

            var major_results = Majors(courses, majorId);
            foreach (var result in major_results)
            {
                requirements.Add(result);
            }

            return requirements;
        }

        public Requirements StateRequirements(IEnumerable<CoursesRequest> courses)
        {
            Dictionary<string, List<string>> missing_courses = new Dictionary<string, List<string>>();
            bool communication_complete = false;
            bool humanities_complete = false;
            bool social_complete = false;
            bool math_complete = false;
            bool science_complete = false;

            var name = "State of Florida Requirements";
            int total_credits = 15;
            int earned_credits = 0;

            foreach (CoursesRequest course in courses)
            {
                var course_id = course.CourseId;

                if (earned_credits >= total_credits) { break; }

                if (course_id == "ENC1101" && !communication_complete)
                {
                    earned_credits += course.Credits;
                    communication_complete = true;
                }

                else if (humanities.Contains(course_id) && !humanities_complete)
                {
                    earned_credits += course.Credits;
                    humanities_complete = true;
                    humanities.Remove(course_id); 
                }

                else if (social.Contains(course_id) && !social_complete)
                {
                    earned_credits += course.Credits;
                    social_complete = true;
                    social.Remove(course_id);
                }

                else if (math.Contains(course_id) && !math_complete)
                {
                    earned_credits += course.Credits;
                    math_complete = true;
                    math.Remove(course_id);
                }

                else if (science.Contains(course_id) && !science_complete)
                {
                    earned_credits += course.Credits;
                    science_complete = true;
                    science.Remove(course_id);
                }
            }

            var satisfied = false;
            if (earned_credits >= total_credits)
            {
                earned_credits = total_credits;
                satisfied = true;
            }

            if (!communication_complete)
            {
                missing_courses[$"Must complete 1 of the following course(s) "] = new List<string>() { "ENC1101" };
            }

            if (!humanities_complete)
            {
                missing_courses[$"Must complete 1 of the following course(s) "] = humanities;
            }
            if (!social_complete)
            {
                missing_courses[$"Must complete 1 of the following course(s) "] = social;
            }

            if (!math_complete)
            {
                missing_courses[$"Must complete 1 of the following course(s) "] = math;
            }
            if (!science_complete)
            {
                missing_courses[$"Must complete 1 of the following course(s) "] = science;
            }

            return new Requirements(name, earned_credits, total_credits, satisfied, missing_courses);
        }

        public Requirements UNFGeneralRequirements(IEnumerable<CoursesRequest> courses)
        {
            List<string> writing_selection_courses1 = new List<string>() { "ENC1143", "IDS1932" };
            List<string> writing_selection_courses2 = new List<string>() { "ENC 1102", "CRW2000", "CRW2100", "CRW2201", "CRW2300", "CRW2400", "CRW2600", "ENC2210", "ENC2443", "ENC2451", "ENC2461", "ENC3202", "ENC3246", "ENC3250" };
            List<string> thinking_selection_courses1 = new List<string>() { "ANT2000", "ANT2423", "ASN2003", "EDF2085", "GEB2956", "GEO2420", "MMC2701", "MUH2501", "PUP2312", "REL2300", "SYG2013" };
            List<string> thinking_selection_courses2 = new List<string>() { "ARH2050", "ARH2051", "CCJ2002", "FIL2000", "HSC2100", "LDR3003", "MUH2012", "MUH2017", "MUH2018", "MUT1111", "PHI2100", "PHI2630", "WOH1012", "WOH1022" };
            List<string> analysis_selection_courses1 = new List<string>() { "MGF1113", "PHI2101", "IDC2000", "CHM1025", "GLY2010", "HUN2201", "PHY1028" };
            List<string> analysis_selection_courses2 = new List<string>() { "AST2002L", "BSC1005L", "CHM1025L", "CHM2045L", "ESC2000L", "PHY1020L", "PHY1028L" };

            Dictionary<string, List<string>> missing_courses = new Dictionary<string, List<string>>();

            int writing_selection_limit1 = 1;
            int writing_selection_limit2 = 1;
            int thinking_selection_limit1 = 1;
            int thinking_selection_limit2 = 1;
            int analysis_selection_limit1 = 1;
            int analysis_selection_limit2 = 1;

            var name = "UNF General Education Requirements";
            int total_credits = 21;
            int earned_credits = 0;

            foreach (CoursesRequest course in courses)
            {
                var course_id = course.CourseId;
                if(writing_selection_limit1 > 0 && writing_selection_courses1.Contains(course_id))
                {
                    earned_credits += course.Credits;
                    writing_selection_limit1--;
                }
                else if(writing_selection_limit2 > 0 && writing_selection_courses2.Contains(course_id))
                {
                    earned_credits += course.Credits;
                    writing_selection_limit2--;
                }
                else if (thinking_selection_limit1 > 0 && thinking_selection_courses1.Contains(course_id))
                {
                    earned_credits += course.Credits;
                    writing_selection_limit1--;
                }
                else if (thinking_selection_limit2 > 0 && thinking_selection_courses2.Contains(course_id) || thinking_selection_courses1.Contains(course_id) || social.Contains(course_id) || humanities.Contains(course_id))
                {
                    earned_credits += course.Credits;
                    writing_selection_limit2--;
                }
                else if (analysis_selection_limit1 > 0 && analysis_selection_courses1.Contains(course_id) || math.Contains(course_id) || science.Contains(course_id))
                {
                    earned_credits += course.Credits;
                    analysis_selection_limit1--;
                }
                else if (analysis_selection_limit2 > 0 && analysis_selection_courses2.Contains(course_id))
                {
                    earned_credits += course.Credits;
                    analysis_selection_limit2--;
                }

            }

            var satisfied = false;
            if (earned_credits >= total_credits)
            {
                earned_credits = total_credits;
                satisfied = true;
            }

            if(writing_selection_limit1 > 0)
            {
                missing_courses[$"Must complete {writing_selection_limit1} of the following course(s) "] = writing_selection_courses1;
            }

            if (writing_selection_limit2 > 0)
            {
                missing_courses[$"Must complete {writing_selection_limit1} of the following course(s) s) "] = writing_selection_courses2;
            }

            if (thinking_selection_limit1 > 0)
            {
                missing_courses[$"Must complete {thinking_selection_limit1} of the following course(s) "] = thinking_selection_courses1;
            }

            if (thinking_selection_limit2 > 0)
            {
                missing_courses[$"Must complete {thinking_selection_limit2} of the following course(s) "] = thinking_selection_courses2;
            }

            if (analysis_selection_limit1 > 0)
            {
                missing_courses[$"Must complete  {analysis_selection_limit1}  of the following course(s) "] = analysis_selection_courses1;
            }

            if (analysis_selection_limit2 > 0)
            {
                missing_courses[$"Must complete  {analysis_selection_limit2}  of the following course(s) "] = analysis_selection_courses2;
            }

            return new Requirements(name, earned_credits, total_credits, satisfied, missing_courses);
        }

        public List<Requirements> Majors(IEnumerable<CoursesRequest> courses, int majorId)
        {
            List<Requirements> result = null;

            switch (majorId)
            {
                case 1: result = new ComputerScience().CheckAll(courses); break;
                case 2: result = new DataScience().CheckAll(courses); break;
                case 3: result = new InformationTechnology().CheckAll(courses); break;
                case 4: result = new InformationSystems().CheckAll(courses); break;
                case 5: result = new InformationScience().CheckAll(courses); break;
            }

            return result;
        }
    }
}
