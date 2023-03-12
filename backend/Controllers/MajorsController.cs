namespace backend.Controllers;

using Microsoft.AspNetCore.Mvc;
using backend.Authorization;
using AutoMapper;
using backend.Entities;
using backend.Services;
using backend.Helpers;
using Microsoft.Extensions.Options;


[ApiController]
[Route("[controller]")]
[Authorize]
public class MajorsController : Controller
{
    private readonly ICourseService _courseService;

    public MajorsController(ICourseService courseService)
    {
        _courseService = courseService;
   
    }

    [HttpGet("CS")]
    public IActionResult GetComputerScience()
    {
        var courses = _courseService.GetMajorCourses(ComputerScience.Courses());
        return Ok(courses);
    }
}

