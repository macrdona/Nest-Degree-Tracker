namespace backend.Controllers;

using Microsoft.AspNetCore.Mvc;
using backend.Authorization;
using AutoMapper;
using backend.Entities;
using backend.Services;
using backend.Helpers;
using Microsoft.Extensions.Options;

//[Authorize]
[ApiController]
[Route("[controller]")]
[Authorize]
public class CoursesController : ControllerBase
{
    private readonly ICourseService _courseService;
    private readonly IMapper _mapper;
    private readonly AppSettings _appSettings;
    private readonly User _userContext;

    public CoursesController(
        ICourseService courseService,
        IMapper mapper,
        IOptions<AppSettings> appSettings,
        IHttpContextAccessor context)
    {
        _courseService = courseService;
        _mapper = mapper;
        _appSettings = appSettings.Value;
        _userContext = (User)context.HttpContext.Items["User"];
    }

    [HttpGet("{id}")]
    public IActionResult GetById(string id)
    {
        var course = _courseService.GetByID(id);
        return Ok(course);
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var courses = _courseService.GetAll();
        return Ok(courses);
    }

    [HttpGet("recommendations")]
    public IActionResult Recommendations()
    {
        var response = _courseService.CourseRecommendations(_userContext.UserId);
        return Ok(response);
    }
}