using backend.Entities;
using Microsoft.EntityFrameworkCore;

namespace backend.Helpers
{
    class UserAuthorization
    {
        public static bool IsUser(User _userContext, int id)
        {
            try
            {
                if (_userContext.UserId == id) return true;

            }
            catch {
                throw new Exception();
            }

            return false;

        }
    }
}
