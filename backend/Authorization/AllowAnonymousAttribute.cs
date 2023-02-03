
/*
 * This class has the same functionality as [AllowAnonymous]. 
 */
namespace backend.Authorization
{
    [AttributeUsage(AttributeTargets.Method)]
    public class AllowAnonymousAttribute : Attribute
    { }
}