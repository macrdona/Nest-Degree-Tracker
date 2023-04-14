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