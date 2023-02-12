import sqlite3 as sq
import pandas as pd

connection = sq.connect(r"C:\\Users\\gabim\source\\repos\Software-Engineering-Group-6\backend\Database\\test.db")

df = pd.read_csv("src\output_v2\output.csv", index_col=False)

df = df.drop(["Unnamed: 0"], axis=1)

df.to_sql("Courses", connection, if_exists="append", index=False)

connection.close()