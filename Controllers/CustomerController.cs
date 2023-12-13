using CustomerManagementService.Dtos;
using CustomerManagementService.Services.AccountServices;
using CustomerManagementService.Services.CustomerServices;
using CustomerManagementService.Services.ProfileServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CustomerManagementService.Controllers;

[Authorize]
[Route("[controller]")]
public class CustomerController : ControllerBase
{
    private readonly ICustomerService _customerService;
    private readonly IAccountService _accountService;
    private readonly IProfileService _profileService;

    public CustomerController(ICustomerService customerService, IAccountService accountService,
        IProfileService profileService)
    {
        _customerService = customerService;
        _accountService = accountService;
        _profileService = profileService;
    }

    [HttpGet]
    [Route("GetCustomerDetails/{id}")]
    public async Task<ActionResult> GetCustomerDetails(string id)
    {
        try
        {
            await _customerService.GetCustomerByIdAsync(id);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost]
    [Route("CreateCustomer")]
    public async Task<ActionResult> CreateCustomer([FromBody] CustomerDto customerDto)
    {
        try
        {
            var id = await _customerService.CreateCustomerAsync(customerDto);
            if (id != null)
            {
                var locationUri = Url.Action(nameof(GetCustomerDetails), new { id = id });
                return Created(locationUri, new { id = id });
            }
            else
            {
                return BadRequest("Failed to create the customer.");
            }
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return BadRequest("An error occurred while processing your request.");
        }
    }

    [HttpDelete]
    [Route("DeleteCustomer/{id}")]
    public async Task<ActionResult> DeleteCustomer(string id)
    {
        try
        {
            await _customerService.DeleteCustomerAsync(id);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}