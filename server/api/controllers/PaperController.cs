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
    [Route("create")]
    [HttpPost]
    public ActionResult<PaperDto> CreatePaper([FromBody] CreatePaperDto createPaperDto)
    {
        try
        {
            return Ok(paperService.CreatePaper(createPaperDto).Result);
        }
        catch (Exception e)
        {
            HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            HttpContext.Response.WriteAsJsonAsync("Something went wrong");
            throw;
        }
    }
    
    [Route("discontinue")]
    [HttpPut]
    public async Task<ActionResult> DiscontinuePaper([FromBody] DiscontinuePaperDto discontinuePaperDto)
    {
        try
        {
            bool success = await paperService.DiscontinuePaper(discontinuePaperDto);

            if (success)
                return Ok();

            throw new Exception("Failed to discontinue paper");
        }
        catch (Exception e)
        {
            HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            HttpContext.Response.WriteAsJsonAsync("Something went wrong");
            throw;
        }
    }
    
    [Route("stock")]
    [HttpPut]
    public async Task<ActionResult> ChangePaperStock([FromBody] ChangePaperStockDto changePaperStockDto)
    {
        try
        {
            bool success = await paperService.ChangePaperStock(changePaperStockDto);

            if (success)
                return Ok();

            throw new Exception("Failed to change paper stock");
        }
        catch (Exception e)
        {
            HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            HttpContext.Response.WriteAsJsonAsync("Something went wrong");
            throw;
        }
    }
    
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