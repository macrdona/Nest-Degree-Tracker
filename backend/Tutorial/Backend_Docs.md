# Program.cs

- In this file, we configure services and declare dependancies that need to be resolved by the .NET framework, then the application applies these configurations and exectutes the app. 

## Types of Configurations & Services

|Configurations & Services|Description|
|-------------------------|-----------|
|AddDbContext|DbContext calls our database context class to intialize a connection with our database and allows us to us communicate and perform operations to our database|
|AddCors|Cors(cross-origin resources sharing) is a mechanism that allows our server to indicate from which origins our application will accept requests from. Ex. Which domains or ports are able to call our APIs|
|AddAutoMapper|AutoMapper allows us to use mapping functions which allow us to map data from one object into another object|
|AddScoped|Binds an interface with its derived class. Whenever the interface is referenced, DI(Dependancy Injection) creates an instance of the class and handles all its dependancies to be able to call any methods or perform operations involving this class. This instance is created once per request, and it lasts until the request has been fulfilled|
|UseMiddleware|Allows us to define our own custome middleware. Ex. Defining our own error handling|

# Authorization 

- This directory will contain any classes that handle authorization.

## AllowAnonymousAttribute

- This class defines the functionality for the [AllowAnonymous] attribute

## AuthorizeAttribute

- This class defines the functionality for the [Authorize] attribute

|Method|Parameters|Return|Description|
|------|----------|------|-----------|
|OnAuthorization()|AuthorizationFilterContext|void|If user has been authorized, the user should be attached to the current HTTP request. HttpContext.Items["User"] is called to find if there is an user attached to the request, if not then an "Unauthorized" message error will be returned|

## JwtMiddleware

- This middleware class is a component which is executed on every request made to our server

- Middleware classes must include:
    - A public constructor with a parameter of type RequestDelegate
    - A public method Invoke or InvokeAsync
    - Return a Task
    - Accept a first parameter of type HttpContext
    - Any additional parameters are populated by dependancy injection

|Method|Parameters|Return|Description|
|------|----------|------|-----------|
|Invoke()|HttpContext, IUSerService, IJwtUtils|Task|This method extracts JWT token from the request Authorization header, validates the token, and if valid it attaches the user who was given that token and assigns it to context.Items["User]|

## IJwtUtils & JwtUtils

- IJwtUtils is an interface to the JwtUtils class.

- The JwtUtils class provides the functionality for generating and validating tokens. 

|Method|Parameters|Return|Description|
|------|----------|------|-----------|
|GenerateToken()|User|string|This method will generate a JWT token using our app secrets and attaches it to the given user|
|ValidateToken()|string|int|This method will validate a JWT token. If token is valid, it should return the ID of the user attched to that token|

# Controller

- This directory will contain any classes/controllers that manage HTTP actions/requests

## UsersController
- The UsersControlelr manages various requests regarding user's accounts, such as Authentication(login), Registration, GetById, and Update. 

- These methods all have the same return method 'Ok()'. This method returns a json formatted response, with a status code.

|Method|Parameters|Return|Description|
|------|----------|------|-----------|
|Authenticate()|AuthenticateRequest|IActionResult|This POST method handles requests for /users/login|
|Register()|RegisterRequest|IActionResult|This POST method handles requests for users/register|
|GetById()|int|IActionResult|This GET method handles request for users/{id}|
|Update()|int,UpdateRequest|IActionResult|This PUT method handles requests for users/{id}|

# Entities

- This directory will contain any classes that involve data models.

|Class|Description|
|-----|-----------|
|User|This is the base User class. This class specifies the user attributes that will be stored in the database|
|RegisterRequest|This class specifies the user attributes needed to register a user|
|UpdateRequest|This class specifies the user attributes needed to update a user's information|
|AuthenticateResponse|This class specifies the user attributes needed for authenticating a user|
|AuthenticateRequest|This class specifies the user attributes needed for authenticating a request|

# Helpers

- This directory contains classes provide functionality that assists our program, such as mapping data between models, or handling errors

|Class|Description|
|-----|-----------|
|AppException|This class provides various methods that can be called when raising exceptions in our app|
|AppSettings|This is a model that is used to bind our app's secret|
|AutoMapper|This class defines a set of classes that can bind data from one object to another|
|ErrorHanlderMiddleware|This class defines the functionality to handle  app exceptions|

# Models

- This directory contains classes that are essential to our database context

## DataContext 

- DbSet<'model'> defines an object using a model as its type. When doing a database migration, our DbSet object is used to create a query which creates a table based on the model assigned to DbSet<>. 

- This object allows us to communicate with out database and perform operations to its corresponding table. 

- Ex. ```DbSet<'User'> Users``` is an object of type User. When migrating to the database, a User table will be created using the class atributes as columns. Through this object we can communicate with our database and perform operations (such as adding and updating data) to the User table.


|Method|Parameters|Return|Description|
|------|----------|------|-----------|
|OnConfiguring()|DbContextOptionsBuilder|void|This method configures and creates a connection to our database by grabbing the conenction string from appsettings.json|

# Services

- This directory will contain classes that acts as services, such as a service that interacts with our database to perform changes. 

## IUserService & UserService

- IUserService is a interface for the UserService class

- The UserService class handles the database operations.
- The DataContext object (_context) provides us with access to the DbSet objects defined in the DataContext class. Ex. _context.Users, where Users was an object we created by defining ```DbSet<'User'> Users```
- Through _context.Users we can call various functions that will allow us to perform operation to the Users table

|Method|Parameters|Return|Description|
|------|----------|------|-----------|
|Authenticate()|AuthenticateRequest|AuthenticateResponse|This method will check if the user passed through the model exists in the database. It will validate the provided password with the password stored in the database, and it will generate a token that is then assigned to the user|
|Register()|RegisterRequest|void|This method will validate the user data, will hash and store the password and then it will save the user in the database if validation is succesful|
|Update()|int,UpdateRequest|void|This method will validate if user exists, and if data is correct. If so, then it will update the user record in the database|
|GetById()|int|User|This method will find an user in the database based on the given user id, and it will return the user data if a user is found|