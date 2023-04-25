using Microsoft.AspNetCore.Mvc;
using backend.Authorization;
using AutoMapper;
using backend.Models;
using backend.Services;
using backend.Helpers;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers
{
    [Authorize]
    [ApiController]
    [ValidateModel]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _user_service;
        private readonly User _user_context;

        public UsersController(IUserService user_service, IHttpContextAccessor context)
        {
            _user_service = user_service;
            _user_context = (User)context.HttpContext.Items["User"];
        }

        //Ok() will return a response with a status code and formatted data
        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Authenticate(AuthenticateRequest model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = _user_service.Authenticate(model);
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register(RegisterRequest model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = _user_service.Register(model);
            return Ok(response);
        }

        [HttpGet("info")]
        public IActionResult GetById()
        {
            if (_user_context == null) throw new AppException("Invalid token");

            var user = _user_service.GetById(_user_context.UserId);
            return Ok(user);
        }

        [HttpPut("update-account")]
        public IActionResult Update(UpdateRequest model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (_user_context == null) throw new AppException("Invalid token");

            _user_service.Update(_user_context.UserId, model);
            return Ok(new { message = "User updated successfully" });
        }

        [HttpPost("enrollment-form")]
        public IActionResult Enrollment(EnrollmentFormRequest form)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (_user_context == null) throw new AppException("Invalid token");

            form.UserId = _user_context.UserId;
            
            var response = _user_service.EnrollmentForm(form);

            return Ok(response);
        }

        [HttpGet("enrollment-data")]
        public IActionResult EnrollmentData()
        {
            if (_user_context == null) throw new AppException("Invalid token");

            var response = _user_service.UserEnrollment(_user_context.UserId);

            return Ok(response);
        }
    }
}
