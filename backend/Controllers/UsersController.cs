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

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            if (!UserAuthorization.IsUser(_userContext, id)) throw new AppException("Unauthorized Request");

            var user = _userService.GetById(id);
            return Ok(user);
        }

        [HttpPut("update-account/{id}")]
        public IActionResult Update(int id, UpdateRequest model)
        {
            if (!UserAuthorization.IsUser(_userContext, id)) throw new AppException("Unauthorized Request");

            _userService.Update(id, model);
            return Ok(new { message = "User updated successfully" });
        }

        [HttpPost("enrollment")]
        public IActionResult Enrollment(EnrollmentFormRequest form)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                form.UserId = _userContext.UserId; 
            }
            catch (Exception)
            {
                throw new AppException("Unauthorized Request");
            }

            var response = _userService.EnrollmentForm(form);

            return Ok(response);
        }

        [HttpGet("enrollment/{id}")]
        public IActionResult UserEnrollment(int id)
        {
            if (!UserAuthorization.IsUser(_userContext, id)) throw new AppException("Unauthorized Request");
            var response = _userService.UserEnrollment(id);

            return Ok(response);
        }
    }
}
