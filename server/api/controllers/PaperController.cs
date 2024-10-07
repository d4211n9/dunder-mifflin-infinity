using data_access.data_transfer_objects;
using data_access.models;
using Microsoft.AspNetCore.Mvc;
using service.interfaces;

namespace api.controllers;

[Route("api/[controller]")]
[ApiController]
public class PaperController(
    IPaperService paperService, 
    IPaperPropertyService paperPropertyService) : ControllerBase
{
    [HttpPost]
    public ActionResult<SelectionWithPaginationDto<PaperDto>> GetPapers([FromBody] PaperSearchDto paperSearchDto)
    {
        try
        {
            return Ok(paperService.GetPapers(paperSearchDto).Result);
        }
        catch (Exception e)
        {
            HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            HttpContext.Response.WriteAsJsonAsync("Something went wrong");
            throw;
        }
    }
    
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