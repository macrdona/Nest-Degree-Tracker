namespace backend.Controllers;

using Microsoft.AspNetCore.Mvc;
using backend.Authorization;
using AutoMapper;
using backend.Entities;
using backend.Services;
using backend.Helpers;
using Microsoft.Extensions.Options;
using backend.Models;

//[Authorize]
[ApiController]
[Route("[controller]")]
[Authorize]
public class CoursesController : ControllerBase
{
    private readonly ICourseService _course_service;
    private readonly User _user_context;

    public CoursesController(
        ICourseService courseService,
        IHttpContextAccessor context)
    {
        _course_service = courseService;
        _user_context = (User)context.HttpContext.Items["User"];
    }

    [HttpGet("{id}")]
    public IActionResult GetById(string id)
    {
        var response = _course_service.GetCourseById(id);
        return Ok(response);
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        if (_user_context == null) throw new AppException("Invalid token");

        var response = _course_service.GetAll(_user_context.UserId);
        return Ok(response);
    }

    [HttpGet("recommendations")]
    public IActionResult Recommendations()
    {
        if (_user_context == null) throw new AppException("Invalid token");
        var response = _course_service.CourseRecommendations(_user_context.UserId);
        return Ok(response);
    }

    [HttpPost("add")]
    public IActionResult AddCourse(CompletedCourses new_course)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (_user_context == null) throw new AppException("Invalid token");

        new_course.UserId = _user_context.UserId;
        _course_service.AddCourse(new_course);
        return Ok(new {Message = "Course has been added."});
    }

    [HttpPost("remove")]
    public IActionResult RemoveCourse(CompletedCourses course)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (_user_context == null) throw new AppException("Invalid token");

        course.UserId = _user_context.UserId;
        _course_service.RemoveCourse(course);
        return Ok(new { Message = "Course has been removed." });
    }
}