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
        var response = _courseService.GetCourseById(id);
        return Ok(response);
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        if (_userContext == null) throw new AppException("Invalid token");

        var response = _courseService.GetAll(_userContext.UserId);
        return Ok(response);
    }

    [HttpGet("recommendations")]
    public IActionResult Recommendations()
    {
        if (_userContext == null) throw new AppException("Invalid token");
        var response = _courseService.CourseRecommendations(_userContext.UserId);
        return Ok(response);
    }

    [HttpPost("add")]
    public IActionResult AddCourse(CompletedCourses newCourse)
    {
        if (_userContext == null) throw new AppException("Invalid token");

        newCourse.UserId = _userContext.UserId;
        _courseService.AddCourse(newCourse);
        return Ok(new {Message = "Course has been added."});
    }

    [HttpPost("remove")]
    public IActionResult RemoveCourse(CompletedCourses course)
    {
        if (_userContext == null) throw new AppException("Invalid token");

        course.UserId = _userContext.UserId;
        _courseService.RemoveCourse(course);
        return Ok(new { Message = "Course has been removed." });
    }
}