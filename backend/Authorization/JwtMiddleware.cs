using Microsoft.Extensions.Options;
using backend.Helpers;
using backend.Services;

namespace backend.Authorization
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IUserService user_service, IJwtUtils jwtUtils)
        {
            //extracts the JWT token from the request Authorization header
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            //validate the given token
            var userId = jwtUtils.ValidateToken(token);

            if (userId != null)
            {
                try
                {
                    // attach user to context on successful jwt validation
                    context.Items["User"] = user_service.GetById(userId.Value);
                }
                catch(Exception ex) 
                {
                    throw new AppException(ex.Message);
                }
               
            }

            await _next(context);
        }
    }
}
