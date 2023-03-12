using backend.Services;

namespace backend.Entities
{
    public class ComputerScience
    {
        public static string? Name { get; } = "Computer Science";
        public static string? Degree { get; } = "Bachelor of Science (BS)";
        public static string? Description { get; } = "It focuses on studying the theoretical foundations of the computing field and system-level programming. Students study the intricacies and design principals of sophisticated computing systems such as compilers, operating systems, algorithm analysis and design, and artificial intelligence.";

        private readonly static List<string>? CourseList = new() { "COP 2220", "MAC 2311", "MAC 2312", "PHY 2048", "PHY 2048L", "PHY 2049", "PHY 2049L", "COT 3100", "COP 3530", "COP 3503", "CIS 3253", "COP 3703", "CNT 4504", "CDA 3100", "COT 3210", "COP 3404", "CEN 4010", "COP 4610", "COP 4620", "CAP 4630", "COT 4400", "MAS 3105", "STA 3032" };
        public static List<string>? Courses () => CourseList;
    }
}
