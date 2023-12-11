using Microsoft.AspNetCore.Authorization;

namespace CustomerManagementService.Authorisation
{
    public class HasPermissionRequirement : IAuthorizationRequirement
    {
        public string ValidPermission { get; } = "manage:account";
    }
}
