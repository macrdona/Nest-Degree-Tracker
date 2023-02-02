using BCryptNet = BCrypt.Net.BCrypt;
using System.Text.Json.Serialization;
using backend.Authorization;
using backend.Entities;
using backend.Helpers;
using backend.Services;

namespace backend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
           
            builder.Services.AddDbContext<DataContext>();
            builder.Services.AddCors();
            builder.Services.AddControllers().AddJsonOptions(x =>
            {
                // serialize enums as strings in api responses (e.g. Role)
                x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

            // configure strongly typed settings object
            builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

            // configure DI for application services
            builder.Services.AddScoped<IJwtUtils, JwtUtils>();
            builder.Services.AddScoped<IUserService, UserService>();

            var app = builder.Build();

            // configure HTTP request pipeline
            {
                // global cors policy
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

            // create hardcoded test users in db on startup
            {
                var testUsers = new List<User>
                {
                    new User { Id = 1, FirstName = "Admin", LastName = "User", Username = "admin", PasswordHash = BCryptNet.HashPassword("admin"), Role = Role.Admin },
                    new User { Id = 2, FirstName = "Normal", LastName = "User", Username = "user", PasswordHash = BCryptNet.HashPassword("user"), Role = Role.User }
                };

                using var scope = app.Services.CreateScope();
                var dataContext = scope.ServiceProvider.GetRequiredService<DataContext>();
                dataContext.Users.AddRange(testUsers);
                dataContext.SaveChanges();
            }

            app.Run("http://localhost:4000");
        }
    }
}