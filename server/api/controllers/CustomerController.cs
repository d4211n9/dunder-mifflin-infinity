using data_access.data_transfer_objects;
using data_access.models;
using Microsoft.AspNetCore.Mvc;
using service.interfaces;

namespace api.controllers;

[Route("api/[controller]")]
[ApiController]
public class CustomerController(ICustomerService customerService) : ControllerBase
{
    [HttpGet]
    public ActionResult<SelectionWithPaginationDto<Customer>> GetCustomers([FromQuery] CustomerSearchDto customerSearchDto)
    {
        try
        {
            return Ok(customerService.GetCustomers(customerSearchDto));
        }
        catch (Exception e)
        {
            HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            HttpContext.Response.WriteAsJsonAsync("Something went wrong");
            throw;
        }
    }
}