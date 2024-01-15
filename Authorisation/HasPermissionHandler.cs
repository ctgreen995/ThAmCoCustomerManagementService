using Microsoft.AspNetCore.Authorization;
using CustomerManagementService.Authorisation;

namespace CustomerManagementService.Authorisation
{
    public class HasPermissionHandler : AuthorizationHandler<HasPermissionRequirement>
    {

        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            HasPermissionRequirement requirement)
        {
            if (context.User != null && requirement.ValidPermissions.Any())
            {
                var permissionsClaim = context.User.Claims
                    .FirstOrDefault(c => c.Type == "permissions");

                if (permissionsClaim != null)
                {
                    var permissions = permissionsClaim.Value.Split(' ');
                    if (requirement.ValidPermissions.Any(requiredPermission =>
                        permissions.Contains(requiredPermission)))
                    {
                        context.Succeed(requirement);
                    }
                }
            }

            return Task.CompletedTask;
        }
    }
}
