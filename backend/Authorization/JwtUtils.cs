using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using backend.Models;
using backend.Helpers;

namespace backend.Authorization
{
    public interface IJwtUtils
    {
        //Generates JWT token with the id of the specified user
        public string GenerateToken(User user);
        //Validates given token
        public int? ValidateToken(string token);
    }

    public class JwtUtils : IJwtUtils
    {
        private readonly AppSettings _app_settings;

        public JwtUtils(IOptions<AppSettings> appSettings)
        {
            _app_settings = appSettings.Value;
        }

        //Generates JWT token with the id of the specified user
        public string GenerateToken(User user)
        {
            // generate token that is valid for 7 days
            var token_handler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_app_settings.Secret);
            var token_descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.UserId.ToString()), new Claim("username", user.Username.ToString()), new Claim("firstName", user.FirstName.ToString()), new Claim("lastName", user.LastName.ToString()), new Claim("completed", user.EnrollmentCompleted.ToString().ToLower()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = token_handler.CreateToken(token_descriptor);
            return token_handler.WriteToken(token);
        }

        //Validates given token
        public int? ValidateToken(string token)
        {
            if (token == null)
                return null;

            var token_handler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_app_settings.Secret);
            try
            {
                token_handler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwt_token = (JwtSecurityToken)validatedToken;
                var user_id = int.Parse(jwt_token.Claims.First(x => x.Type == "id").Value);

                // return user id from JWT token if validation successful
                return user_id;
            }
            catch
            {
                // return null if validation fails
                return null;
            }
        }
    }
}
