using CustomerManagementService.Authorisation;

namespace CustomerManagementService.StartupConfigurations;

public static class AuthorizationConfiguration
{
    public static void AddAuthorizationServices(this IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            foreach (var policy in AuthorisationPolicies.Default.Policies)
            {
                options.AddPolicy(policy.PolicyName, policyBuilder =>
                    policyBuilder.Requirements.Add(new HasPermissionRequirement(policy.Permissions)));
            }
        });
    }
}
