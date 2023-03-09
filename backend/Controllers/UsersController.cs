using Microsoft.AspNetCore.Mvc;
using backend.Authorization;
using AutoMapper;
using backend.Entities;
using backend.Services;
using backend.Helpers;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc.ModelBinding;

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

        public UsersController(
            IUserService userService,
            IMapper mapper,
            IOptions<AppSettings> appSettings)
        {
            _userService = userService;
            _mapper = mapper;
            _appSettings = appSettings.Value;
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
            var user = _userService.GetById(id);
            return Ok(user);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, UpdateRequest model)
        {
            _userService.Update(id, model);
            return Ok(new { message = "User updated successfully" });
        }

        [AllowAnonymous]
        [HttpGet("enrollment/{id}")]
        public IActionResult IsEnrolled(int id)
        {
            return Ok(_userService.IsEnrolled(id));
        }

        [AllowAnonymous]
        [HttpPost("enrollment/form")]
        public IActionResult Enrollment(EnrollmentForm form)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = _userService.EnrollmentForm(form);

            return Ok(response);
        }
    }
}
