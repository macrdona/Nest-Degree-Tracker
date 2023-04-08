using Microsoft.AspNetCore.Mvc;
using backend.Authorization;
using AutoMapper;
using backend.Entities;
using backend.Services;
using backend.Helpers;
using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations;
using Duende.IdentityServer.Extensions;

namespace backend.Entities
{
    public class Course
    {
        public string? CourseId { get; set; }
        public string? CourseName { get; set; }
        public int? Credits { get; set; }

        public string? Prerequisites { get; set; }

        public string? CoRequisites { get; set; }
        public string? Description { get; set; }
        public string? Availability { get; set; }
    }

    public class Recommendations
    {
        public List<Course>? Courses { get; set; }
    }

    public class RequirementsCheck
    {
        public Dictionary<string,bool> met { get; set; }
    }

    public static class UniversityRequirements 
    {

        static List<string> humanities = new List<string> { "ARH2000", "HUM2020", "LIT2020", "MUL2010", "PHI2010", "THE2000" };
        static List<string> social = new List<string> { "AMH2020", "ANT2000", "ECO2013", "POS2041", "PSY2012", "SYG2000" };
        static List<string> math = new List<string> { "MAC1105", "MGF1106", "MGF1107", "STA2023", "STA2014", "MAC1101C", "MAC1105C", "MAC1147", "MAC1114", "MAC2233", "MAC2311" };
        static List<string> science = new List<string> { "AST2002", "BSC1005", "BSC1010C", "CHM2045", "ESC2000", "PHY1020", "CHM1020", "EVR1001" };


        public static void CheckRequirements(Dictionary<string, bool> met, List<string> courses) 
        {

            foreach(string course in courses)
            {
                if (!met.ContainsKey("Communication") && course == "ENC1101")
                {
                    met["Communication"] = true;
                }

                if (!met.ContainsKey("Humanities") && humanities.Contains(course))
                {
                    met["Humanities"] = true;
                }

                if (!met.ContainsKey("Social Science") && social.Contains(course))
                {
                    met["Social Science"] = true;
                }

                if (!met.ContainsKey("Math & Statistics") && math.Contains(course))
                {
                    met["Math & Statistics"] = true;
                }

                if (!met.ContainsKey("Natural & Physical Sciences") && science.Contains(course))
                {
                    met["Natural & Physical Sciences"] = true;
                }
            }

            if (!met.ContainsKey("Communication")) { met["Communication"] = false; }
            if (!met.ContainsKey("Humanities")) { met["Humanities"] = false; }
            if (!met.ContainsKey("Social Science")) { met["Social Science"] = false; }
            if (!met.ContainsKey("Math & Statistics")) { met["Math & Statistics"] = false; }
            if (!met.ContainsKey("Natural & Physical Sciences")) { met["Natural & Physical Sciences"] = false; }

        }
    }


}