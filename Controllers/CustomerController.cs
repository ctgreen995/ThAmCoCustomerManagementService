using CustomerManagementService.Dtos;
using CustomerManagementService.Services.CustomerServices;
using CustomerManagementService.Services.RequestDeleteCustomerSevices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CustomerManagementService.Controllers;

[Authorize]
[Route("[controller]")]
public class CustomerController : ControllerBase
{
    private readonly ICustomerService _customerService;
    private readonly IRequestDeleteCustomerService _requestDeleteCustomerService;

    public CustomerController(ICustomerService customerService,
        IRequestDeleteCustomerService requestDeleteCustomerService)
    {
        _customerService = customerService;
        _requestDeleteCustomerService = requestDeleteCustomerService;
    }

    [HttpGet]
    [Authorize(Policy = "ReadCustomer")]
    [Route("getCustomerDetailsById/{id}")]
    public async Task<ActionResult> GetCustomerDetails(string id)
    {
        try
        {
            var customerDto = await _customerService.GetCustomerByIdAsync(id);
            return Ok(customerDto);
        }
        catch (KeyNotFoundException e)
        {
            return NotFound("Customer details not found.\n" + e.Message);
        }
        catch (Exception e)
        {
            return BadRequest("An error occurred while processing your request to get customer details.\n"
                              + e.Message);
        }
    }

    [HttpPost]
    [Authorize(Policy = "CreateCustomer")]
    [Route("createCustomer")]
    public async Task<ActionResult> CreateCustomer([FromBody] CustomerDto customerDto)
    {
        try
        {
            var id = await _customerService.CreateCustomerAsync(customerDto);
            return id != null ? Ok(id) : BadRequest("Failed to create the customer.");
        }
        catch (ArgumentNullException e)
        {
            return BadRequest("Customer id cannot be null.\n" + e.Message);
        }
        catch (InvalidOperationException e)
        {
            return BadRequest("Invalid operation.\n" + e.Message);
        }

        catch (DbUpdateException e)
        {
            return BadRequest("An error occurred while attempting to create the user in the database.\n"
                              + e.Message);
        }
        catch (Exception ex)
        {
            return BadRequest("An error occurred while processing your request to create a customer.\n"
                              + ex.Message);
        }
    }

    [HttpPatch]
    [Authorize(Policy = "UpdateCustomer")]
    [Route("updateCustomerById/{id}")]
    public async Task<ActionResult> UpdateCustomer([FromBody] CustomerDto customerDto)
    {
        try
        {
            await _customerService.UpdateCustomerAsync(customerDto);
            return Ok();
        }
        catch (KeyNotFoundException e)
        {
            return NotFound("Customer not found.\n" + e.Message);
        }
        catch (ArgumentNullException e)
        {
            return BadRequest("Customer id cannot be null.\n" + e.Message);
        }
        catch (Exception e)
        {
            return BadRequest("An error occurred while processing your request to update a customer.\n"
                              + e.Message);
        }
    }

    [HttpDelete]
    [Authorize(Policy = "DeleteCustomer")]
    [Route("deleteCustomerByAuthId/{id}")]
    public async Task<ActionResult> DeleteCustomer(string id)
    {
        try
        {
            await _customerService.DeleteCustomerAsync(id);
            return Ok();
        }
        catch (KeyNotFoundException e)
        {
            return NotFound("Customer not found.\n" + e.Message);
        }
        catch (ArgumentNullException e)
        {
            return BadRequest("Customer id cannot be null.\n" + e.Message);
        }
        catch (Exception e)
        {
            return BadRequest("An error occurred while processing your request to delete a customer.\n"
                              + e.Message);

        }
    }

    [HttpDelete]
    [Authorize(Policy = "RequestToDeleteCustomer")]
    [Route("requestDeleteCustomer/{id}")]
    public async Task<ActionResult> RequestCustomerDeletion(string id)
    {
        try
        {
            var customerDto = await _customerService.GetCustomerByIdAsync(id);
            await _requestDeleteCustomerService.RequestDeleteCustomerAsync(customerDto);
            return Ok();
        }
        catch (KeyNotFoundException e)
        {
            return NotFound("Customer not found.\n" + e.Message);
        }
        catch (ArgumentNullException e)
        {
            return BadRequest("Customer id cannot be null.\n" + e.Message);
        }
        catch (Exception e)
        {
            return BadRequest("An error occurred while processing your request to delete a customer.\n"
                              + e.Message);

        }
    }
}