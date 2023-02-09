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
