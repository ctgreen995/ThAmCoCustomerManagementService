using CustomerManagementService.Dtos;
using CustomerManagementService.Services.AccountServices;
using Microsoft.AspNetCore.Mvc;

namespace CustomerManagementService.Controllers;

[Route("[controller]")]
public class CustomerAccountController : ControllerBase
{
    private readonly ICustomerAccountService _customerAccountService;
    
    public CustomerAccountController(ICustomerAccountService customerAccountService)
    {
        _customerAccountService = customerAccountService;
    }
    
    [HttpPatch]
    [Route("updateAccountByAuthId/{id}")]
    public async Task<ActionResult> UpdateAccountByAuthId(string authId, [FromBody] CustomerAccountDto customerAccountDto)
    {
        try
        {
            await _customerAccountService.UpdateAccountByAuthIdAsync(authId, customerAccountDto);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest("An error occurred while processing your request to update account details.\n"
                              + e.Message);
        }
    }
}