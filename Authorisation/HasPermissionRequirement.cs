using Microsoft.AspNetCore.Authorization;

namespace CustomerManagementService.Authorisation
{
    public class HasPermissionRequirement : IAuthorizationRequirement
    {

        public IEnumerable<string> ValidPermissions { get; }

        public HasPermissionRequirement(IEnumerable<string> validPermissions)
        {
            ValidPermissions = validPermissions;
        }
    }
}
