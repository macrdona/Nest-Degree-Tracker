using backend.Authorization;
using backend.Helpers;
using backend.Models;
using backend.Services;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Mime;

namespace backend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var services = builder.Services;
            var env = builder.Environment;

            //registering DatabaseContext
            //This will allow us to communicate and perform operation to our database through a data context object
            services.AddDbContext<DataContext>();

            services.AddCors();

            //registering service for overriding API ModelState error response
            services.AddControllers().ConfigureApiBehaviorOptions(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var result = new ValidationFailedResult(context.ModelState);

                    //responses are formatted to JSON
                    result.ContentTypes.Add(MediaTypeNames.Application.Json);
                    return result;
                };
            });

            //allow use of swagger for API testing
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            // configure automapper with all automapper profiles from this assembly
            services.AddAutoMapper(typeof(Program));

            // configure strongly typed settings object
            services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

            // configure DI for application services
            /*
             * Any object/variable of type IJwtUtils will have all its dependecies resolved by .NET
             * meaning we do not have to create an instance of the class or any classes that it depends on,
             * dependancy injection handles that logic.
             */
            services.AddScoped<IJwtUtils, JwtUtils>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICourseService, CourseService>();
            services.AddScoped<IMajorService, MajorService>();

            //allowing use of HTTPContext in my other services
            services.AddScoped<IUrlHelper>(factory =>
            {
                var actionContext = factory.GetService<IActionContextAccessor>()
                                           .ActionContext;
                return new UrlHelper(actionContext);
            });
            services.AddHttpContextAccessor();

            var app = builder.Build();

            // migrate any database changes on startup (includes initial db creation)
            using (var scope = app.Services.CreateScope())
            {
                var dataContext = scope.ServiceProvider.GetRequiredService<DataContext>();
                dataContext.Database.Migrate();
            }

            if(app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // configure HTTP request pipeline
            {
                //allow calls to our API from any origin
                app.UseCors(x => x
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());

                // global error handler
                app.UseMiddleware<ErrorHandlerMiddleware>();

                // custom jwt auth middleware
                app.UseMiddleware<JwtMiddleware>();

                app.MapControllers();
            }

            app.Run("http://localhost:4000");
        }
    }
}