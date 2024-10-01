using data_access.data_transfer_objects;
using Microsoft.AspNetCore.Mvc;
using service.interfaces;

namespace api.controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderController(IOrderService orderService) : ControllerBase
{
    [Route("status")]
    [HttpPut]
    public async Task<ActionResult> ChangeStatus([FromBody] ChangeOrderStatusDto changeOrderStatusDto)
    {
        try
        {
            bool isSuccess = await orderService.ChangeStatus(changeOrderStatusDto);
            
            if (isSuccess)
                return Ok();

            throw new Exception("Failed to update status");
        }
        catch (Exception e)
        {
            HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError; 
            await HttpContext.Response.WriteAsJsonAsync("Something went wrong");
            throw;
        }
    }
}