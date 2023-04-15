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

    public MajorsController(IMajorService majorService)
    {
        _majorService = majorService;

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

    [HttpGet]
    public IActionResult GetMajors()
    {
        var response = _majorService.GetAll();

        return Ok(response);
    }
}

