namespace backend.Entities
{

    public static class ComputerScience
    {
        public static Requirements Requisites(IEnumerable<CoursesRequest> courses)
        {
            var name = "Requisites";
            var total_credits = 3;
            var earned_credits = 0;

            foreach (CoursesRequest course in courses)
            {
                if (course.CourseId.Contains("SPC"))
                {
                    earned_credits += course.Credits;
                    break;
                }
            }

            return new Requirements(name, earned_credits, total_credits, (earned_credits == total_credits) ? true : false);
        }
        public static Requirements Prerequisites(IEnumerable<CoursesRequest> courses)
        {
            List<string> taken = new List<string>();

            var name = "Prerequisites";
            var total_credits = 25;
            var earned_credits = 0;
            var additional_courses = 2;

            foreach(CoursesRequest course in courses)
            {
                var courseId = course.CourseId;

                if (courseId == "COP2220" || courseId == "MAC2311" || courseId == "MAC2312")
                {
                    earned_credits += course.Credits;
                    taken.Add(courseId);
                }
                else if(!taken.Contains("PHY2048C") && courseId == "PHY2048")
                {
                    earned_credits += course.Credits;
                    taken.Add(courseId);
                }
                else if (!taken.Contains("PHY2048C") && courseId == "PHY2048L")
                {
                    earned_credits += course.Credits;
                    taken.Add(courseId);
                }
                else if (!taken.Contains("PHY2048") || !taken.Contains("PHY2048L") && courseId == "PHY2048C")
                {
                    earned_credits += course.Credits;
                    taken.Add(courseId);
                }
                else if (!taken.Contains("PHY2049C") && courseId == "PHY2049")
                {
                    earned_credits += course.Credits;
                    taken.Add(courseId);
                }
                else if (!taken.Contains("PHY2049C") && courseId == "PHY2049L")
                {
                    earned_credits += course.Credits;
                    taken.Add(courseId);
                }
                else if (!taken.Contains("PHY2049") || !taken.Contains("PHY2049L") && courseId == "PHY2049C")
                {
                    earned_credits += course.Credits;
                    taken.Add(courseId);
                }
                else if(additional_courses > 0 && courseId == "AST2002" || courseId == "BSC1010C" || courseId == "BSC1011C" || courseId == "CHM2045" || courseId == "CHM2046" || courseId == "ESC2000")
                {
                    earned_credits += course.Credits;
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

        public static Requirements CoreReqs(IEnumerable<CoursesRequest> courses)
        {
            var name = "Core Requirements";
            var total_credits = 18;
            var earned_credits = 0;

            foreach (CoursesRequest course in courses)
            {
                var courseId = course.CourseId;

                if (courseId == "COT3100" || courseId == "COP3503" || courseId == "COP3530" || courseId == "CIS3253" || courseId == "COP3703" || courseId == "CNT4504")
                {
                    earned_credits += course.Credits;
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

        public static Requirements MajorReqs(IEnumerable<CoursesRequest> courses)
        {
            var name = "Major Requirements";
            var total_credits = 32;
            var earned_credits = 0;

            foreach (CoursesRequest course in courses)
            {
                var courseId = course.CourseId;

                if (courseId == "CDA3100" || courseId == "COT3210" || courseId == "COP3404" || courseId == "CEN4010" || courseId == "COP4610" || courseId == "COP4620" || courseId == "CAP4630" || courseId == "CAP4630" || courseId == "MAS3105" || courseId == "STA3032")
                {
                    earned_credits += course.Credits;
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

        public static Requirements Other(IEnumerable<CoursesRequest> courses)
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
            return new Requirements(name, earned_credits, total_credits, satisfied);
        }

        public static List<Requirements> CheckAll(IEnumerable<CoursesRequest> courses)
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

    public static class DataScience
    {
        public static Requirements Requisites(IEnumerable<CoursesRequest> courses)
        {
            var name = "Requisites";
            var total_credits = 14;
            var earned_credits = 0;
            var speaking_req = false;
            var sequence1 = false;
            var sequence2 = false;
            var sequence3 = false;

            List<string> taken = new List<string>();

            foreach (CoursesRequest course in courses)
            {
                var courseId = course.CourseId;

                if (!speaking_req && course.CourseId.Contains("SPC"))
                {
                    earned_credits += course.Credits;
                    speaking_req = true;
                }
                else if(courseId == "ENC2210")
                {
                    earned_credits += course.Credits;
                }
                else if(!sequence2 && !sequence3 && courseId == "BSC1010" || courseId == "BSC1011")
                {
                    earned_credits++;
                    sequence1 = true;
                }
                else if (!sequence1 && !sequence3 && courseId == "CHM2045" || courseId == "CHM2045L" || courseId == "CHM2046L" || courseId == "CHM2046L")
                {
                    earned_credits++;
                    sequence2 = true;
                }
                else if (!sequence2 && !sequence1)
                {
                    if (!taken.Contains("PHY2048C") && courseId == "PHY2048")
                    {
                        earned_credits += course.Credits;
                        taken.Add(courseId);
                        sequence3 = true;
                    }
                    else if (!taken.Contains("PHY2048C") && courseId == "PHY2048L")
                    {
                        earned_credits += course.Credits;
                        taken.Add(courseId);
                        sequence3 = true;
                    }
                    else if (!taken.Contains("PHY2048") || !taken.Contains("PHY2048L") && courseId == "PHY2048C")
                    {
                        earned_credits += course.Credits;
                        taken.Add(courseId);
                        sequence3 = true;
                    }
                    else if (!taken.Contains("PHY2049C") && courseId == "PHY2049")
                    {
                        earned_credits += course.Credits;
                        taken.Add(courseId);
                        sequence3 = true;
                    }
                    else if (!taken.Contains("PHY2049C") && courseId == "PHY2049L")
                    {
                        earned_credits += course.Credits;
                        taken.Add(courseId);
                        sequence3 = true;
                    }
                    else if (!taken.Contains("PHY2049") || !taken.Contains("PHY2049L") && courseId == "PHY2049C")
                    {
                        earned_credits += course.Credits;
                        taken.Add(courseId);
                        sequence3 = true;
                    }
                }

            }

            return new Requirements(name, earned_credits, total_credits, (earned_credits == total_credits) ? true : false);
        }
        public static Requirements Prerequisites(IEnumerable<CoursesRequest> courses)
        {

            var name = "Prerequisites";
            var total_credits = 11;
            var earned_credits = 0;

            foreach (CoursesRequest course in courses)
            {
                var courseId = course.CourseId;

                if (courseId == "COP2220" || courseId == "MAC2311" || courseId == "MAC2312")
                {
                    earned_credits += course.Credits;
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

        public static Requirements CoreReqs(IEnumerable<CoursesRequest> courses)
        {
            var name = "Core Requirements";
            var total_credits = 18;
            var earned_credits = 0;
            var sequence1 = 1;

            foreach (CoursesRequest course in courses)
            {
                var courseId = course.CourseId;

                if (courseId == "COP3503" || courseId == "COP3530" || courseId == "CIS3253" || courseId == "COP3703" || courseId == "CNT4504")
                {
                    earned_credits += course.Credits;
                }
                else if (sequence1 > 0 && courseId == "COT3100" || courseId == "MAD3107")
                {
                    earned_credits += course.Credits;
                    sequence1--;
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

        public static Requirements MajorReqs(IEnumerable<CoursesRequest> courses)
        {
            var name = "Major Requirements";
            var total_credits = 33;
            var earned_credits = 0;
            var sequence1 = 1;
            var sequence2 = 1;
            var sequence3 = 1;

            foreach (CoursesRequest course in courses)
            {
                var courseId = course.CourseId;

                if (courseId == "MAS3105" || courseId == "STA3163" || courseId == "STA3164" || courseId == "STA4321" || courseId == "CAP4784" || courseId == "CAP4770" || courseId == "COT4400")
                {
                    earned_credits += course.Credits;
                }
                else if (sequence1 > 0 && courseId == "COT4560" || courseId == "COT4111" || courseId == "COT4461" || courseId == "MAD4301" || courseId == "MAD4203" || courseId == "MAD4505")
                {
                    earned_credits += course.Credits;
                    sequence1--;
                }
                else if (sequence2 > 0 && courseId == "STA4502" || courseId == "STA4504")
                {
                    earned_credits += course.Credits;
                    sequence2--;
                }
                else if (sequence3 > 0 && courseId == "STA4945" || courseId == "CIS4900" || courseId == "MAS4932" || courseId == "MAT4906")
                {
                    earned_credits += course.Credits;
                    sequence3--;
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

        public static Requirements Other(IEnumerable<CoursesRequest> courses)
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
            return new Requirements(name, earned_credits, total_credits, satisfied);
        }

        public static List<Requirements> CheckAll(IEnumerable<CoursesRequest> courses)
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

    public static class InformationScience
    {
        public static Requirements Requisites(IEnumerable<CoursesRequest> courses)
        {
            var name = "Requisites";
            var total_credits = 3;
            var earned_credits = 0;

            foreach (CoursesRequest course in courses)
            {
                if (course.CourseId.Contains("SPC"))
                {
                    earned_credits += course.Credits;
                    break;
                }
            }

            return new Requirements(name, earned_credits, total_credits, (earned_credits == total_credits) ? true : false);
        }
        public static Requirements Prerequisites(IEnumerable<CoursesRequest> courses)
        {
            List<string> taken = new List<string>();

            var name = "Prerequisites";
            var total_credits = 12;
            var earned_credits = 0;

            foreach (CoursesRequest course in courses)
            {
                var courseId = course.CourseId;

                if (courseId == "COP2220" || courseId == "CGS1570" )
                {
                    earned_credits += course.Credits;
                    taken.Add(courseId);
                }
                else if(!taken.Contains("MAC2233") && courseId == "MAC2311")
                {
                    earned_credits += course.Credits;
                    taken.Add(courseId);
                }
                else if (!taken.Contains("MAC2311") && courseId == "MAC2233")
                {
                    earned_credits += course.Credits;
                    taken.Add(courseId);
                }
                else if (!taken.Contains("STA2023") && courseId == "STA2122")
                {
                    earned_credits += course.Credits;
                    taken.Add(courseId);
                }
                else if (!taken.Contains("STA2122") && courseId == "STA2023")
                {
                    earned_credits += course.Credits;
                    taken.Add(courseId);
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

        public static Requirements CoreReqs(IEnumerable<CoursesRequest> courses)
        {
            var name = "Core Requirements";
            var total_credits = 18;
            var earned_credits = 0;

            foreach (CoursesRequest course in courses)
            {
                var courseId = course.CourseId;

                if (courseId == "COT3100" || courseId == "COP3503" || courseId == "COP3530" || courseId == "CIS3253" || courseId == "COP3703" || courseId == "CNT4504")
                {
                    earned_credits += course.Credits;
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

        public static Requirements MajorReqs(IEnumerable<CoursesRequest> courses)
        {
            var name = "Major Requirements";
            var total_credits = 19;
            var earned_credits = 0;

            foreach (CoursesRequest course in courses)
            {
                var courseId = course.CourseId;

                if (courseId == "COP3855" || courseId == "CDA4010" || courseId == "COP4813" || courseId == "CAP4784" || courseId == "CIS4327" || courseId == "CIS4328")
                {
                    earned_credits += course.Credits;
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

        public static Requirements Other(IEnumerable<CoursesRequest> courses)
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
            return new Requirements(name, earned_credits, total_credits, satisfied);
        }

        public static List<Requirements> CheckAll(IEnumerable<CoursesRequest> courses)
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

    public static class InformationSystems
    {
        public static Requirements Requisites(IEnumerable<CoursesRequest> courses)
        {
            var name = "Requisites";
            var total_credits = 3;
            var earned_credits = 0;

            foreach (CoursesRequest course in courses)
            {
                if (course.CourseId.Contains("SPC"))
                {
                    earned_credits += course.Credits;
                    break;
                }
            }

            return new Requirements(name, earned_credits, total_credits, (earned_credits == total_credits) ? true : false);
        }
        public static Requirements Prerequisites(IEnumerable<CoursesRequest> courses)
        {
            List<string> taken = new List<string>();

            var name = "Prerequisites";
            var total_credits = 24;
            var earned_credits = 0;

            foreach (CoursesRequest course in courses)
            {
                var courseId = course.CourseId;

                if (courseId == "COP2220" || courseId == "ACG2021" || courseId == "ACG2071" || courseId == "CGS1100" || courseId == "ECO2013" || courseId == "ECO2023")
                {
                    earned_credits += course.Credits;
                    taken.Add(courseId);
                }
                else if (!taken.Contains("MAC2233") && courseId == "MAC2311")
                {
                    earned_credits += course.Credits;
                    taken.Add(courseId);
                }
                else if (!taken.Contains("MAC2311") && courseId == "MAC2233")
                {
                    earned_credits += course.Credits;
                    taken.Add(courseId);
                }
                else if (!taken.Contains("STA2023") && courseId == "STA2122")
                {
                    earned_credits += course.Credits;
                    taken.Add(courseId);
                }
                else if (!taken.Contains("STA2122") && courseId == "STA2023")
                {
                    earned_credits += course.Credits;
                    taken.Add(courseId);
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

        public static Requirements CoreReqs(IEnumerable<CoursesRequest> courses)
        {
            var name = "Core Requirements";
            var total_credits = 18;
            var earned_credits = 0;

            foreach (CoursesRequest course in courses)
            {
                var courseId = course.CourseId;

                if (courseId == "COT3100" || courseId == "COP3503" || courseId == "COP3530" || courseId == "CIS3253" || courseId == "COP3703" || courseId == "CNT4504")
                {
                    earned_credits += course.Credits;
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

        public static Requirements MajorReqs(IEnumerable<CoursesRequest> courses)
        {
            var name = "Major Requirements";
            var total_credits = 31;
            var earned_credits = 0;

            foreach (CoursesRequest course in courses)
            {
                var courseId = course.CourseId;

                if (courseId == "COP3855" || courseId == "CDA4010" || courseId == "COP3813" || courseId == "COP4854" || courseId == "CAP4784" || courseId == "CIS4327" || courseId == "CIS4328" || courseId == "ISM4011" || courseId == "MAN3025" || courseId == "FIN3403")
                {
                    earned_credits += course.Credits;
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

        public static Requirements Other(IEnumerable<CoursesRequest> courses)
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
            return new Requirements(name, earned_credits, total_credits, satisfied);
        }

        public static List<Requirements> CheckAll(IEnumerable<CoursesRequest> courses)
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

    public static class InformationTechnology
    {
        public static Requirements Prerequisites(IEnumerable<CoursesRequest> courses)
        {
            List<string> taken = new List<string>();

            var name = "Prerequisites";
            var total_credits = 15;
            var earned_credits = 0;
            var speaking_req_complete = false;

            foreach (CoursesRequest course in courses)
            {
                var courseId = course.CourseId;

                if (courseId == "COP2220" || courseId == "CGS1570" )
                {
                    earned_credits += course.Credits;
                    taken.Add(courseId);
                }
                else if (!speaking_req_complete && courseId.Contains("SPC"))
                {
                    earned_credits += course.Credits;
                    speaking_req_complete = true;
                }
                else if (!taken.Contains("MAC2233") && courseId == "MAC2311")
                {
                    earned_credits += course.Credits;
                    taken.Add(courseId);
                }
                else if (!taken.Contains("MAC2311") && courseId == "MAC2233")
                {
                    earned_credits += course.Credits;
                    taken.Add(courseId);
                }
                else if (!taken.Contains("STA2023") && courseId == "STA2122")
                {
                    earned_credits += course.Credits;
                    taken.Add(courseId);
                }
                else if (!taken.Contains("STA2122") && courseId == "STA2023")
                {
                    earned_credits += course.Credits;
                    taken.Add(courseId);
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

        public static Requirements CoreReqs(IEnumerable<CoursesRequest> courses)
        {
            var name = "Core Requirements";
            var total_credits = 18;
            var earned_credits = 0;

            foreach (CoursesRequest course in courses)
            {
                var courseId = course.CourseId;

                if (courseId == "COT3100" || courseId == "COP3503" || courseId == "COP3530" || courseId == "CIS3253" || courseId == "COP3703" || courseId == "CNT4504")
                {
                    earned_credits += course.Credits;
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

        public static Requirements MajorReqs(IEnumerable<CoursesRequest> courses)
        {
            var name = "Major Requirements";
            var total_credits = 30;
            var earned_credits = 0;

            foreach (CoursesRequest course in courses)
            {
                var courseId = course.CourseId;

                if (courseId == "CIS3526" || courseId == "COP4640" || courseId == "CIS4360" || courseId == "CIS4362" || courseId == "CIS4364" || courseId == "CIS4365" || courseId == "CIS4366" || courseId == "CNT4406" || courseId == "CEN4083" || courseId == "CIS4325")
                {
                    earned_credits += course.Credits;
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

        public static Requirements Other(IEnumerable<CoursesRequest> courses)
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
            return new Requirements(name, earned_credits, total_credits, satisfied);
        }

        public static List<Requirements> CheckAll(IEnumerable<CoursesRequest> courses)
        {
            List<Requirements> reqs = new List<Requirements>();
            var results = Prerequisites(courses);
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
}
