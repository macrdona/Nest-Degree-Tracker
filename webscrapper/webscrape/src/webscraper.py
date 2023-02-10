from selenium import webdriver
from bs4 import BeautifulSoup
from selenium.webdriver.support.ui import WebDriverWait
from selenium.webdriver.support import expected_conditions as EC
from selenium.webdriver.common.by import By

#web drivers used to access web pages
chrome = r"C:\\Users\\gabim\\WebBrowserDriver\\chromedriver.exe"
edge = r"C:\\Users\\gabim\\WebBrowserDriver\\msedgedriver.exe"
driver = webdriver.Chrome(executable_path=chrome)
#driver = webdriver.Edge(executable_path=edge)

#url to navigate 
url = "https://www.unf.edu/catalog/courses/?level=ug"
driver.get(url)

#have website wait until "accordion-item" class elements have been loaded
WebDriverWait(driver,30).until(EC.presence_of_element_located((By.CLASS_NAME, "accordion-item")))

html = driver.page_source

soup = BeautifulSoup(html, "html.parser")
elements = soup.find_all("li")

f = open("src\output\out.txt", "a", encoding="utf-8")

for e in elements:
    f.write(
    e.get_text()
    )
driver.close()