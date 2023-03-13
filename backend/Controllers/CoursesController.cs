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

    public CoursesController(
        ICourseService courseService,
        IMapper mapper,
        IOptions<AppSettings> appSettings)
    {
        _courseService = courseService;
        _mapper = mapper;
        _appSettings = appSettings.Value;
    }

    [HttpGet("findCourse")]
    public IActionResult GetById(CourseRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var course = _courseService.GetByID(request.CourseId);
        return Ok(course);
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var courses = _courseService.GetAll();
        return Ok(courses);
    }
}