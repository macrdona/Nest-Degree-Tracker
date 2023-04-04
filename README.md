# Software-Engineering-Group-6

## Setting up web scraper venv

1. In VS code, make sure you have the python extension installed
2. Ctrl+Shift+P, Python: Create Environment
3. Select .venv
4. Go with the defaults
5. Open a terminal
   - Make sure it activates the virtual environment and has a (.venv) before the path name
   - If not, run ./.venv/Scripts/activate
6. Run `pip install -r requirements.txt`
7. Run `cd webscrapper/webscrape` then `python src/webscraper.py`

## Database Setup 

1. Download https://sqlitebrowser.org/dl/
2. Create test.db file in the Database folder
3. Open Visual Studio and run application to apply DatabaseMigrations
   - Migrations will export the Entities and Models as tables in the database
4. If there are changes that need to be updated in the database:
   - Open Package Manager Console and Run dotnet ef migrations add [create name] --context DataContext --output-dir Migrations/SqliteMigrations
5. To add Courses and Majors data to the database, run both dataExport.py scripts
   - NOTE: make sure to adjust the directory where the test.db file is located locally