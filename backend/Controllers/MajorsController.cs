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
    private readonly IMajorService _majorService;
    private readonly User _userContext;
    private readonly ICourseService _courseService;

    public MajorsController(IMajorService majorService, IHttpContextAccessor context, ICourseService courseService)
    {
        _majorService = majorService;
        _userContext = (User)context.HttpContext.Items["User"];
        _courseService = courseService;
    }

    [HttpGet]
    public IActionResult GetMajors()
    {
        var response = _majorService.GetAll();

        return Ok(response);
    }

    [HttpGet("byName/{name}")]
    public IActionResult GetByName(string name)
    {
        var response = _majorService.GetByName(name);

        return Ok(response);
    }

    [HttpGet("byId/{id}")]
    public IActionResult GetByID(int id)
    {
        var response = _majorService.GetById(id);

        return Ok(response);
    }

    [HttpGet("check-requirements")]
    public IActionResult Requirements()
    {
        if (_userContext == null) throw new AppException("Invalid token");
        var response = _majorService.CheckRequirements(_userContext.UserId, _courseService.GetAll(_userContext.UserId));
        return Ok(response);
    }

    [HttpPost("user-specific-requirements")]
    public IActionResult UserSpecificRequirements(SpecificRequirements user_requirements)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (_userContext == null) throw new AppException("Invalid token");
        _majorService.UpdateSpecificRequirements(_userContext.UserId,user_requirements);
        return Ok(new { Message = "Requirements have been updated"});
    }
}

