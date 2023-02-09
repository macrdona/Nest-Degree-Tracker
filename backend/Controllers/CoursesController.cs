namespace backend.Controllers;

using Microsoft.AspNetCore.Mvc;
using backend.Authorization;
using AutoMapper;
using backend.Entities;
using backend.Services;
using backend.Helpers;
using Microsoft.Extensions.Options;

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

    [HttpGet]
    public IActionResult Create(CreateCourseRequest model)
    {
        _courseService.create(model);
        return Ok(new { message = "Course Created" });
    }
    [HttpGet("{id}")]
    public IActionResult GetById(string id)
    {
        var course = _courseService.getByID(id);
        return Ok(course);
    }
    [HttpPut("{id}")]
    public IActionResult Update(string id, UpdateCourseRequest model)
    {
        _courseService.update(id, model);
        return Ok(new { message = "Course updated successfully" });
    }
    [HttpDelete("{id}")]
    public IActionResult Delete(string id)
    {
        _courseService.delete(id);
        return Ok(new { message = "Course deleted successfully" });
    }
}