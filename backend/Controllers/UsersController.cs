using Microsoft.AspNetCore.Mvc;
using backend.Authorization;
using AutoMapper;
using backend.Entities;
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
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly AppSettings _appSettings;
        private readonly User _userContext;

        public UsersController(IUserService userService, IMapper mapper, IOptions<AppSettings> appSettings, IHttpContextAccessor context)
        {
            _userService = userService;
            _mapper = mapper;
            _appSettings = appSettings.Value;
            _userContext = (User)context.HttpContext.Items["User"];
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

            var response = _userService.Authenticate(model);
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

            var response = _userService.Register(model);
            return Ok(response);
        }

        [HttpGet("info")]
        public IActionResult GetById()
        {
            if (_userContext == null) throw new AppException("Invalid token");

            var user = _userService.GetById(_userContext.UserId);
            return Ok(user);
        }

        [HttpPut("update-account")]
        public IActionResult Update(UpdateRequest model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (_userContext == null) throw new AppException("Invalid token");

            _userService.Update(_userContext.UserId, model);
            return Ok(new { message = "User updated successfully" });
        }

        [HttpPost("enrollment-form")]
        public IActionResult Enrollment(EnrollmentFormRequest form)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (_userContext == null) throw new AppException("Invalid token");

            form.UserId = _userContext.UserId;
            
            var response = _userService.EnrollmentForm(form);

            return Ok(response);
        }

        [HttpGet("enrollment-data")]
        public IActionResult EnrollmentData()
        {
            if (_userContext == null) throw new AppException("Invalid token");

            var response = _userService.UserEnrollment(_userContext.UserId);

            return Ok(response);
        }
    }
}
