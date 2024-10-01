using data_access.models;
using Microsoft.AspNetCore.Mvc;
using service.interfaces;

namespace api.controllers;

[Route("api/[controller]")]
[ApiController]
public class PaperController(IPaperPropertyService paperPropertyService) : ControllerBase
{
    [Route("property/create/{propertyName}")]
    [HttpPost]
    public ActionResult<Property> CreateProperty([FromRoute] string propertyName)
    {
        try
        {
            return Ok(paperPropertyService.CreateProperty(propertyName));
        }
        catch (Exception e)
        {
            HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            HttpContext.Response.WriteAsJsonAsync("Something went wrong");
            throw;
        }
    }
}