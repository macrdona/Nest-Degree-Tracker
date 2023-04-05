using backend.Entities;
using Microsoft.EntityFrameworkCore;

namespace backend.Helpers
{
    class UserAuthorization
    {
        public static bool IsUser(IHttpContextAccessor _context, int id)
        {
            try
            {
                var user = (User)_context.HttpContext.Items["User"];
                if (user.UserId == id) return true;

            }
            catch {
                throw new Exception();
            }

            return false;

        }
    }
}
