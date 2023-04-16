namespace backend.Entities
{

    public static class ComputerScience
    {
        public static Requirements Prerequisites(List<CompletedCourses> completedCourses, List<Course> courses)
        {
            List<string> taken = new List<string>();

            var name = "Prerequisites";
            var total_credits = 25;
            var earned_credits = 0;
            var additional_courses = 2;

            foreach(CompletedCourses course in completedCourses)
            {
                var courseId = course.CourseId;

                if (courseId == "COP2220" || courseId == "MAC2311" || courseId == "MAC2312")
                {
                    earned_credits += (int)courses.FirstOrDefault(x => x.CourseId == courseId).Credits;
                    taken.Add(courseId);
                }
                else if(!taken.Contains("PHY2048C") && courseId == "PHY2048")
                {
                    earned_credits += (int)courses.FirstOrDefault(x => x.CourseId == courseId).Credits;
                    taken.Add(courseId);
                }
                else if (!taken.Contains("PHY2048C") && courseId == "PHY2048L")
                {
                    earned_credits += (int)courses.FirstOrDefault(x => x.CourseId == courseId).Credits;
                    taken.Add(courseId);
                }
                else if (!taken.Contains("PHY2048") && courseId == "PHY2048C")
                {
                    earned_credits += (int)courses.FirstOrDefault(x => x.CourseId == courseId).Credits;
                    taken.Add(courseId);
                }
                else if (!taken.Contains("PHY2048C") && courseId == "PHY2048")
                {
                    earned_credits += (int)courses.FirstOrDefault(x => x.CourseId == courseId).Credits;
                    taken.Add(courseId);
                }
                else if(additional_courses > 0 && courseId == "AST2002" || courseId == "BSC1010C" || courseId == "BSC1011C" || courseId == "CHM2045" || courseId == "CHM2046" || courseId == "ESC2000")
                {
                    earned_credits += (int)courses.FirstOrDefault(x => x.CourseId == courseId).Credits;
                    additional_courses--;
                }
            }

            var satisfied = false;
            if(earned_credits >= total_credits)
            {
                earned_credits = total_credits;
                satisfied = true;
            }
            return new Requirements(name,earned_credits,total_credits, satisfied);
        }

        public static Requirements CoreReqs(List<CompletedCourses> completedCourses, List<Course> courses)
        {
            var name = "Core Requirements";
            var total_credits = 18;
            var earned_credits = 0;

            foreach (CompletedCourses course in completedCourses)
            {
                var courseId = course.CourseId;

                if (courseId == "COT3100" || courseId == "COP3503" || courseId == "COP3530" || courseId == "CIS3253" || courseId == "COP3703" || courseId == "CNT4504")
                {
                    earned_credits += (int)courses.FirstOrDefault(x => x.CourseId == courseId).Credits;
                }
            }

            var satisfied = false;
            if (earned_credits >= total_credits)
            {
                earned_credits = total_credits;
                satisfied = true;
            }
            return new Requirements(name, earned_credits, total_credits, satisfied);
        }

        public static Requirements MajorReqs(List<CompletedCourses> completedCourses, List<Course> courses)
        {
            var name = "Major Requirements";
            var total_credits = 32;
            var earned_credits = 0;

            foreach (CompletedCourses course in completedCourses)
            {
                var courseId = course.CourseId;

                if (courseId == "CDA3100" || courseId == "COT3210" || courseId == "COP3404" || courseId == "CEN4010" || courseId == "COP4610" || courseId == "COP4620" || courseId == "CAP4630" || courseId == "CAP4630" || courseId == "MAS3105" || courseId == "STA3032")
                {
                    earned_credits += (int)courses.FirstOrDefault(x => x.CourseId == courseId).Credits;
                }
            }

            var satisfied = false;
            if (earned_credits >= total_credits)
            {
                earned_credits = total_credits;
                satisfied = true;
            }
            return new Requirements(name, earned_credits, total_credits, satisfied);
        }

        public static Requirements Other(List<CompletedCourses> completedCourses, List<Course> courses, int other_credits)
        {
            var name = "Total Credits";
            var total_credits = 120;
            var earned_credits = 0;

            foreach (CompletedCourses course in completedCourses)
            {
                var courseId = course.CourseId;
                earned_credits += (int)courses.FirstOrDefault(x => x.CourseId == courseId).Credits;
            }

            var satisfied = false;
            if (earned_credits >= total_credits)
            {
                earned_credits = total_credits;
                satisfied = true;
            }
            return new Requirements(name, earned_credits, total_credits, satisfied);
        }

        public static List<Requirements> CheckAll(List<CompletedCourses> completedCourses, List<Course> courses)
        {
            List<Requirements> reqs = new List<Requirements>();
            var results = Prerequisites(completedCourses, courses);
            var credits = results.CompletedCredits;
            reqs.Add(results);

            results = CoreReqs(completedCourses, courses);
            credits += results.CompletedCredits;
            reqs.Add(results);

            results = MajorReqs(completedCourses, courses);
            credits += results.CompletedCredits;
            reqs.Add(results);

            results = Other(completedCourses, courses, credits);
            reqs.Add(results);

            return reqs;
        }
    }
}
