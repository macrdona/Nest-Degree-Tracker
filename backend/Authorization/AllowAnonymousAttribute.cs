
//Custom [AllowAnonymous] tag to keep consistancy with the Custom [Authorize] attribute
namespace backend.Authorization
{
    [AttributeUsage(AttributeTargets.Method)]
    public class AllowAnonymousAttribute : Attribute { }
}
