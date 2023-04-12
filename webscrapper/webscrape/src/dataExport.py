import sqlite3 as sq
import pandas as pd
import os
import re

path, fileName = os.path.split(__file__)
connection = sq.connect(path + "/../../../backend/Database/test.db")

df = pd.read_csv(path + "/output_v3/output.csv", index_col=False)

df = df.drop(["Unnamed: 0"], axis=1)
df = df.drop(["Repeatability"], axis=1)
df = df.drop(["CourseFees"], axis=1)

df["CourseId"] = df["CourseId"].apply(lambda x: re.sub(" ", "", x))
df.to_sql("Courses", connection, if_exists="append", index=False)

connection.close()