﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using backend.Models;

/*
 * This class provides the functionality for [Authorization]. 
 */
namespace backend.Authorization
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {

        public void OnAuthorization(AuthorizationFilterContext context)
        {

            // skip authorization if action is decorated with [AllowAnonymous] attribute
            var allow_anonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();
            if (allow_anonymous)
                return;

            /*
             * This block checks if a user is attached to the current request. 
             * 
             * JWTMiddleware is invoked first to validate the JWT token. If user attached to the token exists, then authorize access. 
             */
            var user = (User)context.HttpContext.Items["User"];
            if (user == null)
                context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
        }
    }

}
