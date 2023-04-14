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

    public MajorsController(IMajorService majorService, IHttpContextAccessor context)
    {
        _majorService = majorService;
        _userContext = (User)context.HttpContext.Items["User"];

    }

    [HttpGet("byName/{name}")]
    public IActionResult GetByName(string name)
    {
        //var response = _majorService.GetByName(name);

        return Ok();
    }

    [HttpGet("byId/{id}")]
    public IActionResult GetByID(int id)
    {
        //var response = _majorService.GetById(id);

        return Ok();
    }

    [HttpGet]
    public IActionResult GetMajors()
    {
        var response = _majorService.GetAll();

        return Ok(response);
    }

    [HttpGet("check-requirements")]
    public IActionResult Requirements()
    {
        if (_userContext == null) throw new AppException("Invalid token");
        var response = _majorService.CheckRequirements(new RequirementsCheck(), _userContext.UserId);
        return Ok(response);
    }
}

