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
public class CoursesController : ControllerBase
{
    private ICourseService _courseService;
    private IMapper _mapper;
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

    [HttpGet("{id}")]
    public IActionResult GetById(string courseId)
    {
        var course = _courseService.getByID(courseId);
        return Ok(course);
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var courses = _courseService.getAll();
        return Ok(courses);
    }
}