import sqlite3 as sq
import pandas as pd

connection = sq.connect(r"C:\\Users\\gabim\source\\repos\Software-Engineering-Group-6\backend\Database\\test.db")

majors = {
    0:{
        "MajorId": 1,
        "MajorName":"Computer Science", 
        "Degree": "Bachelor of Science (BS)",
        "Description": "It focuses on studying the theoretical foundations of the computing field and system-level programming. Students study the intricacies and design principals of sophisticated computing systems such as compilers, operating systems, algorithm analysis and design, and artificial intelligence."
    },
    1:{
        "MajorId": 2,
        "MajorName":"Data Science", 
        "Degree": "Bachelor of Science (BS)",
        "Description": "Emphasis on studying methods for managing and analyzing large datasets. It also has a significant component of math and science courses. With courses focused on statistics, database systems, algorithm design and analysis, and data analytics graduates of the program will be able to design, implement, and use methods for the discovery of patterns and prediction of future trends from datasets."
    },
    2:{
        "MajorId": 3,
        "MajorName":"Information Technology", 
        "Degree": "Bachelor of Science (BS)",
        "Description": "Students completing this program will be specialists ready to face high expectations of organizations with respect to planning, design, implementation, configuration, and maintenance of a computing infrastructure. They will be able to apply computing principles and concepts by participating in practical activities throughout the program."
    },
    3:{
        "MajorId": 4,
        "MajorName":"Information Systems", 
        "Degree": "Bachelor of Science (BS)",
        "Description": "Computer courses include systems analysis, systems implementation, computer communications, database processing, and other courses focused on implementation of computer solutions to business problems. Graduates will be prepared for careers as applications programmers, systems analysts, or information systems managers."
    },
    4:{
        "MajorId": 5,
        "MajorName":"Information Science", 
        "Degree": "Bachelor of Science (BS)",
        "Description": "Computer courses include systems analysis, systems implementation, computer communications, database processing, and other courses focused on implementation of computer solutions to business problems. Graduates will be prepared for careers as applications programmers, systems analysts, or information systems managers."
   }
}
majors = pd.DataFrame(majors)
majors = majors.transpose()
majors.to_sql("Majors", connection, if_exists="append", index=False)

majorCourses = {
    "001": ["COP2220", "MAC2311", "MAC2312", "PHY2048", "PHY2048L", "PHY2049", "PHY2049L", "COT3100", "COP3530", "COP3503", "CIS3253", "COP3703", "CNT4504", "CDA3100", "COT3210", "COP3404", "CEN4010", "COP4610", "COP4620", "CAP4630", "COT4400", "MAS3105", "STA3032"],
    "002": ["COP2220", "MAC2311", "MAC2312", "COP3503", "COP3530", "CIS3253", "COP3703", "CNT4504", "COT3100", "MAD3107", "MAS3105", "STA3163", "STA3164", "STA4321", "CAP4784", "CAP4770", "COT4400"],
    "003": ["COP2220", "CGS1570", "MAC2233", "STA2023", "COT3100", "COP3503", "COP3530", "CIS3253", "COP3703", "CNT4504", "CIS3526", "COP4640", "CIS4360", "CIS4362", "CIS4364", "CIS4365", "CIS4366", "CNT4406", "CEN4083", "CIS4325" ],
    "004": ["COP2220","ACG2021","ACG2071","CGS1100","ECO2013","ECO2023","MAC2233","STA2023","COT3100", "COP3503","COP3530","CIS3253","COP3703","CNT4504","COP3855","CDA4010","COP3813","COP4854","CAP4784","CIS4327","CIS4328","ISM4011","MAN3025","FIN3403"],
    "005": ["COP2220", "CGS1570", "MAC2233", "STA2023", "COT3100", "COP3503", "COP3530", "CIS3253", "COP3703", "CNT4504", "COP3855", "CDA4010", "COP4813", "CAP4784", "CIS4327", "CIS4328"]
}
courses = dict()

counter = 0
for x,y in majorCourses.items():
    for k in y:
        courses[counter] = {"MajorId":x, "CourseId":k}
        counter += 1

courses = pd.DataFrame(courses)
courses = courses.transpose()
courses.to_sql("MajorCourses", connection, if_exists="append", index=False)

connection.close()