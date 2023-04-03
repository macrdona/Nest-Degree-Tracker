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

    [HttpGet]
    public IActionResult GetMajor(MajorSelectionRequest model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var response = _majorService.GetMajorCourses(model.MajorName);

        return Ok(response);
    }
}

