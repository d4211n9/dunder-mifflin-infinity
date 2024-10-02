using data_access.data_transfer_objects;
using data_access.models;
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

    [Route("customer")]
    [HttpGet]
    public ActionResult<SelectionWithPaginationDto<Order>> GetOrdersForCustomer([FromQuery] GetCustomerOrdersDto getCustomerOrdersDto)
    {
        try
        {
            return Ok(orderService.GetOrdersForCustomer(getCustomerOrdersDto));
        }
        catch (Exception e)
        {
            HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            HttpContext.Response.WriteAsync("Something went wrong");
            throw;
        }
    }
}