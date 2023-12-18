using CustomerManagementService.Dtos;
using CustomerManagementService.Services.ProfileServices;
using Microsoft.AspNetCore.Mvc;

namespace CustomerManagementService.Controllers;

[Route("[controller]")]
public class CustomerProfileController : ControllerBase
{
    private readonly ICustomerProfileService _customerProfileService;

    public CustomerProfileController(ICustomerProfileService customerProfileService)
    {
        _customerProfileService = customerProfileService;
    }

    [HttpPatch]
    [Route("updateProfileByAuthId/{id}")]
    public async Task<ActionResult> UpdateProfileByAuthId(string id, [FromBody] CustomerProfileDto customerProfileDto)
    {
        try
        {
            await _customerProfileService.UpdateProfileByAuthIdAsync(id, customerProfileDto);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest("An error occurred while processing your request to update profile details.\n"
                              + e.Message);
        }
    }
}