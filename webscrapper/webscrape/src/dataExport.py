import sqlite3 as sq
import pandas as pd
import os
import re

#format Prereqes and Coreqs data
def formatData(df,col):
    ex = df[col].copy()
    list1 = list()
    list2 = list()
    for x in range(len(ex)):
        if ex[x]:
            list1.append(re.findall("[A-Z]{3}[0-9]{4}",str(ex[x]).replace(" ","")))

    for x in range(len(ex)):
        if ex[x]:
            list2.append(re.findall("[A-Z]{3}[0-9]{4}[A-Z]{1}",str(ex[x]).replace(" ","")))

    new_list = list()
    for x,y,i in zip(list1,list2,range(len(ex))):
        
        if x and y:
            x.extend(y)
            new_list.append(",".join(x))
        elif x:
            new_list.append(",".join(x))
        elif y:
            new_list.append(",".join(y))
        else:
            new_list.append("N/A")

    df[col] = df[col].apply(lambda x: lamb(new_list))


path, fileName = os.path.split(__file__)
connection = sq.connect(path + "/../../../backend/Database/test.db")

df = pd.read_csv(path + "/output_v3/output.csv", index_col=False)

df = df.drop(["Unnamed: 0"], axis=1)
df = df.drop(["Repeatability"], axis=1)
df = df.drop(["CourseFees"], axis=1)

df["CourseId"] = df["CourseId"].apply(lambda x: re.sub(" ", "", x))

counter = 0
def lamb(new_list):
    global counter
    val = new_list[counter]
    counter+=1
    return val
formatData(df,"Prerequisites")

counter = 0
def lamb(new_list):
    global counter
    val = new_list[counter]
    counter+=1
    return val
formatData(df,"CoRequisites")

df.to_sql("Courses", connection, if_exists="replace", index=False)

connection.close()