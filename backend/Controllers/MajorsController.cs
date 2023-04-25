namespace backend.Controllers;

using Microsoft.AspNetCore.Mvc;
using backend.Authorization;
using AutoMapper;
using backend.Entities;
using backend.Services;
using backend.Helpers;
using Microsoft.Extensions.Options;
using backend.Models;

[ApiController]
[Route("[controller]")]
[Authorize]
public class MajorsController : Controller
{
    private readonly IMajorService _major_service;
    private readonly User _user_context;
    private readonly ICourseService _courseService;

    public MajorsController(IMajorService majorService, IHttpContextAccessor context, ICourseService courseService)
    {
        _major_service = majorService;
        _user_context = (User)context.HttpContext.Items["User"];
        _courseService = courseService;
    }

    [HttpGet]
    public IActionResult GetMajors()
    {
        var response = _major_service.GetAll();

        return Ok(response);
    }

    [HttpGet("byName/{name}")]
    public IActionResult GetByName(string name)
    {
        var response = _major_service.GetByName(name);

        return Ok(response);
    }

    [HttpGet("byId/{id}")]
    public IActionResult GetByID(int id)
    {
        var response = _major_service.GetById(id);

        return Ok(response);
    }

    [HttpGet("check-requirements")]
    public IActionResult Requirements()
    {
        if (_user_context == null) throw new AppException("Invalid token");
        var response = _major_service.CheckRequirements(_user_context.UserId, _courseService.GetAll(_user_context.UserId));
        return Ok(response);
    }

    [HttpPost("update-specific-requirements")]
    public IActionResult UserSpecificRequirements(SpecificRequirements user_requirements)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (_user_context == null) throw new AppException("Invalid token");
        _major_service.UpdateSpecificRequirements(_user_context.UserId,user_requirements);
        return Ok(new { Message = "Requirements have been updated"});
    }
}

