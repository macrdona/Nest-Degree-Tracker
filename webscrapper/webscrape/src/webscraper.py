from selenium import webdriver
from bs4 import BeautifulSoup
from selenium.webdriver.support.ui import WebDriverWait, Select
from selenium.webdriver.support import expected_conditions as EC
from selenium.webdriver.common.by import By
import re
import pandas as pd
import pandas as pd

"""
SETUP
"""
#web drivers used to access web pages
chrome = r"C:\\Users\\gabim\\WebBrowserDriver\\chromedriver.exe"
driver = webdriver.Chrome(executable_path=chrome)

#url to navigate 
url = "https://www.unf.edu/catalog/courses/?level=ug"
driver.get(url)

dropdown = WebDriverWait(driver, 10).until(EC.element_to_be_clickable((By.ID, "searchDepartments")))
select = Select(driver.find_element(By.ID, "searchDepartments"))
# Click on the dropdown to open it
dropdown.click()

select.select_by_value("Computing")
WebDriverWait(driver,4)
#have website wait until "accordion-item" class elements have been loaded
WebDriverWait(driver,30).until(EC.presence_of_element_located((By.CLASS_NAME, "accordion-item")))

html = driver.page_source

soup = BeautifulSoup(html, "html.parser")

query = soup.find_all(class_="accordion-item")
"""
WEB SCRAPING
"""

records = {}
record_error = 0
for index,q in enumerate(query):
    try:
        data = list()

        #extracting data from html
        a_tag = str(q.a.get_text()).replace(u'\xa0', u' ').split("\n")
        for br in q.section.find_all("br"):
            br.replace_with("\n")
        section_tag = str(q.section.get_text()).replace(u'\xa0', u' ').split("\n")

        data.extend(a_tag)
        data.extend(section_tag)

        #removing white spaces
        for x in range(len(data)):
            data[x] = " ".join(data[x].split())
            
        #remove empty strings
        data = list(filter(None,data))

        #remove duplicates
        data: list[str] = list(dict.fromkeys(data))

        
        #separating course number and course name
        for i,s in enumerate(data.pop(0).split(":",1)):
            data.insert(i,s)
        
        #create record
        record = {"CourseID": data[0], "CourseName": data[1], "Credits": int(re.sub('[^0-9]', "", data[2]).strip()), "Prerequisites": "N/A", "CoRequisites": "N/A", "Description": "N/A", "Availability": "N/A", "Repeatability": "N/A", "CourseFees": "N/A"}

        #adding info to records
        for d in data[3:]:
            if "prerequisite:" in d.lower() or "prerequisites:" in d.lower():
                record["Prerequisites"] = d.split(':', 1)[1].strip()
            elif "co-requisite:" in d.lower() or "co-requisites:" in d.lower():
                record["CoRequisites"] = d.split(':', 1)[1].strip()
            elif "description:" in d.lower():
                record["Description"] = d.split(':', 1)[1].strip()
            elif "availability:" in d.lower():
                record["Availability"] = d.split(':', 1)[1].strip()
            elif "repeatability:" in d.lower():
                record["Repeatability"] = d.split(':', 1)[1].strip()
            elif "course fees:" in d.lower() or "course fee:" in d.lower():
                record["CourseFees"] = d.split(':', 1)[1].strip()
            else:
                record["Description"] = d.strip()
            
            if (record["Description"] == "N/A" and '.' in record["Prerequisites"]):
                # The description was probably captured by the "Prerequisites" thing due to the description not being on its own line. >:(
                split = record["Prerequisites"].split(".", 1)
                record["Prerequisites"] = split[0].strip()
                record["Description"] = split[1].strip()
        records[index] = record

    except AttributeError:
        record_error += 1

print(f"Number of records that could not be processed {record_error}")

"""
STORING DATA
"""

df_csv = pd.DataFrame(records)
df_json = pd.DataFrame(records)

df_csv = df_csv.swapaxes("index", "columns")

df_json.to_json("src/output_v3/output.json")
df_csv.to_csv("src/output_v3/output.csv")